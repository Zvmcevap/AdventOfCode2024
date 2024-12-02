using AdventOfCode2024.Core;


namespace AdventOfCode2024
{
	internal class Day_01: Solver
	{
		private List<string> _inputLines = FileReader.ReadInput("Day01/input");

		private readonly List<int> _firstRow = new();
		private readonly List<int> _secondRow = new();

		public Day_01()
		{
			foreach (var line in _inputLines)
			{
				var words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
					.Where(word => !string.IsNullOrWhiteSpace(word))
					.ToArray();

				_firstRow.Add(int.Parse(words[0]));
				_secondRow.Add(int.Parse(words[1]));
			}
		}

		public override void PartOne()
		{
			_secondRow.Sort();
			_firstRow.Sort();

			int solution = 0;
			for (int i = 0; i < _firstRow.Count; i++)
			{
				solution += Math.Abs(_firstRow[i] - _secondRow[i]);
			}

			WriteSolution(solution);
		}

		public override void PartTwo()
		{
			Dictionary<int, int> firstRowAppearances = _firstRow
															.GroupBy(x => x)
															.ToDictionary(g => g.Key, g => g.Count());
			Dictionary<int, int> secondRowAppearances = _secondRow
															.GroupBy(x => x)
															.ToDictionary(g => g.Key, g => g.Count());
			int solution = 0;
			foreach (int key in _firstRow)
			{
				if (secondRowAppearances.ContainsKey(key))
				{
					solution += key * firstRowAppearances[key] * secondRowAppearances[key];
				}
			}

			WriteSolution(solution, false);
		}
	}
}
