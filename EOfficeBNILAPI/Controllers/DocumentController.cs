using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EOfficeBNILAPI.DataAccess;
using EOfficeBNILAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace EOfficeBNILAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider; 
        GeneralOutputModel output = new GeneralOutputModel();
        SessionUser sessionUser = new SessionUser();
        public DocumentController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet(Name = "SetSession")]
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
        [Route("GetDocumentMobile")]
        [HttpPost]
        public ActionResult GetDocument([FromForm] ParamGetDocument pr)
        {
            try
            {
                GeneralOutputModel retrn = _dataAccessProvider.GetDataDocument(pr.pageNumber, pr.idUnit, pr.idBranch);

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
        [Route("GetDocument")]
        [HttpPost]
        public ActionResult GetDocumentWeb([FromForm] ParamGetDocumentWeb pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDataDocumentWeb(sessionUser,pr);

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
        [Route("GetDocumentByTrackingNumber")]
        [HttpPost]
        public ActionResult GetDocumentByTrackingNumber([FromForm] ParamCheckTrackingNumber pr)
        {
            try
            {
                GeneralOutputModel retrn = _dataAccessProvider.GetDocumentByTrackingNumber(pr.trackingNumber);

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
        [Route("InsertDocument")]
        [HttpPost]
        public ActionResult InsertDocument([FromForm] ParamInsertDocument pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertDataDocument(pr, sessionUser);

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
        [Route("GetDetailDocument")]
        [HttpPost]
        public ActionResult GetDocumentById([FromForm] ParamGetDetailDocument pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailDocument(pr,sessionUser);

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
        [Route("GetDetailDocumentDistribution")]
        [HttpPost]
        public ActionResult GetDetailDocumentDistribution([FromForm] ParamGetDetailDocumentReceiver pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailDocumentDistribution(pr, sessionUser);

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
        [Route("UpdateDocument")]
        [HttpPost]
        public ActionResult UpdateDocument([FromForm] ParamUpdateDocument pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UpdateDataDocument(pr, sessionUser);

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
        [Route("ReceiveDocument")]
        [HttpPost]
        public ActionResult ReceiveDocument([FromForm] ParamReceiveDocument pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.ReceiveDocument(pr, sessionUser);

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

        [Route("BarcodeMallingRoom")]
        [HttpPost]
        public ActionResult BarcodeMallingRoom([FromForm] ParamInsertGenerateNoDoc pr)
        {
            try
            {
               
                sessionUser = SetSession();
                GeneralOutputModel output = new GeneralOutputModel();
                var retrn = _dataAccessProvider.GenerateNoDoc(pr.type,pr.userCode,sessionUser);

                var insrtBrcd= _dataAccessProvider.InsrtBrcd(retrn,pr.type ,sessionUser);

                if (retrn !=null)
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
        [Route("ValidasiBarcodeMallingRoom")]
        [HttpPost]
        public ActionResult BarcodeValidation([FromForm] ParamGetDetailNoresi pr)
        {
            try
            {

                sessionUser = SetSession();
                GeneralOutputModel output = new GeneralOutputModel();
                var retrn = _dataAccessProvider.GetValidationNoresi(pr, sessionUser);
                if (retrn.Status == "OK")
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
        [Route("UploadDocument")]
        [HttpPost]
        public ActionResult UploadDocument([FromForm] ParamUploadDocumentString pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UploadDataDocument(pr, sessionUser);

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
        [Route("GetReportDocument")]
        [HttpPost]
        public ActionResult GetReportDocument([FromForm] ParamGetDetailReportDocument pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailReportDocument(pr, sessionUser);

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
        [Route("ReceiveCheckedDoc")]
        [HttpPost]
        public ActionResult ReceiveCheckedDoc([FromForm] ParamReceiveCheckedDoc pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.ReceiveCheckedDoc(pr, sessionUser);

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
        [Route("GetSearchReportDocument")]
        [HttpPost]
        public ActionResult GetSearchDocument([FromForm] ParamGetDetailSearchoutgoingDocument pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetSearchDetailReportDocument(pr, sessionUser);

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

        #region Ekspedisi Non-eoffice


        [Authorize]
        [Route("GetDataSuratKeluarNon")]
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
        [Route("GetAdminDivisionNonEoffice")]
        [HttpGet]
        public ActionResult GetAdminDivision()
        {
            try
            {
                GeneralOutputModel retrn = _dataAccessProvider.GetAdminDivisionNonEoffice();

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
        [Route("ParamInsertNonEoffice")]
        [HttpPost]

        public ActionResult InsertLetterNonEoffice([FromForm] ParamInsertNonEoffice pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertDataLetterNonEoffice(pr, sessionUser);

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
        [Route("ParamUpdateNonEoffice")]
        [HttpPost]

        public ActionResult UpdateLetterNonEoffice([FromForm] UpdateNonEoffice pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UpdateDataLetterNonOffice_(pr, sessionUser);

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
        [Route("InsertEkspedisiUsingUpload")]
        [HttpPost]
        public ActionResult InsertDataUsingUploadEkspedisiNonOffice([FromForm] ParamUploadLetterNonOfficeString pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertDataUsingUploadEkspedisiNonOffice_(pr, sessionUser);

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
        [Route("GetDataEkspedisiNonEoffice")]
        [HttpPost]
        public ActionResult GetDataEkspediNon([FromForm] string keyword)
        {
            try
            {

                GeneralOutputModel retrn = _dataAccessProvider.GetDataEkspedisiNonEoffice(keyword);

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



        [Route("QrCodeMallingRoomNonEoffice")]
        [HttpPost]
        public ActionResult QrcodeMallingRoomNon([FromForm] ParamInsertGenerateNoDocNonEoffice pr)
        {
            try
            {

                sessionUser = SetSession();
                GeneralOutputModel output = new GeneralOutputModel();
                var retrn = _dataAccessProvider.GenerateNonDocoutgoingmail(pr.type, pr.userCode, sessionUser);
                pr.qrcodenumber = retrn;
                var data = _dataAccessProvider.UpdateQrcodeKurirNonEoffice(pr, sessionUser);

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



        [Route("getByIdNonEoffice")]
        [HttpPost]
        public ActionResult getByIdNonEofficeMailing([FromForm] getByIdNonEoffice pr)
        {
            try
            {

                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetByIdDocoutgoingmail(pr,sessionUser);

                return Ok(retrn);

            }
            catch (Exception ex)
            {
                output.Status = "NG";
                output.Message = ex.ToString();

                return BadRequest(output);
            }
        }

        #endregion


        #region Kurir Non Eoffcie

        [Authorize]
        [Route("GetDataSuratKeluarKurirNonWeb")]
        [HttpGet]
        public ActionResult GetDataSuratKeluarKurirNon()
        {
            try
            {
                GeneralOutputModel retrn = _dataAccessProvider.GetDataKurirSuratKeluarNon();

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
        [Route("ParamUpdateKurirNonEofficeWeb")]
        [HttpPost]

        public ActionResult UpdateKuriLetterNonEoffice([FromForm] UpdateNonEoffice pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UpdateDataKurirLetterNonOffice_(pr, sessionUser);

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


        [Authorize]
        [Route("GetDataOutgoingMailEksnKurir")]
        [HttpPost]
        public ActionResult GetSearchDocument([FromForm] ParamReportOutgoingMailEksnKurir pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetSearchReportOutgoingMailEksnKur(pr, sessionUser);

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
        [Route("UpdateEkspedisiNonUsingUploadWeb")]
        [HttpPost]
        public ActionResult UpdateEkspedisiNonUsingUpload([FromForm] ParamUploadLetterNonOfficeString pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UpdateUploadDocNonOffice_(pr, sessionUser);

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
        [Route("GetDataKurirNameNonEofficeWeb")]
        [HttpPost]
        public ActionResult GetDataKurirNameNon([FromForm] string keyword)
        {
            try
            {

                GeneralOutputModel retrn = _dataAccessProvider.GetDataKurirNameNonEoffice(keyword);

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
        [Route("GetDetailsEkspedisiNonEofficeWeb")]
        [HttpPost]
        public ActionResult GetDetailsEkspedisiNonEoffic([FromForm] getByIdNonEoffice pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetDetailsEkspedisiNonEoffic(pr,sessionUser);

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
        [Route("GetDetailsKurirNonEofficeWeb")]
        [HttpPost]
        public ActionResult GetDetailsKurirNonEoffice([FromForm] getByIdNonEoffice pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetDetailsKurirNonEoffice(pr, sessionUser);

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
        [Route("DetailLetterNonEofficeNotifikasiWeb")]
        [HttpPost]
        public ActionResult DetailLetterNonEofficeNotifikasiWeb([FromForm] getByIdNonEoffice pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.DetailNotifNonEoffice_(pr, sessionUser);

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

        //[Authorize]
        //[Route("NotifikasiNonEofficeWeb")]
        //[HttpPost]
        //public ActionResult DetailNotifNonEoffice([FromForm] getByIdNonEoffice pr)
        //{
        //    try
        //    {
        //        sessionUser = SetSession();

        //        GeneralOutputModel retrn = _dataAccessProvider.DetailNotifNonEoffice_(pr, sessionUser);

        //        if (retrn.Status == "OK")
        //        {
        //            return Ok(retrn);
        //        }
        //        return BadRequest(retrn);
        //    }
        //    catch (Exception ex)
        //    {
        //        output.Status = "NG";
        //        output.Message = ex.ToString();

        //        return BadRequest(output);
        //    }
        //}

        //[Authorize]
        //[Route("DetailApprovalUserProfileSignatureWeb")]
        //[HttpPost]
        //public ActionResult DetailApprovalUserProfileSignature([FromForm] OuputSignature pr)
        //{
        //    try
        //    {
        //        sessionUser = SetSession();

        //        GeneralOutputModel retrn = _dataAccessProvider.DetailApprovalUserProfileSignature(pr, sessionUser);

        //        if (retrn.Status == "OK")
        //        {
        //            return Ok(retrn);
        //        }
        //        return BadRequest(retrn);
        //    }
        //    catch (Exception ex)
        //    {
        //        output.Status = "NG";
        //        output.Message = ex.ToString();

        //        return BadRequest(output);
        //    }
        //}



        [Authorize]
        [Route("GetSearchOutgoingLetterNumber")]
        [HttpPost]
        public ActionResult GetOutgoingLetterNumber([FromForm] ParamGetReportSeacrhoutGoing pr)
        {
           
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetSearchOutgoingLetterNumber(pr, sessionUser);

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
