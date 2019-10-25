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
using System.Data.SQLite;
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
        public Dictionary<string, Course> NumtoCourse = new Dictionary<string, Course>();
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

        private Dictionary<string, double> GradeToNum = new Dictionary<string, double>()
        {
            {"", -1 },
            {"A", 4 },
            {"B+", 3.5 },
            {"B", 3 },
            {"C+", 2.5 },
            {"C", 2 },
            {"D+", 1.5 },
            {"D", 1 },
            {"F", 0 }
        };
        private UserCourseDB UserCoursedb;
        public MainWindow()
        {
            InitializeComponent();
            InitialSemester();

            LoadData();
            UserCoursedb = new UserCourseDB();
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
            if (!File.Exists("./UserCourse.db"))
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
                    NumtoCourse.Add(c.Id, c);

                }
            } //if run first time
            else
            {
                #region Load Semester Count here
                string command = @"SELECT count FROM `semesters`";

                using (SQLiteConnection sql_con = new SQLiteConnection("Data Source=UserCourse.db"))
                {
                    using (SQLiteCommand sql_cmd = new SQLiteCommand(command, sql_con))
                    {
                        sql_con.Open();
                        sql_cmd.CommandType = System.Data.CommandType.Text;
                        SQLiteDataReader sql_datareader = sql_cmd.ExecuteReader();
                        int semesterCount = 8;
                        while (sql_datareader.Read())
                        {
                            semesterCount = Convert.ToInt32(sql_datareader["count"]);
                        }

                        for (int i = 8; i < semesterCount; i++)
                        {
                            Semesters.Add(new Semester());
                        }

                    }
                }
                #endregion
                #region Load Data Here
                command = @"SELECT * FROM `courses`";

                using (SQLiteConnection sql_con = new SQLiteConnection("Data Source=UserCourse.db"))
                {
                    using (SQLiteCommand sql_cmd = new SQLiteCommand(command, sql_con))
                    {
                        sql_con.Open();
                        sql_cmd.CommandType = System.Data.CommandType.Text;
                        SQLiteDataReader sql_datareader = sql_cmd.ExecuteReader();

                        while (sql_datareader.Read())
                        {
                            
                            Course _course = new Course();
                            _course.Name = Convert.ToString(sql_datareader["name"]);
                            _course.Id = Convert.ToString(sql_datareader["id"]);
                            _course.PreRequired = Convert.ToString(sql_datareader["id"]).Split(',').ToList();
                            _course.Grade = Convert.ToString(sql_datareader["grade"]);
                            _course.Semester = Convert.ToString(sql_datareader["semester"]);
                            _course.Status = null;
                            _course.Weight = Convert.ToString(sql_datareader["weight"]);
                            _course.Type = Convert.ToString(sql_datareader["type"]);

                            
                            if (Convert.ToString(sql_datareader["semester"]) == "d")
                            {
                                DeletedCourse.Add(_course);
                            }
                            else if (Convert.ToString(sql_datareader["semester"]) == "u")
                            {
                                UnplannedCourse.Add(_course);
                            }
                            else
                            {
                                int _term = int.Parse(Convert.ToString(sql_datareader["semester"]));
                                Semesters[_term - 1].Courses.Add(_course);
                            }

                        }
                        sql_con.Close();
                    }
                }

                selectableCourse.Add("Elective(Main)", new ObservableCollection<Course>());
                selectableCourse.Add("Elective(Art)", new ObservableCollection<Course>());
                foreach (Course c in AllCourse)  // add Course
                {
                    if (c.Semester != null)
                    {
                        if (c.Semester.All(char.IsNumber))
                        {
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
                    NumtoCourse.Add(c.Id, c);
                }
                #endregion
            }
        }
        void ReloadGPA()
        {
            double totalGrade = 0;
            double Credit = 0;
            
            foreach (Course c in Semesters[tabControl.SelectedIndex].Courses)
            {
                double GradeNum = GradeToNum[c.Grade];
                double cWeight = double.Parse(c.Weight);
                Console.WriteLine(GradeNum);
                if ( GradeNum != -1)
                {
                    totalGrade += GradeNum * cWeight;
                    Credit += cWeight;
                }
            }
            
            if (Credit == 0) Credit = 1;

            double gpa = totalGrade / Credit ;
            
            Semesters[tabControl.SelectedIndex].GPA = gpa;
            GPA_label.Content = String.Format("GPA: {0:0.00}", gpa);

        }
        void ReloadStatus()
        {
            Semester _Clicked_Semester = Semesters[tabControl.SelectedIndex];
            Stat_label.Content = String.Format("Status: {0}", _Clicked_Semester.Status);
        }
        void ReloadMaxCredit()
        {
            Semester _Clicked_Semester = Semesters[tabControl.SelectedIndex];
         
            Credit_label.Content = String.Format("Credit: {0}/{1}", _Clicked_Semester.Credit, _Clicked_Semester.MaxCredit);
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
            Stat_label.Content = String.Format("Status: {0}", _Clicked_Semester.Status);
            
            

        }
        #endregion

        private void MainCourseItem_DropClick(object sender, RoutedEventArgs e, string clickedId)
        {
            Course selectedCourse = Semesters[tabControl.SelectedIndex].GetCourse(clickedId);
            selectedCourse.Grade = "";
            Semesters[tabControl.SelectedIndex].Courses.Remove(selectedCourse);
            UnplannedCourse.Add(selectedCourse);
            
        }
        private void MainCourseItem_SubmitClick(object sender, RoutedEventArgs e, string clickedId, string grade)
        {
            Console.WriteLine(Semesters[tabControl.SelectedIndex].GetCourse(clickedId).Grade);
            
        }
        private void MainCourseItem_GradeCBChanged(object sender, SelectionChangedEventArgs e,string clickedId)
        {
            ReloadGPA();
            ReloadStatus();
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

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Unplanned_lbx.SelectedIndex != -1)
            {
                Course _selectedCourse = UnplannedCourse[Unplanned_lbx.SelectedIndex];
                UnplannedCourse.RemoveAt(Unplanned_lbx.SelectedIndex);
                DeletedCourse.Add(_selectedCourse);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {//Save DataHere

            if (File.Exists("./UserCourse.db"))
            {
                Console.WriteLine("Delete file!");
                File.Delete("./UserCourse.db");
            }
            
            UserCoursedb.CreateTable();
            UserCoursedb.Exec(String.Format(@"
                                INSERT INTO `semesters`
                                (`count`) 
                                VALUES 
                                ('{0}')",
                                 Semesters.Count)
                        );
            foreach (Semester sem in Semesters)
            {
                foreach(Course c in sem.Courses)
                {
                    Console.WriteLine(c.Name);

                    UserCoursedb.Exec(String.Format(@"
                                INSERT INTO `courses`
                                (`name`,`id`,`semester`,`type`,`grade`,`weight`,`prerequired`) 
                                VALUES 
                                ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                c.Name,
                                c.Id,
                                sem.Number.ToString(),
                                c.Type,
                                c.Grade,
                                c.Weight,
                                string.Join("," , c.PreRequired))
                        );


                }
            }
            foreach (Course c in UnplannedCourse)
            {
                UserCoursedb.Exec(String.Format(@"
                                INSERT INTO `courses`
                                (`name`,`id`,`semester`,`type`,`grade`,`weight`,`prerequired`) 
                                VALUES 
                                ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                c.Name,
                                c.Id,
                                "u",
                                c.Type,
                                c.Grade,
                                c.Weight,
                                string.Join(",", c.PreRequired))
                        );
            }
            foreach (Course c in DeletedCourse)
            {
                UserCoursedb.Exec(String.Format(@"
                                INSERT INTO `courses`
                                (`name`,`id`,`semester`,`type`,`grade`,`weight`,`prerequired`) 
                                VALUES 
                                ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                c.Name,
                                c.Id,
                                "d",
                                c.Type,
                                c.Grade,
                                c.Weight,
                                string.Join(",", c.PreRequired))
                        );
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserCoursedb.GetAll();
        }
    }

    #region Class definition
    public class Semester : ObservableCollection<Course>
    {
        public static int NextNumber = 1;

        public int Credit = 0;
        public int MaxCredit { get; set; } = 22;
        public Double GPA { get; set; } = 0;
        public string Status { get; set; } = "Normal";
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
        public string Grade { get; set; } = "";
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
    public class UserCourseDB
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;

        
        private SQLiteDataReader sql_datareader;

        public UserCourseDB()
        {
            sql_con = new SQLiteConnection("Data Source=UserCourse.db");
        }

        public void CreateTable()
        {
            string command = @"
                            CREATE TABLE IF NOT EXISTS `courses` (
	                            `name`	TEXT,
	                            `id`	TEXT,
	                            `semester`	TEXT,
	                            `type`	TEXT,
	                            `grade`	TEXT,
	                            `weight`	TEXT,
	                            `prerequired`	TEXT,
	                            PRIMARY KEY(`id`)
                            )";
            using (SQLiteConnection sql_con = new SQLiteConnection("Data Source=UserCourse.db"))
            {
                using (SQLiteCommand sql_cmd = new SQLiteCommand(command, sql_con))
                {
                    sql_con.Open();
                    sql_cmd.ExecuteNonQuery();
                    sql_con.Close();
                }
            }

            command = @" CREATE TABLE IF NOT EXISTS `semesters` ( `count`	TEXT)";
            using (SQLiteConnection sql_con = new SQLiteConnection("Data Source=UserCourse.db"))
            {
                using (SQLiteCommand sql_cmd = new SQLiteCommand(command, sql_con))
                {
                    sql_con.Open();
                    sql_cmd.ExecuteNonQuery();
                    sql_con.Close();
                }
            }
        }
        public void Exec(string command)
        {
            using (SQLiteConnection sql_con = new SQLiteConnection("Data Source=UserCourse.db"))
            {
                using (SQLiteCommand sql_cmd = new SQLiteCommand(command, sql_con))
                {
                    sql_con.Open();
                    sql_cmd.ExecuteNonQuery();
                    sql_con.Close();
                }
            }
        }
        public void GetAll()
        {
            string command = @"SELECT * FROM `courses`";
            
            using (SQLiteConnection sql_con = new SQLiteConnection("Data Source=UserCourse.db"))
            {
                using (SQLiteCommand sql_cmd = new SQLiteCommand(command, sql_con))
                {
                    sql_con.Open();
                    sql_cmd.CommandType = System.Data.CommandType.Text;
                    SQLiteDataReader sql_datareader = sql_cmd.ExecuteReader();
                    while(sql_datareader.Read())
                    {
                        Console.WriteLine(sql_datareader);
                    }
                    sql_con.Close();
                }
            }

        }


    }
}
