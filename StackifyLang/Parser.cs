namespace StackifyLang;

public class Parser(List<Token> tokens)
{
    private readonly List<Token> _tokens = tokens;
    private int _current = 0;
    private static readonly TokenType[] s_literals = [TokenType.Integer, TokenType.Float, TokenType.String, TokenType.True, TokenType.False, TokenType.Nil];
    private static readonly TokenType[] s_ops = [
        TokenType.Plus, TokenType.Minus, TokenType.Star, TokenType.Slash, TokenType.Bang, TokenType.Equal, TokenType.BangEqual
    ];

    private class ParseError : Exception
    {
        public ParseError()
        {
        }

        public ParseError(string message) : base(message)
        {
        }

        public ParseError(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public List<Stmt> Parse()
    {
        var stmts = new List<Stmt>();
        while (!IsAtEnd())
        {
            if (Match(s_literals))
            {
                stmts.Add(new Stmt.LiteralStmt(Advance()));
            }
            else if (Match(s_ops))
            {
                stmts.Add(new Stmt.OpStmt(Advance()));
            }
        }
        return stmts;
    }

    private bool Match(params TokenType[] types)
    {
        foreach (var type in types)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
        }
        return false;
    }

    private Token Consume(TokenType type, string message)
    {
        if (Check(type)) return Advance();
        throw Error(Peek(), message);
    }

    private bool Check(TokenType type)
    {
        if (IsAtEnd()) return false;
        return Peek().Type == type;
    }

    private Token Advance()
    {
        if (!IsAtEnd()) _current += 1;
        return Previous();
    }

    private bool IsAtEnd()
    {
        return Peek().Type == TokenType.Eof;
    }

    private Token Peek()
    {
        return _tokens[_current];
    }

    private Token Previous()
    {
        return _tokens[_current - 1];
    }

    private static ParseError Error(Token token, string message)
    {
        Stackify.Error(token, message);
        return new ParseError();
    }
}
