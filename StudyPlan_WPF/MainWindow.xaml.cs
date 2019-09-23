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

        public MainWindow()
        {
            InitializeComponent();
            InitialSemester();

            tabControl.ItemsSource = Semesters;


        }
        void InitialSemester()
        {
            Semesters = new ObservableCollection<Semester>();
            Semesters.Add(new Semester()
            {
                Courses = new ObservableCollection<Course>()  // 1st 
                {
                    new Course()
                    {
                        Id = "010123102",
                        Name = "Programming Fundamentals",
                        Weight = "3"
                    },
                    new Course()
                    {
                        Id = "010123130",
                        Name = "Introduction to Engineering",
                        Weight = "1"
                    },
                    new Course()
                    {
                        Id = "040203111",
                        Name = "Engineering Mathematics I",
                        Weight = "3"
                    },
                    new Course()
                    {
                        Id = "040313005",
                        Name = "Physics I",
                        Weight = "3"
                    },
                    new Course()
                    {
                        Id = "040313006",
                        Name = "Physics Laboratory I",
                        Weight = "1"
                    },
                    new Course()
                    {
                        Id = "??????",
                        Name = "Language Elective Course **",
                        Weight = "3"
                    },
                    new Course()
                    {
                        Id = "??????",
                        Name = "Physical Education Elective Course **",
                        Weight = "3"
                    },
                    new Course()
                    {
                        Id = "??????",
                        Name = "Social Sciences Elective Course **",
                        Weight = "3"
                    }
                }
            });
            Semesters.Add(new Semester()
            {
                Courses = new ObservableCollection<Course>()  // 2nd
                {
                    new Course()
                    {
                        Id = "010113010",
                        Name = "Electric Circuit Theory",
                        Weight = "3"
                    },
                    new Course()
                    {
                        Id = "010113011",
                        Name = "Electric Circuit Laboratory",
                        Weight = "1"
                    },
                    new Course()
                    {
                        Id = "010123103",
                        Name = "Algorithms and Data Structures",
                        Weight = "3"
                    },
                    new Course()
                    {
                        Id = "010403006",
                        Name = "Work Ethics",
                        Weight = "1"
                    },
                    new Course()
                    {
                        Id = "040203112",
                        Name = "Engineering Mathematics II",
                        Weight = "3"
                    },
                    new Course()
                    {
                        Id = "040313007",
                        Name = "Physics II",
                        Weight = "3"
                    },
                    new Course()
                    {
                        Id = "??????",
                        Name = "Language Elective Course **",
                        Weight = "3"
                    },
                    new Course()
                    {
                        Id = "??????",
                        Name = "Physical Education Elective Course",
                        Weight = "1"
                    }
                }
            });
            Semesters.Add(new Semester());
            Semesters.Add(new Semester());
            Semesters.Add(new Semester());
            Semesters.Add(new Semester());
            Semesters.Add(new Semester());
            Semesters.Add(new Semester());
        }
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            Semesters.Add(new Semester());
        }

        private void DE_btn_Click(object sender, RoutedEventArgs e)
        {
            ContentPresenter cp = FindVisualChild<ContentPresenter>(this.tabControl);
            ListBox lbx = cp.ContentTemplate.FindName("listBox", cp) as ListBox;

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
        private static int NextNumber = 1;

        public int Credit = 0;
        public int Number { get; set; }

        public ObservableCollection<Course> Courses { get; set; }

        public Semester()
        {
            Courses = new ObservableCollection<Course>();
            this.Number = NextNumber;
            NextNumber += 1;
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
