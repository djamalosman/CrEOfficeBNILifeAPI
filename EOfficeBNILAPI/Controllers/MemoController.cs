using EOfficeBNILAPI.DataAccess;
using EOfficeBNILAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EOfficeBNILAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        GeneralOutputModel output = new GeneralOutputModel();
        SessionUser sessionUser = new SessionUser();
        public MemoController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        [HttpGet(Name = "SetSessionMemo")]
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
        [Route("InsertAttachmentMemoWeb")]
        [HttpPost]
        public ActionResult InsertAttachmentMemo([FromForm] ParamInsertAttachmentMemo pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertDataAttachmentMemo(pr, sessionUser);

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
        [Route("InsertMemoLetter")]
        [HttpPost]
        public ActionResult InsertMemo([FromForm] ParamInsertMemo pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertMemo(pr, sessionUser);

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
        [Route("GetDistribusiMemoWeb")]
        [HttpPost]
        public ActionResult GetDistribusiMemo([FromForm] ParamGetMemoWeb pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDistribusiMemo(pr, sessionUser);

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
        [Route("GetDraftMemoWeb")]
        [HttpPost]
        public ActionResult GetDraftMemo([FromForm] ParamGetMemoWeb pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetMemoDraft(pr, sessionUser);

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
        [Route("GetDetailMemoWeb")]
        [HttpPost]
        public ActionResult GetDetailMemo([FromForm] ParamGetDetailMemo pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailsMemo(pr, sessionUser);

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
        [Route("DeleteAttachmentMemoWeb")]
        [HttpPost]
        public ActionResult DeleteAttachmentMemo([FromForm] ParamDeleteAttachmentMemo pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.DeleteAttachmentMemo(pr, sessionUser);

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
        [Route("ApprovalMemoWeb")]
        [HttpPost]
        public ActionResult ApprovalMemo([FromForm] ParamApprovalLetterMemo pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.ApprovalLetterMemo(pr, sessionUser);

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
        [Route("MemoLetterNotifSecretaryWeb")]
        [HttpPost]
        public ActionResult MemoLetterNotifSecretary([FromForm] ParamGetDetailLetter pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.MemoLetterNotifSecretary(pr, sessionUser);

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
        [Route("GetStringmapMemoWeb")]
        [HttpPost]
        public ActionResult GetStringmapMemo([FromForm] ParamGetStringmap pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetStringMapMemo_(pr, sessionUser);

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
        [Route("GetMemotypeByIdWeb")]
        [HttpPost]
        public ActionResult GetMemotypeById([FromForm] ParamGetMemoTypeById pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetMemoTypeById_(pr, sessionUser);


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
        [Route("ApprovalDelebrationMemoWeb")]
        [HttpPost]
        public ActionResult ApprovalDelebrationMemo([FromForm] ParamApprovalLetterMemo pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.ApprovalDelebrationMemo_(pr, sessionUser);

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
        [Route("InsertMemoBackDateLetter")]
        [HttpPost]
        public ActionResult InsertMemoBackDate([FromForm] ParamInsertMemoBackDate pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertMemoBackDate(pr, sessionUser);

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



        #region Pengadaan

        [Authorize]
        [Route("GetAllPengadaanWeb")]
        [HttpGet]
        public ActionResult GetAllPengadaan()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllPengadaan_(sessionUser);


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
        [Route("GetPengadaanByIdWeb")]
        [HttpPost]
        public ActionResult GetPengadaanById([FromForm] ParamGetMemoPengadaanById pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetPengadaanById_(pr, sessionUser);


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
        [Route("GetDataMinMaxNomialByIdWeb")]
        [HttpPost]
        public ActionResult GetDataMinMaxNomialById([FromForm] ParamCheckMinMaxNomial pr)
        {
            try
            {
                GeneralOutputModel retrn = _dataAccessProvider.GetDataNominalMinMaxById(pr);

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
        [Route("GetApprovalPengadaandWeb")]
        [HttpPost]
        public ActionResult GetApprovalPengadaanById([FromForm] ParamGetMemoPengadaanById pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetApprovalPengadaanById_(pr, sessionUser);


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
        [Route("GetDetailMemoBackDateWeb")]
        [HttpPost]
        public ActionResult GetDetailMemoBackDate([FromForm] ParamGetDetailMemo pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailsMemoBackDate(pr, sessionUser);

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
        [Route("InsertMemoPengadaanWeb")]
        [HttpPost]
        public ActionResult InsertMemoPengadaan([FromForm] ParamInsertMemoPengadaan pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.InsertMemoPengadaan(pr, sessionUser);

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
        [Route("GetDetailMemoPengadaanWeb")]
        [HttpPost]
        public ActionResult GetDetailMemoPengadaan([FromForm] ParamGetDetailMemo pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailsMemoPengadaan(pr, sessionUser);

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
        [Route("ApprovalMemoPengadaanWeb")]
        [HttpPost]
        public ActionResult ApprovalMemoPengadaan([FromForm] ParamApprovalLetterMemo pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.ApprovalMemoPengadaan_(pr, sessionUser);

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
        [Route("GetDetailMemoWebPriviewWeb")]
        [HttpPost]
        public ActionResult GetDetailMemoWebPriview([FromForm] ParamGetDetailMemo pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailMemoWebPriview_(pr, sessionUser);

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
        [Route("GetDetailsMemoPengadaanWebPrivieWeb")]
        [HttpPost]
        public ActionResult GetDetailsMemoPengadaanWebPriview([FromForm] ParamGetDetailMemo pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailsMemoPengadaanWebPriview_(pr, sessionUser);

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
