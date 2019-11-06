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
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace StudyPlan_WPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        public ChartValues<double> grades = new ChartValues<double>();
        
        public GraphWindow(ChartValues<double> mainGrade,double overAllGPA, int currentTerm)
        {

            InitializeComponent();

            grades = mainGrade;
            lss.Values = null;
            lss.Values = grades;

            gpaTexblock.Text = string.Format("{0:0.00}", overAllGPA);
            currentSemesterBlock.Text = string.Format("{0}", currentTerm);
        }
    }
}
