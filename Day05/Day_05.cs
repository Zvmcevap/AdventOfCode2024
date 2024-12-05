using AdventOfCode2024.Core;


namespace AdventOfCode2024
{

	internal class Day_05: Solver
	{
		private readonly List<string> _inputLines = FileReader.ReadInput("Day05/input");
		private readonly Dictionary<string, List<string>> _pageOrderings = new();
		private readonly List<string[]> _printer = new();

		private readonly List<string[]> _valids = new();
		private readonly List<string[]> _invalids = new();

		public Day_05()
		{
			bool endOrdering = false;
			foreach (var line in _inputLines)
			{
				if (line == "")
				{
					endOrdering = true;
				}
				else if (!endOrdering)
				{
					string[] input = line.Split('|');
					if (!_pageOrderings.ContainsKey(input[0]))
					{
						_pageOrderings[input[0]] = new();
					}
					_pageOrderings[input[0]].Add(input[1]);
				}
				else
				{
					_printer.Add(line.Split(","));
				}
			}

			foreach (string[] sortering in _printer)
			{
				bool IamValid = true;
				for (int i = sortering.Length - 1; i >= 0; i--)
				{
					string current = sortering[i];
					if (_pageOrderings.TryGetValue(current, out List<string> value))
					{
						if (!IsCorrectOrdering(value, sortering, i))
						{
							IamValid = false;
							_invalids.Add(sortering);
							break;
						}
					}
				}
				if (IamValid)
				{
					_valids.Add(sortering);
				}
			}

		}

		public override void PartOne()
		{
			List<string> solutions = new();
			foreach (string[] valid in _valids)
			{
				solutions.Add(valid[valid.Length / 2]);
			}

			WriteSolution(solutions.Select(int.Parse).Sum());
		}

		public override void PartTwo()
		{
			foreach (string[] invalid in _invalids)
			{
				bool IamStillInvalid = true;
				while (IamStillInvalid)
				{
					IamStillInvalid = false;
					for (int i = 0; i < invalid.Length - 1; i++)
					{
						string first = invalid[i];
						string second = invalid[i + 1];
						if (_pageOrderings.TryGetValue(second, out List<string> beforeThis) && beforeThis.Contains(first))
						{
							invalid[i] = second;
							invalid[i + 1] = first;
							IamStillInvalid = true;
						}
					}
				}
			}
			List<string> solutions = _invalids.Select(x => x[x.Length / 2]).ToList();
			WriteSolution(solutions.Select(int.Parse).Sum(), false);
		}

		private bool IsCorrectOrdering(List<string> beforeThis, string[] restOfOrdering, int upTo)
		{
			for (int i = 0; i < upTo; i++)
			{
				if (beforeThis.Contains(restOfOrdering[i]))
					return false;
			}
			return true;
		}
	}
}
