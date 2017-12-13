using System.Collections.Generic;
using KTaNE_Helper.VanillaRuleSetGenerator.RuleSets;

namespace KTaNE_Helper.VanillaRuleSetGenerator.RuleSetGenerators
{
	public class MazeRuleSetGenerator : AbstractRuleSetGenerator
	{
		protected override AbstractRuleSet CreateRules(bool useDefault)
		{
			MazeRuleSet mazeRuleSet = new MazeRuleSet();
			List<MazeRuleSetGenerator.MazePoint> list = new List<MazeRuleSetGenerator.MazePoint>();
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					list.Add(new MazeRuleSetGenerator.MazePoint(i, j));
				}
			}
			for (int k = 0; k < 9; k++)
			{
				Maze maze = MazeBuilder.BuildMaze(6, this.rand);
				mazeRuleSet.AddMaze(maze);
				MazeRuleSetGenerator.MazePoint mazePoint = list[this.rand.Next(list.Count)];
				list.Remove(mazePoint);
				MazeRuleSetGenerator.MazePoint mazePoint2 = list[this.rand.Next(list.Count)];
				list.Remove(mazePoint2);
				maze.SetIndicators(mazePoint.x, mazePoint.y, mazePoint2.x, mazePoint2.y);
			}
			return mazeRuleSet;
		}

		private const int NUM_MAZES = 9;

		private const int MAZE_SIZE = 6;

		private class MazePoint
		{
			public MazePoint(int a, int b)
			{
				this.x = a;
				this.y = b;
			}

			public int x;

			public int y;
		}
	}
}
