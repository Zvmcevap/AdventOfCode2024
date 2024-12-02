namespace AdventOfCode2024.Core
{


	internal static class FileReader
	{
		public static List<string> ReadInput(string fileName)
		{
			List<string> input = new List<string>();

			// Automatically determine the file path
			string defaultDirectory = AppDomain.CurrentDomain.BaseDirectory; // Current working directory
			string filePath = Path.Combine(defaultDirectory, fileName);

			try
			{
				if (!File.Exists(filePath))
				{
					Console.WriteLine($"File not found: {filePath}");
					return input; // Return empty list if the file doesn't exist
				}

				using (StreamReader reader = new StreamReader(filePath))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						input.Add(line);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
			}

			return input;
		}
	}

}
