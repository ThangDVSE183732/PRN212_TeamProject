using BLL.Services;
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
using System.Windows.Shapes;

namespace PRN212_TeamProject
{
    /// <summary>
    /// Interaction logic for SlotScheduleControl.xaml
    /// </summary>
    public partial class SlotScheduleControl : Window
    {
        private int _id;
        private SlotService slotService;
        public SlotScheduleControl(int id)
        {
            InitializeComponent();
            _id = id;
            slotService = new SlotService();
        }

        private void dgSlot_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SlotLoader(object sender, RoutedEventArgs e)
        {
            loadDataInit();
        }

        public void loadDataInit()
        {
            try
            {
                this.dgSlot.ItemsSource = slotService.GetSlottByShiftId(_id)
                    .Select(r => new
                    {
                        r.SlotId,
                        StartTime = r.StartTime.HasValue
                            ? DateTime.Today.Add(r.StartTime.Value.ToTimeSpan()).ToString("hh:mm tt")
                            : "",
                        EndTime = r.EndTime.HasValue
                            ? DateTime.Today.Add(r.EndTime.Value.ToTimeSpan()).ToString("hh:mm tt")
                            : "",
                        r.Status,
                        Shift = r.Shift != null ? r.Shift.Name : "Shift Empty"
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
