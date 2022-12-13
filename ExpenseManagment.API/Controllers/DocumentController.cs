﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public DocumentController(IWebHostEnvironment _environment)
        {
            this._environment = _environment;
        }

        [HttpPost("UploadImage")]
        public async Task<ActionResult> UploadImage()
        {
            bool results = false;
            try
            {
                var _uploadedFiles = Request.Form.Files;
                foreach (IFormFile source in _uploadedFiles)
                {
                    string Filename = source.FileName;
                    string Filepath = GetFilePath(Filename);

                    if (!System.IO.Directory.Exists(Filepath))
                    {
                        System.IO.Directory.CreateDirectory(Filepath);
                    }

                    string Imagepath = Filepath + "\\image.png";

                    if (System.IO.File.Exists(Imagepath))
                    {
                        System.IO.File.Delete(Imagepath);
                    }

                    using(FileStream stream = System.IO.File.Create(Imagepath))
                    {
                        await source.CopyToAsync(stream);
                        results = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(results);
        }

        [NonAction]
        private string GetFilePath(string code)
        {
            return this._environment.WebRootPath + "\\uploads\\expense" + code;
        }
        [NonAction]
        private string GetImageByExpense(string ExpenseId)
        {
            string ImageUrl = string.Empty;
            string HostUrl = "https://localhost:7084/";
            string FilePath = GetFilePath(ExpenseId);
            string ImagePath = FilePath + "\\image.png";
            if (!System.IO.File.Exists(FilePath))
            {
                ImageUrl = HostUrl + "/uploads/common/noimage.png";
            }
            else
            {
                ImageUrl = HostUrl + "/uploads/expense/" + ExpenseId + "/image.png";
            }
            return ImageUrl;
        }
    }
}