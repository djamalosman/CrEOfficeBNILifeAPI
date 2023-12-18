using EOfficeBNILAPI.DataAccess;
using EOfficeBNILAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EOfficeBNILAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LetterController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        GeneralOutputModel output = new GeneralOutputModel();
        SessionUser sessionUser = new SessionUser();
        public LetterController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        [HttpGet(Name = "SetSessionLetter")]
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
                Guid directorIdUnit = new Guid(dict.ElementAt(11).Value);

                sessionUserOutput.idUser = idUser;
                sessionUserOutput.nip = Nip;
                sessionUserOutput.nama = Nama;
                sessionUserOutput.idUnit = idUnit;
                sessionUserOutput.idBranch = idBranch;
                sessionUserOutput.unitCode = unitCode;
                sessionUserOutput.idPosition = idPosition;
                sessionUserOutput.idGroup = idGroup;
                sessionUserOutput.directorIdUnit = directorIdUnit;
            }
            catch (Exception ex)
            {
                return sessionUserOutput;
            }

            return sessionUserOutput;
        }

        [Authorize]
        [Route("InsertAttachment")]
        [HttpPost]
        public ActionResult InsertAttachment([FromForm] ParamInsertAttachment pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertDataAttachmentLetter(pr, sessionUser);

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
        [Route("InsertLetter")]
        [HttpPost]
        public ActionResult InsertLetter([FromForm] ParamInsertLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertLetter(pr, sessionUser);

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
        [Route("GetDraftLetter")]
        [HttpPost]
        public ActionResult GetDraftLetter([FromForm] ParamGetLetterWeb pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetLetterDraft(pr, sessionUser);

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
        [Route("GetInbox")]
        [HttpPost]
        public ActionResult GetInbox([FromForm] ParamGetLetterWeb pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetInboxLetter(pr, sessionUser);

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
        [Route("GetOutbox")]
        [HttpPost]
        public ActionResult GetOutbox([FromForm] ParamGetLetterWeb pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetOutboxLetter(pr, sessionUser);

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
        [Route("DeleteLetter")]
        [HttpPost]
        public ActionResult DeleteLetter([FromForm] ParamDeleteLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.DeleteLetter(pr, sessionUser);

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
        [Route("GetDetailLetterSM")]
        [HttpPost]
        public ActionResult GetDetailLetterSM([FromForm] ParamGetDetailLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailLetterSM(pr, sessionUser);

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
        [Route("DeleteAttachment")]
        [HttpPost]
        public ActionResult DeleteAttachment([FromForm] ParamDeleteAttachment pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.DeleteAttachment(pr, sessionUser);

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
        [Route("PreviewNoDoc")]
        [HttpPost]
        public ActionResult PreviewNoDoc([FromForm] ParamPreviewNoLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.PreviewNoDoc(pr.letterType, sessionUser);

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
        [Route("InsertDisposition")]
        [HttpPost]
        public ActionResult InsertDisposition([FromForm] ParamInsertLetterDisposition pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertDisposition(pr, sessionUser);

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
        [Route("InsertLetterOutbox")]
        [HttpPost]
        public ActionResult InsertLetterOutbox([FromForm] ParamInsertLetterOutbox pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertLetterOutbox(pr, sessionUser);

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
        [Route("GetDetailLetterSK")]
        [HttpPost]
        public ActionResult GetDetailLetterSK([FromForm] ParamGetDetailLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailLetterSK(pr, sessionUser);

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
        [Route("ApprovalLetter")]
        [HttpPost]
        public ActionResult ApprovalLetter([FromForm] ParamApprovalLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.ApprovalLetter(pr, sessionUser);

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
        [Route("DeliveryLetterList")]
        [HttpPost]
        public ActionResult DeliveryLetterList([FromForm] ParamGetSKDelivery pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetSKEskpedisi(pr,sessionUser);

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
        [Route("InsertDelivery")]
        [HttpPost]
        public ActionResult InsertDelivery([FromForm] ParamInsertDelivery pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertDelivery(pr,sessionUser);

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
        [Route("LetterNotifSecretary")]
        [HttpPost]
        public ActionResult LetterNotifSecretary([FromForm] ParamGetDetailLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.LetterNotifSecretary(pr, sessionUser);

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
        [Route("GenerateQrCodeDelivery")]
        [HttpPost]
        public ActionResult GenerateQrCodeDelivery([FromForm] ParamGenerateDeliveryNumber pr)
        {
            try
            {
                sessionUser = SetSession();
                ParamUpdateDelivery prUpdate = new ParamUpdateDelivery();
                var retrn = _dataAccessProvider.GenerateNonDocoutgoingmail(pr.delivType, "01", sessionUser);

                prUpdate.saveType = 2;
                prUpdate.idDelivery = pr.idDeliv;
                prUpdate.deliveryNumber = retrn;

                var updateDelivNumber = _dataAccessProvider.UpdateDeliveryEoffice(prUpdate, sessionUser);

                if (retrn != null)
                {
                    output.Result = retrn;
                    output.Status = "OK";
                    return Ok(output);
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
        [Route("GetDetailDeliveryEoffice")]
        [HttpPost]
        public ActionResult GetDetailDeliveryEoffice([FromForm] ParamGetDetailDelivery pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailDeliveryEoffice(pr, sessionUser);

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
        [Route("UpdateDeliveryEoffice")]
        [HttpPost]
        public ActionResult UpdateDeliveryEoffice([FromForm] ParamUpdateDelivery pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UpdateDeliveryEoffice(pr, sessionUser);

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
        [Route("DeliveryLetterReportList")]
        [HttpPost]
        public ActionResult DeliveryLetterReportList([FromForm] ParamGetReportEkspedisiEoffice pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetReportEkspedisiEoffice(pr, sessionUser);

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
        [Route("UploadExpeditionEoffice")]
        [HttpPost]
        public ActionResult UploadExpeditionEoffice([FromForm] ParamUploadEkspedisiEofficeString pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UploadExpeditionEoffice(pr, sessionUser);

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
        [Route("InsertLetterOutboxBackdate")]
        [HttpPost]
        public ActionResult InsertLetterOutboxBackdate([FromForm] ParamInsertLetterOutboxBackdate pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertLetterOutboxBackdate(pr, sessionUser);

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
        [Route("GetDetailLetterSKBackdate")]
        [HttpPost]
        public ActionResult GetDetailLetterSKBackdate([FromForm] ParamGetDetailLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailLetterSKBackdate(pr, sessionUser);

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
