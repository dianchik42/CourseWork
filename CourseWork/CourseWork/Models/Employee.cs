using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
	
	public class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Department { get; set; }
		public string Role { get; set; }
		public int Age { get; set; }
		public int? AuthorshipCount { get; set; } // Для конструкторов
		public List<string> Equipment { get; set; } // Для техников
		public bool IsProjectManager { get; set; }
		public bool IsContractManager { get; set; }
	}
}
