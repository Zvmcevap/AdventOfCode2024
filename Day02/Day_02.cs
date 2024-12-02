using AdventOfCode2024.Core;


namespace AdventOfCode2024
{
	internal class Day_02: Solver
	{
		private List<string> _inputLines = FileReader.ReadInput("Day02/input");
		private List<int[]> _reports = new();

		private string _debugString;

		public Day_02()
		{
			foreach (string line in _inputLines)
			{
				int[] report = line.Split(' ').Select(int.Parse).ToArray();
				_reports.Add(report);
			}
		}

		public override void PartOne()
		{
			int solution = 0;
			foreach (int[] report in _reports)
			{
				if (IsSafe(report))
					solution++;
			}

			WriteSolution(solution);
		}

		public override void PartTwo()
		{
			int solution = 0;
			foreach (int[] report in _reports)
			{
				if (IsSafe(report, true))
					solution++;

			}
			WriteSolution(solution, false);
		}

		private bool IsSafe(int[] report, bool withDampener = false)
		{
			bool goingUp = report[0] < report[1];
			for (int i = 0; i < report.Length - 1; i++)
			{
				int delta = CheckPair(report[i], report[i + 1], goingUp);

				if (delta < 1 || delta > 3)
				{
					if (!withDampener)
					{
						return false;
					}

					bool amFixed = false;
					int[] fixedReport = report.Where((_, index) => i != index).ToArray();
					amFixed = IsSafe(fixedReport, false);

					if (!amFixed)
					{
						fixedReport = report.Where((_, index) => i + 1 != index).ToArray();
						amFixed = IsSafe(fixedReport, false);
					}

					if (!amFixed)
					{
						fixedReport = report.Where((_, index) => i - 1 != index).ToArray();
						amFixed = IsSafe(fixedReport, false);
					}
					return amFixed;
				}
			}
			return true;
		}


		private int CheckPair(int a, int b, bool goingUp)
		{
			return goingUp ? b - a : a - b;
		}
	}
}
