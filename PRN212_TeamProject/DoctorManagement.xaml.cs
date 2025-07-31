using DLL.Services;
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
    /// Interaction logic for DoctorManagement.xaml
    /// </summary>
    public partial class DoctorManagement : UserControl
    {
        private readonly DoctorService _doctorService = new DoctorService();

        public DoctorManagement()
        {
            InitializeComponent();
            DoctorContentControl.Content = new SearchDoctorControl();
        }

        private void SearchDoctor_Click(object sender, RoutedEventArgs e)
        {
            DoctorContentControl.Content = new SearchDoctorControl();

        }

        private void AddDoctor_Click(object sender, RoutedEventArgs e)
        {
            DoctorContentControl.Content = new AddDoctorControl();
        }

        private void UpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorContentControl.Content is SearchDoctorControl searchControl)
            {
                var selectedDoctor = searchControl.SelectedDoctor;
                if (selectedDoctor == null)
                {
                    MessageBox.Show("Please select a doctor to update.", "Warning", MessageBoxButton.OK);
                    return;
                }
                DoctorContentControl.Content = new UpdateDoctorControl(selectedDoctor);
            }
            else
            {
                MessageBox.Show("You must search and select a doctor first.", "Warning", MessageBoxButton.OK);
            }
        }

        private void btnDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorContentControl.Content is SearchDoctorControl searchControl)
            {
                var selectedDoctor = searchControl.SelectedDoctor;
                if (selectedDoctor == null)
                {
                    MessageBox.Show("Please select a doctor to delete.", "Warning", MessageBoxButton.OK);
                    return;
                }

                var confirm = MessageBox.Show($"Are you sure to delete doctor: {selectedDoctor.DoctorName}?",
                                              "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirm == MessageBoxResult.Yes)
                {
                    try
                    {
                        _doctorService.DeleteDoctor(selectedDoctor);
                        MessageBox.Show("Doctor deleted successfully!", "Success", MessageBoxButton.OK);
                        searchControl.ReloadDoctorsAfterDelete();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting doctor: {ex.Message}", "Error", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("You must search and select a doctor first.", "Warning", MessageBoxButton.OK);
            }
        }
    }
}
