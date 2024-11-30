namespace StackifyLang;

public class Scanner(string source)
{
    private readonly string _source = source;
    private readonly List<Token> _tokens = [];
    private int _start = 0;
    private int _current = 0;
    private int _line = 1;

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
                AddToken(TokenType.Minus);
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
                Stackify.Error(_line, "Unexpected character.");
                break;
        }
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
        string value = _source.Substring(_start + 1, _current - 1);
        AddToken(TokenType.String, value);
    }

    private char Peek()
    {
        if (IsAtEnd()) return '\0';
        return _source[_current];
    }

    private char Advance()
    {
        _current += 1;
        return _source[_current];
    }

    private void AddToken(TokenType type)
    {
        AddToken(type, null);
    }

    private void AddToken(TokenType type, object? literal)
    {
        var text = _source.Substring(_start, _current);
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