using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
	public class Equipment
	{
		public int EquipmentId { get; set; }
		public string Name { get; set; }
		public string AssignedDepartment { get; set; }
		public bool IsShared { get; set; } // Совместное использование
		public bool IsInUse { get; set; }
	}
}
