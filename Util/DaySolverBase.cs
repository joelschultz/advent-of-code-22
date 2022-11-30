public abstract class DaySolverBase {
    public abstract string Example1 { get; }
    public virtual string Example2 => Example1;
    
    public abstract object Solve1(string raw);
    public abstract object Solve2(string raw);
}