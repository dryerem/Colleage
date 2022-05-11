using System.Windows.Controls;

namespace MatrixGame
{
	internal static class Helper
	{
		public static Frame MainFrame { get; set; }
		public static uint Max(uint a, uint b, uint c)
		{
			uint max = a;

			if (b > max)
			{
				max = b;
			}

			if (c > max)
			{
				max = c;
			}

			return max;
		}

		public static uint Min(uint a, uint b, uint c)
		{
			uint min = a;

			if (b < min)
			{
				min = b;
			}

			if (c < min)
			{
				min = c;
			}

			return min;
		}
	}
}
