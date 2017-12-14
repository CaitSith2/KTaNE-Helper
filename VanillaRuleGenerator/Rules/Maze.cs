using System.Collections.Generic;
using Random = VanillaRuleGenerator.Helpers.MonoRandom;

namespace VanillaRuleGenerator.Rules
{
    public class Maze
    {
        public MazeCell GetCell(int x, int y)
        {
            try
            {
                return this.CellGrid[x][y];
            }
            catch
            {
                return new MazeCell(x,y);
            }
        }

        public MazeCell GetNextNeighbour(MazeCell cell, Random rand)
        {
            List<MazeCell> list = new List<MazeCell>();
            if (cell.X > 0 && !this.CellGrid[cell.X - 1][cell.Y].Visited)
            {
                list.Add(this.CellGrid[cell.X - 1][cell.Y]);
            }
            if (cell.X < this.CellGrid.Count - 1 && !this.CellGrid[cell.X + 1][cell.Y].Visited)
            {
                list.Add(this.CellGrid[cell.X + 1][cell.Y]);
            }
            if (cell.Y > 0 && !this.CellGrid[cell.X][cell.Y - 1].Visited)
            {
                list.Add(this.CellGrid[cell.X][cell.Y - 1]);
            }
            if (cell.Y < this.CellGrid[cell.X].Count - 1 && !this.CellGrid[cell.X][cell.Y + 1].Visited)
            {
                list.Add(this.CellGrid[cell.X][cell.Y + 1]);
            }
            if (list.Count > 0)
            {
                return list[rand.Next(0, list.Count)];
            }
            return null;
        }

        private int[] ToIntArray()
        {
            int[] array = new int[this.Size * this.Size + 1];
            array[0] = this.Size;
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    array[i * this.Size + j + 1] = this.CellGrid[i][j].ToInt();
                }
            }
            return array;
        }

        public override string ToString()
        {
            int[] array = this.ToIntArray();
            string text = string.Empty;
            foreach (int num in array)
            {
                text = text + num + ",";
            }
            return text.Substring(0, text.Length - 1);
        }

        public void FromString(string value)
        {
            string[] array = value.Split(new char[]
            {
                ','
            });
            this.Size = int.Parse(array[0]);
            this.CellGrid = new List<List<MazeCell>>();
            int num = -1;
            int num2 = this.Size;
            List<MazeCell> list = null;
            for (int i = 1; i < array.Length; i++)
            {
                if (num2 == this.Size)
                {
                    list = new List<MazeCell>();
                    this.CellGrid.Add(list);
                    num++;
                    num2 = 0;
                }
                MazeCell mazeCell = new MazeCell(num, num2);
                mazeCell.FromInt(int.Parse(array[i]));
                list.Add(mazeCell);
                num2++;
            }
        }

        public void SetIndicators(int x1, int y1, int x2, int y2)
        {
            this.CellGrid[x1][y1].IsIdentifier = true;
            this.CellGrid[x2][y2].IsIdentifier = true;
        }

        public string ToSVG()
        {
            int num = 300;
            int num2 = 300;
            SVGGenerator svggenerator = new SVGGenerator(num, num2);
            float num3 = (float)num / (float)this.Size;
            float num4 = (float)num2 / (float)this.Size;
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    MazeCell mazeCell = this.CellGrid[i][j];
                    float num5 = (float)i * num3;
                    float num6 = (float)j * num4;
                    svggenerator.DrawCircle(num5 + num3 / 2f, num6 + num4 / 2f, 3f, true);
                    if (mazeCell.IsIdentifier)
                    {
                        svggenerator.DrawCircle(num5 + num3 / 2f, num6 + num4 / 2f, 15f, false);
                    }
                    if (mazeCell.WallAbove && (!mazeCell.HideAbove || i <= 0))
                    {
                        string strokeWidth = "3";
                        if (j == 0)
                        {
                            strokeWidth = "10";
                        }
                        svggenerator.DrawLine(num5, num6, num5 + num3, num6, strokeWidth, string.Empty);
                    }
                    if (mazeCell.WallLeft && (!mazeCell.HideLeft || j <= 0))
                    {
                        string strokeWidth2 = "3";
                        if (i == 0)
                        {
                            strokeWidth2 = "10";
                        }
                        svggenerator.DrawLine(num5, num6, num5, num6 + num4, strokeWidth2, string.Empty);
                    }
                    if (i == this.Size - 1)
                    {
                        svggenerator.DrawLine(num5 + num3, num6, num5 + num3, num6 + num4, "10", string.Empty);
                    }
                    if (j == this.Size - 1)
                    {
                        svggenerator.DrawLine(num5, num6 + num4, num5 + num3, num6 + num4, "10", string.Empty);
                    }
                }
            }
            return svggenerator.ToString();
        }

        public List<List<MazeCell>> CellGrid;

        public int Size;
    }
}
