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
    public class ExamOption
    {
        public string Name { get; }
        public decimal Price { get; }

        public string DisplayText => $"{Name} - {Price:N0} đ";

        public ExamOption(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
    /// <summary>
    /// Interaction logic for ScheduleManagement.xaml
    /// </summary>
    public partial class BookManagement : UserControl
    {
        private readonly Action _onNext;
        public BookManagement(Action onNext)
        {
            InitializeComponent();
            _onNext = onNext;
            ExamTypeComboBox.ItemsSource = _examOptions;

        }
        private void UpdatePriceSummary()
        {
            if (_selectedExam != null)
            {
                SelectedExamText.Text = $"Loại: {_selectedExam.Name}";
                PriceText.Text = $"Giá: {_selectedExam.Price:N0} đ";
            }
            else
            {
                SelectedExamText.Text = "Chưa chọn loại khám";
                PriceText.Text = "Giá: 0 đ";
            }
        }
        private ExamOption? _selectedExam;

        private readonly List<ExamOption> _examOptions = new()
        {
            new ExamOption("Tổng quát", 10000000),          
            new ExamOption("Từng bộ phận", 2000000),    
            new ExamOption("Da liễu", 2500000),         
            new ExamOption("Sinh lý", 1500000),            
        };
        private void ExamTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedExam = ExamTypeComboBox.SelectedItem as ExamOption;
            UpdatePriceSummary();
        }
        private void PayOButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to pay?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(CardRadio.IsChecked == true)

                MessageBox.Show("Payment successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    _onNext.Invoke();
                } 
                    
            }
        }
    }
}
