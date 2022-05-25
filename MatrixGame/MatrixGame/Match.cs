namespace MatrixGame
{
	public struct Match
	{
		public int k { get; set; }
		public string tm1 { get; set; }
		public string tm2 { get; set; }
		public double a { get; set; }
		public double b { get; set; }
		public double c { get; set; }
		public double alpha { get; set; }
		public double betta { get; set; }
		public double gamma { get; set; }
		public double vi1 { get; set; }
		public double vi2 { get; set; }

		/* Color section */
		public bool IsAColor { get; set; }
		public bool IsBColor { get; set; }
		public bool IsCColor { get; set; }
		public bool IsAlphaColor { get; set; }
		public bool IsBettaColor { get; set; }
		public bool IsGammaColor { get; set; }
		public bool IsMinViColor { get; set; }
		public bool IsMaxViColor { get; set; }
	}
}
