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
	/// Логика взаимодействия для WorkDetailEditWindow.xaml
	/// </summary>
	public partial class WorkDetailEditWindow : Window
	{
		public SubcontractedWork SubcontractedWork { get; private set; }
		private readonly JsonDataHandler jsonDataHandler;
		private List<Project> projects;

		public WorkDetailEditWindow(SubcontractedWork work = null)
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadProjects();

			SubcontractedWork = work ?? new SubcontractedWork();

			DescriptionTextBox.Text = SubcontractedWork.Description;
			CostTextBox.Text = SubcontractedWork.Cost.ToString();

			ProjectComboBox.ItemsSource = projects;
			ProjectComboBox.SelectedItem = projects.Find(p => p.ProjectId == SubcontractedWork.ProjectId);
		}

		private void LoadProjects()
		{
			projects = jsonDataHandler.LoadData<List<Project>>(AppConfig.ProjectFilePath) ?? new List<Project>();
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			SubcontractedWork.Description = DescriptionTextBox.Text;
			SubcontractedWork.Cost = decimal.TryParse(CostTextBox.Text, out var cost) ? cost : 0;
			if (ProjectComboBox.SelectedItem is Project selectedProject)
			{
				SubcontractedWork.ProjectId = selectedProject.ProjectId;
			}

			DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
