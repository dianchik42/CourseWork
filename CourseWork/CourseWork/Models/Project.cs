using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
	public class Project
	{
		public int ProjectId { get; set; }
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public decimal Cost { get; set; }
		public string Manager { get; set; }
		public List<Employee> Team { get; set; }
		public List<Equipment> EquipmentUsed { get; set; }
	}
}
