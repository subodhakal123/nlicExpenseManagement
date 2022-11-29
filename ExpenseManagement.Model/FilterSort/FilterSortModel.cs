using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model
{
    public class FilterSortModel
    {
        public FilterSortModel()
        {
            Filter = new FilterModel();
            sort = new List<SortModel>();
        }

        public FilterModel Filter { get; set; }
        public int take { get; set; }
        public int skip { get; set; }
        public List<SortModel> sort { get; set; }
        public int AdvanceId { get; set; }
        public string? BudgetYear { get; set; }
        public string? GovtTypeCd { get; set; }
        public int Sno { get; set; }
        public int Id { get; set; }
        public string? flag { get; set; }
        public int NavigationMenuId { get; set; }
        public int RoleId { get; set; }
        public string? CultureCode { get; set; }
        public int UserId { get; set; }
        public int ModuleId { get; set; }
        public string? POCd { get; set; }
        public string? SOURCE_FLAG { get; set; }
        public string? TransactionType { get; set; }
        public int BudgetId { get; set; }
        public int BudgetDetailId { get; set; }
        public int MainProjectId { get; set; }
        public string? Type { get; set; }
        public int OfficeId { get; set; }
        public bool IsDeleted { get; set; }
        public string? SearchTxt { get; set; }
        public string? OfficeCd { get; set; }
        public string? NameNep { get; set; }
        public string? ClaimStatus { get; set; }
        public string? ProposalStatus { get; set; }
        public string? EndorsementStatus { get; set; }
        public string? PolicyStatus { get; set; }
        public string? CustomCase { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? PolicyNo { get; set; }
        public string? Comments { get; set; }

    }
}
