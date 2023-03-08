
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
            string[] _allowedFileTypes = { "pdf", "doc", "docx" };
            long _maxFileSize = 2097152; // 2MB in bytes
            try
            {
                List<FileModel> filedata = new List<FileModel>();
                string path = "";

                string expId = ContentDispositionHeaderValue.Parse(files[0].ContentDisposition).Name.Trim('"');
                string dirPath = Path.GetFullPath(Path.Combine("D:\\New folder\\", "fileUploadFolder", expId));
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(dirPath);
                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                foreach (IFormFile file in files)
                {
                    var fileExtension = Path.GetExtension(file.FileName).Substring(1);
                    if (!_allowedFileTypes.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
                    {
                        return "Files only with extensions pdf, doc and docx are accepted!!";
                    }
                    else
                    {
                        FileModel fileModeldata = new FileModel();
                        if(file.Length > _maxFileSize)
                        {
                            return "files above 2mb are not accepted";
                        }
                        else if (file.Length > 0)
                        {
                            try
                            {
                                fileModeldata.ExpenseId = int.Parse(ContentDispositionHeaderValue.Parse(file.ContentDisposition).Name.Trim('"'));
                                fileModeldata.Uri = file.FileName;
                                filedata.Add(fileModeldata);

                                path = Path.Combine(dirPath, file.FileName);
                                using (var fileStream = new FileStream(path, FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                }
                            }
                            catch (Exception ex)
                            {
                                strReturnMsg = "Error: " + ex.Message.ToString();
                            }
                        }
                        else
                        {
                            return "Please Input Bills First";
                        }
                    }
                }

                //save the bill metadata to the database
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
                throw new Exception("File Copy Failed", ex);
            }
            return strReturnMsg;
        }

        public async Task<List<string>> GetFile(int expenseId)
        {
            string filePath = Path.GetFullPath(Path.Combine("D:\\New folder\\", "fileUploadFolder", expenseId+"\\"));
            List<string> filenames = new List<string>();
            if (Directory.Exists(filePath))
            {
                string[] filePaths = Directory.GetFiles(filePath);
                foreach (string eachFilePath in filePaths)
                {
                    filenames.Add(Path.GetFileName(expenseId + "\\" + eachFilePath));
                }
            }
            
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
