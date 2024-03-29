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
using System.Data.SQLite;
using System.ComponentModel;
using LiveCharts;
using LiveCharts.Wpf;

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

        private bool isReset = false;
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
            selectableCourse.Add("FREE-GEN", new ObservableCollection<Course>());
            selectableCourse.Add("FREE-HUMAN", new ObservableCollection<Course>());
            selectableCourse.Add("FREE-LANG", new ObservableCollection<Course>());
            selectableCourse.Add("FREE-PE", new ObservableCollection<Course>());
            selectableCourse.Add("FREE-SCIMATH", new ObservableCollection<Course>());
            selectableCourse.Add("FREE-SOC", new ObservableCollection<Course>());

            selectableCourse.Add("GEN-HUMAN", new ObservableCollection<Course>());
            selectableCourse.Add("GEN-LANG", new ObservableCollection<Course>());
            selectableCourse.Add("GEN-PE", new ObservableCollection<Course>());
            selectableCourse.Add("GEN-SCIMATH", new ObservableCollection<Course>());
            selectableCourse.Add("GEN-SOC", new ObservableCollection<Course>());

            selectableCourse.Add("SPEC-ELEC", new ObservableCollection<Course>());
            selectableCourse.Add("SPEC-REQ", new ObservableCollection<Course>());
            selectableCourse.Add("SPEC-SPEC", new ObservableCollection<Course>());

            /*

            using (StreamReader r = new StreamReader("../../Resource/Course/B-Eng/CpreCourse.json"))
            {
                string json = r.ReadToEnd();
                Dictionary<string, Course> d = JsonConvert.DeserializeObject<Dictionary<string, Course>>(json);
                AllCourse = new ObservableCollection<Course>(d.Values);
               
            }
            */

                               

        }
        void InitialData()
        {
            if (!File.Exists("./UserCourse.db")) //if run first time
            {
                selectableCourse.Add("Elective(Main)", new ObservableCollection<Course>());
                selectableCourse.Add("Elective(Art)", new ObservableCollection<Course>());
                string command = @"SELECT  * FROM `Courselist`";

                using (SQLiteConnection sql_con = new SQLiteConnection("Data Source=../../Resource/Course/B-Eng/Courselist.db"))
                {
                    using (SQLiteCommand sql_cmd = new SQLiteCommand(command, sql_con))
                    {
                        sql_con.Open();
                        sql_cmd.CommandType = System.Data.CommandType.Text;
                        SQLiteDataReader sql_datareader = sql_cmd.ExecuteReader();


                        while (sql_datareader.Read())
                        {

                            string _semester = Convert.ToString(sql_datareader["semester"]);
                           

                            if (_semester != "")
                            {
                                if (_semester.All(char.IsNumber))
                                {
                                    Course c = new Course()
                                    {
                                        Name = Convert.ToString(sql_datareader["name"]),
                                        Id = Convert.ToString(sql_datareader["course_id"]),
                                        Weight = Convert.ToString(sql_datareader["credit"]),
                                        PreRequired = Convert.ToString(sql_datareader["prerequisite"]).Split(',').ToList<string>(),
                                        Semester = _semester,
                                        Type = Convert.ToString(sql_datareader["category"])
                                    };

                                    int _term = int.Parse(_semester);

                                    Semesters[_term - 1].Courses.Add(c);

                                    CourseMap.Add(c.Id, c.Name);
                                    NumtoCourse.Add(c.Id, c);
                                }
                            }
                            else
                            {
                                Course c = new Course()
                                {
                                    Name = Convert.ToString(sql_datareader["name"]),
                                    Id = Convert.ToString(sql_datareader["course_id"]),
                                    Weight = Convert.ToString(sql_datareader["credit"]),
                                    PreRequired = Convert.ToString(sql_datareader["prerequisite"]).Split(',').ToList<string>(),
                                    //Semester = _semester,
                                    Type = Convert.ToString(sql_datareader["category"])
                                };
                                
                                selectableCourse[c.Type].Add(c);
                                CourseMap.Add(c.Id, c.Name);
                                NumtoCourse.Add(c.Id, c);
                            }

                        }

                    }
                }
            } 
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
                            _course.PreRequired = Convert.ToString(sql_datareader["prerequired"]).Split(',').ToList();
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
                /*
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
                           
                        }
                    }
                    else
                    {
                        selectableCourse[c.Type].Add(c);
                    }
                    
                }
                */
                //LOAD ELECTIVE COURSE
                command = @"SELECT  * FROM `Courselist`";
                using (SQLiteConnection sql_con = new SQLiteConnection("Data Source=../../Resource/Course/B-Eng/Courselist.db"))
                {
                    using (SQLiteCommand sql_cmd = new SQLiteCommand(command, sql_con))
                    {
                        sql_con.Open();
                        sql_cmd.CommandType = System.Data.CommandType.Text;
                        SQLiteDataReader sql_datareader = sql_cmd.ExecuteReader();

                        while (sql_datareader.Read())
                        {
                            string _semester = Convert.ToString(sql_datareader["semester"]);
                            

                            if (_semester != "")
                            {
                                if (_semester.All(char.IsNumber))
                                {
                                    
                                }
                            }
                            else
                            {
                                Course c = new Course()
                                {
                                    Name = Convert.ToString(sql_datareader["name"]),
                                    Id = Convert.ToString(sql_datareader["course_id"]),
                                    Weight = Convert.ToString(sql_datareader["credit"]),
                                    PreRequired = Convert.ToString(sql_datareader["prerequisite"]).Split(',').ToList<string>(),
                                    //Semester = _semester,
                                    Type = Convert.ToString(sql_datareader["category"])
                                };
                                
                                selectableCourse[c.Type].Add(c);
                                CourseMap.Add(c.Id, c.Name);
                                NumtoCourse.Add(c.Id, c);
                            }

                        }

                    }
                }

                foreach (Semester sem in Semesters) //reload GPA for all semesters
                {
                    double totalGrade = 0;
                    double Credit = 0;

                    foreach (Course c in sem.Courses)
                    {
                        double GradeNum = GradeToNum[c.Grade];
                        double cWeight = double.Parse(c.Weight);

                        if (GradeNum != -1)
                        {
                            totalGrade += GradeNum * cWeight;
                            Credit += cWeight;
                        }
                    }

                    if (Credit == 0) Credit = 1;

                    double gpa = totalGrade / Credit;

                    sem.GPA = gpa;
                }
                #endregion
            }
        }
        #endregion
        void ReloadGPA()
        {
            double totalGrade = 0;
            double Credit = 0;
            
            foreach (Course c in Semesters[tabControl.SelectedIndex].Courses)
            {
                double GradeNum = GradeToNum[c.Grade];
                double cWeight = double.Parse(c.Weight);
                
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
            
        }
        void ReloadCredit()
        {
            Semester _Clicked_Semester = Semesters[tabControl.SelectedIndex];
            
            #region CurrentCredit
            int credit = 0;
            foreach (Course c in _Clicked_Semester.Courses)
            {
                credit += int.Parse(c.Weight);
            }
            _Clicked_Semester.Credit = credit;
            #endregion

            if (_Clicked_Semester.Number == 1)
            {
                Credit_label.Content = String.Format("Credits: {0}/{1}", _Clicked_Semester.Credit, _Clicked_Semester.MaxCredit);
                Stat_label.Content = String.Format("Status: {0}", _Clicked_Semester.Status);
                return;
            }


            #region MaxCredit
            int currentTerm = 0;
            List<int> gradeAddedTerm = new List<int>();
            foreach (Semester sem in Semesters)
            {
                foreach (Course c in sem.Courses)
                {
                    if (c.Grade != "") //has least one course Grade != ""
                    {
                        currentTerm = sem.Number;
                    }
                }
                gradeAddedTerm.Add(currentTerm);
                
            }

            
            List<double> gpaList = new List<double>();
            for(int i = 1; i <= _Clicked_Semester.Number -1; i++)
            {
                if (gradeAddedTerm.Contains(i))
                {
                    gpaList.Add(Semesters[i-1].GPA);
                    
                }
            }

            double gpaListAverage = gpaList.Sum() / gpaList.Count;
            if (gpaListAverage < 2 && gpaListAverage > 0)
            {
                _Clicked_Semester.Status = "Probation";
                _Clicked_Semester.MaxCredit = 16;
            }
            else
            {
                _Clicked_Semester.Status = "Normal";
                _Clicked_Semester.MaxCredit = 22;
            }
            #endregion
            Credit_label.Content = String.Format("Credits: {0}/{1}", _Clicked_Semester.Credit, _Clicked_Semester.MaxCredit);
            Stat_label.Content = String.Format("Status: {0}", _Clicked_Semester.Status);
        }
        

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
            ReloadGPA();
            ReloadCredit();

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
            
            

            ReloadGPA();
            ReloadCredit();
            
            
        }
        

        private void MainCourseItem_DropClick(object sender, RoutedEventArgs e, string clickedId)
        {
            Course selectedCourse = Semesters[tabControl.SelectedIndex].GetCourse(clickedId);
            selectedCourse.Grade = "";
            Semesters[tabControl.SelectedIndex].Courses.Remove(selectedCourse);
            UnplannedCourse.Add(selectedCourse);
            
        }
        private void MainCourseItem_DeleteClick(object sender, RoutedEventArgs e, string clickedId)
        {
            Course selectedCourse = Semesters[tabControl.SelectedIndex].GetCourse(clickedId);

            string sMessageBoxText = "Do you want to delete this subject?";
            string sCaption = selectedCourse.Name;

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                selectedCourse.Grade = "";
                Semesters[tabControl.SelectedIndex].Courses.Remove(selectedCourse);
                DeletedCourse.Add(selectedCourse);
            }

        }
        private void MainCourseItem_GradeCBChanged(object sender, SelectionChangedEventArgs e,string clickedId)
        {
            ReloadGPA();
            ReloadCredit();
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
                
                if (pr.Count == 0 || pr[0] == "")
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

            if (isReset) return;

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

        private void GraphButton_Click(object sender, RoutedEventArgs e)
        {
            ChartValues<double> gpaValues = new ChartValues<double>();
            gpaValues.Add(double.NaN);

            double gpaSum = 0;
            int presentSem = 1;
            int semCount = 0;
            List<double> gpaList = new List<double>();
            
            

            int currentTerm = 0;
            List<int> gradeAddedTerm = new List<int>();
            foreach (Semester sem in Semesters)
            {
                foreach (Course c in sem.Courses)
                {
                    if (c.Grade != "")
                    {
                        currentTerm = sem.Number;

                    }
                }
                gradeAddedTerm.Add(currentTerm);
            }

            for (int i=0;i<=currentTerm-1;i++)
            {
                if (gradeAddedTerm.Contains(i+1))
                {
                    gpaList.Add(Semesters[i].GPA);
                }
                
            }
            double overallGPA = (gpaList.Sum() / gpaList.Count);
            
            foreach (Semester sem in Semesters) // in each semester, if that
            {
                bool isAllEmpty = true;
                gpaSum += sem.GPA;
                foreach (Course c in sem.Courses)
                {
                    if(c.Grade != "")
                    {
                        isAllEmpty = false;
                        presentSem = sem.Number;
                        semCount += 1;
                        break;
                    }
                }

                if(!isAllEmpty)
                {
                    gpaValues.Add(sem.GPA);
                }
                else
                {
                    gpaValues.Add(double.NaN);
                }
            }
            

            GraphWindow GraphWin = new GraphWindow(gpaValues,overallGPA,currentTerm);
            GraphWin.Show();
            
        }
        #endregion

        private void Reset_btn_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("./UserCourse.db"))
            {
                Console.WriteLine("Delete file!");
                File.Delete("./UserCourse.db");
            }
            isReset = true;
            System.Windows.Forms.Application.Restart();

            System.Windows.Application.Current.Shutdown();
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
        public string Descript { get; set; } = null;
        public string Weight { get; set; }
        public string Grade { get; set; } = "";
        public string Semester { get; set; } = null;
        public string Status { get; set; } = null;
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
       


    }
    #endregion
}
