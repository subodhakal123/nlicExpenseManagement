using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model
{
    public class signIn
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UserDto
    {
        public signIn login { get; set; }
        public UserViewModel register { get; set; }
        public string access_token { get; set; }
    }

    public class UserViewModel
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string StaffId { get; set; }
        public int DepartmentId { get; set; }
        public int AuthorityToId { get; set; }
        public string Gender { get; set; }
        public string JoinDate { get; set; }
        public string JoinDateNp { get; set; }
        public string JoinDateEn { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
        public string Photo { get; set; }
        public string access_token { get; set; }
        public string IsLocked { get; set; }
        public string IsBlocked { get; set; }
        public string IsHead { get; set; }
        public string IsOnForcedLeave { get; set; }
        public string IsSupervisor { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public List<RolesModel> userRole { get; set; }
        public string LoginUserName { get; set; }
        public string BrCode { get; set; }
        public int PostId { get; set; }
        public string PostName { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string FullImagePath { get; set; }

        public Int64? UndMedicalLimit { get; set; }
        public Int64? UndNonMedicalLimit { get; set; }
        public Int64? MaturityLimit { get; set; }
        public Int64? LoanLimit { get; set; }
        public Int64? SurrenderLimit { get; set; }
        public Int64? SurvivalLimit { get; set; }
        public object EncryptSymmetric { get; private set; }
    }
}
