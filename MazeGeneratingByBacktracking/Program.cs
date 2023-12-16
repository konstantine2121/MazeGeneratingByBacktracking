using MazeGenerating;
using MazeGenerating.Data;
using MazeGenerating.DebugViews;

namespace MazeGeneratingByBacktracking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var generator = new MazeGenerator();
            var maze = generator.Generate(new Size(80,25));
            var view = new MazeDebugView(maze);
            int d = 0;
        }
    }
}
