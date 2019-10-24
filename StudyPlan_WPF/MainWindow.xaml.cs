﻿using System;
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
using System.ComponentModel;

namespace StudyPlan_WPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public Dictionary<string, ObservableCollection<Course>> selectableCourse = new Dictionary<string, ObservableCollection<Course>>();
        public static ObservableCollection<Semester> Semesters;

        public ObservableCollection<Course> UnplannedCourse = new ObservableCollection<Course>();
        public ObservableCollection<Course> DeletedCourse = new ObservableCollection<Course>();
        public ObservableCollection<Course> AllCourse;

        public Dictionary<string, string> CourseMap = new Dictionary<string, string>();

        public ObservableCollection<string> GradeList = new ObservableCollection<string>() {"A","B"};

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
            Semesters.Add(new Semester());
            Semesters.Add(new Semester());
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
                Console.WriteLine(AllCourse[3]);
            }

        }
        void InitialData()
        {
            selectableCourse.Add("Elective(Main)", new ObservableCollection<Course>());
            selectableCourse.Add("Elective(Art)", new ObservableCollection<Course>());

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
                    else
                    {
                        selectableCourse[c.Type].Add(c);
                        Console.WriteLine(selectableCourse["Elective(Main)"]);
                    }
                }
                else
                {
                    selectableCourse[c.Type].Add(c);
                }
                CourseMap.Add(c.Id, c.Name);
                
            }
        }
        void ReloadGPA()
        {

        }
        #endregion

        #region EventHandler
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Semester.NextNumber != 17)
            {
                Semesters.Add(new Semester());
            }
            else
            {
                return;
            }
        }

        private void DE_btn_Click(object sender, RoutedEventArgs e)
        {
            ContentPresenter cp = FindVisualChild<ContentPresenter>(this.tabControl);
            ListBox lbx = cp.ContentTemplate.FindName("listBox", cp) as ListBox;
            
            if(Semesters.Count == 1 &&
                Semesters[this.tabControl.SelectedIndex].Courses.Count == 0) // only 1 tab left
            {
                return;
            }
            

            if (this.tabControl.SelectedIndex == tabControl.Items.Count - 1 && 
                Semesters[this.tabControl.SelectedIndex].Courses.Count == 0)  /// lastest tab
            {
                Semester.NextNumber -= 1;
                Semesters.RemoveAt(this.tabControl.SelectedIndex);

            }
            else  /// any tab
            {
                foreach (Course c in Semesters[this.tabControl.SelectedIndex].Courses)
                {
                    UnplannedCourse.Add(c);
                }
                Semesters[this.tabControl.SelectedIndex].Courses.Clear();
            }
            
        }

        private void AddNewCourse_btn_Click(object sender, RoutedEventArgs e)
        {
            Add_New_Course addNewCourseWindow = new Add_New_Course(this, selectableCourse);
            addNewCourseWindow.Show();

        }

        private void ListBox_Empty_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /*
            ContentPresenter cp = FindVisualChild<ContentPresenter>(this.tabControl);
            ListBox lbx = cp.ContentTemplate.FindName("listBox", cp) as ListBox;

            HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (r.VisualHit.GetType() != typeof(ListBoxItem))
            {
                Add_New_Course addNewCourseWindow = new Add_New_Course(this, selectableCourse);
                addNewCourseWindow.Show();
            }
            */
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (tabControl.SelectedIndex == -1)
            {
                tabControl.SelectedIndex = Semesters.Count - 1;
            }
            
            Semester _Clicked_Semester = Semesters[tabControl.SelectedIndex];
            Credit_label.Content = String.Format("Credits: {0}/{1}", _Clicked_Semester.Credit, _Clicked_Semester.MaxCredit);
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

                Course selesctedCourse = Semesters[tabControl.SelectedIndex].GetCourse(clickedId).Get();
                selesctedCourse.Grade = "A";
                Console.WriteLine(Semesters[tabControl.SelectedIndex].GetCourse(clickedId).Grade);
            
        }

        private void CourseMoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Unplanned_lbx.SelectedIndex != -1)
            {
                Course selectedCourse = UnplannedCourse[Unplanned_lbx.SelectedIndex].Get();
                List<string> pr = selectedCourse.PreRequired;
                
                for (int semesterIndex = 0; semesterIndex < tabControl.SelectedIndex; semesterIndex++)
                {
                    foreach (Course c in Semesters[semesterIndex].Courses)
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
                    ContentPresenter cp = FindVisualChild<ContentPresenter>(this.tabControl);
                    ListBox lbx = cp.ContentTemplate.FindName("listBox", cp) as ListBox;
                    
                    lbx.UpdateLayout();
                }
                else
                {
                    string requireC = "";

                    foreach (string c in pr)
                    {
                        requireC += CourseMap[c] + ",";
                    }
                    if (requireC[requireC.Length - 1] == ',')
                    {
                        requireC = requireC.Substring(0, requireC.Length - 2);
                    }
                    string text = string.Format("{0} require {1}", selectedCourse.Name, requireC);
                    ErrorText.Text = text;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Unplanned_lbx.SelectedIndex != -1)
            {
                Course _selectedCourse = UnplannedCourse[Unplanned_lbx.SelectedIndex];
                UnplannedCourse.RemoveAt(Unplanned_lbx.SelectedIndex);
                DeletedCourse.Add(_selectedCourse);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {/*
            if (MessageBox.Show("Close Application ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                MessageBoxResult SaveDialog = MessageBox.Show("Do you want to save ?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (SaveDialog == MessageBoxResult.Yes)
                {

                   
                }
                else if (SaveDialog == MessageBoxResult.No)
                {
                    
                }
                else
                {
                    e.Cancel = true; // still on window
                }

            }
            else
            {
                e.Cancel = true; // still on window  
            }*/
        }
    }

    #region Class definition
    public class Semester : ObservableCollection<Course>
    {
        public static int NextNumber = 1;

        public int Credit = 0;
        public int MaxCredit = 22;
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
