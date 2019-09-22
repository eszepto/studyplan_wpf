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
        
        public ObservableCollection<TodoItem> items { get; set; }
        public ObservableCollection<ObservableCollection<Course>> Semester;

        public MainWindow()
        {
            
            InitializeComponent();

            tabControl.it
        }

    }
    public class TodoItem
    {
        public string Title { get; set; }
        public int Completion { get; set; }

        public override String ToString()
        {
            return Title;
        }
    }

    public class Course: ObservableCollection<Semester>
    {

    }
    public class Semester
    {
        int number { get; set; }
    }
}
