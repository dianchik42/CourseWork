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

namespace CourseWork.Views.SubcontractorView
{
	/// <summary>
	/// Логика взаимодействия для SubcontractorEditWindow.xaml
	/// </summary>
	public partial class SubcontractorEditWindow : Window
	{
		public Subcontractor Subcontractor { get; private set; }

		public SubcontractorEditWindow(Subcontractor subcontractor = null)
		{
			InitializeComponent();
			Subcontractor = subcontractor ?? new Subcontractor { WorkDetails = new List<SubcontractedWork>() };

			NameTextBox.Text = Subcontractor.Name;
			WorkDetailsListBox.ItemsSource = Subcontractor.WorkDetails;
		}

		private void AddWorkDetail_Click(object sender, RoutedEventArgs e)
		{
			var workDetailWindow = new WorkDetailEditWindow();
			if (workDetailWindow.ShowDialog() == true)
			{
				Subcontractor.WorkDetails.Add(workDetailWindow.SubcontractedWork);
				WorkDetailsListBox.Items.Refresh();
			}
		}

		private void RemoveWorkDetail_Click(object sender, RoutedEventArgs e)
		{
			if (WorkDetailsListBox.SelectedItem is SubcontractedWork selectedWork)
			{
				Subcontractor.WorkDetails.Remove(selectedWork);
				WorkDetailsListBox.Items.Refresh();
			}
			else
			{
				MessageBox.Show("Пожалуйста, выберите работу для удаления.");
			}
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			Subcontractor.Name = NameTextBox.Text;
			DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
