namespace StackifyLang;

public abstract class Stmt
{
    public interface IVisitor<R>
    {
        R VisitBlockStmt(BlockStmt stmt);
        R VisitLiteralStmt(LiteralStmt stmt);
        R VisitOpStmt(OpStmt stmt);
        R VisitVariableStmt(VariableStmt stmt);
        R VisitFunctionStmt(FunctionStmt stmt);
        R VisitIfStmt(IfStmt stmt);
        R VisitWhileStmt(WhileStmt stmt);
    }

    public abstract R Accept<R>(IVisitor<R> visitor);

    public class BlockStmt(List<Stmt> stmts) : Stmt
    {
        public readonly List<Stmt> Stmts = stmts;

        public override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitBlockStmt(this);
        }
    }

    public class LiteralStmt(Token literal) : Stmt
    {
        public readonly Token Literal = literal;

        public override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitLiteralStmt(this);
        }
    }

    public class OpStmt(Token op) : Stmt
    {
        public readonly Token Op = op;

        public override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitOpStmt(this);
        }
    }

    public class VariableStmt(Token name) : Stmt
    {
        public readonly Token Name = name;

        public override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitVariableStmt(this);
        }
    }

    public class FunctionStmt(Token name, List<Token> parameters, List<Stmt> block) : Stmt
    {
        public readonly Token Name = name;
        public readonly List<Token> Parameters = parameters;
        public readonly List<Stmt> Block = block;

        public override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitFunctionStmt(this);
        }
    }

    public class IfStmt(List<Stmt> cond, List<Stmt> thenBlock, List<Stmt> elifConds, List<Stmt> elifBlocks, List<Stmt> elseBlock) : Stmt
    {
        public readonly List<Stmt> Cond = cond;
        public readonly List<Stmt> ThenBlock = thenBlock;
        public readonly List<Stmt> ElifConds = elifConds;
        public readonly List<Stmt> ElifBlocks = elifBlocks;
        public readonly List<Stmt> ElseBlock = elseBlock;

        public override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitIfStmt(this);
        }
    }

    public class WhileStmt(List<Stmt> cond, List<Stmt> block) : Stmt
    {
        public readonly List<Stmt> Cond = cond;
        public readonly List<Stmt> Block = block;

        public override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitWhileStmt(this);
        }
    }
}
