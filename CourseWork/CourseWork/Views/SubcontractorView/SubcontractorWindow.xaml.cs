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

namespace CourseWork.Views.SubcontractorView
{
	/// <summary>
	/// Логика взаимодействия для SubcontractorWindow.xaml
	/// </summary>
	public partial class SubcontractorWindow : Window
	{
		private List<Subcontractor> subcontractors;
		private readonly JsonDataHandler jsonDataHandler;

		public SubcontractorWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadSubcontractors();
		}

		private void LoadSubcontractors()
		{
			subcontractors = jsonDataHandler.LoadData<List<Subcontractor>>(AppConfig.SubcontractorFilePath) ?? new List<Subcontractor>();
			SubcontractorDataGrid.ItemsSource = subcontractors;
		}

		private void AddSubcontractor(object sender, RoutedEventArgs e)
		{
			SubcontractorEditWindow editWindow = new SubcontractorEditWindow();
			if (editWindow.ShowDialog() == true)
			{
				Subcontractor newSubcontractor = editWindow.Subcontractor;
				newSubcontractor.SubcontractorId = subcontractors.Count + 1;
				subcontractors.Add(newSubcontractor);
				SubcontractorDataGrid.Items.Refresh();
			}
		}

		private void EditSubcontractor(object sender, RoutedEventArgs e)
		{
			if (SubcontractorDataGrid.SelectedItem is Subcontractor selectedSubcontractor)
			{
				SubcontractorEditWindow editWindow = new SubcontractorEditWindow(selectedSubcontractor);
				if (editWindow.ShowDialog() == true)
				{
					SubcontractorDataGrid.Items.Refresh();
				}
			}
		}

		private void DeleteSubcontractor(object sender, RoutedEventArgs e)
		{
			if (SubcontractorDataGrid.SelectedItem is Subcontractor selectedSubcontractor)
			{
				subcontractors.Remove(selectedSubcontractor);
				SubcontractorDataGrid.Items.Refresh();
			}
		}

		private void SaveSubcontractors(object sender, RoutedEventArgs e)
		{
			jsonDataHandler.SaveData(subcontractors, AppConfig.SubcontractorFilePath);
			MessageBox.Show("Данные о подрядчиках сохранены.");
		}
	}
}
