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
    /// Interaction logic for PatientProfileManagement.xaml
    /// </summary>
    public partial class PatientProfileManagement : UserControl
    {
        private PatientService patientService;
        private Patient? _patient;
        public PatientProfileManagement(Patient? patient)
        {
            InitializeComponent();
            patientService = new PatientService();
            _patient = patient;
        }



        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient updatePatient = patientService.GetPatientProfile(_patient.Email, _patient.Password);
                if(updatePatient != null)
                {

                    updatePatient.Name = txtName.Text;
                    updatePatient.Password = txtPassword.Text;
                    updatePatient.Email = txtEmailAdress.Text;
                    updatePatient.Phone = txtPhone.Text;
                    updatePatient.EmergencyPhoneNumber = txtEmergencyPhoneNumber.Text;
                    updatePatient.BloodType = txtBloodType.Text;
                    updatePatient.Address = txtAddress.Text;
                    updatePatient.DateOfBirth = DateOnly.FromDateTime(txtDateOfBirth.SelectedDate ?? DateTime.Today);
                }
                if (patientService.UpdatePatient(updatePatient))
                {
                    MessageBox.Show("Patient updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to updated patient. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred while updating the profile: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PatientProfileLoader(object sender, RoutedEventArgs e)
        {
            txtName.Text = _patient != null ? _patient.Name : "";
            txtPassword.Text = _patient != null ? _patient.Password : "";
            txtPhone.Text = _patient != null ? _patient.Phone : "";
            txtEmailAdress.Text = _patient != null ? _patient.Email : "";
            txtBloodType.Text = _patient != null ? _patient.BloodType : "";
            txtAddress.Text = _patient != null ? _patient.Address : "";
            txtDateOfBirth.SelectedDate = _patient != null ?
                _patient.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue) : null;
            txtEmergencyPhoneNumber.Text = _patient != null ? _patient.EmergencyPhoneNumber : "";

        }
    }
}
