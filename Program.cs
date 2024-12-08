using AdventOfCode2024.Core;

namespace AdventOfCode2024
{
	internal static class Program
	{
		public static List<Solver> Solvers = [
			new Day_01(),
			new Day_02(),
			new Day_03(),
			new Day_04(),
			new Day_05(),
			new Day_06(),
			new Day_07(),
			new Day_08(),
			];

		static void Main(string[] args)
		{
			while (UI(Solvers)) { }
		}

		private static bool UI(List<Solver> solvers)
		{
			int selection = -1;
			while (selection < 0)
			{
				Console.WriteLine("\t****");
				Console.WriteLine("Select Day");
				for (int i = 0; i < solvers.Count; i++)
				{
					Console.WriteLine($"{i + 1} - {solvers[i].ToString()}");
				}
				Console.WriteLine("0 - back");

				try
				{
					selection = int.Parse(Console.ReadLine());
				}
				catch (Exception)
				{
					Console.WriteLine("Invalid Selection!");
				}

				if (selection == 0)
				{
					return false;
				}

				if (selection < 0 || selection > solvers.Count)
				{
					Console.WriteLine("Invalid Selection!");
				}
				else
				{
					solvers[selection - 1].UI();
				}
				break;
			}

			return true;
		}

	}
}
