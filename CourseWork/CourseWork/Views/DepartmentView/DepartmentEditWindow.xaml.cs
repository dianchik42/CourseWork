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
	/// Логика взаимодействия для DepartmentEditWindow.xaml
	/// </summary>
	public partial class DepartmentEditWindow : Window
	{
		public Department Department { get; private set; }
		private readonly JsonDataHandler jsonDataHandler;
		private List<Employee> employees;

		public DepartmentEditWindow(Department department = null)
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadEmployees();

			Department = department ?? new Department();

			DepartmentNameTextBox.Text = Department.Name;

			HeadComboBox.ItemsSource = employees;
			HeadComboBox.SelectedItem = employees.Find(e => e.Name == Department.Head);
		}

		private void LoadEmployees()
		{
			employees = jsonDataHandler.LoadData<List<Employee>>(AppConfig.EmployeeFilePath) ?? new List<Employee>();
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			Department.Name = DepartmentNameTextBox.Text;

			// Получаем нового руководителя из ComboBox
			var newHead = HeadComboBox.SelectedItem as Employee;

			if (newHead != null)
			{
				// Если в отделе уже есть руководитель
				if (!string.IsNullOrEmpty(Department.Head))
				{
					// Находим текущего руководителя
					var currentHead = employees.FirstOrDefault(ex => ex.Name == Department.Head);

					if (currentHead != null)
					{
						// Меняем роль текущего руководителя на "Менеджер"
						currentHead.Role = "Менеджер";
					}
				}

				// Назначаем нового руководителя
				Department.Head = newHead.Name;

				// Меняем роль нового руководителя на "Руководитель отдела"
				newHead.Role = "Руководитель отдела";
			}
			else
			{
				// Если руководитель не выбран, очищаем поле Head
				Department.Head = string.Empty;
			}

			// Сохраняем изменения в JSON
			jsonDataHandler.SaveData(employees, AppConfig.EmployeeFilePath);

			DialogResult = true; // Закрывает окно и возвращает успешный результат
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false; // Закрывает окно без сохранения изменений
		}
	}
}
