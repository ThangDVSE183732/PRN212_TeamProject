using BLL.Services;
using DAL.Entities;
using DLL.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PRN212_TeamProject
{
    public partial class SearchDoctorControl : UserControl
    {
        private readonly DoctorService _doctorService = new DoctorService();

        public DAL.Entities.Doctor? SelectedDoctor => doctorTableControl.dgDoctor.SelectedItem as DAL.Entities.Doctor;


        public SearchDoctorControl()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            var results = _doctorService.GetAllDoctors()
                .Where(x => x.DoctorName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            x.Phone.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            x.Specialization.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
            if (results.Any())
            {
                doctorTableControl.SetDoctors(results);
                doctorTableControl.Visibility = Visibility.Visible;
            }
            else
            {
                doctorTableControl.Visibility = Visibility.Collapsed;
                MessageBox.Show("No doctors found.", "Search", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public Doctor? GetSelectedDoctor()
        {
            return doctorTableControl.GetSelectedDoctor();
        }

        public void ReloadDoctorsAfterDelete()
        {
            var doctors = _doctorService.GetAllDoctors();
            doctorTableControl.SetDoctors(doctors);
        }
    }
}
