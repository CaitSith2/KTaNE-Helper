using System;
using System.Collections.Generic;

namespace VanillaRuleGenerator.Rules
{
	public class MazeRuleSet : AbstractRuleSet
	{
		public MazeRuleSet()
		{
			this.mazes = new List<Maze>();
		}

		public void AddMaze(Maze maze)
		{
			this.mazes.Add(maze);
		}

		public Maze GetRandomMaze()
		{
			return this.mazes[new Random().Next(0, this.mazes.Count)];
		}

		public List<Maze> GetMazes()
		{
			return this.mazes;
		}

	    public void ClearMazes()
	    {
	        mazes.Clear();
	    }

		public override string ToString()
		{
			string text = string.Empty;
			for (int i = 0; i < this.mazes.Count; i++)
			{
				text += string.Format("Maze {0}: {1}\n", i, this.mazes[i]);
			}
			return text;
		}

		private List<Maze> mazes;
	}
}
