using CourseWork.Models;
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

namespace CourseWork.Views.ProjectView
{
	/// <summary>
	/// Логика взаимодействия для AddTeamMemberWindow.xaml
	/// </summary>
	public partial class AddTeamMemberWindow : Window
	{
		public Employee SelectedEmployee { get; private set; }
		private readonly List<Employee> employees;
		private List<string> departments;

		public AddTeamMemberWindow(List<Employee> employees)
		{
			InitializeComponent();
			this.employees = employees;
			LoadDepartments();
		}

		private void LoadDepartments()
		{
			// Получаем уникальные отделы
			departments = employees.Select(e => e.Department).Distinct().ToList();
			DepartmentComboBox.ItemsSource = departments;
		}

		private void DepartmentComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			// Фильтруем сотрудников по выбранному отделу
			if (DepartmentComboBox.SelectedItem is string selectedDepartment)
			{
				EmployeeListBox.ItemsSource = employees.Where(emp => emp.Department == selectedDepartment).ToList();
			}
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			SelectedEmployee = EmployeeListBox.SelectedItem as Employee;
			DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
