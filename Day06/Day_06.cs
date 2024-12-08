using AdventOfCode2024.Core;


namespace AdventOfCode2024
{

	internal class Day_06: Solver
	{
		private readonly List<string> _inputLines = FileReader.ReadInput("Day06/input");
		private char[,] _map;
		private int _width, _height;

		private Vector2Int _startPos;

		private readonly Vector2Int[] _directions = [
			new Vector2Int(0, -1),
			new Vector2Int(1, 0),
			new Vector2Int(0, 1),
			new Vector2Int(-1, 0),
			];

		private readonly HashSet<Vector2Int> _solutionsPartDos = new();
		private readonly Dictionary<Vector2Int, Task<bool>> _wallSolutions = new();

		public Day_06()
		{
			_height = _inputLines.Count;
			_width = _inputLines[0].Length;
			_map = new char[_height, _width];
			for (int y = 0; y < _width; y++)
			{
				for (int x = 0; x < _width; x++)
				{
					_map[y, x] = _inputLines[y][x];
					if (_inputLines[y][x] == '^')
					{
						_startPos = new Vector2Int(x, y);
					}
				}
			}
		}

		public override void PartOne()
		{
			HashSet<Vector2Int> visited = new();
			Dictionary<(Vector2Int, int), int> visWithDir = new();
			Task<bool> solution = TakeAWalk(_startPos, 0, _map, visited, visWithDir);
			if (solution.IsCompleted)
			{
				WriteSolution(visited.Count);
			}
		}

		public override void PartTwo()
		{
			HashSet<Vector2Int> visited = new();
			Dictionary<(Vector2Int, int), int> visitedWDirections = new();
			Task<bool> partTwo = TakeAWalk(_startPos, 0, _map, visited, visitedWDirections, true);
			while (!partTwo.IsCompleted) { }

			bool all_finished = false;
			while (!all_finished)
			{
				all_finished = true;
				foreach ((Vector2Int wall, Task<bool> task) in _wallSolutions)
				{
					if (task.IsCompleted)
					{
						if (task.Result)
							_solutionsPartDos.Add(wall);
					}
					else
						all_finished = false;
				}
			}
			WriteSolution(_solutionsPartDos.Count, false);

		}

		public async Task<bool> TakeAWalk(Vector2Int currentPos, int currentDir, char[,] map, HashSet<Vector2Int> visited, Dictionary<(Vector2Int, int), int> visWithDir, bool partTwo = false)
		{
			while (true)
			{
				visited.Add(currentPos);
				if (!visWithDir.ContainsKey((currentPos, currentDir)))
				{
					visWithDir[(currentPos, currentDir)] = 0;
				}
				visWithDir[(currentPos, currentDir)] += 1;

				if (visWithDir[(currentPos, currentDir)] > 1)
					return true;

				while (IsBlokade(currentPos + _directions[currentDir], map)) //While next is not '#'
				{
					currentDir = (currentDir + 1) % _directions.Length;
				}

				if (!PosInRange(currentPos + _directions[currentDir], map)) // if next is in bounds
				{
					return false;
				}


				if (partTwo)
				{
					Vector2Int wallPos = currentPos + _directions[currentDir];
					if (_wallSolutions.ContainsKey(wallPos))
					{
						bool isWall = await _wallSolutions[wallPos];
						if (isWall)
						{
							_solutionsPartDos.Add(wallPos);
							_wallSolutions.Remove(wallPos);
						}
					}
					if (!visited.Contains(wallPos) && !_solutionsPartDos.Contains(wallPos)) // if next is not in visited or in the solution
					{
						char[,] nuMap = CopyMap();
						nuMap[wallPos.y, wallPos.x] = '#';
						int nuDirection = (currentDir + 1) % _directions.Length;

						while (IsBlokade(currentPos + _directions[nuDirection], nuMap))
						{
							nuDirection = (nuDirection + 1) % _directions.Length;
						}
						Vector2Int nuPos = currentPos + _directions[nuDirection];

						if (PosInRange(nuPos, map))
						{
							Dictionary<(Vector2Int, int), int> nuVisitedWDir = CopyVisitedWDirections(visWithDir);
							HashSet<Vector2Int> nuVisited = new HashSet<Vector2Int>(visited);

							_wallSolutions[wallPos] = Task.Run(() => TakeAWalk(nuPos, nuDirection, nuMap, nuVisited, nuVisitedWDir, false));
						}
					}
				}
				currentPos += _directions[currentDir];
			}
		}

		public bool PosInRange(Vector2Int pos, char[,] map)
		{
			return (pos.x >= 0 && pos.y >= 0 && pos.x < _width && pos.y < _height);
		}

		public bool IsBlokade(Vector2Int pos, char[,] map)
		{
			if (PosInRange(pos, map))
			{
				return map[pos.y, pos.x] == '#';
			}
			return false;
		}

		public char[,] CopyMap()
		{
			char[,] map = new char[_height, _width];
			for (int y = 0; y < _width; y++)
			{
				for (int x = 0; x < _width; x++)
				{
					map[y, x] = _inputLines[y][x];
				}
			}
			return map;
		}

		public void PrintMap(char[,] map)
		{
			for (int y = 0; y < _width; y++)
			{
				for (int x = 0; x < _width; x++)
				{
					Console.Write(map[y, x]);
				}
				Console.WriteLine();
			}
		}

		Dictionary<(Vector2Int, int), int> CopyVisitedWDirections(Dictionary<(Vector2Int, int), int> org)
		{
			Dictionary<(Vector2Int, int), int> copy = new();

			foreach ((var key, var value) in org)
			{
				(Vector2Int, int) nukey = (key.Item1, key.Item2);
				copy[nukey] = value;
			}
			return copy;
		}
	}
}
