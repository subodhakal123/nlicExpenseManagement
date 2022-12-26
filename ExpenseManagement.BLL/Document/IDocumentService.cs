using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Document
{
    public interface IDocumentService
    {
        public Task<bool> UploadFile(List<IFormFile> file);
        //public File DownloadFile(int id);
    }
}
