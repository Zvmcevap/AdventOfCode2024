namespace AdventOfCode2024.Core
{
	public static class Extensions
	{
		public static string ArrayToString<T>(this IEnumerable<T> enumerable)
		{
			{
				if (enumerable == null) return "null";

				return $"[{string.Join(", ", enumerable)}]";
			}
		}

		public static string ArrayToString<T>(this T[] array)
		{
			{
				if (array == null) return "null";

				return $"[{string.Join(", ", array)}]";
			}
		}
	}
}
