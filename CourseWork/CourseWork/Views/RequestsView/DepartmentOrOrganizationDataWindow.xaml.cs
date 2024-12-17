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
	/// Логика взаимодействия для DepartmentOrOrganizationDataWindow.xaml
	/// </summary>
	public partial class DepartmentOrOrganizationDataWindow : Window
	{
		private readonly JsonDataHandler jsonDataHandler;
		private List<Employee> employees;
		private List<string> departments;

		public DepartmentOrOrganizationDataWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadData();
		}

		private void LoadData()
		{
			// Загружаем список сотрудников и уникальные отделы
			employees = jsonDataHandler.LoadData<List<Employee>>(AppConfig.EmployeeFilePath) ?? new List<Employee>();

			// Формируем список отделов с добавлением "Все" для выбора всей организации
			departments = employees.Select(e => e.Department).Distinct().ToList();
			departments.Insert(0, "Все"); // Вставляем "Все" в начало списка

			// Устанавливаем Departments в ItemsSource ComboBox
			DepartmentComboBox.ItemsSource = departments;
			DepartmentComboBox.SelectedIndex = 0;
		}

		private void ExecuteQuery(object sender, RoutedEventArgs e)
		{
			// Получаем фильтры
			string selectedDepartment = DepartmentComboBox.SelectedItem.ToString();
			string selectedCategory = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
			int.TryParse(AgeFromTextBox.Text, out int ageFrom);
			int.TryParse(AgeToTextBox.Text, out int ageTo);

			// Фильтруем список сотрудников
			var filteredEmployees = employees.Where(emp =>
				(selectedDepartment == "Все" || emp.Department == selectedDepartment) &&
				(selectedCategory == "Все" || emp.Role == selectedCategory) &&
				(ageFrom == 0 || emp.Age >= ageFrom) &&
				(ageTo == 0 || emp.Age <= ageTo)
			).ToList();

			// Отображаем результаты
			ResultsListBox.ItemsSource = filteredEmployees.Select(emp =>
				$"{emp.Name} - {emp.Role}, {emp.Department}, Возраст: {emp.Age}");
		}
	}
}
