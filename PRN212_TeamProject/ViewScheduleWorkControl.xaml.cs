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
    /// Interaction logic for ViewScheduleWorkControl.xaml
    /// </summary>
    public partial class ViewScheduleWorkControl : UserControl
    {
        private Doctor _doctor;
        private ShiftService _shiftService;
        public ViewScheduleWorkControl(Doctor doctor)
        {
            InitializeComponent();
            _doctor = doctor;
            _shiftService = new ShiftService();
        }

        public void loadDataInit()
        {
            try
            {
                this.dgScheduleWork.ItemsSource = _shiftService.GetShiftByDoctorId(_doctor.DoctorId)
                    .Select(r => new
                    {
                        r.ShiftId,
                        r.Name,
                        StartTime = r.StartTime.HasValue
                            ? DateTime.Today.Add(r.StartTime.Value.ToTimeSpan()).ToString("hh:mm tt")
                            : "",
                        EndTime = r.EndTime.HasValue
                            ? DateTime.Today.Add(r.EndTime.Value.ToTimeSpan()).ToString("hh:mm tt")
                            : "",
                        r.DateWork,
                        r.Status
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ScheduleWorkLoad(object sender, RoutedEventArgs e)
        {
            loadDataInit();
        }

        private void dgScheduleWork_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void dgSlotList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement(dgScheduleWork, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null || row.Item == null)
                return;

            // If you use anonymous objects for ItemsSource, cast accordingly
            var selectedItem = row.Item;

            if (selectedItem != null)
            {
                var shiftIdProp = selectedItem.GetType().GetProperty("ShiftId");
                int id = shiftIdProp != null ? (int)shiftIdProp.GetValue(selectedItem) : 0;
                var slotSchedule = new SlotScheduleControl(id);
                slotSchedule.Show();
            }
        }
    }
}
