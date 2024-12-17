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

namespace CourseWork.Views.ProjectView
{
	/// <summary>
	/// Логика взаимодействия для ProjectEditWindow.xaml
	/// </summary>
	public partial class ProjectEditWindow : Window
	{
		public Project Project { get; private set; }
		private readonly JsonDataHandler jsonDataHandler;
		private List<Employee> employees;
		private List<Equipment> equipments;

		public ProjectEditWindow(Project project = null)
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadEmployees();
			LoadEquipment();

			Project = project ?? new Project
			{
				StartDate = DateTime.Now,
				EndDate = DateTime.Now.AddMonths(1),
				Team = new List<Employee>(),
				EquipmentUsed = new List<Equipment>()
			};

			NameTextBox.Text = Project.Name;
			StartDatePicker.SelectedDate = Project.StartDate;
			EndDatePicker.SelectedDate = Project.EndDate;
			CostTextBox.Text = Project.Cost.ToString();

			ManagerComboBox.ItemsSource = employees;
			ManagerComboBox.SelectedItem = employees.Find(e => e.Name == Project.Manager);

			// Отображение команды и оборудования проекта
			TeamListBox.ItemsSource = Project.Team ?? new List<Employee>();
			EquipmentListBox.ItemsSource = Project.EquipmentUsed ?? new List<Equipment>();
		}

		private void LoadEmployees()
		{
			employees = jsonDataHandler.LoadData<List<Employee>>(AppConfig.EmployeeFilePath) ?? new List<Employee>();
		}

		private void LoadEquipment()
		{
			equipments = jsonDataHandler.LoadData<List<Equipment>>(AppConfig.EquipmentFilePath) ?? new List<Equipment>();
		}

		private void AddTeamMember_Click(object sender, RoutedEventArgs e)
		{
			var addMemberWindow = new AddTeamMemberWindow(employees);
			if (addMemberWindow.ShowDialog() == true)
			{
				var selectedEmployee = addMemberWindow.SelectedEmployee;
				if (selectedEmployee != null && !Project.Team.Contains(selectedEmployee))
				{
					Project.Team.Add(selectedEmployee);
					TeamListBox.Items.Refresh();
				}
			}
		}

		private void RemoveTeamMember_Click(object sender, RoutedEventArgs e)
		{
			if (TeamListBox.SelectedItem is Employee selectedEmployee)
			{
				Project.Team.Remove(selectedEmployee);
				TeamListBox.Items.Refresh();
			}
			else
			{
				MessageBox.Show("Пожалуйста, выберите сотрудника для удаления.");
			}
		}

		private void AddEquipment_Click(object sender, RoutedEventArgs e)
		{
			var addEquipmentWindow = new AddEquipmentWindow(equipments);
			if (addEquipmentWindow.ShowDialog() == true)
			{
				var selectedEquipment = addEquipmentWindow.SelectedEquipment;
				if (selectedEquipment != null && !Project.EquipmentUsed.Contains(selectedEquipment))
				{
					Project.EquipmentUsed.Add(selectedEquipment);
					EquipmentListBox.Items.Refresh();
				}
			}
		}

		private void RemoveEquipment_Click(object sender, RoutedEventArgs e)
		{
			if (EquipmentListBox.SelectedItem is Equipment selectedEquipment)
			{
				Project.EquipmentUsed.Remove(selectedEquipment);
				EquipmentListBox.Items.Refresh();
			}
			else
			{
				MessageBox.Show("Пожалуйста, выберите оборудование для удаления.");
			}
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			// Проверка названия проекта
			Project.Name = NameTextBox.Text;
			if (string.IsNullOrWhiteSpace(Project.Name))
			{
				MessageBox.Show("Введите название проекта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Проверка даты начала
			Project.StartDate = StartDatePicker.SelectedDate ?? DateTime.Now;

			// Проверка даты окончания
			Project.EndDate = EndDatePicker.SelectedDate;
			if (Project.EndDate.HasValue && Project.EndDate < Project.StartDate)
			{
				MessageBox.Show("Дата окончания не может быть раньше даты начала.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Проверка стоимости
			if (decimal.TryParse(CostTextBox.Text, out var cost) && cost >= 0)
			{
				Project.Cost = cost;
			}
			else
			{
				MessageBox.Show("Введите корректное значение для стоимости (положительное число).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Проверка менеджера
			Project.Manager = (ManagerComboBox.SelectedItem as Employee)?.Name ?? string.Empty;
			if (string.IsNullOrWhiteSpace(Project.Manager))
			{
				MessageBox.Show("Выберите менеджера проекта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Закрытие окна с успешным результатом
			DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
