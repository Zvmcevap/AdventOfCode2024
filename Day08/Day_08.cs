using AdventOfCode2024.Core;


namespace AdventOfCode2024
{

	internal class Day_08: Solver
	{
		private readonly List<string> _inputLines = FileReader.ReadInput("Day08/input");

		private readonly Dictionary<char, List<Vector2Int>> _antennas = new();

		public Day_08()
		{
			for (int y = 0; y < _inputLines.Count; y++)
			{
				for (int x = 0; x < _inputLines[0].Length; x++)
				{
					char val = _inputLines[y][x];
					if (val != '.')
					{
						if (!_antennas.ContainsKey(val))
						{
							_antennas[val] = new();
						}
						_antennas[val].Add(new Vector2Int(x, y));
					}
				}
			}
		}

		public override void PartOne()
		{
			HashSet<Vector2Int> antinodes = new();
			foreach (List<Vector2Int> antennaLocs in _antennas.Values)
			{
				for (int i = 0; i < antennaLocs.Count - 1; i++)
				{
					for (int j = i + 1; j < antennaLocs.Count; j++)
					{
						Vector2Int a = antennaLocs[i];
						Vector2Int b = antennaLocs[j];

						int deltaX = Math.Abs(a.x - b.x);
						int deltaY = Math.Abs(a.y - b.y);

						Vector2Int firstAntinode = new Vector2Int((a.x < b.x) ? a.x - deltaX : a.x + deltaX, (a.y < b.y) ? a.y - deltaY : a.y + deltaY);
						Vector2Int secondAntinode = new Vector2Int((b.x < a.x) ? b.x - deltaX : b.x + deltaX, (b.y < a.y) ? b.y - deltaY : b.y + deltaY);

						if (IsWithinBounds(firstAntinode))
							antinodes.Add(firstAntinode);
						if (IsWithinBounds(secondAntinode))
							antinodes.Add(secondAntinode);
					}
				}
			}
			WriteSolution(antinodes.Count);

		}

		public override void PartTwo()
		{
			HashSet<Vector2Int> antinodes = new();
			foreach (List<Vector2Int> antennaLocs in _antennas.Values)
			{
				for (int i = 0; i < antennaLocs.Count - 1; i++)
				{
					for (int j = i + 1; j < antennaLocs.Count; j++)
					{
						Vector2Int a = antennaLocs[i];
						antinodes.Add(a);
						Vector2Int b = antennaLocs[j];
						antinodes.Add(b);

						int deltaX = Math.Abs(a.x - b.x);
						int deltaY = Math.Abs(a.y - b.y);

						Vector2Int firstAntinode = new Vector2Int((a.x < b.x) ? a.x - deltaX : a.x + deltaX, (a.y < b.y) ? a.y - deltaY : a.y + deltaY);
						Vector2Int secondAntinode = new Vector2Int((b.x < a.x) ? b.x - deltaX : b.x + deltaX, (b.y < a.y) ? b.y - deltaY : b.y + deltaY);

						while (IsWithinBounds(firstAntinode))
						{
							antinodes.Add(firstAntinode);
							firstAntinode = new Vector2Int((a.x < b.x) ? firstAntinode.x - deltaX : firstAntinode.x + deltaX, (a.y < b.y) ? firstAntinode.y - deltaY : firstAntinode.y + deltaY);
						}
						while (IsWithinBounds(secondAntinode))
						{
							antinodes.Add(secondAntinode);
							secondAntinode = new Vector2Int((b.x < a.x) ? secondAntinode.x - deltaX : secondAntinode.x + deltaX, (b.y < a.y) ? secondAntinode.y - deltaY : secondAntinode.y + deltaY);
						}
					}
				}
			}
			WriteSolution(antinodes.Count, false);
		}

		public bool IsWithinBounds(Vector2Int location)
		{
			return location.x >= 0 && location.y >= 0 && location.x < _inputLines[0].Length && location.y < _inputLines.Count;
		}
	}
}
