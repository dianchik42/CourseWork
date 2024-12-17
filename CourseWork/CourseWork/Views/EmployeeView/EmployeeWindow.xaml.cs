using CourseWork.Config;
using CourseWork.Models;
using CourseWork.Service;
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

namespace CourseWork.Views.EmployeeView
{
	/// <summary>
	/// Логика взаимодействия для EmployeeWindow.xaml
	/// </summary>
	public partial class EmployeeWindow : Window
	{
		private List<Employee> employees;
		private readonly string employeeFilePath = AppConfig.EmployeeFilePath;
		private JsonDataHandler jsonDataHandler;

		public EmployeeWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadEmployees();
		}

		private void LoadEmployees()
		{
			employees = jsonDataHandler.LoadData<List<Employee>>(employeeFilePath) ?? new List<Employee>();
			EmployeeDataGrid.ItemsSource = employees;
		}

		private void AddEmployee(object sender, RoutedEventArgs e)
		{
			EmployeeEditWindow editWindow = new EmployeeEditWindow();
			if (editWindow.ShowDialog() == true)
			{
				Employee newEmployee = editWindow.Employee;
				newEmployee.Id = employees.Count + 1; // Присваиваем новый ID
				employees.Add(newEmployee);
				EmployeeDataGrid.Items.Refresh();
			}
		}

		private void EditEmployee(object sender, RoutedEventArgs e)
		{
			if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
			{
				EmployeeEditWindow editWindow = new EmployeeEditWindow(selectedEmployee);
				if (editWindow.ShowDialog() == true)
				{
					// Обновляем данные в списке
					EmployeeDataGrid.Items.Refresh();
				}
			}
		}

		private void DeleteEmployee(object sender, RoutedEventArgs e)
		{
			if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
			{
				employees.Remove(selectedEmployee);
				EmployeeDataGrid.Items.Refresh();
			}
		}

		private void SaveEmployees(object sender, RoutedEventArgs e)
		{
			jsonDataHandler.SaveData(employees, employeeFilePath);
			MessageBox.Show("Данные сотрудников сохранены.");
		}
	}
}
