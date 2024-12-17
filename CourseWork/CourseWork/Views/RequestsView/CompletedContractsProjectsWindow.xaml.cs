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

namespace CourseWork.Views.RequestsView
{
	/// <summary>
	/// Логика взаимодействия для CompletedContractsProjectsWindow.xaml
	/// </summary>
	public partial class CompletedContractsProjectsWindow : Window
	{
		private readonly JsonDataHandler jsonDataHandler;
		private List<Contract> contracts;
		private List<Project> projects;

		public CompletedContractsProjectsWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadData();
			CalculateCompletedContractsAndProjects();
		}

		private void LoadData()
		{
			// Загружаем данные о договорах и проектах
			contracts = jsonDataHandler.LoadData<List<Contract>>(AppConfig.ContractFilePath) ?? new List<Contract>();
			projects = jsonDataHandler.LoadData<List<Project>>(AppConfig.ProjectFilePath) ?? new List<Project>();
		}

		private void CalculateCompletedContractsAndProjects()
		{
			DateTime currentDate = DateTime.Now;

			// Фильтруем завершенные проекты
			var completedProjects = projects.Where(p => p.EndDate <= currentDate).ToList();
			ProjectsListBox.ItemsSource = completedProjects.Select(p =>
				$"{p.Name} (Срок: {p.StartDate.ToShortDateString()} - {p.EndDate}, Стоимость: {p.Cost})");

			// Фильтруем завершенные договоры
			var completedContracts = contracts.Where(c =>
				c.Projects.All(p => p.EndDate <= currentDate)).ToList();
			ContractsListBox.ItemsSource = completedContracts.Select(c =>
				$"{c.ClientName} (Общая стоимость: {c.TotalCost})");

			// Вычисляем отдельные стоимости
			decimal totalProjectsCost = completedProjects.Sum(p => p.Cost);
			decimal totalContractsCost = completedContracts.Sum(c => c.TotalCost);

			// Отображаем общие стоимости
			TotalCostTextBlock.Text = $"Общая стоимость выполненных проектов: {totalProjectsCost:C}\n" +
									  $"Общая стоимость выполненных договоров: {totalContractsCost:C}";
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}

}
