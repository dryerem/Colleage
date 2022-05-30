using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;

namespace Malitus
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnCalculateButtonClick(object sender, RoutedEventArgs e)
        {
            PlotModel plotModel = new PlotModel { Title = "Биологическая модель Мальтуса" };
            LineSeries series = new LineSeries();

            if (!float.TryParse(input_k.Text, out float k)) { ShowError(); return; }
            if (!float.TryParse(input_q.Text, out float q)) { ShowError(); return; }
            if (!int.TryParse(input_years.Text, out int y)) { ShowError(); return; }

            int prevCount = int.Parse(input_startfish.Text);
            for (int year = 1; year <= y; year++)
			{
                // кол-во рыб + рождаемость * кол-во рыб - смертность * (кол-во рыб^2) 
                prevCount = (int)(prevCount + k * prevCount - q * (prevCount * prevCount));
                series.Points.Add(new DataPoint(prevCount, year));
			}

            plotModel.Series.Add(series);
            oxyGraph.Model = plotModel;
            oxyGraph.Model.Axes[0].Title = "Год";
            oxyGraph.Model.Axes[1].Title = "Число рыб";
        }

        private void ShowError()
		{
            MessageBox.Show("Проверьте корректность ввода полей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
		}
    }
}
