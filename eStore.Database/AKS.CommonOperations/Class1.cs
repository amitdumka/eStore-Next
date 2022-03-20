namespace AKS.CommonOperations;
public class ComOpers
{
    public static void info()
    {
        Console.WriteLine("Common Operations Helps to write/ develop clean codes, so need to reinvent or rewrite same line.");
        Console.WriteLine("Functions/Helpers List below:");
        Write("1). Extensions:\n a). Date\nb). Time");

        
    }

    public static void Write(string line) => Console.WriteLine(line);
}

