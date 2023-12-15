using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace MazeGeneratingByBacktracking
{
    [DebuggerTypeProxy(typeof(MazeDebugView))]
    internal class MazeGenerator
    {
        private static readonly Random _random = new Random();

        public Maze Generate(Size size)
        {
            CheckSize(size);

            var newSize = size.AddWalls();
            var maze = new Maze(newSize);
            var visitedCells = new bool[newSize.Height, newSize.Width];
            var stack = new Stack<Point>();
            var start = new Point(0, 0);
            var currient = start;
            //1. Сделайте начальную клетку текущей и отметьте ее как посещенную.
            MarkPoint(visitedCells, start);

            //2. Пока есть непосещенные клетки
            while (HasUnvisitedCells(visitedCells))
            {
                var unvisitedNeighbors = GetUnvisitedNeighbors(currient, visitedCells, maze);

                #region 1. Если текущая клетка имеет непосещенных «соседей»
                if (unvisitedNeighbors.Any())
                {
                    stack.Push(currient);
                    //var next = GetRandomNeighbor(currient, maze);
                    var next = unvisitedNeighbors[_random.Next(unvisitedNeighbors.Length)];
                    RemoveWall(currient, next, maze);
                    currient = next;
                    MarkPoint(visitedCells, currient);
                }
                #endregion 1. Если текущая клетка имеет непосещенных «соседей»

                #region 2. Иначе если стек не пуст
                else if (stack.Any())
                {
                    currient = stack.Pop();
                }
                #endregion 2. Иначе если стек не пуст

                #region 3. Иначе
                else
                {
                    currient = GetRandomUnvisitedNeighbor(visitedCells);
                    MarkPoint(visitedCells, currient);
                }
                #endregion 3. Иначе
            }

            return maze;
        }

        private static void MarkPoint(bool[,] visitedCells, Point point)
        {
            visitedCells[point.Y, point.X] = true;
        }

        private static void CheckSize(Size size)
        {
            if (size.Width == 0 || size.Height == 0)
            {
                throw new InvalidOperationException($"{nameof(size)} has 0 value dimension");
            }
        }

        private static bool HasUnvisitedCells(bool[,] visitedCells)
        {
            for (int i = 0;i < visitedCells.GetLength(0); i++) 
            {
                for (int j = 0; j < visitedCells.GetLength(1); j++)
                {
                    if (!visitedCells[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static Point[] GetUnvisitedNeighbors(Point point, bool[,] visitedCells, Maze maze)
        {
            var neighbors = GetNeighbors(point, maze);

            return neighbors
                .Where(neighbor => !visitedCells[neighbor.Y, neighbor.X])
                .ToArray();
        }

        private static Point[] GetNeighbors(Point point, Maze maze)
        {
            const int step = 2;

            var upPoint = new Point(0, -step) + point;
            var downPoint = new Point(0, step) + point;
            var leftPoint = new Point(-step, 0) + point;
            var rightPoint = new Point(step, 0) + point;

            Point[] neighborsPoints =
            {
                upPoint,
                downPoint,
                leftPoint,
                rightPoint
            };

            return neighborsPoints
                .Where(neighbor => maze.Contains(neighbor))
                .ToArray();
        }

        private static Point GetRandomNeighbor(Point point, Maze maze)
        {
            var neighbors = GetNeighbors(point, maze);
            var index = _random.Next(neighbors.Length);

            return neighbors[index];
        }

        private static void RemoveWall(Point currient, Point next, Maze maze)
        {
            var centerX = (currient.X + next.X) / 2; 
            var centerY = (currient.Y + next.Y) / 2;

            maze[centerX, centerY] = CellType.Floor;
        }

        private static Point GetRandomUnvisitedNeighbor(bool[,] visitedCells)
        {
            var unvisitedPoints = new List<Point>();

            for (int i = 0; i < visitedCells.GetLength(0); i++)
            {
                for (int j = 0; j < visitedCells.GetLength(1); j++)
                {
                    if (!visitedCells[i, j])
                    {
                        unvisitedPoints.Add(new Point(j, i));
                    }
                }
            }

            var index = _random.Next(unvisitedPoints.Count);

            return unvisitedPoints[index];
        }
    }
}
