using ExpenseManagement.BLL.Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ExpenseManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _document;
        public DocumentController(IDocumentService _document)
        {
            this._document = _document;
        }


        //private readonly IWebHostEnvironment _environment;
        //public DocumentController(IWebHostEnvironment _environment)
        //{
        //    this._environment = _environment;
        //}
        //
        //[HttpPost("UploadImage")]
        //public async Task<ActionResult> UploadImage()
        //{
        //    bool results = false;
        //    try
        //    {
        //        var _uploadedFiles = Request.Form.Files;
        //        foreach (IFormFile source in _uploadedFiles)
        //        {
        //            string Filename = source.FileName;
        //            string Filepath = GetFilePath(Filename);
        //
        //            if (!System.IO.Directory.Exists(Filepath))
        //            {
        //                System.IO.Directory.CreateDirectory(Filepath);
        //            }
        //
        //            string Imagepath = Filepath + "\\image.png";
        //
        //            if (System.IO.File.Exists(Imagepath))
        //            {
        //                System.IO.File.Delete(Imagepath);
        //            }
        //
        //            using(FileStream stream = System.IO.File.Create(Imagepath))
        //            {
        //                await source.CopyToAsync(stream);
        //                results = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //
        //    }
        //    return Ok(results);
        //}
        //
        //[NonAction]
        //private string GetFilePath(string code)
        //{
        //    return this._environment.WebRootPath + "\\uploads\\expense" + code;
        //}
        //[NonAction]
        //private string GetImageByExpense(string ExpenseId)
        //{
        //    string ImageUrl = string.Empty;
        //    string HostUrl = "https://localhost:7084/";
        //    string FilePath = GetFilePath(ExpenseId);
        //    string ImagePath = FilePath + "\\image.png";
        //    if (!System.IO.File.Exists(FilePath))
        //    {
        //        ImageUrl = HostUrl + "/uploads/common/noimage.png";
        //    }
        //    else
        //    {
        //        ImageUrl = HostUrl + "/uploads/expense/" + ExpenseId + "/image.png";
        //    }
        //    return ImageUrl;
        //}
        //
        //[HttpPost]
        //public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        //{
        //
        //    return Ok(Ok(files));
        [HttpPost("Upload")]
        public async Task<string> Upload()
        {
            string s;
            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    List<IFormFile> files = Request.Form.Files.ToList();

                    long size = files.Sum(f => f.Length);

                    if (await _document.UploadFile(files))
                    {
                        s = "File Upload Successful";
                    }
                    else
                    {
                        s = "File Upload Failed";
                    }
                }
                catch (Exception ex)
                {
                    //Log ex
                    s = "File Upload Failed";
                }
            }
            else
            {
                s = "File Upload Failed";

            }
            
            return s;
        }

        [HttpGet("GetFile")]
        public List<FileContentResult> GetFile()
        {
            string filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));


            string[] filePaths = Directory.GetFiles(filePath);
            var provider = new FileExtensionContentTypeProvider();
            List<FileContentResult> files = new List<FileContentResult>();
            foreach (string eachFilePath in filePaths)
            {
                byte[] fileContent = System.IO.File.ReadAllBytes(eachFilePath);
                string fileName = Path.GetFileName(eachFilePath);

                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                files.Add(File(fileContent, contentType, fileName));
            }

            return files;
        }
    }   
}       
        