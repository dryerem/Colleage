using MatrixGame.View;
using System.Windows;

namespace MatrixGame
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Helper.MainFrame = MainFrame;
			MainFrame.Navigate(new InputPage());
		}
	}
}
