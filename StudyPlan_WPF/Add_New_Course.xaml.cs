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
using System.Collections.ObjectModel;

namespace StudyPlan_WPF
{
    /// <summary>
    /// Interaction logic for Add_New_Course.xaml
    /// </summary>
    public partial class Add_New_Course : Window
    {
        private MainWindow MainWin;

        public ObservableCollection<Course> previewCourse = new ObservableCollection<Course>();
        public ObservableCollection<Course> electiveArt;
        public ObservableCollection<Course> electiveMain;
        
        #region init
        public Add_New_Course(MainWindow m, Dictionary<string, ObservableCollection<Course>> selectCourse)
        {
            InitializeComponent();

            this.MainWin = m;
            this.electiveArt = selectCourse["Elective(Art)"];
            this.electiveMain = selectCourse["Elective(Main)"];



        }

        #endregion



        #region EventHandler
        private void CourseGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected =((ComboBoxItem)courseGroup.SelectedItem).Content.ToString();
            Console.WriteLine(selected == "Elective Main Course");
            if (selected == "Elective Main Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveMain;
                previewCourse = electiveMain;
            }
            else if (selected == "Elective Art Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveArt;
                previewCourse = electiveArt;
            }
            else if (selected == "Deleted Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveArt;
                previewCourse = electiveArt;
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (courseTable.SelectedItem != null)
            {
                MainWin.UnplannedCourse.Add(previewCourse[courseTable.SelectedIndex]);
                this.Close();
            }
        }


        #endregion
    }
}
