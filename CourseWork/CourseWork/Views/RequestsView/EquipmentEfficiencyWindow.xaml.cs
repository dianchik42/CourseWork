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
	/// Логика взаимодействия для EquipmentEfficiencyWindow.xaml
	/// </summary>
	public partial class EquipmentEfficiencyWindow : Window
	{
		private readonly JsonDataHandler jsonDataHandler;
		private List<Equipment> equipmentList;
		private List<Project> projects;

		public EquipmentEfficiencyWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadData();
			DisplayEquipmentEfficiency();
		}

		private void LoadData()
		{
			// Загружаем данные об оборудовании и проектах
			equipmentList = jsonDataHandler.LoadData<List<Equipment>>(AppConfig.EquipmentFilePath) ?? new List<Equipment>();
			projects = jsonDataHandler.LoadData<List<Project>>(AppConfig.ProjectFilePath) ?? new List<Project>();
		}

		private void DisplayEquipmentEfficiency()
		{
			EquipmentListBox.Items.Clear();

			foreach (var equipment in equipmentList)
			{
				// Считаем количество проектов, в которых используется оборудование
				int projectsCount = projects.Count(p => p.EquipmentUsed != null && p.EquipmentUsed.Any(ex => ex.EquipmentId == equipment.EquipmentId));

				// Формируем строку с данными об эффективности использования оборудования
				string efficiencyInfo = $"Оборудование: {equipment.Name}\n" +
										$"Задействовано в проектах: {projectsCount}\n";

				EquipmentListBox.Items.Add(efficiencyInfo);
			}
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
