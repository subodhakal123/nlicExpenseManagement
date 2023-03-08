using ExpenseManagement.BLL.Document;
using ExpenseManagement.Model.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ExpenseManagment.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _document;
        public DocumentController(IDocumentService _document)
        {
            this._document = _document;
        }

        [HttpPost("Upload")]
        public async Task<string> Upload()
        {
            string s = "";
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
        