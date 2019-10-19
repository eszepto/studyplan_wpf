using System;
using System.IO;
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
using Newtonsoft.Json;

namespace StudyPlan_WPF
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        public static ObservableCollection<Semester> Semesters;
        public ObservableCollection<Course> UnplannedCourse;
        public ObservableCollection<Course> AllCourse;
        public childItem FindVisualChild<childItem>(DependencyObject obj)

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
        public MainWindow()
        {
            InitializeComponent();
            InitialSemester();

            LoadData();
            InitialData();

            tabControl.ItemsSource = Semesters;
            Unplanned_lbx.ItemsSource = UnplannedCourse;
            
        }
        #region Initial
        void InitialSemester()
        {
            UnplannedCourse = new ObservableCollection<Course>();
            Semesters = new ObservableCollection<Semester>();
            Semesters.Add(new Semester()
            {
               // Credit = 19,
                /* Courses = new ObservableCollection<Course>()  // 1st 
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
                         Name = "Com Exploration",
                         Weight = "1"
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
                         Weight = "1"
                     },
                     new Course()
                     {
                         Id = "??????",
                         Name = "Social Sciences Elective Course **",
                         Weight = "3"
                     }
                 } */
            });
            Semesters.Add(new Semester()
            {
               // GPA = 3.51F,
              //  Credit = 19,
          /*      Courses = new ObservableCollection<Course>()  // 2nd
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
                }*/
            });
            Semesters.Add(new Semester());
            Semesters.Add(new Semester());
            Semesters.Add(new Semester());
            Semesters.Add(new Semester());
            Semesters.Add(new Semester());
            Semesters.Add(new Semester());
        }

        void LoadData()
        {
            using (StreamReader r = new StreamReader("../../Resource/Course/B-Eng/CpreCourse.json"))
            {
                string json = r.ReadToEnd();
                Dictionary<string, Course> d = JsonConvert.DeserializeObject<Dictionary<string, Course>>(json);
                AllCourse = new ObservableCollection<Course>(d.Values);
                Console.WriteLine(AllCourse[0]);
            }

        }
        void InitialData()
        {
            foreach (Course c in AllCourse)  // add Course
            {
                if (c.Semester != null)
                {
                    if (c.Semester.All(char.IsNumber))
                    {
                        int _term = int.Parse(c.Semester);
                        Semesters[_term - 1].Courses.Add(c);
                        Semesters[_term - 1].Credit += int.Parse(c.Weight);
                    }
                }
            }
        }
        void ReloadGPA()
        {

        }
        #endregion

        #region EventHandler
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            Semesters.Add(new Semester());
            
        }

        private void DE_btn_Click(object sender, RoutedEventArgs e)
        {
            ContentPresenter cp = FindVisualChild<ContentPresenter>(this.tabControl);
            ListBox lbx = cp.ContentTemplate.FindName("listBox", cp) as ListBox;

            if (this.tabControl.SelectedIndex == Semesters.Count - 1)  /// Any tab
            {
                Semesters.RemoveAt(this.tabControl.SelectedIndex);
                Semester.NextNumber -= 1;
            }
            else  /// lastest tab
            {
                Semesters[this.tabControl.SelectedIndex].Courses.Clear();
            }
        }

        private void AddNewCourse_btn_Click(object sender, RoutedEventArgs e)
        {
            Add_New_Course addNewCourseWindow = new Add_New_Course(this);
            addNewCourseWindow.Show();

        }

        private void ListBox_Empty_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ContentPresenter cp = FindVisualChild<ContentPresenter>(this.tabControl);
            ListBox lbx = cp.ContentTemplate.FindName("listBox", cp) as ListBox;

            HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (r.VisualHit.GetType() != typeof(ListBoxItem))
            {
                Add_New_Course addNewCourseWindow = new Add_New_Course(this);
                addNewCourseWindow.Show();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Semester _Clicked_Semester = (this.tabControl.SelectedItem as Semester).Get();
            Credit_label.Content = String.Format("Credits: {0}/22", _Clicked_Semester.Credit);
            GPA_label.Content = String.Format("GPA: {0:0.00}", _Clicked_Semester.GPA);
        }
        #endregion

        private void MainCourseItem_DropClick(object sender, RoutedEventArgs e, string clickedId)
        {
            Course selesctedCourse = Semesters[tabControl.SelectedIndex].GetCourse(clickedId);
            Semesters[tabControl.SelectedIndex].Courses.Remove(selesctedCourse);
            UnplannedCourse.Add(selesctedCourse);
            
        }
        private void MainCourseItem_SubmitClick(object sender, RoutedEventArgs e, string clickedId, string grade)
        {
            if (grade != "")   //when not click
            {
                Course selesctedCourse = Semesters[tabControl.SelectedIndex].GetCourse(clickedId);
                selesctedCourse.Grade = grade;
                Console.WriteLine(Semesters[tabControl.SelectedIndex].GetCourse(clickedId).Grade);

            }
        }

        private void CourseMoveBtn_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = UnplannedCourse[Unplanned_lbx.SelectedIndex].Get();
            List<string> pr = selectedCourse.PreRequired;

            for (int semesterIndex = 0; semesterIndex < tabControl.SelectedIndex; semesterIndex++)
            {
                foreach (Course c in Semesters[semesterIndex])
                {
                    if (pr.Contains(c.Id))
                    {
                        pr.Remove(c.Id);
                    }
                }
            }


            if (pr.Count == 0)
            {
                UnplannedCourse.Remove(selectedCourse);
                Semesters[tabControl.SelectedIndex].Courses.Add(selectedCourse);
                ErrorText.Text = "";
            }
            else
            {
                string text = string.Format("{0} require {1}", selectedCourse.Name, string.Join(",", pr));
                ErrorText.Text = text;
            }

        }
    }

    #region Class definition
    public class Semester : ObservableCollection<Course>
    {
        public static int NextNumber = 1;

        public int Credit = 0;
        public float GPA = 0;
        public int Number { get; set; }

        public ObservableCollection<Course> Courses { get; set; }

        public Semester()
        {
            Courses = new ObservableCollection<Course>();
            this.Number = NextNumber;
            NextNumber += 1;
        }
        public void Add(Course c)
        {
            Credit += int.Parse(c.Weight);
            this.Courses.Add(c);
        }

        public Semester Get()
        {
            return this;
        }

        public Course GetCourse(string id)
        {
            foreach (Course i in this.Courses)
            {
                if (i.Id == id)
                {
                    return i;
                }
                
            }
            return null;
        }
        public Course Pop(Course e)
        {
            this.Remove(e);
            return e;
        }
    }

    public class Course
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Descript { get; set; }
        public string Weight { get; set; }
        public string Grade { get; set; }
        public string Semester { get; set; }
        public string Status { get; set; }
        public List<string> PreRequired { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public Course Get()
        {
            return this;
        }
    }
    #endregion

}
