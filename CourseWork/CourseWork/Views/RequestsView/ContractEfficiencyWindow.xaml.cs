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
	/// Логика взаимодействия для ContractEfficiencyWindow.xaml
	/// </summary>
	public partial class ContractEfficiencyWindow : Window
	{
		private readonly JsonDataHandler jsonDataHandler;
		private List<Contract> contracts;

		public ContractEfficiencyWindow()
		{
			InitializeComponent();
			jsonDataHandler = new JsonDataHandler();
			LoadData();
			DisplayContractEfficiency();
		}

		private void LoadData()
		{
			// Загружаем список договоров
			contracts = jsonDataHandler.LoadData<List<Contract>>(AppConfig.ContractFilePath) ?? new List<Contract>();
		}

		private void DisplayContractEfficiency()
		{
			DateTime currentDate = DateTime.Now;

			ContractsListBox.Items.Clear();

			foreach (var contract in contracts)
			{
				// Определяем общий прогресс выполнения по проектам
				var completedProjects = contract.Projects.Where(p => p.EndDate <= currentDate);
				var completedCost = completedProjects.Sum(p => p.Cost);
				var remainingCost = contract.TotalCost - completedCost;

				// Считаем прогресс в процентах
				decimal completionPercentage = (contract.TotalCost > 0) ? (completedCost / contract.TotalCost) * 100 : 0;

				// Находим оставшиеся дни до окончания самого позднего проекта
				// Проверяем наличие проекта с датой окончания и берем максимальную дату
				var latestEndDate = contract.Projects
					.Where(p => p.EndDate.HasValue) 
					.Max(p => p.EndDate.Value);    
				int daysLeft = (latestEndDate > currentDate) ? (latestEndDate - currentDate).Days : 0;

				// Формируем строку для отображения
				string efficiencyInfo = $"Договор: {contract.ClientName}\n" +
										$"Завершено: {completionPercentage:F2}%\n" +
										$"Дней до завершения: {daysLeft}\n" +
										$"Заработано: {completedCost:C}\n" +
										$"Осталось заработать: {remainingCost:C}\n";

				ContractsListBox.Items.Add(efficiencyInfo);
			}
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
