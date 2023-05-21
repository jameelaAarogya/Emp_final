﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private HttpClient _httpClient;
        private string _baseUrl = ConfigurationManager.AppSettings["_baseUrl"];
        private readonly string apiUrl = "https://gorest.co.in/public/v2/users";
        private string _accessToken = ConfigurationManager.AppSettings["_accessToken"];      

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            var employees = await GetEmployees();
            dataGrid.ItemsSource = employees;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            AddAuthenticationHeader();

        var response = await _httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Employee>>(content);
                }
        private void AddAuthenticationHeader()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        }        

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Get employee details from UI input fields
            var selectedEmployee = dataGrid.SelectedItem as Employee;
<<<<<<< Updated upstream
            selectedEmployee.Name = txtName.Text;
            selectedEmployee.Email = txtEmail.Text;
            selectedEmployee.Gender = txtgender.Text;
            selectedEmployee.Status = txtstatus.Text;
           
                try
                {
=======
            
            // Check if the selectedEmployee is null or if the name is invalid
            if (selectedEmployee == null || string.IsNullOrWhiteSpace(selectedEmployee.Name))
            {
                MessageBox.Show("Invalid employee or name value. Please select a valid employee and ensure that the name is not empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check if the selectedEmployee is null or the gender is invalid
            if (selectedEmployee == null || !IsValidGender(selectedEmployee.Gender.ToLowerInvariant()))
            {
                MessageBox.Show("Invalid employee or gender value. Please select a valid employee and ensure that the gender is 'male' or 'MAE'.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if the selectedEmployee is null or the email is invalid
            if (selectedEmployee == null || !IsValidEmail(selectedEmployee.Email))
            {
                MessageBox.Show("Invalid employee or email value. Please select a valid employee and ensure that the email address is in the correct format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check if the selectedEmployee is null or the status is invalid
            if (selectedEmployee == null || !IsValidStatus(selectedEmployee.Status.ToLowerInvariant()))
            {
                MessageBox.Show("Invalid employee or status value. Please select a valid employee and ensure that the status is 'active' or 'inactive'.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
>>>>>>> Stashed changes
                    // Call the API to update the employee
                    var updatedEmployee = await UpdateEmployeeAsync(selectedEmployee);
                    MessageBox.Show($"Employee updated successfully");
                }
                catch (Exception ex)
                {
                    // Handle any API errors or display error message
                    MessageBox.Show($"Error updating employee: {ex.Message}");
                }            
        }

        private async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var employeeJson = JsonConvert.SerializeObject(employee);
            var content = new StringContent(employeeJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"users/{employee.Id}", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var updatedEmployee = JsonConvert.DeserializeObject<Employee>(responseContent);

            return updatedEmployee;
        }  
      
       private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected employee from the DataGrid
            var selectedEmployee = dataGrid.SelectedItem as Employee;
            if (selectedEmployee == null)
            {
                MessageBox.Show("Please select an employee to delete.");
                return;
            }

            // Confirm the deletion with the user
            var result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Deletion", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                // Call the API to delete the employee
                await DeleteEmployeeAsync(selectedEmployee.Id);

                // Refresh the employee list
                LoadEmployees();

                // Clear the input fields
                ClearFields();
            }
        }
        private async Task DeleteEmployeeAsync(int employeeId)
        {
            var response = await _httpClient.DeleteAsync($"users/{employeeId}");
            response.EnsureSuccessStatusCode();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        { 
            txtName.Text = "";
            txtEmail.Text = "";
            txtgender.Text = "";
            txtstatus.Text = "";
            dataGrid.SelectedItem = null;
        }
    }

}
