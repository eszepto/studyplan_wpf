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
        public ObservableCollection<Semester> Semesters;

        #region TATA        
        public int NUM_Semesters;
        public TabItem a;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            Semesters = new ObservableCollection<Semester>();
            tabControl.ItemsSource = Semesters;

        }
        void InitialSemester()
        {
            Semesters.Add(new Semester()
            {
                Courses = new ObservableCollection<Course>()
                {
                    new Course()
                    {

                    }
                }
            });
        }
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            int NUM_Semesters = Semesters.Count;
            NUM_Semesters++;
            Semesters.Add(new Semester()
            {
                Number = NUM_Semesters,
                Courses = new ObservableCollection<Course>()
                {
                    new Course() {Name = "abcd", Weight = "3" },
                    new Course() {Name = "Statistic", Weight = "3"}
                }
            });
        }

        private void DE_btn_Click(object sender, RoutedEventArgs e)
        {
            ContentPresenter cp = FindVisualChild<ContentPresenter>(this.tabControl);
            ListBox lbx = cp.ContentTemplate.FindName("listBox", cp) as ListBox;

            MessageBox.Show(lbx.SelectedIndex.ToString());
            Semesters.RemoveAt(this.tabControl.SelectedIndex);
        }


        
        private childItem FindVisualChild<childItem>(DependencyObject obj)

            where childItem : DependencyObject

        {

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)

            {

                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is childItem)

                    return (childItem)child;

                else

                {

                    childItem childOfChild = FindVisualChild<childItem>(child);

                    if (childOfChild != null)

                        return childOfChild;

                }

            }

            return null;

        }

    }

    public class Semester : ObservableCollection<Course>
    {
        public int Credit = 0;
        public int Number { get; set; }
        public ObservableCollection<Course> Courses { get; set; }

        public Semester()
        {
            Courses = new ObservableCollection<Course>();
            this.Number = 10;
        }
        public void Append(Course c)
        {
            Credit += int.Parse(c.Weight);
            this.Courses.Add(c);
        }
    }

    public class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Descript { get; set; }
        public string Weight { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }


    

}
