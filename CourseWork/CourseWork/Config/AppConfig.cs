using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Config
{
	public static class AppConfig
	{
		private static readonly string BasePath = Directory.GetCurrentDirectory();
		public static string EmployeeFilePath => Path.Combine(BasePath, "Data", "employees.json");
		public static string DepartmentFilePath => Path.Combine(BasePath, "Data", "departments.json");
		public static string ProjectFilePath => Path.Combine(BasePath, "Data", "projects.json");
		public static string ContractFilePath => Path.Combine(BasePath, "Data", "contracts.json");
		public static string EquipmentFilePath => Path.Combine(BasePath, "Data", "equipment.json");
		public static string SubcontractorFilePath => Path.Combine(BasePath, "Data", "subcontractors.json");
	}
}
