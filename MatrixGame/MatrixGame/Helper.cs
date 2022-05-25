using System.Linq;
using System.Windows.Controls;

namespace MatrixGame
{
	internal static class Helper
	{
		public static Frame MainFrame { get; set; }
		public static double Max(double a, double b, double c)
		{
			double[] array = { a, b, c };

			return array.Max<double>(); 
		}

		public static double Min(double a, double b, double c)
		{
			double[] array = { a, b, c };

			return array.Min<double>();
		}
	}
}
