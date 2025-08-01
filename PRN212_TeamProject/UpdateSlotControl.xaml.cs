using BLL.Services;
using DAL.Entities;
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
using System.Xml.Linq;

namespace PRN212_TeamProject
{
    /// <summary>
    /// Interaction logic for UpdateSlotControl.xaml
    /// </summary>
    public partial class UpdateSlotControl : UserControl
    {
        private Slot _slot;
        private SlotService _slotService; 
        private ShiftService shiftService;
        public UpdateSlotControl(Slot slot)
        {
            InitializeComponent();
            _slot = slot;
            _slotService = new SlotService();
            shiftService = new ShiftService();
        }

        private void SlotLoader(object sender, RoutedEventArgs e)
        {
            if (_slot == null)
            {
                MessageBox.Show("Please choose at least one slot to update. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            txtStartTime.Value = _slot.StartTime.HasValue
                    ? DateTime.Today.Add(_slot.StartTime.Value.ToTimeSpan())
                    : null;
            txtEndTime.Value = _slot.EndTime.HasValue
                    ? DateTime.Today.Add(_slot.EndTime.Value.ToTimeSpan())
                    : null;
            cnbStatus.SelectedItem = cnbStatus.Items.Cast<ComboBoxItem>().FirstOrDefault(c => c.Content?.ToString().Equals(_slot.Status.ToString(), StringComparison.OrdinalIgnoreCase) == true);
            cnbShift.ItemsSource = shiftService.GetAll();
            cnbShift.DisplayMemberPath = "DisplayTime";
            cnbShift.SelectedValuePath = "ShiftId";
            cnbShift.SelectedValue = _slot.ShiftId;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtStartTime.Value == null ||
                txtEndTime.Value == null ||
                cnbStatus.SelectedValue == null ||
                cnbShift.SelectedValue == null)
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                if (txtStartTime.Value >= txtEndTime.Value)
                {
                    MessageBox.Show("Start time must be before End time.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var selectedStatus = cnbStatus.SelectedItem as ComboBoxItem;
                Slot updatedSlot = _slotService.GetSlottById(_slot.SlotId);
                updatedSlot.StartTime = TimeOnly.FromDateTime(txtStartTime.Value.Value);
                updatedSlot.EndTime = TimeOnly.FromDateTime(txtEndTime.Value.Value);
                updatedSlot.ShiftId = int.Parse(cnbShift.SelectedValue.ToString());
                updatedSlot.Status = selectedStatus?.Content.ToString() ?? "";
                if (_slotService.UpdateSlot(updatedSlot))
                {
                    MessageBox.Show("Slot updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update slot. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
