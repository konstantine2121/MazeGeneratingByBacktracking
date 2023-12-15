using System.Text;

namespace MazeGeneratingByBacktracking
{
    internal class MazeDebugView
    {
        private const char Border = '█';
        private const char Wall = '#';
        private const char Floor = ' ';

        private readonly Maze _maze;

        public MazeDebugView(Maze maze)
        {
            _maze = maze;
        }

        public string View
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();

                var borderWidth = _maze.Width + 2;
                var topDownBorder = new string(Border, borderWidth);

                stringBuilder.AppendLine(topDownBorder);

                for (int y = 0; y < _maze.Height; y++)
                {
                    AddRowText(stringBuilder, y);
                }

                stringBuilder.AppendLine(topDownBorder);

                return stringBuilder.ToString();
            }
        }

        private void AddRowText(StringBuilder stringBuilder, int y)
        {
            stringBuilder.Append(Border);

            for (int x = 0; x < _maze.Width; x++)
            {
                stringBuilder.Append(_maze[x, y] == CellType.Wall ? Wall : Floor);
            }

            stringBuilder.Append(Border);
            stringBuilder.AppendLine();
        }
    }
}
