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

namespace StudyPlan_WPF.Resource.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MainCourseItem : UserControl
    {
        public MainCourseItem()
        {
            InitializeComponent();
            
        }

        public delegate void DropBtnHandler(object sender, RoutedEventArgs e, string clickedId);
        public event DropBtnHandler DropClick;
        private void DropBtn_Click_1(object sender, RoutedEventArgs e)
        {
            //Capture event from usercontrol and execute defined event
            if (DropClick != null)
            {
                DropClick(sender, e, this.txtId.Text);
            }
        }

        

       
        
        public delegate void GradeCBHandler(object sender, SelectionChangedEventArgs e, string clickedId);
        public event GradeCBHandler GradeCBChanged;
        private void GradeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GradeCBChanged != null)
            {
                GradeCBChanged(sender, e,this.txtId.Text);
            }
        }
        public delegate void SubmitBtnHandler(object sender, RoutedEventArgs e, string clickedId, string grade);
        public event SubmitBtnHandler SubmitClicked;
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if(SubmitClicked != null)
            {
                SubmitClicked(sender, e, this.txtId.Text, ((ComboBoxItem)gradeCB.SelectedItem).Content.ToString());
            }
        }
    }
}
