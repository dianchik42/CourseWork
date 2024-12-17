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

namespace CourseWork.Views.EquipmentView
{
	/// <summary>
	/// Логика взаимодействия для EquipmentEditWindow.xaml
	/// </summary>
	public partial class EquipmentEditWindow : Window
	{
		public Equipment Equipment { get; private set; }
		private readonly JsonDataHandler jsonDataHandler;
		private List<Department> departments;

		public EquipmentEditWindow(Equipment equipment = null)
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadDepartments();

			Equipment = equipment ?? new Equipment();

			// Инициализируем поля данными, если редактируем существующее оборудование
			NameTextBox.Text = Equipment.Name;
			IsSharedCheckBox.IsChecked = Equipment.IsShared;
			IsInUseCheckBox.IsChecked = Equipment.IsInUse;

			// Устанавливаем список отделов в ComboBox и выделяем текущий отдел, если он указан
			DepartmentComboBox.ItemsSource = departments;
			DepartmentComboBox.SelectedItem = departments.Find(d => d.Name == Equipment.AssignedDepartment);
		}

		private void LoadDepartments()
		{
			departments = jsonDataHandler.LoadData<List<Department>>(AppConfig.DepartmentFilePath) ?? new List<Department>();
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			// Проверка названия оборудования
			Equipment.Name = NameTextBox.Text;
			if (string.IsNullOrWhiteSpace(Equipment.Name))
			{
				MessageBox.Show("Введите название оборудования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Проверка статуса оборудования (в совместном использовании)
			Equipment.IsShared = IsSharedCheckBox.IsChecked ?? false;

			// Проверка статуса оборудования (используется ли)
			Equipment.IsInUse = IsInUseCheckBox.IsChecked ?? false;

			// Проверка выбранного отдела
			Equipment.AssignedDepartment = (DepartmentComboBox.SelectedItem as Department)?.Name ?? string.Empty;
			if (string.IsNullOrWhiteSpace(Equipment.AssignedDepartment) && !Equipment.IsShared)
			{
				MessageBox.Show("Оборудование должно быть привязано к отделу, если оно не является общим.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Закрытие окна с успешным результатом
			DialogResult = true;
		}


		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false; // Закрывает окно без сохранения изменений
		}
	}
}
