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
            try
            {
                MessageBoxResult msg = MessageBox.Show("Do you want delete shift?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (msg == MessageBoxResult.Yes)
                {

                    if (selectedSlot != null)
                    {
                        slotService.DeleteShift(selectedSlot.SlotId);
                        MessageBox.Show("Shift deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        view.loadDataInit();
                        SlotContentControl.Content = view;
                    }
                    else
                    {
                        MessageBox.Show("Please choose at least one shift to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnSlotSelected(Slot slot)
        {
            selectedSlot = slot;
        }
    }
}
