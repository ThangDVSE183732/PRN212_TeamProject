using BLL.Services;
using DAL.Entities;
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
        private Slot selectedSlot;
        private ViewSlotControl view = new ViewSlotControl();
        private SlotService slotService;
        public SlotManagement()
        {
            InitializeComponent();
            view.SlotSelected += OnSlotSelected;
            SlotContentControl.Content = view;
            slotService = new SlotService();
        }

        private void ViewSlot_Click(object sender, RoutedEventArgs e)
        {
            view.SlotSelected += OnSlotSelected;
            SlotContentControl.Content = view;
        }

        private void AddSlot_Click(object sender, RoutedEventArgs e)
        {
            SlotContentControl.Content = new AddSlotControl();

        }

        private void UpdateSlot_Click(object sender, RoutedEventArgs e)
        {
            SlotContentControl.Content = new UpdateSlotControl(selectedSlot);

        }

        private void DeleteSlot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnSlotSelected(Slot slot)
        {
            selectedSlot = slot;
        }
    }
}
