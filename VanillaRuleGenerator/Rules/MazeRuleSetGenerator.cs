using System.Collections.Generic;

namespace VanillaRuleGenerator.Rules
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
		    MakeMazes(mazeRuleSet, list);
			return mazeRuleSet;
		}

	    public void MakeMazes(MazeRuleSet mazeRuleSet, List<MazeRuleSetGenerator.MazePoint> list=null)
	    {
	        for (int k = 0; k < 9; k++)
	        {
	            Maze maze = MazeBuilder.BuildMaze(6, this.rand);
	            mazeRuleSet.AddMaze(maze);
	            if (list != null)
	            {
	                MazeRuleSetGenerator.MazePoint mazePoint = list[this.rand.Next(list.Count)];
	                list.Remove(mazePoint);
	                MazeRuleSetGenerator.MazePoint mazePoint2 = list[this.rand.Next(list.Count)];
	                list.Remove(mazePoint2);
	                maze.SetIndicators(mazePoint.x, mazePoint.y, mazePoint2.x, mazePoint2.y);
	            }
	            else
	            {
	                rand.Next();
	                rand.Next();
	            }
	        }
        }

		private const int NUM_MAZES = 9;

		private const int MAZE_SIZE = 6;

		public class MazePoint
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
