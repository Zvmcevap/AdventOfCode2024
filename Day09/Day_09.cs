using AdventOfCode2024.Core;


namespace AdventOfCode2024
{

	internal class Day_09: Solver
	{
		private readonly List<string> _inputLines = FileReader.ReadInput("Day09/input");

		private readonly Dictionary<int, Vector2Int> _freeSpaces = new();
		private readonly List<MemoryBlock> _memoryBlocks = new();

		public Day_09()
		{

		}

		private void ResetValues()
		{
			_memoryBlocks.Clear();
			_freeSpaces.Clear();

			string line = _inputLines[0];
			int id = 0;
			int position = 0;
			for (int i = 0; i < line.Length; i++)
			{
				int value = int.Parse(line[i].ToString());
				if (i % 2 == 0)
				{
					MemoryBlock mb = new MemoryBlock(id, position, value);
					_memoryBlocks.Add(mb);
				}
				else
				{
					_freeSpaces[id] = new Vector2Int(position, value);
					id++;
				}
				position += value;
			}

		}

		public override void PartOne()
		{
			ResetValues();
			int freeSpaceId = 0;
			for (int j = _memoryBlocks.Count - 1; j > 0; j--)
			{
				MemoryBlock mb = _memoryBlocks[j];
				freeSpaceId = mb.Move(freeSpaceId, _freeSpaces);
			}

			long solution = 0;
			foreach (MemoryBlock mb in _memoryBlocks)
			{
				solution += mb.GetValue();
			}

			WriteSolution(solution);
		}

		public override void PartTwo()
		{
			ResetValues();
			for (int j = _memoryBlocks.Count - 1; j > 0; j--)
			{
				_memoryBlocks[j].MoveAsBlock(_freeSpaces);
			}

			double solution = 0;
			foreach (MemoryBlock mb in _memoryBlocks)
			{
				solution += mb.GetValue();
			}

			WriteSolution(solution, false);
		}

		private class MemoryBlock
		{
			public int ID { get; }

			public int Start { get; }
			public int SizeUnmoved { get; set; }

			public Dictionary<int, int> Moved = new();

			public MemoryBlock(int id, int start, int size)
			{
				ID = id;
				Start = start;
				SizeUnmoved = size;
			}

			public int Move(int freeID, Dictionary<int, Vector2Int> freeSpaces)
			{
				while (SizeUnmoved > 0 && freeSpaces.ContainsKey(freeID))
				{
					Vector2Int posNSize = freeSpaces[freeID];
					if (posNSize.x >= Start)
					{
						return freeID;
					}
					if (posNSize.y > SizeUnmoved)
					{
						Moved[posNSize.x] = SizeUnmoved;
						posNSize.y -= SizeUnmoved;
						posNSize.x += SizeUnmoved;
						freeSpaces[freeID] = posNSize;
						SizeUnmoved = 0;
					}
					else // if SizeUnmoved >= size of free space posNSize.y
					{
						Moved[posNSize.x] = posNSize.y;
						SizeUnmoved -= posNSize.y;
						freeSpaces.Remove(freeID);
						freeID++;
					}
				}

				return freeID;
			}
			public void MoveAsBlock(Dictionary<int, Vector2Int> freeSpaces)
			{
				foreach (int key in freeSpaces.Keys)
				{
					Vector2Int startNSize = freeSpaces[key];
					if (startNSize.x < Start && startNSize.y >= SizeUnmoved)
					{
						Moved[startNSize.x] = SizeUnmoved;
						startNSize.x += SizeUnmoved;
						startNSize.y -= SizeUnmoved;
						if (startNSize.y == 0)
						{
							freeSpaces.Remove(key);
						}
						else
						{
							freeSpaces[key] = startNSize;
						}
						SizeUnmoved = 0;
						return;
					}
				}
			}
			public long GetValue()
			{
				long sum = 0;
				foreach ((int start, int size) in Moved)
				{
					for (int i = start; i < start + size; i++)
					{
						sum += (long)(ID * i);
					}
				}

				for (int i = Start; i < SizeUnmoved + Start; i++)
				{
					sum += (long)(ID * i);
				}
				return sum;

			}
		}
	}
}
