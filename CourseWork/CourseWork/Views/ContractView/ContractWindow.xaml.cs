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

namespace CourseWork.Views.ContractView
{
	/// <summary>
	/// Логика взаимодействия для ContractWindow.xaml
	/// </summary>
	public partial class ContractWindow : Window
	{
		private List<Contract> contracts;
		private readonly JsonDataHandler jsonDataHandler;

		public ContractWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadContracts();
		}

		private void LoadContracts()
		{
			contracts = jsonDataHandler.LoadData<List<Contract>>(AppConfig.ContractFilePath) ?? new List<Contract>();
			ContractDataGrid.ItemsSource = contracts;
		}

		private void AddContract(object sender, RoutedEventArgs e)
		{
			ContractEditWindow editWindow = new ContractEditWindow();
			if (editWindow.ShowDialog() == true)
			{
				Contract newContract = editWindow.Contract;
				newContract.ContractId = contracts.Count + 1;
				contracts.Add(newContract);
				ContractDataGrid.Items.Refresh();
			}
		}

		private void EditContract(object sender, RoutedEventArgs e)
		{
			if (ContractDataGrid.SelectedItem is Contract selectedContract)
			{
				ContractEditWindow editWindow = new ContractEditWindow(selectedContract);
				if (editWindow.ShowDialog() == true)
				{
					ContractDataGrid.Items.Refresh();
				}
			}
		}

		private void DeleteContract(object sender, RoutedEventArgs e)
		{
			if (ContractDataGrid.SelectedItem is Contract selectedContract)
			{
				contracts.Remove(selectedContract);
				ContractDataGrid.Items.Refresh();
			}
		}

		private void SaveContracts(object sender, RoutedEventArgs e)
		{
			jsonDataHandler.SaveData(contracts, AppConfig.ContractFilePath);
			MessageBox.Show("Данные о контрактах сохранены.");
		}
	}
}
