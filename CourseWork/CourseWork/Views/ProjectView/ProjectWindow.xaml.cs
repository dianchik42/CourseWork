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

namespace CourseWork.Views.ProjectView
{
	/// <summary>
	/// Логика взаимодействия для ProjectWindow.xaml
	/// </summary>
	public partial class ProjectWindow : Window
	{
		private List<Project> projects;
		private readonly JsonDataHandler jsonDataHandler;

		public ProjectWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadProjects();
		}

		private void LoadProjects()
		{
			projects = jsonDataHandler.LoadData<List<Project>>(AppConfig.ProjectFilePath) ?? new List<Project>();
			ProjectDataGrid.ItemsSource = projects;
		}

		private void AddProject(object sender, RoutedEventArgs e)
		{
			ProjectEditWindow editWindow = new ProjectEditWindow();
			if (editWindow.ShowDialog() == true)
			{
				Project newProject = editWindow.Project;
				newProject.ProjectId = projects.Count + 1;
				projects.Add(newProject);
				ProjectDataGrid.Items.Refresh();
			}
		}

		private void EditProject(object sender, RoutedEventArgs e)
		{
			if (ProjectDataGrid.SelectedItem is Project selectedProject)
			{
				ProjectEditWindow editWindow = new ProjectEditWindow(selectedProject);
				if (editWindow.ShowDialog() == true)
				{
					ProjectDataGrid.Items.Refresh();
				}
			}
		}

		private void DeleteProject(object sender, RoutedEventArgs e)
		{
			if (ProjectDataGrid.SelectedItem is Project selectedProject)
			{
				projects.Remove(selectedProject);
				ProjectDataGrid.Items.Refresh();
			}
		}

		private void SaveProjects(object sender, RoutedEventArgs e)
		{
			jsonDataHandler.SaveData(projects, AppConfig.ProjectFilePath);
			MessageBox.Show("Данные о проектах сохранены.");
		}
	}
}
