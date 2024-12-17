using CourseWork.Models;
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
	/// Логика взаимодействия для AddEquipmentWindow.xaml
	/// </summary>
	public partial class AddEquipmentWindow : Window
	{
		public Equipment SelectedEquipment { get; private set; }
		private readonly List<Equipment> allEquipment;
		private List<string> departments;

		public AddEquipmentWindow(List<Equipment> equipment)
		{
			InitializeComponent();
			allEquipment = equipment;
			LoadDepartments();
		}

		private void LoadDepartments()
		{
			// Получаем уникальные отделы из списка оборудования
			departments = allEquipment.Select(e => e.AssignedDepartment).Distinct().ToList();
			DepartmentComboBox.ItemsSource = departments;
		}

		private void DepartmentComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			// Фильтруем оборудование по выбранному отделу
			if (DepartmentComboBox.SelectedItem is string selectedDepartment)
			{
				EquipmentListBox.ItemsSource = allEquipment.Where(eq => eq.AssignedDepartment == selectedDepartment).ToList();
			}
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			SelectedEquipment = EquipmentListBox.SelectedItem as Equipment;
			DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
