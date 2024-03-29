﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.Expense
{
    public class ItemExpenseModel
    {
        public int? ExpenseId { get; set; }
        public string ExpenseTitle { get; set; }
        public string ExpenseBy { get; set; }
        public int? BrCode { get; set; }
        public string? BranchName { get; set; }
        public string? AppliedBy { get; set; }
        public int? Status { get; set; }
        public string? ExpenseDate { get; set; }
        public List<ItemModel>? Item { get; set; }
        public bool? IsRecommended { get; set; }
        public int? DepartmentId { get; set; }
        public string? Recommender { get; set; }
        public float? TotalAmount { get; set; }
        public string Comment { get; set; }
        public int? IsApproved { get; set; }
        public string? ApprovedBy { get; set; }
        public string? ApprovedDate { get; set; }
        public string? ApproverName { get; set; }

    }

    public class ItemExpenseViewModel
    {
        public int ExpenseId { get; set; }
        public string ExpenseTitle { get; set; }
        public string ExpenseBy { get; set; }
        public int BrCode { get; set; }
        public string BranchName { get; set; }
        public string AppliedBy { get; set; }
        public int Status { get; set; }
        public string ExpenseDate { get; set; }
        public List<ItemModel> Item { get; set; }
        public bool IsRecommended { get; set; }
        public int DepartmentId { get; set; }
        public string Recommender { get; set; }
        public float TotalAmount { get; set; }
        public string Comment { get; set; }
        public int IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string ApproverName { get; set; }
        public int IsAuthorisedToApprove { get; set; }

    }
}
