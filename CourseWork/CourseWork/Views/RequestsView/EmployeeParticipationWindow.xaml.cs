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
	/// Логика взаимодействия для EmployeeParticipationWindow.xaml
	/// </summary>
	public partial class EmployeeParticipationWindow : Window
	{
		private readonly JsonDataHandler jsonDataHandler;
		private List<Employee> employees;
		private List<Project> projects;

		public EmployeeParticipationWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadData();
		}

		private void LoadData()
		{
			// Загружаем данные о сотрудниках и проектах
			employees = jsonDataHandler.LoadData<List<Employee>>(AppConfig.EmployeeFilePath) ?? new List<Employee>();
			projects = jsonDataHandler.LoadData<List<Project>>(AppConfig.ProjectFilePath) ?? new List<Project>();

			// Заполняем ComboBox с сотрудниками
			EmployeeComboBox.ItemsSource = employees;
		}

		private void ExecuteQuery(object sender, RoutedEventArgs e)
		{
			// Получаем выбранного сотрудника и категорию
			var selectedEmployee = EmployeeComboBox.SelectedItem as Employee;
			string selectedCategory = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

			// Фильтруем проекты по сотруднику
			IEnumerable<Project> filteredProjects;

			if (selectedEmployee != null)
			{
				// Если выбран конкретный сотрудник
				filteredProjects = projects.Where(p => p.Team.Any(ex => ex.Id == selectedEmployee.Id));
			}
			else if (selectedCategory != "Все")
			{
				// Если выбрана категория сотрудников
				filteredProjects = projects.Where(p => p.Team.Any(ex => ex.Role == selectedCategory));
			}
			else
			{
				// Если выбрано "Все"
				filteredProjects = projects;
			}

			// Отображаем результаты
			ResultsListBox.Items.Clear();
			foreach (var project in filteredProjects)
			{
				ResultsListBox.Items.Add($"Проект: {project.Name} (Срок: {project.StartDate.ToShortDateString()} - {project.EndDate})");
			}
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
