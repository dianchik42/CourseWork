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
	/// Логика взаимодействия для ContractsProjectsWindow.xaml
	/// </summary>
	public partial class ContractsProjectsWindow : Window
	{
		private readonly JsonDataHandler jsonDataHandler;
		private List<Contract> contracts;
		private List<Project> projects;

		public ContractsProjectsWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadData();
		}

		private void LoadData()
		{
			// Загружаем данные о договорах и проектах
			contracts = jsonDataHandler.LoadData<List<Contract>>(AppConfig.ContractFilePath) ?? new List<Contract>();
			projects = jsonDataHandler.LoadData<List<Project>>(AppConfig.ProjectFilePath) ?? new List<Project>();
		}

		private void ExecuteQuery(object sender, RoutedEventArgs e)
		{
			// Получаем выбранные даты
			DateTime? startDate = StartDatePicker.SelectedDate;
			DateTime? endDate = EndDatePicker.SelectedDate;

			// Если даты не выбраны, то берем текущую дату
			if (!startDate.HasValue) startDate = DateTime.MinValue;
			if (!endDate.HasValue) endDate = DateTime.MaxValue;

			// Фильтрация договоров
			var filteredContracts = contracts.Where(c =>
				c.Projects.Any(p => p.StartDate >= startDate && p.EndDate <= endDate)).ToList();

			// Фильтрация проектов
			var filteredProjects = projects.Where(p =>
				p.StartDate >= startDate && p.EndDate <= endDate).ToList();

			// Формируем результаты
			ResultsListBox.Items.Clear();
			ResultsListBox.Items.Add("Договоры:");
			foreach (var contract in filteredContracts)
			{
				ResultsListBox.Items.Add($"Договор: {contract.ClientName}, Стоимость: {contract.TotalCost}");
			}

			ResultsListBox.Items.Add("");
			ResultsListBox.Items.Add("Проекты:");
			foreach (var project in filteredProjects)
			{
				ResultsListBox.Items.Add($"Проект: {project.Name}, Даты: {project.StartDate.ToShortDateString()} - {project.EndDate}");
			}
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
