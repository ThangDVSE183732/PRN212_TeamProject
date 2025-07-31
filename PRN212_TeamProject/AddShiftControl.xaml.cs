using BLL.Services;
using DAL.Entities;
using DLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddShiftControl.xaml
    /// </summary>
    public partial class AddShiftControl : UserControl
    {
        private DoctorService doctorService;
        private ShiftService shiftService;
        public AddShiftControl()
        {
            InitializeComponent();
            doctorService = new DoctorService();
            shiftService = new ShiftService();
        }

        private void DoctorLoader(object sender, RoutedEventArgs e)
        {
            cnbDoctor.ItemsSource = doctorService.GetAllDoctors();
            cnbDoctor.DisplayMemberPath = "DoctorName";
            cnbDoctor.SelectedValuePath = "DoctorId";
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                txtDateWork.SelectedDate == null ||
                txtStartTime.Value == null ||
                txtEndTime.Value == null ||
                cnbStatus.SelectedValue == null ||
                cnbDoctor.SelectedValue == null)
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string title = txtName.Text.Trim();
                if (title.Length < 5 || title.Length > 150 || !Regex.IsMatch(title, @"^([A-Z1-9][a-zA-Z0-9]*\s?)+$"))
                {
                    MessageBox.Show("Invalid project title format.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtStartTime.Value >= txtEndTime.Value)
                {
                    MessageBox.Show("Start time must be before End time.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var selectedStatus = cnbStatus.SelectedItem as ComboBoxItem;
                Shift newShift = new Shift();
                newShift.Name = txtName.Text;
                newShift.StartTime = TimeOnly.FromDateTime(txtStartTime.Value.Value);
                newShift.EndTime = TimeOnly.FromDateTime(txtEndTime.Value.Value);
                newShift.DateWork = DateOnly.FromDateTime(txtDateWork.SelectedDate ?? DateTime.Today);
                newShift.Status = selectedStatus?.ToString() ?? "";
                if (shiftService.CreateShift(newShift))
                {
                    MessageBox.Show("Shift created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to create shift. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
