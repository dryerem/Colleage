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
        private readonly FunctionSeries _series = new FunctionSeries();
        public MainWindow()
        {
            InitializeComponent();

            ModelPlot = new PlotModel { Title = "Биологическая модель Мальтуса" };
            DataContext = this;
        }

        public PlotModel ModelPlot { get; private set; }

        public void OnCalculateButtonClick(object sender, RoutedEventArgs e)
        {
            int k = int.Parse(input_k.Text);
            float q = float.Parse(input_q.Text);
            int years = int.Parse(input_years.Text);

            float prevCount = float.Parse(input_startfish.Text);
            for (int year = 1; year <= years; year++)
			{
                // кол-во рыб + рождаемость * кол-во рыб - смертность * (кол-во рыб^2) 
                prevCount = prevCount + k * prevCount - q * (prevCount * prevCount);
                _series.Points.Add(new DataPoint(prevCount, year));
			}
            ModelPlot.Series.Add(_series);
        }
    }
}
