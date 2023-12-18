using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EOfficeBNILAPI.DataAccess;
using EOfficeBNILAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EOfficeBNILAPI.Controllers
{
    [Route("api/[controller]")]
    public class TemplateController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        GeneralOutputModel output = new GeneralOutputModel();
        SessionUser sessionUser = new SessionUser();
        public TemplateController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet(Name = "SetSessionTemplate")]
        public SessionUser SetSession()
        {
            SessionUser sessionUserOutput = new SessionUser();
            try
            {
                var dict = new Dictionary<string, string>();
                HttpContext.User.Claims.ToList()
                   .ForEach(item => dict.Add(item.Type, item.Value));

                Guid idUser = new Guid(dict.ElementAt(2).Value);
                string Nip = dict.ElementAt(3).Value;
                string Nama = dict.ElementAt(4).Value;
                Guid idUnit = new Guid(dict.ElementAt(5).Value);
                Guid idBranch = new Guid(dict.ElementAt(6).Value);
                string unitCode = dict.ElementAt(7).Value;
                Guid idPosition = new Guid(dict.ElementAt(8).Value);
                string idGroup = dict.ElementAt(9).Value;

                sessionUserOutput.idUser = idUser;
                sessionUserOutput.nip = Nip;
                sessionUserOutput.nama = Nama;
                sessionUserOutput.idUnit = idUnit;
                sessionUserOutput.idBranch = idBranch;
                sessionUserOutput.unitCode = unitCode;
                sessionUserOutput.idPosition = idPosition;
                sessionUserOutput.idGroup = idGroup;
            }
            catch (Exception ex)
            {
                return sessionUserOutput;
            }

            return sessionUserOutput;
        }

        [Authorize]
        [Route("GetTemplate")]
        [HttpGet]
        public ActionResult GetTemplate()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetContentTemplate(sessionUser);

                if (retrn.Status == "OK")
                {
                    return Ok(retrn);
                }
                return BadRequest(retrn);
            }
            catch (Exception ex)
            {
                output.Status = "NG";
                output.Message = ex.ToString();

                return BadRequest(output);
            }
        }

        [Authorize]
        [Route("InsertTemplate")]
        [HttpPost]
        public ActionResult InsertTemplate([FromForm] ParamInsertContentTemplate pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.AddContentTemplate(pr,sessionUser);

                if (retrn.Status == "OK")
                {
                    return Ok(retrn);
                }
                return BadRequest(retrn);
            }
            catch (Exception ex)
            {
                output.Status = "NG";
                output.Message = ex.ToString();

                return BadRequest(output);
            }
        }

        [Authorize]
        [Route("UpdateTemplate")]
        [HttpPost]
        public ActionResult UpdateTemplate([FromForm] ParamUpdateContentTemplate pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.UpdateContentTemplate(pr, sessionUser);

                if (retrn.Status == "OK")
                {
                    return Ok(retrn);
                }
                return BadRequest(retrn);
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

