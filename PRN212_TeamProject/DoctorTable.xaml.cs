using BLL.Services;
using DAL.Entities;
using DLL.Services;
using Microsoft.VisualBasic.ApplicationServices;
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
    /// Interaction logic for DoctorTable.xaml
    /// </summary>
    public partial class DoctorTable : UserControl
    {
        private readonly DoctorService _doctorService = new DoctorService();

        public DoctorTable()
        {
            InitializeComponent();
            LoadDoctorData();
        }

        private void LoadDoctorData()
        {
            var users = _doctorService.GetAllDoctors();
            dgDoctor.ItemsSource = users;
        }

        public Doctor GetSelectedDoctor()
        {
            return dgDoctor.SelectedItem as Doctor;
        }

        public void SetDoctors(List<Doctor> doctors)
        {
            dgDoctor.ItemsSource = null;
            dgDoctor.ItemsSource = doctors;
        }
    }
}
