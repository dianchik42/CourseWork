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
	/// Логика взаимодействия для DepartmentHeadsWindow.xaml
	/// </summary>
	public partial class DepartmentHeadsWindow : Window
	{
		private readonly JsonDataHandler jsonDataHandler;
		private List<Employee> employees;

		public DepartmentHeadsWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadDepartmentHeads();
		}

		private void LoadDepartmentHeads()
		{
			// Загружаем список сотрудников
			employees = jsonDataHandler.LoadData<List<Employee>>(AppConfig.EmployeeFilePath) ?? new List<Employee>();

			// Фильтруем список сотрудников, чтобы получить только руководителей отделов
			var departmentHeads = employees.Where(emp => emp.Role == "Руководитель отдела");

			// Отображаем список руководителей в ListBox
			HeadsListBox.ItemsSource = departmentHeads.Select(emp => $"{emp.Name} - {emp.Department}");
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
