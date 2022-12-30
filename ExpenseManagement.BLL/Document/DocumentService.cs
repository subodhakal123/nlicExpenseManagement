
using Dapper;
using ExpenseManagement.Model.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Reflection;

namespace ExpenseManagement.BLL.Document
{
    public class DocumentService : IDocumentService
    {
        private IDbConnection db;
        public DocumentService(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<string> UploadFile(List<IFormFile> files)
        {
            string strReturnMsg = "";
            try
            {
                List<FileModel> filedata = new List<FileModel>();
                string path = "";
                foreach (IFormFile file in files)
                {
                    FileModel fileModeldata = new FileModel();
                    string expId = "";
                    if (file.Length > 0)
                    {
                        expId = ContentDispositionHeaderValue.Parse(file.ContentDisposition).Name.Trim('"');
                        fileModeldata.ExpenseId = int.Parse(ContentDispositionHeaderValue.Parse(file.ContentDisposition).Name.Trim('"'));
                        fileModeldata.Uri = file.FileName;
                        filedata.Add(fileModeldata);

                        path = Path.GetFullPath(Path.Combine("D:\\New folder\\", "fileUploadFolder", expId));
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        path = Path.Combine(path, file.FileName);
                        if (File.Exists(path))
                        { 
                            File.Delete(path);
                        }
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        return "Please Input Bills First";
                    }
                }

                //save the bill metadata to the database
                try
                {
                    DataTable udtBillDetail = ListToDataTable(filedata);
                    var parameter = new DynamicParameters();
                    parameter.Add("@ExpenseId", filedata[0].ExpenseId);
                    parameter.Add("@udtBillDetail", udtBillDetail.AsTableValuedParameter("[dbo].[udtBillDetail]"));
                    parameter.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                    this.db.Execute("[EXP].[usp_Bills_InsUpd]", parameter, commandType: CommandType.StoredProcedure);
                    strReturnMsg = parameter.Get<string>("retMsg");
                }
                catch (Exception ex)
                {
                    strReturnMsg = "Error: " + ex.Message.ToString();
                }

                return strReturnMsg;

            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

        public async Task<List<string>> GetFile(int expenseId)
        {
            string filePath = Path.GetFullPath(Path.Combine("D:\\New folder\\", "fileUploadFolder", expenseId+"\\"));


            string[] filePaths = Directory.GetFiles(filePath);
            List<string> filenames = new List<string>();

            foreach(string eachFilePath in filePaths)
            {
                filenames.Add(Path.GetFileName(expenseId+ "\\" +eachFilePath));
            }

            //var provider = new FileExtensionContentTypeProvider();
            //List<FileContentResult> files = new List<FileContentResult>();
            //foreach (string eachFilePath in filePaths)
            //{
            //    byte[] fileContent = System.IO.File.ReadAllBytes(eachFilePath);
            //    string fileName = Path.GetFileName(eachFilePath);
            //
            //    if (!provider.TryGetContentType(filePath, out var contentType))
            //    {
            //        contentType = "application/octet-stream";
            //    }
            //    files.Add(File(fileContent, contentType, fileName));
            //}
            //
            //return files;

            return filenames;
        }

        public DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable("dt");

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            if (items != null)
            {
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
            }
            dataTable.AcceptChanges();
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
