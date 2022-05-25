using System.Collections.Generic;
using System.Windows.Controls;

namespace MatrixGame.View
{
	/// <summary>
	/// Логика взаимодействия для ResultPage.xaml
	/// </summary>
	public partial class ResultPage : Page
	{
		private readonly List<Match> _matches;
		private readonly Result _result;
		public ResultPage(List<Match> matches, Result result)
		{
			InitializeComponent();
			_matches = matches;
			_result = result;

			ShowResults();
		}

		private void ShowResults()
		{
			dataGrid.ItemsSource = _matches;
			a.Text = _result.A.ToString();
			b.Text = _result.B.ToString();
			c.Text = _result.C.ToString();
			alpha.Text = _result.Alpha.ToString();
			betta.Text = _result.Betta.ToString();
			gamma.Text = _result.Gamma.ToString();

			sedlAlpha.Text = _result.AlphaSedlPoint.ToString();
			sedlBetta.Text = _result.BettaSedlPoint.ToString();

			minVi.Text = _result.MinVi.ToString();
			maxVi.Text = _result.MaxVi.ToString();

			if (_result.SedlPoint)
			{
				sedl.Text = "Да";
			}
			else
			{
				sedl.Text = "Нет";
			}
		}

		private void OnClearButtonClick(object sender, System.Windows.RoutedEventArgs e)
		{
			_ = Helper.MainFrame.Navigate(new InputPage());
		}
	}
}
