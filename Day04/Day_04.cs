using AdventOfCode2024.Core;


namespace AdventOfCode2024
{

	internal class Day_04: Solver
	{
		private List<string> _inputLines = FileReader.ReadInput("Day04/input");

		private readonly char[] _allIWantFor = ['X', 'M', 'A', 'S'];
		private readonly Vector2Int[] _dirs =
			[
				new(1, 0),
				new(-1, 0),
				new(0, 1),
				new(0, -1),
				new(1, 1),
				new(-1, -1),
				new(1, -1),
				new(-1, 1),
			];

		public override void PartOne()
		{
			List<Task<int>> allSearches = new();
			for (int row = 0; row < _inputLines.Count; row++)
			{
				for (int col = 0; col < _inputLines[row].Length; col++)
				{
					if (_inputLines[row][col] == _allIWantFor[0])
						allSearches.Add(AllDirections(new Vector2Int(row, col)));
				}
			}
			Task.WhenAll(allSearches);
			int solution = allSearches.Select(x => x.Result).Sum();

			WriteSolution(solution);
		}

		public override void PartTwo()
		{
			List<Task<bool>> allSearches = new();
			for (int row = 0; row < _inputLines.Count; row++)
			{
				for (int col = 0; col < _inputLines[row].Length; col++)
				{
					if (_inputLines[row][col] == 'A')
						allSearches.Add(ShapeOfYou(new Vector2Int(row, col)));
				}
			}
			Task.WhenAll(allSearches);
			int solution = allSearches.Where(x => x.Result).Count();
			WriteSolution(solution, false);
		}

		async Task<bool> ShapeOfYou(Vector2Int start)
		{
			// 4, 5 one diagonal 6, 7 another
			List<char> diagonal = new();
			for (int i = 4; i <= 5; i++)
			{
				Vector2Int current = start + _dirs[i];
				if (current.x < 0 || current.x >= _inputLines.Count || current.y < 0 || current.y >= _inputLines[current.x].Length)
					return false;
				diagonal.Add(_inputLines[current.x][current.y]);
			}
			if (!(diagonal.Contains('M') && diagonal.Contains('S'))) return false;

			// Second diagonal
			diagonal.Clear();
			for (int i = 6; i <= 7; i++)
			{
				Vector2Int current = start + _dirs[i];
				if (current.x < 0 || current.x >= _inputLines.Count || current.y < 0 || current.y >= _inputLines[current.x].Length)
					return false;
				diagonal.Add(_inputLines[current.x][current.y]);
			}
			if (!(diagonal.Contains('M') && diagonal.Contains('S'))) return false;

			return true;
		}



		async Task<int> AllDirections(Vector2Int start)
		{
			Task<bool>[] findEm = new Task<bool>[_dirs.Length];
			for (int i = 0; i < _dirs.Length; i++)
			{
				findEm[i] = FindXmas(start, _dirs[i]);
			}

			await Task.WhenAll(findEm);
			return findEm.Where(x => x.Result).Count();
		}


		async Task<bool> FindXmas(Vector2Int start, Vector2Int dir)
		{
			Vector2Int current = start;
			for (int i = 1; i < 4; i++)
			{
				current += dir;
				if (current.x < 0 || current.x >= _inputLines.Count || current.y < 0 || current.y >= _inputLines[current.x].Length)
					return false;
				if (!(_inputLines[current.x][current.y] == _allIWantFor[i]))
					return false;
			}

			return true;
		}
	}
}
