namespace StackifyLang;

public class Scanner(string source)
{
    private readonly string _source = source;
    private readonly List<Token> _tokens = [];
    private int _start = 0;
    private int _current = 0;
    private int _line = 1;
    private static readonly Dictionary<string, TokenType> s_keywords = new()
    {
        { "and", TokenType.And },
        { "or", TokenType.Or },
        { "if", TokenType.If },
        { "elif", TokenType.Elif },
        { "else", TokenType.Else },
        { "fn", TokenType.Fn },
        { "end", TokenType.End },
        { "while", TokenType.While },
        { "break", TokenType.Break },
        { "then", TokenType.Then },
        { "nil", TokenType.Nil },
        { "true", TokenType.True },
        { "false", TokenType.False },
        { "type", TokenType.Type },
        { "use", TokenType.Use },
        { "namespace", TokenType.Namespace },
        { "ret", TokenType.Ret },
        { "new", TokenType.New },
        { "print", TokenType.Print },
        { "println", TokenType.Println },
        { "read", TokenType.Read },
        { "readln", TokenType.Readln }
    };

    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            _start = _current;
            ScanToken();
        }
        _tokens.Add(new Token
        {
            Type = TokenType.Eof,
            Lexeme = string.Empty,
            Literal = null,
            Line = _line
        });
        return _tokens;
    }

    private void ScanToken()
    {
        char c = Advance();
        switch (c)
        {
            case '.':
                AddToken(TokenType.Dot);
                break;
            case '-':
                AddToken(Match('>') ? TokenType.FnStart : TokenType.Minus);
                break;
            case '+':
                AddToken(TokenType.Plus);
                break;
            case '!':
                AddToken(Match('=') ? TokenType.BangEqual : TokenType.Bang);
                break;
            case '=':
                AddToken(TokenType.Equal);
                break;
            case '<':
                AddToken(Match('=') ? TokenType.LessEqual : TokenType.Less);
                break;
            case '>':
                AddToken(Match('=') ? TokenType.GreaterEqual : TokenType.Greater);
                break;
            case '*':
                AddToken(TokenType.Star);
                break;
            case '@':
                AddToken(TokenType.At);
                break;
            case ':':
                AddToken(TokenType.Colon);
                break;
            case '/':
                if (Match('/'))
                    while (Peek() != '\n' && !IsAtEnd()) Advance();
                else
                    AddToken(TokenType.Slash);
                break;
            case ' ':
            case '\r':
            case '\t':
                break;
            case '\n':
                _line += 1;
                break;
            case '"':
                AddStringToken();
                break;
            default:
                if (IsDigit(c))
                    AddNumberToken();
                else if (IsAlpha(c))
                    AddIdentifierToken();
                else
                    Stackify.Error(_line, "Unexpected character.");
                break;
        }
    }

    private void AddIdentifierToken()
    {
        while (IsAlphaNumeric(Peek())) Advance();

        var identifier = _source[_start.._current];
        if (s_keywords.TryGetValue(identifier, out var tokenType))
            AddToken(tokenType);
        else
            AddToken(TokenType.Identifier);
    }

    private static bool IsAlphaNumeric(char c)
    {
        return IsAlpha(c) || IsDigit(c);
    }

    private static bool IsAlpha(char c)
    {
        return (c >= 'a' && c <= 'z')
            || (c >= 'A' && c <= 'Z')
            || c == '_';
    }

    private void AddNumberToken()
    {
        while (IsDigit(Peek())) Advance();

        bool isFloat = false;
        if (Peek() == '.' && IsDigit(PeekNext()))
        {
            isFloat = true;
            Advance(); // Consume '.'
            while (IsDigit(Peek())) Advance();
        }

        var text = _source[_start.._current];
        if (isFloat)
            AddToken(TokenType.Float, double.Parse(text));
        else
            AddToken(TokenType.Integer, int.Parse(text));
    }

    private static bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }

    // Todo: Add support for escape sequences like '\n'
    private void AddStringToken()
    {
        while (Peek() != '"' && !IsAtEnd())
        {
            if (Peek() == '\n') _line += 1;
            Advance();
        }

        if (IsAtEnd())
        {
            Stackify.Error(_line, "Unterminated string.");
            return;
        }

        // Closing "
        Advance();

        // Trim the surrounding quotes
        var value = _source[(_start + 1)..(_current - 1)];
        AddToken(TokenType.String, value);
    }

    private char Peek()
    {
        if (IsAtEnd()) return '\0';
        return _source[_current];
    }

    private char PeekNext()
    {
        if (_current + 1 >= _source.Length) return '\0';
        return _source[_current + 1];
    }

    private char Advance()
    {
        var c = _source[_current];
        _current += 1;
        return c;
    }

    private void AddToken(TokenType type)
    {
        AddToken(type, null);
    }

    private void AddToken(TokenType type, object? literal)
    {
        var text = _source[_start.._current];
        _tokens.Add(new Token
        {
            Type = type,
            Lexeme = text,
            Literal = literal,
            Line = _line
        });
    }

    private bool Match(char expected)
    {
        if (IsAtEnd()) return false;
        if (_source[_current] != expected) return false;

        _current += 1;
        return true;
    }

    private bool IsAtEnd()
    {
        return _current >= _source.Length;
    }
}
