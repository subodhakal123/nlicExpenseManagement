using ExpenseManagement.BLL.Document;
using ExpenseManagement.Model.File;
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

                    s = await _document.UploadFile(files);
                }
                catch (Exception ex)
                {
                    //Log ex
                    s = "File Upload Failed";
                }
            }
            else
            {
                s = "Please Input Bills First";

            }
            
            return s;
        }

        [HttpGet("GetFile")]
        public async Task<List<string>> GetFile(int expenseId)
        {
            List<string> filenames = new List<string>();
            try
            {
                filenames = await this._document.GetFile(expenseId);
            }
            catch(Exception ex)
            {

            }
            return filenames;
        }
    }   
}       
        