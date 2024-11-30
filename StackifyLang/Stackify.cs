namespace StackifyLang;

public static class Stackify
{
    private static bool _hadError = false;

    public static void RunStackify(string[] args)
    {
        if (args.Length > 1)
        {
            Console.WriteLine("Usage: Stackify [script]");
            Environment.Exit(65);
        }
        else if (args.Length == 1)
        {
            RunFile(args[0]);
        }
        else
        {
            RunPrompt();
        }
    }
    
    public static void RunFile(string filePath)
    {
        var source = File.ReadAllText(filePath);
    }

    public static void RunPrompt()
    {
        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            if (input == null) break;
        }
    }

    private static void Run(string source)
    {
        var scanner = new Scanner(source);
        var tokens = scanner.ScanTokens();

        foreach (var token in tokens)
        {
            Console.WriteLine(token.TokenString);
        }
    }

    public static void Error(int line, string message)
    {
        Report(line, string.Empty, message);
    }

    private static void Report(int line, string where, string message)
    {
        Console.WriteLine($"[line {line}] Error {where}: {message}");
        _hadError = true;
    }
}
