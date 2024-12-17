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

namespace CourseWork.Views.DepartmentView
{
	/// <summary>
	/// Логика взаимодействия для DepartmentWindow.xaml
	/// </summary>
	public partial class DepartmentWindow : Window
	{
		private List<Department> departments;
		private List<Employee> employees;
		private readonly JsonDataHandler jsonDataHandler;

		public DepartmentWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadDepartments();
			LoadEmployees();
		}

		private void LoadDepartments()
		{
			departments = jsonDataHandler.LoadData<List<Department>>(AppConfig.DepartmentFilePath) ?? new List<Department>();
			DepartmentDataGrid.ItemsSource = departments;
		}

		private void LoadEmployees()
		{
			employees = jsonDataHandler.LoadData<List<Employee>>(AppConfig.EmployeeFilePath) ?? new List<Employee>();
		}

		private void DepartmentDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (DepartmentDataGrid.SelectedItem is Department selectedDepartment)
			{
				// Фильтруем сотрудников, принадлежащих выбранному отделу
				var departmentEmployees = employees.Where(emp => emp.Department == selectedDepartment.Name).ToList();
				EmployeeDataGrid.ItemsSource = departmentEmployees;
			}
		}

		private void AddDepartment(object sender, RoutedEventArgs e)
		{
			DepartmentEditWindow editWindow = new DepartmentEditWindow();
			if (editWindow.ShowDialog() == true)
			{
				Department newDepartment = editWindow.Department;
				newDepartment.Id = departments.Count + 1;
				departments.Add(newDepartment);
				DepartmentDataGrid.Items.Refresh();
			}
		}

		private void EditDepartment(object sender, RoutedEventArgs e)
		{
			if (DepartmentDataGrid.SelectedItem is Department selectedDepartment)
			{
				DepartmentEditWindow editWindow = new DepartmentEditWindow(selectedDepartment);
				if (editWindow.ShowDialog() == true)
				{
					DepartmentDataGrid.Items.Refresh();
				}
			}
		}

		private void DeleteDepartment(object sender, RoutedEventArgs e)
		{
			if (DepartmentDataGrid.SelectedItem is Department selectedDepartment)
			{
				departments.Remove(selectedDepartment);
				DepartmentDataGrid.Items.Refresh();
			}
		}

		private void SaveDepartments(object sender, RoutedEventArgs e)
		{
			jsonDataHandler.SaveData(departments, AppConfig.DepartmentFilePath);
			MessageBox.Show("Данные отделов сохранены.");
		}
	}
}
