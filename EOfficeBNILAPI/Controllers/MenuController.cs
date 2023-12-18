using EOfficeBNILAPI.DataAccess;
using EOfficeBNILAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EOfficeBNILAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        GeneralOutputModel output = new GeneralOutputModel();
        public MenuController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [Authorize]
        [HttpGet("{idGroup}")]
        public ActionResult GetMenu(string idGroup)
        {
            try
            {
                GeneralOutputModel retrn = _dataAccessProvider.GetDataMenu(idGroup);
                return Ok(retrn);
            }
            catch (Exception ex)
            {
                output.Status = "NG";
                output.Message = ex.ToString();

                return BadRequest(output);
            }
        }

        [Authorize]
        [Route("GetMenuMobile")]
        [HttpGet]
        public ActionResult GetMenuMobile()
        {
            try
            {
                GeneralOutputModel retrn = _dataAccessProvider.GetDataMenuMobile();

                return Ok(retrn);

            }
            catch (Exception ex)
            {
                output.Status = "NG";
                output.Message = ex.ToString();

                return BadRequest(output);
            }
        }
    }
}
