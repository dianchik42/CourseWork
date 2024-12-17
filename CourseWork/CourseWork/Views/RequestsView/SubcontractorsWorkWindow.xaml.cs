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
	/// Логика взаимодействия для SubcontractorsWorkWindow.xaml
	/// </summary>
	public partial class SubcontractorsWorkWindow : Window
	{
		private readonly JsonDataHandler jsonDataHandler;
		private List<Subcontractor> subcontractors;

		public SubcontractorsWorkWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadData();
		}

		private void LoadData()
		{
			// Загружаем список субподрядчиков
			subcontractors = jsonDataHandler.LoadData<List<Subcontractor>>(AppConfig.SubcontractorFilePath) ?? new List<Subcontractor>();

			// Заполняем ListBox субподрядчиков
			SubcontractorListBox.ItemsSource = subcontractors.Select(s => $"{s.Name} (ID: {s.SubcontractorId})");
		}

		private void OnSubcontractorSelected(object sender, RoutedEventArgs e)
		{
			// Получаем выбранного субподрядчика
			int selectedIndex = SubcontractorListBox.SelectedIndex;
			if (selectedIndex < 0 || selectedIndex >= subcontractors.Count)
			{
				WorkListBox.ItemsSource = null;
				TotalCostTextBlock.Text = string.Empty;
				return;
			}

			var selectedSubcontractor = subcontractors[selectedIndex];

			// Заполняем список работ выбранного субподрядчика
			WorkListBox.ItemsSource = selectedSubcontractor.WorkDetails.Select(w =>
				$"{w.Description} - Стоимость: {w.Cost:C}");

			// Вычисляем общую стоимость работ
			decimal totalCost = selectedSubcontractor.WorkDetails.Sum(w => w.Cost);
			TotalCostTextBlock.Text = $"Общая стоимость работ: {totalCost:C}";
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
