using AdventOfCode2024.Core;
using System.Text.RegularExpressions;


namespace AdventOfCode2024
{

	internal class Day_03: Solver
	{
		private List<string> _inputLines = FileReader.ReadInput("Day03/input");


		public override void PartOne()
		{
			List<int> mules = new();
			string regexMatch = @"mul\(\d+,\d+\)";
			foreach (string line in _inputLines)
			{
				MatchCollection matches = Regex.Matches(line, regexMatch);
				List<int> lineMuls = matches
								.Select(x => FindFactorsAndMule(x.Value))
								.ToList();

				mules.AddRange(lineMuls);
			}
			int solution = mules.Sum();
			WriteSolution(solution);
		}

		public override void PartTwo()
		{
			string regexMatch = @"mul\(\d+,\d+\)|don't\(\)|do\(\)";

			int solution = 0;
			bool doDaDid = true;
			foreach (string line in _inputLines)
			{
				MatchCollection matches = Regex.Matches(line, regexMatch);
				foreach (Match m in matches)
				{
					switch (m.Value)
					{
						case "do()":
							doDaDid = true;
							break;
						case "don't()":
							doDaDid = false;
							break;
						default:
							if (doDaDid) solution += FindFactorsAndMule(m.Value);
							break;
					}
				}
			}
			WriteSolution(solution);
		}
		private int FindFactorsAndMule(string mulane)
		{
			MatchCollection maches = Regex.Matches(mulane, @"\d+");
			return int.Parse(maches[0].Value) * int.Parse(maches[1].Value);
		}
	}
}
