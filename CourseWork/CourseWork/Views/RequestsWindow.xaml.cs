using CourseWork.Views.RequestsView;
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

namespace CourseWork.Views
{
	/// <summary>
	/// Логика взаимодействия для RequestsWindow.xaml
	/// </summary>
	public partial class RequestsWindow : Window
	{
		public RequestsWindow()
		{
			InitializeComponent();
		}

		private void GetDepartmentOrOrganizationData(object sender, RoutedEventArgs e)
		{
			DepartmentOrOrganizationDataWindow dataWindow = new DepartmentOrOrganizationDataWindow();
			dataWindow.ShowDialog();
		}

		private void GetDepartmentHeads(object sender, RoutedEventArgs e)
		{
			DepartmentHeadsWindow dataWindow = new DepartmentHeadsWindow();
			dataWindow.ShowDialog();
		}

		private void GetCurrentOrPeriodContractsProjects(object sender, RoutedEventArgs e)
		{
			ContractsProjectsWindow dataWindow = new ContractsProjectsWindow();
			dataWindow.ShowDialog();
			
		}

		private void GetCompletedContractsProjectsCost(object sender, RoutedEventArgs e)
		{
			CompletedContractsProjectsWindow dataWindow = new CompletedContractsProjectsWindow();
			dataWindow.ShowDialog();
		}


		private void GetEmployeeParticipationInProjects(object sender, RoutedEventArgs e)
		{
			EmployeeParticipationWindow dataWindow = new EmployeeParticipationWindow();
			dataWindow.ShowDialog();
			
		}

		private void GetSubcontractorWorkCost(object sender, RoutedEventArgs e)
		{
			SubcontractorsWorkWindow dataWindow = new SubcontractorsWorkWindow();
			dataWindow.ShowDialog();
		}


		private void GetEquipmentEfficiency(object sender, RoutedEventArgs e)
		{
			EquipmentEfficiencyWindow dataWindow = new EquipmentEfficiencyWindow();
			dataWindow.ShowDialog();
		}

		private void GetContractsEfficiency(object sender, RoutedEventArgs e)
		{
			ContractEfficiencyWindow dataWindow = new ContractEfficiencyWindow();
			dataWindow.ShowDialog();
		}
	}
}
