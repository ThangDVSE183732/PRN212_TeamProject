using BLL.Services;
using DAL.Entities;
using DLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace PRN212_TeamProject
{
    public partial class DoctorWindow : Window
    {
        private readonly DoctorService _doctorService;
        private DispatcherTimer _timer;
        private List<Doctor> _allDoctors;
        private List<Doctor> _filteredDoctors;

        public DoctorWindow()
        {
            InitializeComponent();
            try
            {
                _doctorService = new DoctorService(new Prn212Context());
                InitializeTimer();
                LoadSpecializations();
                LoadDoctors();
                UpdateTimeDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing DoctorWindow: {ex.Message}", "Initialization Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) => UpdateTimeDisplay();
            _timer.Start();
        }

        private void UpdateTimeDisplay()
        {
            try
            {
                if (TimeTextBlock != null)
                {
                    TimeTextBlock.Text = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss tt");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating time display: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadSpecializations()
        {
            try
            {
                var specializations = _doctorService.GetUniqueSpecializations();
                SpecializationComboBox.Items.Clear();
                SpecializationComboBox.Items.Add("All");

                foreach (var specialization in specializations)
                {
                    SpecializationComboBox.Items.Add(specialization);
                }

                SpecializationComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading specializations: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadDoctors();
                UpdateTimeDisplay();
                StatusTextBlock.Text = "Dashboard refreshed";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?",
                    "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during logout: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDoctors_Click(object sender, RoutedEventArgs e)
        {
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            try
            {
                if (_doctorService != null)
                {
                    _allDoctors = _doctorService.GetAllDoctors();
                    ApplyFilters();
                    StatusTextBlock.Text = "Doctors loaded successfully";
                }
                else
                {
                    MessageBox.Show("Doctor service is not initialized", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                StatusTextBlock.Text = "Error loading doctors";
            }
        }

        private void ApplyFilters()
        {
            try
            {
                _filteredDoctors = _allDoctors?.ToList() ?? new List<Doctor>();

                // Apply specialization filter
                if (SpecializationComboBox.SelectedItem?.ToString() != "All")
                {
                    var selectedSpecialization = SpecializationComboBox.SelectedItem?.ToString();
                    _filteredDoctors = _filteredDoctors.Where(d => d.Specialization == selectedSpecialization).ToList();
                }

                // Apply status filter
                if (StatusComboBox.SelectedItem is ComboBoxItem statusItem && statusItem.Content.ToString() != "All")
                {
                    var selectedStatus = statusItem.Content.ToString();
                    _filteredDoctors = _filteredDoctors.Where(d => d.Status == selectedStatus).ToList();
                }

                DoctorsGrid.ItemsSource = _filteredDoctors;
                RecordCountTextBlock.Text = $"Total Records: {_filteredDoctors.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filters: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SpecializationFilter_Changed(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void StatusFilter_Changed(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void AddDoctor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var addDoctorDialog = new AddDoctorDialog();
                bool? result = addDoctorDialog.ShowDialog();

                if (result == true)
                {
                    var doctor = addDoctorDialog.Doctor;
                    if (doctor != null && _doctorService != null)
                    {
                        bool success = _doctorService.CreateDoctor(doctor);
                        if (success)
                        {
                            MessageBox.Show("Doctor added successfully!", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadDoctors();
                            LoadSpecializations(); // Refresh specializations list
                            StatusTextBlock.Text = "Doctor added successfully";
                        }
                        else
                        {
                            MessageBox.Show("Failed to add doctor. Please check input validation or ensure email/phone are unique.",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            StatusTextBlock.Text = "Failed to add doctor";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding doctor: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                StatusTextBlock.Text = "Error adding doctor";
            }
        }

        private void EditDoctor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DoctorsGrid?.SelectedItem is Doctor selectedDoctor)
                {
                    var editDoctorDialog = new EditDoctorDialog(selectedDoctor);
                    bool? result = editDoctorDialog.ShowDialog();

                    if (result == true)
                    {
                        var updatedDoctor = editDoctorDialog.Doctor;
                        if (updatedDoctor != null && _doctorService != null)
                        {
                            bool success = _doctorService.UpdateDoctor(updatedDoctor);
                            if (success)
                            {
                                MessageBox.Show("Doctor updated successfully!", "Success",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadDoctors();
                                LoadSpecializations(); // Refresh specializations list
                                StatusTextBlock.Text = "Doctor updated successfully";
                            }
                            else
                            {
                                MessageBox.Show("Failed to update doctor. Please check input validation.",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                StatusTextBlock.Text = "Failed to update doctor";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a doctor to edit.", "Warning",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing doctor: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                StatusTextBlock.Text = "Error editing doctor";
            }
        }

        private void BanDoctor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DoctorsGrid?.SelectedItem is Doctor selected)
                {
                    if (selected.Status == "BAN")
                    {
                        MessageBox.Show("Doctor is already banned.", "Information",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    MessageBoxResult confirmResult = MessageBox.Show(
                        $"Are you sure you want to ban Dr. {selected.DoctorName}?",
                        "Confirm Ban", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (confirmResult == MessageBoxResult.Yes && _doctorService != null)
                    {
                        bool success = _doctorService.BanDoctor(selected.DoctorId);
                        if (success)
                        {
                            MessageBox.Show("Doctor banned successfully.", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadDoctors();
                            StatusTextBlock.Text = "Doctor banned successfully";
                        }
                        else
                        {
                            MessageBox.Show("Failed to ban doctor.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            StatusTextBlock.Text = "Failed to ban doctor";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a doctor to ban.", "Warning",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error banning doctor: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                StatusTextBlock.Text = "Error banning doctor";
            }
        }

        private void UnbanDoctor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DoctorsGrid?.SelectedItem is Doctor selected)
                {
                    if (selected.Status == "ACTIVE")
                    {
                        MessageBox.Show("Doctor is already active.", "Information",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    MessageBoxResult confirmResult = MessageBox.Show(
                        $"Are you sure you want to unban Dr. {selected.DoctorName}?",
                        "Confirm Unban", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (confirmResult == MessageBoxResult.Yes && _doctorService != null)
                    {
                        bool success = _doctorService.UnbanDoctor(selected.DoctorId);
                        if (success)
                        {
                            MessageBox.Show("Doctor unbanned successfully.", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadDoctors();
                            StatusTextBlock.Text = "Doctor unbanned successfully";
                        }
                        else
                        {
                            MessageBox.Show("Failed to unban doctor.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            StatusTextBlock.Text = "Failed to unban doctor";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a doctor to unban.", "Warning",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error unbanning doctor: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                StatusTextBlock.Text = "Error unbanning doctor";
            }
        }

        private void RefreshData()
        {
            try
            {
                LoadDoctors();
                LoadSpecializations();
                UpdateTimeDisplay();
                StatusTextBlock.Text = "Data refreshed successfully";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                StatusTextBlock.Text = "Error refreshing data";
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                _timer?.Stop();
                _timer = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error during window cleanup: {ex.Message}");
            }
            finally
            {
                base.OnClosed(e);
            }
        }
    }

    public class AddDoctorDialog : Window
    {
        public Doctor Doctor { get; private set; }
        private readonly DoctorService _doctorService;

        private TextBox nameTextBox;
        private TextBox phoneTextBox;
        private TextBox emailTextBox;
        private TextBox specializationTextBox;
        private TextBox licenseNumberTextBox;
        private TextBox experienceTextBox;
        private PasswordBox passwordBox;

        public AddDoctorDialog()
        {
            InitializeComponent();
            _doctorService = new DoctorService(new Prn212Context());
        }

        private void InitializeComponent()
        {
            this.Title = "Add New Doctor";
            this.Width = 400;
            this.Height = 450;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.NoResize;

            var grid = new Grid();
            grid.Margin = new Thickness(20);

            for (int i = 0; i < 15; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            var nameLabel = new Label { Content = "Doctor Name:" };
            Grid.SetRow(nameLabel, 0);
            grid.Children.Add(nameLabel);

            nameTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(nameTextBox, 1);
            grid.Children.Add(nameTextBox);

            var phoneLabel = new Label { Content = "Phone:" };
            Grid.SetRow(phoneLabel, 2);
            grid.Children.Add(phoneLabel);

            phoneTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(phoneTextBox, 3);
            grid.Children.Add(phoneTextBox);

            var emailLabel = new Label { Content = "Email:" };
            Grid.SetRow(emailLabel, 4);
            grid.Children.Add(emailLabel);

            emailTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(emailTextBox, 5);
            grid.Children.Add(emailTextBox);

            var specializationLabel = new Label { Content = "Specialization:" };
            Grid.SetRow(specializationLabel, 6);
            grid.Children.Add(specializationLabel);

            specializationTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(specializationTextBox, 7);
            grid.Children.Add(specializationTextBox);

            var licenseNumberLabel = new Label { Content = "License Number:" };
            Grid.SetRow(licenseNumberLabel, 8);
            grid.Children.Add(licenseNumberLabel);

            licenseNumberTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(licenseNumberTextBox, 9);
            grid.Children.Add(licenseNumberTextBox);

            var experienceLabel = new Label { Content = "Experience (years):" };
            Grid.SetRow(experienceLabel, 10);
            grid.Children.Add(experienceLabel);

            experienceTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(experienceTextBox, 11);
            grid.Children.Add(experienceTextBox);

            var passwordLabel = new Label { Content = "Password:" };
            Grid.SetRow(passwordLabel, 12);
            grid.Children.Add(passwordLabel);

            passwordBox = new PasswordBox { Margin = new Thickness(0, 5, 0, 20) };
            Grid.SetRow(passwordBox, 13);
            grid.Children.Add(passwordBox);

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            var saveButton = new Button
            {
                Content = "Save",
                Width = 80,
                Height = 30,
                Margin = new Thickness(0, 0, 10, 0)
            };
            saveButton.Click += SaveButton_Click;

            var cancelButton = new Button
            {
                Content = "Cancel",
                Width = 80,
                Height = 30
            };
            cancelButton.Click += CancelButton_Click;

            buttonPanel.Children.Add(saveButton);
            buttonPanel.Children.Add(cancelButton);

            Grid.SetRow(buttonPanel, 14);
            grid.Children.Add(buttonPanel);

            this.Content = grid;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                {
                    MessageBox.Show("Doctor name is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    nameTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(phoneTextBox.Text))
                {
                    MessageBox.Show("Phone number is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    phoneTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(emailTextBox.Text))
                {
                    MessageBox.Show("Email is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    emailTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(specializationTextBox.Text))
                {
                    MessageBox.Show("Specialization is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    specializationTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(passwordBox.Password))
                {
                    MessageBox.Show("Password is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    passwordBox.Focus();
                    return;
                }

                if (!_doctorService.ValidatePhone(phoneTextBox.Text))
                {
                    MessageBox.Show("Please enter a valid phone number (at least 10 digits).",
                        "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    phoneTextBox.Focus();
                    return;
                }

                // Validate experience if provided
                int experience = 0;
                if (!string.IsNullOrWhiteSpace(experienceTextBox.Text))
                {
                    if (!int.TryParse(experienceTextBox.Text, out experience) || experience < 0)
                    {
                        MessageBox.Show("Please enter a valid experience value (0 or positive number).",
                            "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        experienceTextBox.Focus();
                        return;
                    }
                }

                Doctor = new Doctor
                {
                    DoctorName = nameTextBox.Text.Trim(),
                    Phone = phoneTextBox.Text.Trim(),
                    Specialization = specializationTextBox.Text.Trim(),
                    LicenseNumber = licenseNumberTextBox.Text.Trim(),
                    Experience = experience,
                    Status = "ACTIVE"
                };

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving doctor: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }

    public class EditDoctorDialog : Window
    {
        public Doctor Doctor { get; private set; }
        private readonly DoctorService _doctorService;

        private TextBox nameTextBox;
        private TextBox phoneTextBox;
        private TextBox emailTextBox;
        private TextBox specializationTextBox;
        private TextBox licenseNumberTextBox;
        private TextBox experienceTextBox;
        private PasswordBox passwordBox;

        public EditDoctorDialog(Doctor doctor)
        {
            InitializeComponent();
            _doctorService = new DoctorService(new Prn212Context());
            Doctor = doctor;
            LoadDoctorData();
        }

        private void InitializeComponent()
        {
            this.Title = "Edit Doctor";
            this.Width = 400;
            this.Height = 450;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.NoResize;

            var grid = new Grid();
            grid.Margin = new Thickness(20);

            for (int i = 0; i < 15; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            var nameLabel = new Label { Content = "Doctor Name:" };
            Grid.SetRow(nameLabel, 0);
            grid.Children.Add(nameLabel);

            nameTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(nameTextBox, 1);
            grid.Children.Add(nameTextBox);

            var phoneLabel = new Label { Content = "Phone:" };
            Grid.SetRow(phoneLabel, 2);
            grid.Children.Add(phoneLabel);

            phoneTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(phoneTextBox, 3);
            grid.Children.Add(phoneTextBox);

            var emailLabel = new Label { Content = "Email:" };
            Grid.SetRow(emailLabel, 4);
            grid.Children.Add(emailLabel);

            emailTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(emailTextBox, 5);
            grid.Children.Add(emailTextBox);

            var specializationLabel = new Label { Content = "Specialization:" };
            Grid.SetRow(specializationLabel, 6);
            grid.Children.Add(specializationLabel);

            specializationTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(specializationTextBox, 7);
            grid.Children.Add(specializationTextBox);

            var licenseNumberLabel = new Label { Content = "License Number:" };
            Grid.SetRow(licenseNumberLabel, 8);
            grid.Children.Add(licenseNumberLabel);

            licenseNumberTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(licenseNumberTextBox, 9);
            grid.Children.Add(licenseNumberTextBox);

            var experienceLabel = new Label { Content = "Experience (years):" };
            Grid.SetRow(experienceLabel, 10);
            grid.Children.Add(experienceLabel);

            experienceTextBox = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            Grid.SetRow(experienceTextBox, 11);
            grid.Children.Add(experienceTextBox);

            var passwordLabel = new Label { Content = "Password:" };
            Grid.SetRow(passwordLabel, 12);
            grid.Children.Add(passwordLabel);

            passwordBox = new PasswordBox { Margin = new Thickness(0, 5, 0, 20) };
            Grid.SetRow(passwordBox, 13);
            grid.Children.Add(passwordBox);

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            var saveButton = new Button
            {
                Content = "Update",
                Width = 80,
                Height = 30,
                Margin = new Thickness(0, 0, 10, 0)
            };
            saveButton.Click += SaveButton_Click;

            var cancelButton = new Button
            {
                Content = "Cancel",
                Width = 80,
                Height = 30
            };
            cancelButton.Click += CancelButton_Click;

            buttonPanel.Children.Add(saveButton);
            buttonPanel.Children.Add(cancelButton);

            Grid.SetRow(buttonPanel, 14);
            grid.Children.Add(buttonPanel);

            this.Content = grid;
        }

        private void LoadDoctorData()
        {
            try
            {
                if (Doctor != null)
                {
                    nameTextBox.Text = Doctor.DoctorName ?? "";
                    phoneTextBox.Text = Doctor.Phone ?? "";
                    specializationTextBox.Text = Doctor.Specialization ?? "";
                    licenseNumberTextBox.Text = Doctor.LicenseNumber ?? "";
                    experienceTextBox.Text = Doctor.Experience?.ToString() ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctor data: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                {
                    MessageBox.Show("Doctor name is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    nameTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(phoneTextBox.Text))
                {
                    MessageBox.Show("Phone number is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    phoneTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(emailTextBox.Text))
                {
                    MessageBox.Show("Email is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    emailTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(specializationTextBox.Text))
                {
                    MessageBox.Show("Specialization is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    specializationTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(passwordBox.Password))
                {
                    MessageBox.Show("Password is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    passwordBox.Focus();
                    return;
                }

                if (!_doctorService.ValidatePhone(phoneTextBox.Text))
                {
                    MessageBox.Show("Please enter a valid phone number (at least 10 digits).",
                        "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    phoneTextBox.Focus();
                    return;
                }

                int experience = 0;
                if (!string.IsNullOrWhiteSpace(experienceTextBox.Text))
                {
                    if (!int.TryParse(experienceTextBox.Text, out experience) || experience < 0)
                    {
                        MessageBox.Show("Please enter a valid experience value (0 or positive number).",
                            "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        experienceTextBox.Focus();
                        return;
                    }
                }

                Doctor.DoctorName = nameTextBox.Text.Trim();
                Doctor.Phone = phoneTextBox.Text.Trim();
                Doctor.Specialization = specializationTextBox.Text.Trim();
                Doctor.LicenseNumber = licenseNumberTextBox.Text.Trim();
                Doctor.Experience = experience;

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating doctor: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}