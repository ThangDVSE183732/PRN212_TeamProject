using BLL.Services;
using DAL.Entities;
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
using System.Xml.Linq;

namespace PRN212_TeamProject
{
    /// <summary>
    /// Interaction logic for AddSlotControl.xaml
    /// </summary>
    public partial class AddSlotControl : UserControl
    {
        private ShiftService shiftService;
        private SlotService slotService;
        public AddSlotControl()
        {
            InitializeComponent();
            shiftService = new ShiftService();
            slotService = new SlotService();
        }

        private void SlotLoader(object sender, RoutedEventArgs e)
        {

            cnbShift.ItemsSource = shiftService.GetAll();
            cnbShift.DisplayMemberPath = "DisplayTime";
            cnbShift.SelectedValuePath = "ShiftId";
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
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
                Slot newSlot = new Slot();
                newSlot.StartTime = TimeOnly.FromDateTime(txtStartTime.Value.Value);
                newSlot.EndTime = TimeOnly.FromDateTime(txtEndTime.Value.Value);
                newSlot.ShiftId = int.Parse(cnbShift.SelectedValue.ToString());
                newSlot.Status = selectedStatus?.Content.ToString() ?? "";
                if (slotService.CreateSlot(newSlot))
                {
                    MessageBox.Show("Slot created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to create slot. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
