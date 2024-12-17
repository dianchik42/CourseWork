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

namespace CourseWork.Views.EmployeeView
{
	/// <summary>
	/// Логика взаимодействия для EmployeeEditWindow.xaml
	/// </summary>
	public partial class EmployeeEditWindow : Window
	{
		public Employee Employee { get; private set; }
		private readonly JsonDataHandler jsonDataHandler;
		private List<Department> departments;

		public EmployeeEditWindow(Employee employee = null)
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadDepartments();

			// Инициализация сотрудника
			Employee = employee ?? new Employee();

			// Заполнение данных сотрудника в поля
			NameTextBox.Text = Employee.Name;
			AgeTextBox.Text = Employee.Age.ToString();

			// Устанавливаем выбранную роль в RoleComboBox
			RoleComboBox.SelectedIndex = GetRoleIndex(Employee.Role);

			// Заполняем ComboBox с отделами
			DepartmentComboBox.ItemsSource = departments;
			DepartmentComboBox.SelectedItem = departments.Find(d => d.Name == Employee.Department);
		}

		private int GetRoleIndex(string role)
		{
			for (int i = 0; i < RoleComboBox.Items.Count; i++)
			{
				if (RoleComboBox.Items[i] is ComboBoxItem item && item.Content.ToString() == role)
				{
					return i;
				}
			}
			return -1; // Если роль не найдена, ничего не выбираем
		}

		private void LoadDepartments()
		{
			departments = jsonDataHandler.LoadData<List<Department>>(AppConfig.DepartmentFilePath) ?? new List<Department>();
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			// Проверка имени (только буквы и пробелы)
			Employee.Name = NameTextBox.Text.Trim();
			if (string.IsNullOrWhiteSpace(Employee.Name) || !Employee.Name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
			{
				MessageBox.Show("Имя должно содержать только буквы и пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Проверка возраста (только цифры)
			if (int.TryParse(AgeTextBox.Text.Trim(), out int age) && age > 0)
			{
				Employee.Age = age;
			}
			else
			{
				MessageBox.Show("Введите корректное значение возраста (положительное число).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Проверка роли (выбор из ComboBox)
			if (RoleComboBox.SelectedItem is ComboBoxItem selectedRole)
			{
				Employee.Role = selectedRole.Content.ToString();
			}
			else
			{
				MessageBox.Show("Выберите роль сотрудника.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Проверка отдела
			if (DepartmentComboBox.SelectedItem is Department selectedDepartment)
			{
				Employee.Department = selectedDepartment.Name;
			}
			else
			{
				Employee.Department = string.Empty; // Если отдел не выбран
			}

			// Успешное сохранение
			DialogResult = true;
		}



		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false; // Закрывает окно без сохранения изменений
		}
	}
}
