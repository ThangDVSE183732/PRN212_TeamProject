using BLL.Services;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PRN212_TeamProject
{
    public partial class UpdateScheduleControl : UserControl
    {
        private readonly BookingService _bookingService;
        private readonly ServiceService _serviceService;
        private readonly SlotService _slotService;
        private readonly PatientService _patientService = new PatientService();

        private readonly Booking _selectedBooking;

        public UpdateScheduleControl(Booking booking)
        {
            InitializeComponent();
            _selectedBooking = booking;
            _bookingService = new BookingService();
            _serviceService = new ServiceService();
            _slotService = new SlotService();
            LoadServiceComboBox();
            LoadSlotComboBox();
            LoadData();
        }

        private void LoadServiceComboBox()
        {
            var services = _serviceService.GetServices();
            cbService.ItemsSource = services;
            cbService.DisplayMemberPath = "Name";
            cbService.SelectedValuePath = "ServiceId";
        }

        private void LoadSlotComboBox()
        {
            var slots = _slotService.GetSlot();
            cbSlot.ItemsSource = slots;
            cbSlot.DisplayMemberPath = "DisplayTime";
            cbSlot.SelectedValuePath = "SlotId";
        }

        private void LoadData()
        {
            txtPatientId.Text = _selectedBooking.PatientId.ToString();
            cbService.SelectedValue = _selectedBooking.ServiceId;
            cbSlot.SelectedValue = _selectedBooking.SlotId;
            dpDateBooking.SelectedDate = _selectedBooking.DateBooking;
            txtNote.Text = _selectedBooking.Note;
            txtResult.Text = _selectedBooking.Result;

            // Tìm ComboBoxItem có Content đúng với Status
            foreach (ComboBoxItem item in cbStatus.Items)
            {
                if (item.Content != null && item.Content.ToString() == _selectedBooking.Status)
                {
                    cbStatus.SelectedItem = item;
                    break;
                }
            }
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Validate PatientId
            if (!int.TryParse(txtPatientId.Text.Trim(), out int patientId))
            {
                MessageBox.Show("Invalid Patient ID.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var patient = _patientService.GetById(patientId);
            if (patient == null)
            {
                MessageBox.Show("Patient ID does not exist.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate Service
            if (cbService.SelectedValue == null)
            {
                MessageBox.Show("Please select a service.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate Slot
            if (cbSlot.SelectedValue == null)
            {
                MessageBox.Show("Please select a slot.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isSlotTaken = _bookingService.IsSlotTaken((int)cbSlot.SelectedValue, dpDateBooking.SelectedDate.Value, _selectedBooking.BookingId);
            if (isSlotTaken)
            {
                MessageBox.Show("This slot is already booked on the selected date.", "Slot Conflict", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate Date
            if (!dpDateBooking.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a booking date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var updated = new Booking
            {
                BookingId = _selectedBooking.BookingId,
                PatientId = patientId,
                ServiceId = (int)cbService.SelectedValue,
                SlotId = (int)cbSlot.SelectedValue,
                DateBooking = dpDateBooking.SelectedDate.Value,
                Note = txtNote.Text.Trim(),
                Result = txtResult.Text.Trim(),
                Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? ""
            };

            try
            {
                bool result = _bookingService.Update(updated);
                if (result)
                {
                    MessageBox.Show("Booking updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update booking.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
