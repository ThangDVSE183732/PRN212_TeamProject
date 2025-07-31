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
    /// Interaction logic for ShiftManagement.xaml
    /// </summary>
    public partial class ShiftManagement : UserControl
    {
        private Shift selectedShift;
        private ViewShiftControl view = new ViewShiftControl();
        private ShiftService shiftService;

        public ShiftManagement()
        {
            InitializeComponent();
            view.ShiftSelected += OnShiftSelected;
            ShiftContentControl.Content = view;
            shiftService = new ShiftService();

        }

        private void ViewShift_Click(object sender, RoutedEventArgs e)
        {
            view.ShiftSelected += OnShiftSelected;
            ShiftContentControl.Content = view;
        }

        private void AddShift_Click(object sender, RoutedEventArgs e)
        {
            ShiftContentControl.Content = new AddShiftControl();
        }

        private void UpdateShift_Click(object sender, RoutedEventArgs e)
        {
            ShiftContentControl.Content = new UpdateShiftControl(selectedShift);

        }

        private void btnDeleteShift_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult msg = MessageBox.Show("Do you want delete shift?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (msg == MessageBoxResult.Yes)
                {

                    if (selectedShift != null)
                    {
                        shiftService.DeleteShift(selectedShift.ShiftId);
                        MessageBox.Show("Shift deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        view.loadDataInit();
                        ShiftContentControl.Content = view;
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

        private void OnShiftSelected(Shift shift)
        {
            selectedShift = shift;
        }
    }
}
