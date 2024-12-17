using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
	public class Subcontractor
	{
		public int SubcontractorId { get; set; }
		public string Name { get; set; }
		public List<SubcontractedWork> WorkDetails { get; set; }
	}

	public class SubcontractedWork
	{
		public int ProjectId { get; set; }
		public decimal Cost { get; set; }
		public string Description { get; set; }
	}

}
