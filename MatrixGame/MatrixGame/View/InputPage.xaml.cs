using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MatrixGame.View
{
	/// <summary>
	/// Логика взаимодействия для InputPage.xaml
	/// </summary>
	public partial class InputPage : Page
	{
		private const string _tmA = "A";
		private const string _tmB = "B";
		private const string _tmC = "C";
		private const string _alpha = "α";
		private const string _betta = "β";
		private const string _gamma = "γ";

		private Matrix _matrix;
		private List<Match> _matches;
		private Result _result;
		private uint iter;

		public InputPage()
		{
			InitializeComponent();
			_result = new Result();
			_matches = new List<Match>();
		}

		private void OnCalculateButtonClick(object sender, RoutedEventArgs e)
		{
			/* Save the matrix parameters */
			if (!uint.TryParse(_11.Text, out _matrix._11)) { ShowError(); return; }
			if (!uint.TryParse(_12.Text, out _matrix._12)) { ShowError(); return; }
			if (!uint.TryParse(_13.Text, out _matrix._13)) { ShowError(); return; }

			if (!uint.TryParse(_21.Text, out _matrix._21)) { ShowError(); return; }
			if (!uint.TryParse(_22.Text, out _matrix._22)) { ShowError(); return; }
			if (!uint.TryParse(_23.Text, out _matrix._23)) { ShowError(); return; }

			if (!uint.TryParse(_31.Text, out _matrix._31)) { ShowError(); return; }
			if (!uint.TryParse(_32.Text, out _matrix._32)) { ShowError(); return; }
			if (!uint.TryParse(_33.Text, out _matrix._33)) { ShowError(); return; }

			if (!uint.TryParse(iterations.Text, out iter)) { ShowError(); return; }

			/* Run calculates */
			CalculateGame();

			/* Show results */
			_ = Helper.MainFrame.Navigate(new ResultPage(_matches, _result));
		}

		private void CalculateGame()
		{
			/* Calculate sedl point */
			uint row_1_min = Helper.Min(_matrix._11, _matrix._12, _matrix._13);
			uint row_2_min = Helper.Min(_matrix._21, _matrix._22, _matrix._23);
			uint row_3_min = Helper.Min(_matrix._31, _matrix._32, _matrix._33);

			uint col_1_max = Helper.Max(_matrix._11, _matrix._21, _matrix._31);
			uint col_2_max = Helper.Max(_matrix._12, _matrix._22, _matrix._32);
			uint col_3_max = Helper.Max(_matrix._13, _matrix._23, _matrix._33);

			uint alpha_max = Helper.Max(row_1_min, row_2_min, row_3_min);
			uint betta_min = Helper.Min(col_1_max, col_2_max, col_3_max);

			_result.AlphaSedlPoint = alpha_max;
			_result.BettaSedlPoint = betta_min;
			_result.SedlPoint = alpha_max == betta_min;

			/* First iteration initialize */
			Match match = new Match
			{
				k = 1,
				tm1 = _tmA,
				tm2 = _alpha,
				a = _matrix._11,
				b = _matrix._21,
				c = _matrix._31,
				alpha = _matrix._11,
				betta = _matrix._12,
				gamma = _matrix._13,
				vi1 = Helper.Max(_matrix._11, _matrix._21, _matrix._31) / (float)1,
				vi2 = Helper.Min(_matrix._11, _matrix._12, _matrix._13) / (float)1
			};
			/* Set color state */
			if (Helper.Max(match.a, match.b, match.c) == match.a) { match.IsAColor = true; match.IsBColor = false; match.IsCColor = false; }
			else if (Helper.Max(match.a, match.b, match.c) == match.b) { match.IsAColor = false; match.IsBColor = true; match.IsCColor = false; }
			else if (Helper.Max(match.a, match.b, match.c) == match.c) { match.IsAColor = false; match.IsBColor = false; match.IsCColor = true; }

			/* Set color state */
			if (Helper.Min(match.alpha, match.betta, match.gamma) == match.alpha) { match.IsAlphaColor = true; match.IsBettaColor = false; match.IsGammaColor = false; }
			else if (Helper.Min(match.alpha, match.betta, match.gamma) == match.betta) { match.IsAlphaColor = false; match.IsBettaColor = true; match.IsGammaColor = false; }
			else if (Helper.Min(match.alpha, match.betta, match.gamma) == match.gamma) { match.IsAlphaColor = false; match.IsBettaColor = false; match.IsGammaColor = true; }
			_matches.Add(match);

			float min_vi = match.vi1;
			uint min_vi_k = 1;

			float max_vi = match.vi2;
			uint max_vi_k = 1;

			float _a = 0.0f;
			float _b = 0.0f;
			float _c = 0.0f;

			float _alph = 0.0f;
			float _bett = 0.0f;
			float _gamm = 0.0f;

			/* Calculate all matches*/
			for (uint k = 2; k <= iter; k++)
			{
				Match prevMatch = _matches[(int)k - 2];

				// Find tm1
				if (Helper.Max(prevMatch.a, prevMatch.b, prevMatch.c) == prevMatch.a)
				{
					match.tm1 = _tmA;
				}
				else if (Helper.Max(prevMatch.a, prevMatch.b, prevMatch.c) == prevMatch.b)
				{
					match.tm1 = _tmB;
				}
				else
				{
					match.tm1 = _tmC;
				}

				// Find logo tm2
				if (Helper.Min(prevMatch.alpha, prevMatch.betta, prevMatch.gamma) == prevMatch.alpha)
				{
					match.tm2 = _alpha;
				}
				else if (Helper.Min(prevMatch.alpha, prevMatch.betta, prevMatch.gamma) == prevMatch.betta)
				{
					match.tm2 = _betta;
				}
				else
				{
					match.tm2 = _gamma;
				}

				if (match.tm2 == _alpha)
				{
					match.a = prevMatch.a + _matrix._11;
					match.b = prevMatch.b + _matrix._21;
					match.c = prevMatch.c + _matrix._31;
				}
				else if (match.tm2 == _betta)
				{
					match.a = prevMatch.a + _matrix._12;
					match.b = prevMatch.b + _matrix._22;
					match.c = prevMatch.c + _matrix._32;
				}
				else if (match.tm2 == _gamma)
				{
					match.a = prevMatch.a + _matrix._13;
					match.b = prevMatch.b + _matrix._23;
					match.c = prevMatch.c + _matrix._33;
				}

				if (match.tm1 == _tmA)
				{
					match.alpha = prevMatch.alpha + _matrix._11;
					match.betta = prevMatch.betta + _matrix._12;
					match.gamma = prevMatch.gamma + _matrix._13;
				}
				else if (match.tm1 == _tmB)
				{
					match.alpha = prevMatch.alpha + _matrix._21;
					match.betta = prevMatch.betta + _matrix._22;
					match.gamma = prevMatch.gamma + _matrix._23;
				}
				else if (match.tm1 == _tmC)
				{
					match.alpha = prevMatch.alpha + _matrix._31;
					match.betta = prevMatch.betta + _matrix._32;
					match.gamma = prevMatch.gamma + _matrix._33;
				}

				/* Set color state for ABC*/
				if (Helper.Max(match.a, match.b, match.c) == match.a) { match.IsAColor = true; match.IsBColor = false; match.IsCColor = false; }
				else if (Helper.Max(match.a, match.b, match.c) == match.b) { match.IsAColor = false; match.IsBColor = true; match.IsCColor = false; }
				else if (Helper.Max(match.a, match.b, match.c) == match.c) { match.IsAColor = false; match.IsBColor = false; match.IsCColor = true; }

				/* Set color state  for alpha, betta, gamma*/
				if (Helper.Min(match.alpha, match.betta, match.gamma) == match.alpha) { match.IsAlphaColor = true; match.IsBettaColor = false; match.IsGammaColor = false; }
				else if (Helper.Min(match.alpha, match.betta, match.gamma) == match.betta) { match.IsAlphaColor = false; match.IsBettaColor = true; match.IsGammaColor = false; }
				else if (Helper.Min(match.alpha, match.betta, match.gamma) == match.gamma) { match.IsAlphaColor = false; match.IsBettaColor = false; match.IsGammaColor = true; }

				match.k = k;
				match.vi1 = Helper.Max(match.a, match.b, match.c) / (float)k;
				match.vi2 = Helper.Min(match.alpha, match.betta, match.gamma) / (float)k;

				/* Update max and min vi */
				if (match.vi1 < min_vi) { min_vi = match.vi1; min_vi_k = k - 2; }
				if (match.vi2 > max_vi) { max_vi = match.vi2; max_vi_k = k - 2; }

				/* Calculate result parametrs */
				if (match.tm1 == _tmA) { _a++; }
				if (match.tm1 == _tmB) { _b++; }
				if (match.tm1 == _tmC) { _c++; }

				if (match.tm2 == _alpha) { _alph++; }
				if (match.tm2 == _betta) { _bett++; }
				if (match.tm2 == _gamma) { _gamm++; }

				_matches.Add(match);
			}

			/* Set vi color state */
			Match m = _matches[(int)max_vi_k];
			m.IsMaxViColor = true;
			m.IsMinViColor = false;
			_matches[(int)max_vi_k] = m;

			m = _matches[(int)min_vi_k];
			m.IsMinViColor = true;
			m.IsMaxViColor = false;
			_matches[(int)min_vi_k] = m;

			_result.A = _a / (float)20;
			_result.B = _b / (float)20;
			_result.C = _c / (float)20;
			_result.Alpha = _alph / (float)20;
			_result.Betta = _bett / (float)20;
			_result.Gamma = _gamm / (float)20;
		}

		private void ShowError()
		{
			_ = MessageBox.Show("Заполните все поля и проверьте правильность данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
