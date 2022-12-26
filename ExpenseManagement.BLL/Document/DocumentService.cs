using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Document
{
    public class DocumentService : IDocumentService
    {
        public async Task<bool> UploadFile(List<IFormFile> files)
        {
            string path = "";
            try
            {
                foreach(IFormFile file in files)
                {
                    if (file.Length > 0)
                    {
                        path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}
