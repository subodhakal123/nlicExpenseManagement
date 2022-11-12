using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model
{
	public class RolesModel
	{
		public int RoleId { get; set; }
		public string RoleCode { get; set; }
		public string RoleName { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public int DisplayOrder { get; set; }
		public string UserName { get; set; }
		public string Modules { get; set; }
	}
}
