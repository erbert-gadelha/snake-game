using System.ComponentModel.DataAnnotations;

public class Game {

    public int size {get;}
    private int [,] map;

    private Vector position;
    private Vector direction;

    private bool isRunning;

    private List<Vector> body;

    ConsoleColor [] colors = [ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Yellow];

    char [] chars = ['■', ' '];

    Input input;
    int score = 0;

    public Game(int size) {
        this.size = size;
        map = new int[this.size, this.size];
        input = new Input(Vector.Right);
        position = new Vector();
        direction = new Vector();
        body = new List<Vector>();
    }

    public void Start() {
        DrawMap();

        direction.Set(Vector.Right);
        position.Set(size/2, size/2);
        Set(position, 3);


        body = new List<Vector>();
        int body_size = 3;
        for(int i = 0; i < body_size; i++) {
            Vector v = new Vector(position.x + (body_size - i), position.y);
            body.Add(v);
            Set(v, 1);
        }

        Thread.Sleep(200);
        CreateFood();
        Thread.Sleep(200);

        isRunning = true;
        input.Cast();

        while(isRunning) {
            UpdateInput();
            Move(direction);
            Console.Write($"score: {score}.");
            Thread.Sleep(200 - (score * 5));
        }

        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write($"\n\tGame Over!\n\tYour Score: {score}.");
        Console.ResetColor();
        Console.WriteLine("\n<press any key to continue.>");

        input.Stop();
    }


    private void UpdateInput() {
        if(input.get.x*direction.x != 0 || input.get.y*direction.y != 0)
            return;        
        direction.Set(input.get);
    }

    private void CreateFood() {
        Random random = new Random();
        Vector f_position = new Vector(random.Next()%size, random.Next()%size);
        if(Get(f_position) == 0)
            Set(f_position, 2);
        else 
            CreateFood();
    }


    public void Draw(int x, int y, int value) {
        int currentLineCursor = Console.CursorTop;

        x = (size - x)*2;
        y = Console.CursorTop - y - 2;
        Console.SetCursorPosition(x, y);

        switch(value) {
            case 2: Console.ForegroundColor = colors[2]; break;
            default:  Console.ForegroundColor = colors[0]; break;
        }

        char character;
        if (value == 2)
            character = '⎈';
        else
            character = value==0?chars[1]:chars[0];        

        Console.Write(character);
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public void Draw(Vector v, int value) {
        Draw(v.x, v.y, value);
    }




    public void Draw(int x, int y, char c, ConsoleColor color) {
        int currentLineCursor = Console.CursorTop;

        x = (size - x)*2;
        y = Console.CursorTop - y - 2;

        Console.ForegroundColor = color;

        Console.SetCursorPosition(x, y);
        Console.Write(c);
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public void Draw(Vector v, char c, ConsoleColor color) {
        Draw(v.x, v.y, c, color);
    }




    public void ResetCursor() {
        Console.SetCursorPosition(0,size + 3);
    }


    private string fillLine(char c, int size) {
        string s = "";
        for(int i = 0; i < size; i++) {
            s += c;
            s += chars[1];
        }
        return s;
    }


    bool has_aten;

    public void Move (Vector direction) {

        Set(body[0], 0);
        for(int i = 0; i < body.Count - 1; i++) {
            body[i].Set(body[i+1]);
        }
        body[body.Count-1].Set(position);
        Set(position, 1);

        if(has_aten) {
            Draw(position, chars[0], colors[1]);
            has_aten = false;
        }


        Vector v = body[0] - body[1];
        char c;
        switch(v.Hash()) {
            case 1: c = '⯇'; break;
            case -1: c = '⯈'; break;
            case 2: c = '⯅'; break;
            case -2: c = '⯆'; break;
            default: c = chars[0]; break;
        }
        Draw(body[0], c, colors[0]);


        position += direction;
        if(position.x < 0) position.x = size-1;
        if(position.y < 0) position.y = size-1;
        if(position.x >= size) position.x = 0;
        if(position.y >= size) position.y = 0;

        int value = Get(position);

        if(value == 1) {
            isRunning = false;
            return;
        }

        if(value == 2) {            
            body.Add(position);
            has_aten = true;
            
            score++;
            CreateFood();
            if(score%3==0)
                CreateFood();
        }

        Set(position, 3);
        if(has_aten)
            Draw(position, chars[0], colors[1]);
    }

    private void Set(int x, int y, int value) {
        if(y<0||x<0||x>=size||y>=size)
            return;
        map[y%size, x%size] = value;
        Draw(x, y, value);
    }
    private void Set(Vector v, int value) {
        Set(v.x, v.y, value);
    }

    private int Get(int x, int y) {
        if(y<0||x<0||x>=size||y>=size)
            return -1;
        return map[y%size, x%size];
    }
    private int Get(Vector v) {
        return Get(v.x, v.y);
    }


    public void DrawMap() {
        string fill_full = fillLine(chars[0], size + 2);
        string fill_empty = chars[0] + fillLine(chars[1], size ) + chars[1] + chars[0];
        Console.ForegroundColor = colors[3];
        Console.WriteLine(fill_full);
        for(int y = size-1; y >= 0; y--)
            Console.WriteLine(fill_empty);        
        Console.WriteLine(fill_full);
    }


}