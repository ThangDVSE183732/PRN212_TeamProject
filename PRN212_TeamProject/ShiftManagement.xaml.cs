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

namespace PRN212_TeamProject
{
    /// <summary>
    /// Interaction logic for ShiftManagement.xaml
    /// </summary>
    public partial class ShiftManagement : UserControl
    {
        public ShiftManagement()
        {
            InitializeComponent();
        }

        private void ViewShift_Click(object sender, RoutedEventArgs e)
        {
            ShiftContentControl.Content = new ViewShiftControl();

        }

        private void AddShift_Click(object sender, RoutedEventArgs e)
        {
            ShiftContentControl.Content = new AddShiftControl();
        }

        private void UpdateShift_Click(object sender, RoutedEventArgs e)
        {
            ShiftContentControl.Content = new UpdateShiftControl();

        }

        private void btnDeleteShift_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
