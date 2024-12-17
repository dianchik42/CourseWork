using CourseWork.Models;
using CourseWork.Views;
using CourseWork.Views.ContractView;
using CourseWork.Views.DepartmentView;
using CourseWork.Views.EmployeeView;
using CourseWork.Views.EquipmentView;
using CourseWork.Views.ProjectView;
using CourseWork.Views.SubcontractorView;
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

namespace CourseWork
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		private void ShowEmployees(object sender, RoutedEventArgs e)
		{
			EmployeeWindow employeeWindow = new EmployeeWindow();
			employeeWindow.ShowDialog();
			
		}

		private void ShowDepartments(object sender, RoutedEventArgs e)
		{
			
			DepartmentWindow departmentWindow = new DepartmentWindow();
			departmentWindow.ShowDialog();
			
		}

		private void ShowContracts(object sender, RoutedEventArgs e)
		{
			ContractWindow contractWindow = new ContractWindow();
			contractWindow.ShowDialog();
		}

		private void ShowProjects(object sender, RoutedEventArgs e)
		{
			ProjectWindow projectWindow = new ProjectWindow();
			projectWindow.ShowDialog();
		}

		private void ShowEquipments(object sender, RoutedEventArgs e)
		{
			
			EquipmentWindow equipmentWindow = new EquipmentWindow();
			equipmentWindow.ShowDialog();
			
		}

		private void ShowSubcontractors(object sender, RoutedEventArgs e)
		{
			SubcontractorWindow subcontractorWindow = new SubcontractorWindow();
			subcontractorWindow.ShowDialog();
		}
		private void OpenRequestsWindow(object sender, RoutedEventArgs e)
		{
			RequestsWindow requestsWindow = new RequestsWindow();
			requestsWindow.ShowDialog();
		}
	}
}
