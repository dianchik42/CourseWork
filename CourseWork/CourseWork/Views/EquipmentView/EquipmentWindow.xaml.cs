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
	/// Логика взаимодействия для EquipmentWindow.xaml
	/// </summary>
	public partial class EquipmentWindow : Window
	{
		private List<Equipment> equipmentList;
		private readonly JsonDataHandler jsonDataHandler;

		public EquipmentWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadEquipment();
		}

		private void LoadEquipment()
		{
			equipmentList = jsonDataHandler.LoadData<List<Equipment>>(AppConfig.EquipmentFilePath) ?? new List<Equipment>();
			EquipmentDataGrid.ItemsSource = equipmentList;
		}

		private void AddEquipment(object sender, RoutedEventArgs e)
		{
			EquipmentEditWindow editWindow = new EquipmentEditWindow();
			if (editWindow.ShowDialog() == true)
			{
				Equipment newEquipment = editWindow.Equipment;
				newEquipment.EquipmentId = equipmentList.Count + 1;
				equipmentList.Add(newEquipment);
				EquipmentDataGrid.Items.Refresh();
			}
		}

		private void EditEquipment(object sender, RoutedEventArgs e)
		{
			if (EquipmentDataGrid.SelectedItem is Equipment selectedEquipment)
			{
				EquipmentEditWindow editWindow = new EquipmentEditWindow(selectedEquipment);
				if (editWindow.ShowDialog() == true)
				{
					EquipmentDataGrid.Items.Refresh();
				}
			}
		}

		private void DeleteEquipment(object sender, RoutedEventArgs e)
		{
			if (EquipmentDataGrid.SelectedItem is Equipment selectedEquipment)
			{
				equipmentList.Remove(selectedEquipment);
				EquipmentDataGrid.Items.Refresh();
			}
		}

		private void SaveEquipment(object sender, RoutedEventArgs e)
		{
			jsonDataHandler.SaveData(equipmentList, AppConfig.EquipmentFilePath);
			MessageBox.Show("Данные об оборудовании сохранены.");
		}
	}
}
