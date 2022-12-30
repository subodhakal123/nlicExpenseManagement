using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.File
{
    public class FileModel
    {
        public int ExpenseId { get; set; } = 0;
        public string Uri { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
