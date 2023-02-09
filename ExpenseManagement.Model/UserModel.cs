using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleId { get; set; }
        public string BrName { get; set; }
        public string BrCode { get; set; }
        public string ErrorMessage { get; set; }
        public string access_token { get; set; }
        public string CultureCD { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }

    }
    public class UserRoleSaveModel
    {
        public string RolesList { get; set; }
        public int UserId { get; set; }
        public string LoginUser { get; set; }
    }
    public class UserRolesGetViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class UserNavAuthSaveViewModel
    {
        public int UserId { get; set; }
        public string MenuAuthorityIdRemList { get; set; }
        public string MenuAuthorityIdAddList { get; set; }
    }
    public class UserRolesNagivationMenu
    {
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
    }
    public class UserLimitAuthorityModulesExistModel
    {
        public bool isUnderwriter { get; set; }
        public bool isMaturityApproval { get; set; }
        public bool isSurvivalApproval { get; set; }
        public bool isLoanApproval { get; set; }
        public bool isSurrenderApproval { get; set; }
    }
    public class UserLimitAuthorityModel
    {
        public int? UserId { get; set; }
        public Int64? UndMedicalLimit { get; set; }
        public Int64? UndNonMedicalLimit { get; set; }
        public Int64? MaturityLimit { get; set; }
        public Int64? LoanLimit { get; set; }
        public Int64? SurrenderLimit { get; set; }
        public Int64? SurvivalLimit { get; set; }
        public int? UserAuthorityTo { get; set; }

    }
}
