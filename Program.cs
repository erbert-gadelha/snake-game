

public static class Program {
    
    public static void Main(string [] args) {
        string NaN_message = "Usage: command [size:Number]";
        string bounds_message = "size must be greater than 0.";

        if(args.Length == 0) {
            Console.WriteLine(NaN_message);
            return;
        }

        string s = args[0];
        int i = 5;

        try {
            i = int.Parse(s);
            if(i > 1)
                new Game(i).Start();
            else
                Console.WriteLine(bounds_message);
            return;
        } catch (Exception e) {
            Console.WriteLine(e.Message);
        }
        
        Console.WriteLine(NaN_message);

    }
}
