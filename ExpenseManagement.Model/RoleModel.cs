namespace ExpenseManagement.Model
{
	public class RoleModel
	{
		public int RoleId { get; set; }
		public string RoleName { get; set; }
		public bool isActive { get; set; }
		public int? userRoleId { get; set; }
		public int? userId { get; set; }
	}
	public class RolePageSaveModel
	{
		public string PageList { get; set; }
		public int RoleId { get; set; }
		public string LoginUserName { get; set; }
	}
}
