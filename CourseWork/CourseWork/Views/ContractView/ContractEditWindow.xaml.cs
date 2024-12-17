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

namespace CourseWork.Views.ContractView
{
	/// <summary>
	/// Логика взаимодействия для ContractEditWindow.xaml
	/// </summary>
	public partial class ContractEditWindow : Window
	{
		public Contract Contract { get; private set; }
		private readonly JsonDataHandler jsonDataHandler;
		private List<Employee> employees;
		private List<Project> projects;

		public ContractEditWindow(Contract contract = null)
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			Contract = contract ?? new Contract { Projects = new List<Project>() };

			// Заполняем поля контракта
			ClientNameTextBox.Text = Contract.ClientName;

			// Выбор проектов контракта
			ProjectsListBox.ItemsSource = projects;
			foreach (var project in Contract.Projects)
			{
				ProjectsListBox.SelectedItems.Add(project);
			}

			this.Activated += ContractEditWindow_Activated; // Добавляем событие активации
		}

		// Метод для загрузки менеджеров и проектов при каждом открытии
		private void ContractEditWindow_Activated(object sender, EventArgs e)
		{
			LoadManagers();
			LoadProjects();

			// Обновляем источник данных для ComboBox и ListBox
			ManagerComboBox.ItemsSource = employees;
			ProjectsListBox.ItemsSource = projects;

			// Устанавливаем текущий менеджер, если он есть
			ManagerComboBox.SelectedItem = employees.Find(em => em.Name == Contract.Manager);
		}

		private void LoadManagers()
		{
			employees = jsonDataHandler.LoadData<List<Employee>>(AppConfig.EmployeeFilePath) ?? new List<Employee>();
			employees = employees.Where(e => e.Role == "Менеджер").ToList();
		}

		private void LoadProjects()
		{
			projects = jsonDataHandler.LoadData<List<Project>>(AppConfig.ProjectFilePath) ?? new List<Project>();
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			Contract.ClientName = ClientNameTextBox.Text;
			Contract.Manager = (ManagerComboBox.SelectedItem as Employee)?.Name ?? string.Empty;

			Contract.Projects = new List<Project>();
			foreach (var item in ProjectsListBox.SelectedItems)
			{
				Contract.Projects.Add(item as Project);
			}

			Contract.TotalCost = Contract.Projects.Sum(p => p.Cost);
			DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
