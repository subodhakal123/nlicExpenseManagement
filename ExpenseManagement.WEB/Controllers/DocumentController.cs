using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Web.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            var _uploadedFiles = Request.Form.Files;
            return View();
        }

        public async Task<ActionResult> DownloadFileById(int id)
        {
            try
            {
                string fileContent;
                string directoryPath = @"D:\New folder\fileUploadFolder\" + id;
                string targetDirectoryPath = @"D:\New folder\fileDownloadFolder\" + id;
                DirectoryInfo di = new DirectoryInfo(directoryPath);

                if (!Directory.Exists(targetDirectoryPath))
                {
                    try
                    {
                        Directory.CreateDirectory(targetDirectoryPath);
                    }
                    catch (Exception ex)
                    {
                        //error creating directory
                    }

                }
                else
                {
                    //no need to create directory
                }

                foreach (FileInfo f1 in di.GetFiles())
                {
                    var a = f1.Name;
                    targetDirectoryPath = targetDirectoryPath + @"\" + a;
                    FileInfo f2 = new FileInfo(targetDirectoryPath);

                    FileStream fs = f1.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                    FileStream fsToWrite = f2.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                    //Create object of StreamReader by passing FileStream object on which it needs to operates on
                    StreamReader sr = new StreamReader(fs);

                    //Use ReadToEnd method to read all the content from file
                    fileContent = sr.ReadToEnd();

                    //Close StreamReader object after operation
                    sr.Close();
                    fs.Close();

                    StreamWriter sw = new StreamWriter(fsToWrite);
                    sw.Write(fileContent);
                    sw.Close();

                }

                return Json("files downloaded to download folder");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public FileContentResult GetFile()
        {
            string filePath = "D:\\New folder\fileDownloadFolder\0\\plan91_1657517883.xlsx";
            byte[] fileContents = System.IO.File.ReadAllBytes(filePath);
            string fileName = "plan91_1657517883.xlsx";
            string contentType = "text/plain";

            return File(fileContents, contentType, fileName);

        }
        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
