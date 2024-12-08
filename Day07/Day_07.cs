using AdventOfCode2024.Core;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace AdventOfCode2024
{

	internal class Day_07: Solver
	{
		private readonly List<string> _inputLines = FileReader.ReadInput("Day07/input");
		private List<(long, List<long>)> ResultFactors = new();

		public Day_07()
		{
			foreach (var line in _inputLines)
			{
				MatchCollection matchCollection = Regex.Matches(line, @"\d+");

				List<long> matches = matchCollection.Select(x => long.Parse(x.Value)).ToList();
				ResultFactors.Add((matches[0], matches.Where((x, i) => i > 0).ToList()));
			}
		}

		public override void PartOne()
		{
			Stopwatch sw = new();

			sw.Restart();
			long result = ResultFactors.Sum(x => AllSolutions(x));
			sw.Stop();
			Console.WriteLine($"Single: {sw.Elapsed}");
			WriteSolution<long>(result);

			sw.Restart();
			long plinqResults = ResultFactors.AsParallel().Sum(x => AllSolutions(x));

			sw.Stop();
			Console.WriteLine($"PLINQ: {sw.Elapsed}");
			WriteSolution(plinqResults);
		}

		public override void PartTwo()
		{
			Stopwatch sw = new();

			sw.Restart();
			long result = ResultFactors.Sum(x => AllSolutions(x, true));
			sw.Stop();
			Console.WriteLine($"Single: {sw.Elapsed}");
			WriteSolution<long>(result, false);

			sw.Restart();
			long plinqResults = ResultFactors.AsParallel().Sum(x => AllSolutions(x, true));

			sw.Stop();
			Console.WriteLine($"PLINQ: {sw.Elapsed}");
			WriteSolution(plinqResults, false);
		}

		public long AllSolutions((long, List<long>) resultWFactors, bool partTwo = false)
		{
			List<string> operators = new();
			GetOperators("", resultWFactors.Item2.Count - 1, operators, partTwo);
			foreach (var operation in operators)
			{
				if (CalculationCorrect(resultWFactors, operation))
				{
					return resultWFactors.Item1;
				}
			}
			return 0;
		}

		public static bool CalculationCorrect((long, List<long>) resultWFactors, string operators)
		{
			long properResult = resultWFactors.Item1;
			List<long> factors = resultWFactors.Item2;
			long result = factors[0];
			for (int i = 0; i < factors.Count - 1; i++)
			{
				result = Calculate(result, factors[i + 1], operators[i]);
			}
			return (result == properResult);
		}

		public static long Calculate(long a, long b, char operation)
		{
			switch (operation)
			{
				case '+':
					return a + b;
				case '*':
					return a * b;
				case '/':
					return long.Parse(a.ToString() + b.ToString());
				default:
					throw new Exception($"Invalid operator {operation}");
			}
		}

		public static void GetOperators(string insofar, int operatorsNeeded, List<string> solutions, bool partTwo = false)
		{
			if (insofar.Length == operatorsNeeded)
			{
				solutions.Add(insofar);
				return;
			}

			GetOperators(insofar + "+", operatorsNeeded, solutions, partTwo);
			GetOperators(insofar + "*", operatorsNeeded, solutions, partTwo);
			if (partTwo)
			{
				GetOperators(insofar + "/", operatorsNeeded, solutions, partTwo);
			}
		}
	}
}
