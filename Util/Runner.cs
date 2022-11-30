using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Reflection;

public class Runner {
    private readonly int year;
    
    public Runner(int year) {
        this.year = year;
    }

    public int LatestDay => FindLatestDay();
    
    public async Task Run(int day) {
        WriteIntro(day);
        
        DaySolverBase solver = CreateDaySolver(day);
        Task<string> inputTask = ProblemInputFetcher.Fetch(year, day);
        
        SolveProblem(solver.Example1, solver.Solve1, "Part 1 - Example: ", ConsoleColor.Yellow);
        string input = await inputTask;
        SolveProblem(input, solver.Solve1, "Part 1 - Real input: ", ConsoleColor.Green);
        SolveProblem(solver.Example2, solver.Solve2, "Part 2 - Example: ", ConsoleColor.Yellow);
        SolveProblem(input, solver.Solve2, "Part 2 - Real input: ", ConsoleColor.Green);
    }
    
    private void WriteIntro(int day) {
        Console.Write("Running day ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(day);
        Console.ResetColor();
    }

    private void SolveProblem(string input, Func<string, object> solve, string desc, ConsoleColor resultColor) {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        object result = solve(input);
        stopwatch.Stop();
        Console.Write($"[{stopwatch.ElapsedMilliseconds} ms] {desc}");
        Console.ForegroundColor = resultColor;
        Console.WriteLine(result);
        Console.ResetColor();
    }

    private DaySolverBase CreateDaySolver(int day) {
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        Type type = types
            .First(t => t.Name.StartsWith("Day") 
                && !t.IsAbstract
                && int.TryParse(t.Name.Replace("Day", ""), out int id)
                && id == day);

        return (DaySolverBase)Activator.CreateInstance(type)!;
    }
    
    private int FindLatestDay() {
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        return types
            .Where(t => t.Name.StartsWith("Day") && !t.IsAbstract)
            .Select(t => int.Parse(t.Name.Replace("Day", "")))
            .Max();
    }
}