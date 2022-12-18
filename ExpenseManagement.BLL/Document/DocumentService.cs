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
        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            try
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

        //public File DownloadFile(int id)
        //{
            //var filePath = $"{id}.txt"; // Here, you should validate the request and the existance of the file.
            //var filePath = "D:\New folder\fileDownloadFolder";
            //var bytes = await File.ReadAllBytesAsync(filePath);
            //var abc = new File(bytes,"text/plain",Path.GetFileName(filePath));
            // return File(bytes, "text/plain", Path.GetFileName(filePath));

            //FileStream fs =  File.Open(filePath,FileMode.Open, FileAccess.Write,FileShare.None);
            //
            //fs.Read(bytes);
            //FileStream abc = new File.Read();


        //    return File()
        //}
    }
}
