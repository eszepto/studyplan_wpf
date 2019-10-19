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
        
        public Add_New_Course(MainWindow m)
        {
            InitializeComponent();
            this.MainWin = m;
        }

        private void CourseGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            courseGroup.SelectedItem.ToString();
            
        }
    }
}
