namespace StackifyLang;

public class Token
{
    public TokenType Type { get; set; }

    public string Lexeme { get; set; } = string.Empty;

    public object? Literal { get; set; }

    public int Line { get; set; }

    public string TokenString => Type + " " + Lexeme + " " + Literal;
}
