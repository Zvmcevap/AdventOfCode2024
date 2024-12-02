namespace AdventOfCode2024.Core
{
	internal abstract class Solver
	{
		public abstract void PartOne();
		public abstract void PartTwo();

		#region UI
		public void UI()
		{
			int selection = -1;
			while (selection < 0)
			{
				Console.WriteLine("\t****");
				Console.WriteLine($"Selected: {this}");
				Console.WriteLine("1 - Part One\n2 - Part Two\n0 - Back");

				try
				{
					selection = int.Parse(Console.ReadLine());
				}
				catch (Exception)
				{
					Console.WriteLine("Invalid Selection!");
				}

				switch (selection)
				{
					case 0:
						return;
					case 1:
						PartOne();
						break;
					case 2:
						PartTwo();
						break;
					default:
						break;
				}
				selection = -1;
			}
		}

		public void WriteSolution(int solution, bool firstPart = true)
		{
			string strPart = firstPart ? "one" : "two";
			Console.WriteLine("\t****");
			Console.WriteLine($"Solution part {strPart} = {solution}");
		}

		public override string ToString()
		{
			return this.GetType().Name;
		}
		#endregion
	}
}
