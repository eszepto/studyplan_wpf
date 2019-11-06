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

        public ObservableCollection<Course> electiveFreeLang;
        public ObservableCollection<Course> electiveFreeSocial;
        public ObservableCollection<Course> electiveFreeHuman;
        public ObservableCollection<Course> electiveFreeSciMath;
        public ObservableCollection<Course> electiveFreePE;
        public ObservableCollection<Course> electiveFreeGeneral;

        public ObservableCollection<Course> electiveGeneralLang;
        public ObservableCollection<Course> electiveGeneralSocial;
        public ObservableCollection<Course> electiveGeneralHuman;
        public ObservableCollection<Course> electiveGeneralSciMath;
        public ObservableCollection<Course> electiveGeneralPE;

        public ObservableCollection<Course> specificElective;
        public ObservableCollection<Course> specific;
        public ObservableCollection<Course> specificRequired;


        public ObservableCollection<Course> DeletedCourse;
        #region init
        public Add_New_Course(MainWindow m, Dictionary<string, ObservableCollection<Course>> selectionCourse)
        {
            InitializeComponent();

            this.MainWin = m;
            this.electiveArt = selectionCourse["Elective(Art)"];
            this.electiveMain = selectionCourse["Elective(Main)"];
            this.electiveFreeLang = selectionCourse["FREE-LANG"];
            this.electiveFreeSocial = selectionCourse["FREE-SOC"];
            this.electiveFreeHuman = selectionCourse["FREE-HUMAN"];
            this.electiveFreeSciMath = selectionCourse["FREE-SCIMATH"];
            this.electiveFreePE = selectionCourse["FREE-PE"];
            this.electiveFreeGeneral = selectionCourse["FREE-GEN"];

            this.electiveGeneralLang = selectionCourse["GEN-LANG"];
            this.electiveGeneralSocial = selectionCourse["GEN-SOC"];
            this.electiveGeneralHuman = selectionCourse["GEN-HUMAN"];
            this.electiveGeneralSciMath = selectionCourse["GEN-SCIMATH"];
            this.electiveGeneralPE = selectionCourse["GEN-PE"];

            this.specificElective = selectionCourse["SPEC-ELEC"];
            this.specific = selectionCourse["SPEC-SPEC"];
            this.specificRequired = selectionCourse["SPEC-REQ"];
            this.DeletedCourse = m.DeletedCourse;
            Console.WriteLine(DeletedCourse.Count);
        }

        #endregion



        #region EventHandler
        private void CourseGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected =((ComboBoxItem)courseGroup.SelectedItem).Content.ToString();
            Console.WriteLine(selected);
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
                courseTable.ItemsSource = DeletedCourse;
                previewCourse = DeletedCourse;
            }
            else if (selected == "Free Elective Language Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveFreeLang;
                previewCourse = electiveFreeLang;
            }
            else if (selected == "Free Elective Humanity Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveFreeHuman;
                previewCourse = electiveFreeHuman;
            }
            else if (selected == "Free Elective Social Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveFreeSocial;
                previewCourse = electiveFreeSocial;
            }
            else if (selected == "Free Elective Science and Mathematics Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveFreeSciMath;
                previewCourse = electiveFreeSciMath;
            }
            else if (selected == "Free Elective Physical Education Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveFreePE;
                previewCourse = electiveFreePE;
            }
            else if (selected == "Free Elective General Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveFreeGeneral;
                previewCourse = electiveFreeGeneral;
            }
            else if (selected == "General Language Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveGeneralLang;
                previewCourse = electiveGeneralLang;
            }
            else if (selected == "General Humanity Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveGeneralHuman;
                previewCourse = electiveGeneralHuman;
            }
            else if (selected == "General Science and Mathematics Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveGeneralSciMath;
                previewCourse = electiveGeneralSciMath;
            }
            else if (selected == "General Social Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveGeneralSocial;
                previewCourse = electiveGeneralSocial;
            }
            else if (selected == "General Physical Education Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = electiveGeneralPE;
                previewCourse = electiveGeneralPE;
            }
            else if (selected == "Specific Elective Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = specificElective;
                previewCourse = specificElective;
            }
            else if (selected == "Specific Required Course")
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = specificRequired;
                previewCourse = specificRequired;
            }
            else
            {
                courseTable.ItemsSource = null;
                courseTable.ItemsSource = specific;
                previewCourse = specific;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string selected = ((ComboBoxItem)courseGroup.SelectedItem).Content.ToString();

            if (courseTable.SelectedItem != null)
            {
                MainWin.UnplannedCourse.Add(previewCourse[courseTable.SelectedIndex]);
                if (selected == "Deleted Course")
                {
                    DeletedCourse.RemoveAt(courseTable.SelectedIndex);
                }
                this.Close();
            }
        }


        #endregion
    }
}
