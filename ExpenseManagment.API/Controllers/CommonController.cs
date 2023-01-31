using ExpenseManagement.BLL.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ExpenseManagement.Model.Common.DropDownModel;

namespace ExpenseManagment.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDropDownService _dd;
        public CommonController(IConfiguration configuration, IDropDownService dropDownService)
        {
            _configuration = configuration;
            _dd = dropDownService;
        }

        [HttpPost("GetDropDownList")]
        public List<DropDown> GetDropDownList(DropDownCallParameter callParameter)
        {
            List<DropDown> dropDownsList = new List<DropDown>();
            dropDownsList = _dd.GetDropDowns(callParameter);
            return dropDownsList;
        }
    }
}
