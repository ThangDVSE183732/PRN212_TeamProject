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
    /// Interaction logic for SlotManagement.xaml
    /// </summary>
    public partial class SlotManagement : UserControl
    {
        public SlotManagement()
        {
            InitializeComponent();
        }

        private void ViewSlot_Click(object sender, RoutedEventArgs e)
        {
            SlotContentControl.Content = new ViewSlotControl();
        }

        private void AddSlot_Click(object sender, RoutedEventArgs e)
        {
            SlotContentControl.Content = new AddSlotControl();

        }

        private void UpdateSlot_Click(object sender, RoutedEventArgs e)
        {
            SlotContentControl.Content = new UpdateSlotControl();

        }

        private void DeleteSlot_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
