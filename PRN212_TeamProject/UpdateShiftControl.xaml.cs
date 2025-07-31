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
    /// Interaction logic for UpdateShiftControl.xaml
    /// </summary>
    public partial class UpdateShiftControl : UserControl
    {
        private Shift _shift;
        private ShiftService shiftService;
        private DoctorService doctorService;
        public UpdateShiftControl(Shift shift)
        {
            InitializeComponent();
            _shift = shift;
            shiftService = new ShiftService();
            doctorService = new DoctorService();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
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
                Shift newShift = shiftService.GetShiftById(_shift.ShiftId);
                newShift.Name = txtName.Text;
                newShift.StartTime = TimeOnly.FromDateTime(txtStartTime.Value.Value);
                newShift.EndTime = TimeOnly.FromDateTime(txtEndTime.Value.Value);
                newShift.DateWork = DateOnly.FromDateTime(txtDateWork.SelectedDate ?? DateTime.Today);
                newShift.DoctorId = int.Parse(cnbDoctor.SelectedValue.ToString());
                newShift.Status = selectedStatus?.Content.ToString() ?? "";
                if (shiftService.UpdateShift(newShift))
                {
                    MessageBox.Show("Shift update successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update shift. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void UpdateShiftLoader(object sender, RoutedEventArgs e)
        {
            if(_shift == null)
            {
                MessageBox.Show("Please choose at least one shift to update. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            txtName.Text = _shift.Name;
            txtStartTime.Value = _shift.StartTime.HasValue
                    ? DateTime.Today.Add(_shift.StartTime.Value.ToTimeSpan())
                    : null;
            txtEndTime.Value = _shift.EndTime.HasValue
                    ? DateTime.Today.Add(_shift.EndTime.Value.ToTimeSpan())
                    : null;
            txtDateWork.Text = _shift.DateWork.ToString();
            cnbStatus.SelectedItem = cnbStatus.Items.Cast<ComboBoxItem>().FirstOrDefault(c => c.Content?.ToString().Equals(_shift.Status.ToString(), StringComparison.OrdinalIgnoreCase) == true);
            cnbDoctor.ItemsSource = doctorService.GetAllDoctors();
            cnbDoctor.DisplayMemberPath = "DoctorName";
            cnbDoctor.SelectedValuePath = "DoctorId";
            cnbDoctor.SelectedValue = _shift.DoctorId;

        }
    }
}
