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
    /// Interaction logic for ScheduleManagement.xaml
    /// </summary>
    public partial class QrManagement : UserControl
    {
        private readonly Action _onBack;

        public QrManagement(Action onBack)
        {
            InitializeComponent();
            _onBack = onBack;
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _onBack.Invoke();
        }

    }
}
