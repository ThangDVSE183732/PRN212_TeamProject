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
    /// Interaction logic for ViewSlotControl.xaml
    /// </summary>
    public partial class ViewSlotControl : UserControl
    {
        public event Action<Slot> SlotSelected;
        private SlotService _slotService;
        public ViewSlotControl()
        {
            InitializeComponent();
            _slotService = new SlotService();
        }

        public void loadDataInit()
        {
            try
            {
                this.dgSlot.ItemsSource = _slotService.GetSlot()
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

        private void dgSlot_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSlot.SelectedItem is not null)
            {
                var selectSlot = dgSlot.SelectedItem as dynamic;
                if (selectSlot != null)
                {
                    int id = int.Parse(selectSlot.SlotId.ToString());
                    Slot slot = _slotService.GetSlottById(id);
                    if (slot != null)
                    {
                        SlotSelected?.Invoke(slot);
                    }
                }
            }
        }

        private void SlotLoader(object sender, RoutedEventArgs e)
        {
            loadDataInit();
        }
    }
}
