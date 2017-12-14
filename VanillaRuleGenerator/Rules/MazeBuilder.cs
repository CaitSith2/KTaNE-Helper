using System.Collections.Generic;
using Random = VanillaRuleGenerator.Helpers.MonoRandom;

namespace VanillaRuleGenerator.Rules
{
    public class MazeBuilder
    {
        public static Maze BuildMaze(int numCells, Random rand)
        {
            Maze maze = new Maze();
            maze.Size = numCells;
            maze.CellGrid = new List<List<MazeCell>>();
            for (int i = 0; i < numCells; i++)
            {
                List<MazeCell> list = new List<MazeCell>();
                maze.CellGrid.Add(list);
                for (int j = 0; j < numCells; j++)
                {
                    MazeCell mazeCell = new MazeCell(i, j);
                    list.Add(mazeCell);
                    if (i > 0)
                    {
                        bool flag = rand.NextDouble() < 0.0;
                        if (flag)
                        {
                            mazeCell.HideLeft = true;
                            maze.CellGrid[i - 1][j].HideRight = true;
                        }
                    }
                    if (j > 0)
                    {
                        bool flag2 = rand.NextDouble() < 0.0;
                        if (flag2)
                        {
                            mazeCell.HideAbove = true;
                            maze.CellGrid[i][j - 1].HideBelow = true;
                        }
                    }
                }
            }
            MazeBuilder.PopulateMaze(maze, rand, numCells);
            return maze;
        }

        private static void PopulateMaze(Maze maze, Random rand, int numCells)
        {
            Stack<MazeCell> cellStack = new Stack<MazeCell>();
            int x = rand.Next(0, numCells);
            int y = rand.Next(0, numCells);
            MazeCell cell = maze.GetCell(x, y);
            MazeBuilder.VisitCell(cell, cellStack, maze, rand);
        }

        private static void VisitCell(MazeCell cell, Stack<MazeCell> cellStack, Maze maze, Random rand)
        {
            cell.Visited = true;
            MazeCell mazeCell = maze.GetNextNeighbour(cell, rand);
            if (mazeCell != null)
            {
                MazeCell.RemoveWalls(cell, mazeCell);
                cellStack.Push(cell);
                MazeBuilder.VisitCell(mazeCell, cellStack, maze, rand);
            }
            else if (cellStack.Count > 0)
            {
                mazeCell = cellStack.Pop();
                MazeBuilder.VisitCell(mazeCell, cellStack, maze, rand);
            }
        }

        private const double HIDDEN_CHANCE = 0.0;
    }
}
