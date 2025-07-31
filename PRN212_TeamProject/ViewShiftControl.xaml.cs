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
    /// Interaction logic for ViewShiftControl.xaml
    /// </summary>
    public partial class ViewShiftControl : UserControl
    {
        private ShiftService _service;
        public event Action<Shift> ShiftSelected;
        public ViewShiftControl()
        {
            InitializeComponent();
            _service = new ShiftService();
        }

        public void loadDataInit()
        {
            try
            {
                this.dgShift.ItemsSource = _service.GetAll()
                    .Select(r => new
                    {
                        r.ShiftId,
                        r.Name,
                        r.StartTime,
                        r.EndTime,
                        r.DateWork,
                        r.Status,
                        Doctor = r.Doctor != null ? r.Doctor.DoctorName : "Shift Empty"
                    });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgSlot_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgShift.SelectedItem is not null)
            {
                var selectShift = dgShift.SelectedItem as dynamic;
                if (selectShift != null)
                {
                    int id = int.Parse(selectShift.ShiftId.ToString());
                    Shift shift = _service.GetShiftById(id);
                    if (shift != null)
                    {
                        ShiftSelected?.Invoke(shift);

                    }
                }
            }
        }

        private void ShiftLoader(object sender, RoutedEventArgs e)
        {
            loadDataInit();
        }
    }
}
