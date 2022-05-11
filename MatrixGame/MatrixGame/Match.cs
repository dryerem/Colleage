namespace MatrixGame
{
	public struct Match
	{
		public uint k { get; set; }
		public string tm1 { get; set; }
		public string tm2 { get; set; }
		public uint a { get; set; }
		public uint b { get; set; }
		public uint c { get; set; }
		public uint alpha { get; set; }
		public uint betta { get; set; }
		public uint gamma { get; set; }
		public float vi1 { get; set; }
		public float vi2 { get; set; }

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
