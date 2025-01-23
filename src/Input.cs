public class Input {

    public bool isRunning{get; private set;}
    public Vector get{get{return vector;}}

    private Vector vector = new Vector(0,0);

    public Thread? thread {get; private set;}

    public Input () {
        isRunning = false;
        thread = null;
    }
    
    public Input (Vector initial) {
        isRunning = false;
        thread = null;
        vector.Set(initial);
    }

    public void Stop() {
        isRunning = false;
        if(thread == null)
            return;
            
        thread.Interrupt();
        thread = null;
    }

    public void Cast() {        
        thread = new Thread(_Cast);
        thread.Start();
    }

    private void _Cast() {
        this.isRunning = true;
        while(this.isRunning)
            Evaluate(Console.ReadKey());
    }

    void Evaluate(ConsoleKeyInfo input) {        
            switch (input.Key) {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow: vector.Set(0,1); break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow: vector.Set(1,0); break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow: vector.Set(0,-1); break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow: vector.Set(-1,0); break;
                default:   /*vector.Set(0,0);*/ break;
            }
    }

}