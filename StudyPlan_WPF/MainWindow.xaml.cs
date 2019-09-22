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
using System.Collections.ObjectModel;
namespace StudyPlan_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Bachelor CpreBachelor = new Bachelor();
        public MainWindow()
        {
            InitializeComponent();

            tabControl.ItemsSource = CpreBachelor;
            CpreBachelor.Add(new Semester());
            CpreBachelor.Add(new Semester());
            CpreBachelor.Add(new Semester());

            CpreBachelor[0].Add(new Course() { Id = "5" });  // Sem 0
        }

    }
    
    public class Bachelor:ObservableCollection<Semester>
    {

    }
    public class Semester : ObservableCollection<Course>
    {
        public int Credit = 0;
        private int Number = 0;
        public string Header
        {
            get
            {
                return String.Format("Semester {0}", Number);
            }
        }


        public Semester()
        {

        }
        public override string ToString()
        {
            return String.Format("Semester {0}", Number);
        }
        public void Append(Course c)
        {
            this.Add(c);
            Credit += int.Parse(c.Weight);
        }
        
    }
    public class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Descript { get; set; }
        public string Weight { get; set; }

    }
}
