using System.Collections.Generic;
using System.Linq;
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
			if (!double.TryParse(_11.Text, out _matrix._11)) { ShowError(); return; }
			if (!double.TryParse(_12.Text, out _matrix._12)) { ShowError(); return; }
			if (!double.TryParse(_13.Text, out _matrix._13)) { ShowError(); return; }

			if (!double.TryParse(_21.Text, out _matrix._21)) { ShowError(); return; }
			if (!double.TryParse(_22.Text, out _matrix._22)) { ShowError(); return; }
			if (!double.TryParse(_23.Text, out _matrix._23)) { ShowError(); return; }

			if (!double.TryParse(_31.Text, out _matrix._31)) { ShowError(); return; }
			if (!double.TryParse(_32.Text, out _matrix._32)) { ShowError(); return; }
			if (!double.TryParse(_33.Text, out _matrix._33)) { ShowError(); return; }

			if (!uint.TryParse(iterations.Text, out iter)) { ShowError(); return; }

			/* Run calculates */
			CalculateGame();

			/* Show results */
			_ = Helper.MainFrame.Navigate(new ResultPage(_matches, _result));
		}

		private void CalculateGame()
		{
			/* Calculate sedl point */
			double row_1_min = Helper.Min(_matrix._11, _matrix._12, _matrix._13);
			double row_2_min = Helper.Min(_matrix._21, _matrix._22, _matrix._23);
			double row_3_min = Helper.Min(_matrix._31, _matrix._32, _matrix._33);

			double col_1_max = Helper.Max(_matrix._11, _matrix._21, _matrix._31);
			double col_2_max = Helper.Max(_matrix._12, _matrix._22, _matrix._32);
			double col_3_max = Helper.Max(_matrix._13, _matrix._23, _matrix._33);

			double alpha_max = Helper.Max(row_1_min, row_2_min, row_3_min);
			double betta_min = Helper.Min(col_1_max, col_2_max, col_3_max);

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
				vi1 = Helper.Max(_matrix._11, _matrix._21, _matrix._31) / 1,
				vi2 = Helper.Min(_matrix._11, _matrix._12, _matrix._13) / 1
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

			double min_vi = match.vi1;
			int min_vi_k = 1;

			double max_vi = match.vi2;
			int max_vi_k = 1;

			double _a = 1;
			double _b = 0;
			double _c = 0;

			double _alph = 1;
			double _bett = 0;
			double _gamm = 0;

			/* Calculate all matches*/
			for (int k = 2; k <= iter; k++)
			{
				Match prevMatch = _matches[k - 2];

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
				match.vi1 = Helper.Max(match.a, match.b, match.c) / k;
				match.vi2 = Helper.Min(match.alpha, match.betta, match.gamma) / k;

				/* Update max and min vi */
				//if (match.vi1 < min_vi) { min_vi = match.vi1; min_vi_k = k - 1; }
				//if (match.vi2 > max_vi) { max_vi = match.vi2; max_vi_k = k - 1; }

				/* Calculate result parametrs */
				if (match.tm1 == _tmA) { _a++; }
				if (match.tm1 == _tmB) { _b++; }
				if (match.tm1 == _tmC) { _c++; }

				if (match.tm2 == _alpha) { _alph++; }
				if (match.tm2 == _betta) { _bett++; }
				if (match.tm2 == _gamma) { _gamm++; }

				_matches.Add(match);
			}


			// Я знаю, что это дикие костыли, но мне нужно было как можно скорее сдать работу :) 


			/* Set vi color state */
			//Match m = _matches[max_vi_k];
			//m.IsMaxViColor = true;
			//m.IsMinViColor = false;
			//_matches[max_vi_k] = m;
			//_result.MaxVi = m.vi2;

			List<double> AllVi1 = new List<double>();
			_matches.ForEach(x => AllVi1.Add(x.vi1));
			Match minVi = _matches[AllVi1.IndexOf(AllVi1.Min<double>())];
			int minViIndex = _matches.IndexOf(minVi);

			minVi.IsMinViColor = true;
			minVi.IsMaxViColor = false;
			_result.MinVi = minVi.vi1;
			_matches[minViIndex] = minVi;

			List<double> AllVi2 = new List<double>();
			_matches.ForEach(x => AllVi2.Add(x.vi2));
			Match maxVi = _matches[AllVi2.IndexOf(AllVi2.Max<double>())];
			int maxViIndex = _matches.IndexOf(maxVi);

			maxVi.IsMaxViColor = true;
			maxVi.IsMinViColor = false;
			_result.MaxVi = maxVi.vi2;
			_matches[maxViIndex] = maxVi;

			//m = _matches[min_vi_k];
			//m.IsMinViColor = true;
			//m.IsMaxViColor = false;
			//_matches[min_vi_k] = m;
			//_result.MinVi = m.vi1;

			_result.A = _a / iter;
			_result.B = _b / iter;
			_result.C = _c / iter;
			_result.Alpha = _alph / iter;
			_result.Betta = _bett / iter;
			_result.Gamma = _gamm / iter;
		}

		private void ShowError()
		{
			_ = MessageBox.Show("Заполните все поля и проверьте правильность данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
