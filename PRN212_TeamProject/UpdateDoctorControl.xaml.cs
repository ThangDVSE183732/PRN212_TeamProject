using BLL.Services;
using DAL.Entities;
using DLL.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PRN212_TeamProject
{
    /// <summary>
    /// Interaction logic for UpdateDoctorControl.xaml
    /// </summary>
    public partial class UpdateDoctorControl : UserControl
    {
        private readonly DoctorService _doctorService = new DoctorService();
        private Doctor _selectedDoctor;

        public UpdateDoctorControl(Doctor doctor)
        {
            InitializeComponent();
            _selectedDoctor = doctor;
            LoadDoctor();
        }

        private void LoadDoctor()
        {
            txtId.Text = _selectedDoctor.DoctorId.ToString();
            txtCustomerFullName.Text = _selectedDoctor.DoctorName;
            txtEmail.Text = _selectedDoctor.Email;
            txtCustomerPhone.Text = _selectedDoctor.Phone;
            txtSpecialization.Text = _selectedDoctor.Specialization;
            txtPassword.Password = _selectedDoctor.Password;
            cbCustomerStatus.SelectedIndex = _selectedDoctor.Status == "Active" ? 0 : 1;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtCustomerFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtCustomerPhone.Text.Trim();
            string specialization = txtSpecialization.Text.Trim();
            string password = txtPassword.Password.Trim();
            string status = ((ComboBoxItem)cbCustomerStatus.SelectedItem)?.Content?.ToString() ?? "";

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(specialization))
            {
                MessageBox.Show("Please enter all fields!", "Error", MessageBoxButton.OK);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long!", "Error", MessageBoxButton.OK);
                return;
            }

            try
            {
                var updatedDoctor = new Doctor
                {
                    DoctorId = _selectedDoctor.DoctorId,
                    DoctorName = fullName,
                    Email = email,
                    Phone = phone,
                    Specialization = specialization,
                    Password = password,
                    Status = status
                };

                _doctorService.UpdateDoctorNoReturn(updatedDoctor);
                MessageBox.Show("Update Doctor Successfully!", "Success", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }
    }
}
