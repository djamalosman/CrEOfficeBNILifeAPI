using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EOfficeBNILAPI.DataAccess;
using EOfficeBNILAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Text;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;
using EOfficeBNILAPI.Models.Table;

namespace EOfficeBNILAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonEofficeLettersController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        GeneralOutputModel output = new GeneralOutputModel();
        SessionUser sessionUser = new SessionUser();
        public NonEofficeLettersController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet(Name = "SetSessionAllNonEoffice")]
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
        [Route("GetDataNonEofficeLetter")]
        [HttpPost]
        public ActionResult GetSearchDocument([FromForm] ParamReportNonOuboxLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetSearchReportDocumentNonEoffice(pr, sessionUser);

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
        [Route("GetDataNonEofficeLetterByUser")]
        [HttpPost]
        public ActionResult GetSearchDocumentByUser([FromForm] ParamReportNonOuboxLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetSearchReportDocumentNonEofficeByUser(pr, sessionUser);

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
        [Route("GetDataExpotUsingUpdateNonEofficeEkspedisi")]
        [HttpPost]
        public ActionResult GetDataExpotUsingUpdateNonEofficeEkspedisi([FromForm] ParamReportNonOuboxLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.ExportUpdateNonEofficeEkspedisi_(pr, sessionUser);

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
        [Route("GetDataSuratKeluarNonEoffice")]
        [HttpGet]
        public ActionResult DataSuratKeluarNon()
        {
            try
            {
                GeneralOutputModel retrn = _dataAccessProvider.GetDataSuratKeluarNon();

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
        [Route("GetDataKurirNonEofficeLetter")]
        [HttpPost]
        public ActionResult GetSearchKurirDocument([FromForm] ParamReportNonOuboxLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetSearchKurirReportDocumentNonEoffice(pr, sessionUser);

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

        #region Detail View Delivery Dokumen Non-Eoffice
        [Authorize]
        [Route("GetDetailsViewEkspedisi")]
        [HttpPost]
        public ActionResult GetDetailsViewEkspedisi([FromForm] ParamGetDetailsView pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailsViewEkspedisi_(sessionUser, pr);

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
        [Route("GetDetailsViewKurir")]
        [HttpPost]
        public ActionResult GetDetailsViewKurir([FromForm] ParamGetDetailsView pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailsViewKurir_(sessionUser, pr);

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
        [Route("SearchSuratKeluarKurirNonEoffice")]
        [HttpPost]
        public ActionResult SearchSuratKeluarKurirNonEoffice([FromForm] ParamReportNonOuboxLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.SearchSuratKeluarKurirNonEoffice(pr, sessionUser);

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
        [Route("SearchSuratKeluarEkspedisiNonEoffice")]
        [HttpPost]
        public ActionResult SearchSuratKeluarEkspedisiNonEoffice([FromForm] ParamReportNonOuboxLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.SearchSuratKeluarEkspedisiNonEoffice(pr, sessionUser);

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

        #endregion
    }
}
