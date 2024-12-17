using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
	public class Contract
	{
		public int ContractId { get; set; }
		public string ClientName { get; set; }
		public decimal TotalCost { get; set; } // Рассчитывается на основе проектов
		public List<Project> Projects { get; set; }
		public string Manager { get; set; }
	}
}
