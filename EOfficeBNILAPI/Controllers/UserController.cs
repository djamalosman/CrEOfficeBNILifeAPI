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
using iTextSharp.text.pdf.qrcode;
using OneSignalApi.Model;

namespace EOfficeBNILAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        public MailSettings _mailconfig { get; }

        GeneralOutputModel output = new GeneralOutputModel();
        SessionUser sessionUser = new SessionUser();
        public UserController(IDataAccessProvider dataAccessProvider,IOptions<MailSettings> mailconfig)
        {
            _dataAccessProvider = dataAccessProvider;
            _mailconfig = mailconfig.Value;
        }

        [HttpGet(Name = "SetSessionAllUser")]
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

        [HttpPost]
        [Route("Auth")]
        [AllowAnonymous]
        public bool Authentication(string token)
        {
            bool returns = false;
            try
            {

                string Token = "953a3220084d73ea9948e3046c3c242d";
                if (token == Token)
                {
                    return returns = true;
                }
            }
            catch (Exception ex)
            {
                returns = false;
            }

            return returns;
        }

        [Authorize]
        [Route("GetDataUser")]
        [HttpPost]
        public ActionResult GetDataUserWeb([FromForm] ParamGetUsertWeb pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetUserDataWeb(sessionUser, pr);

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
        [Route("GetDetailUser")]
        [HttpPost]
        public ActionResult GetUserById([FromForm] ParamGetDetailUser pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDetailUser(pr, sessionUser);

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
        [Route("UpdateSettingUser")]
        [HttpPost]
        public ActionResult UpdateusettingserWeb([FromForm] ParamUpdateUser pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UpdateDatauserSetting(pr, sessionUser);

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
        [Route("UpdatePassword")]
        [HttpPost]
        public ActionResult UpdatePassword([FromForm] ParamUpdatePassword pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.ResetPassword(pr, sessionUser);


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
        [Route("GetAllDivision")]
        [HttpGet]
        public ActionResult GetAllDivision()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllDivision(sessionUser);


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
        [Route("GetAllUserAdminDivision")]
        [HttpGet]
        public ActionResult GetAllUserAdminDivision()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllUserAdminDivision(sessionUser);


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
        [Route("GetUserByUnit")]
        [HttpPost]
        public ActionResult GetUserByUnit([FromForm] ParamGetUserByUnit pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetUserByUnit(pr,sessionUser);


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
        [Route("UpdateAdminDivisi")]
        [HttpPost]
        public ActionResult UpdateAdminDivisi([FromForm] ParamUpdateAdminDivisi pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.UpdateAdminDivisi(pr, sessionUser);


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
        [Route("DeleteAdminDivisi")]
        [HttpPost]
        public ActionResult DeleteAdminDivisi([FromForm] ParamUpdateAdminDivisi pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.DeleteAdminDivisi(pr, sessionUser);


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


        [AllowAnonymous]
        [Route("SyncUser")]
        [HttpPost]
        public ActionResult SyncUser(ParamAuthentication pr)
        {
            try
            {
                //ParamAuthentication pr = new ParamAuthentication();
                //pr.token = token;
                //pr.jsonData = jsonData;
                if (Authentication(pr.token))
                {
                    GeneralOutputModel retrn = _dataAccessProvider.SyncUserHCIS(pr.jsonData);


                    if (retrn.Status == "OK")
                    {
                        return Ok(retrn);
                    }
                    return BadRequest(retrn);
                }
                else
                {
                    output.Status = "NG";
                    output.Message = "Invalid Token";

                    return BadRequest(output);
                }
                
            }
            catch (Exception ex)
            {
                output.Status = "NG";
                output.Message = ex.ToString();

                return BadRequest(output);
            }
        }

        //[AllowAnonymous]
        //[Route("SyncDivisi")]
        //[HttpPost]
        //public ActionResult SyncDivisi(ParamAuthenticationDivsi pr)
        //{
        //    try
        //    {
        //        //ParamAuthentication pr = new ParamAuthentication();
        //        //pr.token = token;
        //        //pr.jsonData = jsonData;
        //        if (Authentication(pr.token))
        //        {
        //            GeneralOutputModel retrn = _dataAccessProvider.SyncDivisiHCIS(pr.jsonData);


        //            if (retrn.Status == "OK")
        //            {
        //                return Ok(retrn);
        //            }
        //            return BadRequest(retrn);
        //        }
        //        else
        //        {
        //            output.Status = "NG";
        //            output.Message = "Invalid Token";

        //            return BadRequest(output);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        output.Status = "NG";
        //        output.Message = ex.ToString();

        //        return BadRequest(output);
        //    }
        //}


        //[AllowAnonymous]
        //[Route("SyncPosition")]
        //[HttpPost]
        //public ActionResult SyncPosition(ParamAuthenticationPosition pr)
        //{
        //    try
        //    {
        //        //ParamAuthentication pr = new ParamAuthentication();
        //        //pr.token = token;
        //        //pr.jsonData = jsonData;
        //        if (Authentication(pr.token))
        //        {
        //            GeneralOutputModel retrn = _dataAccessProvider.SyncPositionHCIS(pr.jsonData);


        //            if (retrn.Status == "OK")
        //            {
        //                return Ok(retrn);
        //            }
        //            return BadRequest(retrn);
        //        }
        //        else
        //        {
        //            output.Status = "NG";
        //            output.Message = "Invalid Token";

        //            return BadRequest(output);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        output.Status = "NG";
        //        output.Message = ex.ToString();

        //        return BadRequest(output);
        //    }
        //}


        //[AllowAnonymous]
        //[Route("SyncCuti")]
        //[HttpPost]
        //public ActionResult SyncCuti(ParamAuthenticationCuti pr)
        //{
        //    try
        //    {
        //        //ParamAuthentication pr = new ParamAuthentication();
        //        //pr.token = token;
        //        //pr.jsonData = jsonData;
        //        if (Authentication(pr.token))
        //        {
        //            GeneralOutputModel retrn = _dataAccessProvider.SyncCutiHCIS(pr.jsonData);


        //            if (retrn.Status == "OK")
        //            {
        //                return Ok(retrn);
        //            }
        //            return BadRequest(retrn);
        //        }
        //        else
        //        {
        //            output.Status = "NG";
        //            output.Message = "Invalid Token";

        //            return BadRequest(output);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        output.Status = "NG";
        //        output.Message = ex.ToString();

        //        return BadRequest(output);
        //    }
        //}


        //[AllowAnonymous]
        //[Route("SyncDelegasi")]
        //[HttpPost]
        //public ActionResult SyncDelegasi(ParamAuthenticationDelegasi pr)
        //{
        //    try
        //    {
        //        //ParamAuthentication pr = new ParamAuthentication();
        //        //pr.token = token;
        //        //pr.jsonData = jsonData;
        //        if (Authentication(pr.token))
        //        {
        //            GeneralOutputModel retrn = _dataAccessProvider.SyncDelegasiHCIS(pr.jsonData);


        //            if (retrn.Status == "OK")
        //            {
        //                return Ok(retrn);
        //            }
        //            return BadRequest(retrn);
        //        }
        //        else
        //        {
        //            output.Status = "NG";
        //            output.Message = "Invalid Token";

        //            return BadRequest(output);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        output.Status = "NG";
        //        output.Message = ex.ToString();

        //        return BadRequest(output);
        //    }
        //}


        //[AllowAnonymous]
        //[Route("SyncAbsen")]
        //[HttpPost]
        //public ActionResult SyncAbsen(ParamAuthenticationAbsen pr)
        //{
        //    try
        //    {
        //        //ParamAuthentication pr = new ParamAuthentication();
        //        //pr.token = token;
        //        //pr.jsonData = jsonData;
        //        if (Authentication(pr.token))
        //        {
        //            GeneralOutputModel retrn = _dataAccessProvider.SyncAbsenHCIS(pr.jsonData);


        //            if (retrn.Status == "OK")
        //            {
        //                return Ok(retrn);
        //            }
        //            return BadRequest(retrn);
        //        }
        //        else
        //        {
        //            output.Status = "NG";
        //            output.Message = "Invalid Token";

        //            return BadRequest(output);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        output.Status = "NG";
        //        output.Message = ex.ToString();

        //        return BadRequest(output);
        //    }
        //}


        [AllowAnonymous]
        [Route("MailForgotPassword")]
        [HttpPost]
        public ActionResult MailForgotPassword([FromForm] ParamForgotPassword pr)
        {
            try
            {
                if (Authentication(pr.token))
                {
                    GeneralOutputModel retrn = _dataAccessProvider.GenerateRecoveryToken(pr);
                    //GeneralOutputModel retrn = new GeneralOutputModel();
                    //retrn.Status = "OK";

                    if (retrn.Status == "OK")
                    {
                        Tm_User_Table userOutput = new Tm_User_Table();

                        var jsonApiResponseSerialize = JsonConvert.SerializeObject(retrn.Result);
                        userOutput = JsonConvert.DeserializeObject<Tm_User_Table>(jsonApiResponseSerialize);

                        //setup send mail
                        string mailServer = _mailconfig.mailServer;
                        string mailAddress = _mailconfig.mailAddress;
                        string mailPassword = _mailconfig.mailPassword;
                        int mailPort = Convert.ToInt32(_mailconfig.mailPort);

                        var body = new StringBuilder();
                        var html = "Dear " + userOutput.FULLNAME + " <br><br> you have requested to reset password in E-Office <br><br> Please Click <a href='http://172.20.20.156/EOFFICEBNILWEB/Account/ForgetPassword/" + userOutput.RECOVERY_TOKEN + "'>here</a> to change your password <br><br> Best Regards, <br><br>E-Office System ";
                        body.AppendLine(html);
                        try
                        {
                            using (MailMessage mail = new MailMessage())
                            {
                                mail.From = new MailAddress(mailAddress);
                                mail.To.Add(pr.email);
                                mail.Subject = "Password Recovery";
                                mail.Body = body.ToString();
                                mail.IsBodyHtml = true;

                                //mailConfig.Send(mail);
                                using (SmtpClient smtp = new SmtpClient(mailServer, mailPort))
                                {
                                    smtp.EnableSsl = false;
                                    smtp.UseDefaultCredentials = false;
                                    smtp.Credentials = new NetworkCredential(mailAddress, mailPassword);
                                    smtp.Send(mail);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        return Ok(retrn);
                    }
                    return BadRequest(retrn);
                }
                else
                {
                    output.Status = "NG";
                    output.Message = "Invalid Token";

                    return BadRequest(output);
                }

            }
            catch (Exception ex)
            {
                output.Status = "NG";
                output.Message = ex.ToString();

                return BadRequest(output);
            }
        }

        [AllowAnonymous]
        [Route("UpdateForgotPasswordUser")]
        [HttpPost]
        public ActionResult UpdateForgotPasswordUser([FromForm] ParamUpdateForgotPassword pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.UpdateForgotPasswordUser(pr);

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
        [Route("GetuserPga")]
        [HttpGet]
        public ActionResult GetAlluserPGA()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllPGAUser(sessionUser);
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
        [Route("GetuserDirektur")]
        [HttpGet]
        public ActionResult GetAlluserDirekturWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllDirekturUser(sessionUser);
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
        [Route("GetUserBySeketaris")]
        [HttpPost]
        public ActionResult GetBySeketaris([FromForm] ParamUpdateUserSekdirWeb pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetUserBySeketaris(pr, sessionUser);

                
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
        [Route("GetDataUserSekdir")]
        [HttpGet]
        public ActionResult GetDataUserSekdirWeb()
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetUserSekDirDataWeb(sessionUser);

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
        [Route("UpdateSettingSeketaris")]
        [HttpPost]
        public ActionResult UpdateuserSetingSeketarisWeb([FromForm] ParamUpdateUserSekdirWeb pr)
        {
            try
            {
                sessionUser = SetSession();

                    GeneralOutputModel valdValue = _dataAccessProvider.ValidasiDatauserSeketaris(pr, sessionUser);
                    if (valdValue.Status == "OK")
                    {
                        GeneralOutputModel retrn = _dataAccessProvider.UpdateDatauserSeketaringSetting(pr, sessionUser);
                        if (retrn.Status == "OK")
                        {
                            return Ok(retrn);
                        }
                        return BadRequest(retrn);
                    }
                    return BadRequest(valdValue);
                
            }
            catch (Exception ex)
            {
                output.Status = "NG";
                output.Message = ex.ToString();

                return BadRequest(output);
            }
        }

        [Authorize]
        [Route("DeleteSettingUserSekdir")]
        [HttpPost]
        public ActionResult DeleteusettingUserSekdirWeb([FromForm] ParamUpdateSekdirWeb pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.DeleteDatauserSekdirSetting(pr, sessionUser);

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
        [Route("GetSuperUser")]
        [HttpGet]
        public ActionResult GetAllSuperUserWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllSuperUser(sessionUser);
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
        [Route("GetAllDataSuperUser")]
        [HttpPost]
        public ActionResult GetDataSuperUserWeb([FromForm] ParamGetUsertWeb pr)
        {
            try
            {
                sessionUser = SetSession();



                GeneralOutputModel retrn = _dataAccessProvider.GetSuperUserDataWeb(sessionUser, pr);

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
        [Route("UpdateSuperUser")]
        [HttpPost]
        public ActionResult UpdateSuperUserWeb([FromForm] ParamGetDetailUser pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UpdateDataSuperuser(pr, sessionUser);

                if (retrn.Status == "OK")
                {

                    //Tm_User_Table userOutput = new Tm_User_Table();
                    //var jsonApiResponseSerialize = JsonConvert.SerializeObject(retrn.Result);
                    //userOutput = JsonConvert.DeserializeObject<Tm_User_Table>(jsonApiResponseSerialize);
                    //ParamForgotPassword mlpsd = new ParamForgotPassword();
                    //mlpsd.email = userOutput.EMAIL;
                    ////setup send mail
                    //string mailServer = _mailconfig.mailServer;
                    //string mailAddress = _mailconfig.mailAddress;
                    //string mailPassword = _mailconfig.mailPassword;
                    //int mailPort = Convert.ToInt32(_mailconfig.mailPort);

                    //var body = new StringBuilder();
                    //var html = "Dear " + userOutput.FULLNAME + " <br><br> you have requested to reset password in E-Office <br><br> Please Click <a href='http://10.22.13.34/EOFFICEBNILWEB/Account/ForgetPassword/" + userOutput.RECOVERY_TOKEN + "'>here</a> to change your password <br><br> Best Regards, <br><br>E-Office System ";
                    //body.AppendLine(html);
                    //try
                    //{
                    //    using (MailMessage mail = new MailMessage())
                    //    {
                    //        mail.From = new MailAddress(mailAddress);
                    //        mail.To.Add(mlpsd.email);
                    //        mail.Subject = "Password Recovery";
                    //        mail.Body = body.ToString();
                    //        mail.IsBodyHtml = true;

                    //        //mailConfig.Send(mail);
                    //        using (SmtpClient smtp = new SmtpClient(mailServer, mailPort))
                    //        {
                    //            smtp.EnableSsl = false;
                    //            smtp.UseDefaultCredentials = false;
                    //            smtp.Credentials = new NetworkCredential(mailAddress, mailPassword);
                    //            smtp.Send(mail);
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw ex;
                    //}
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
        [Route("DeleteSuperUser")]
        [HttpPost]
        public ActionResult DeleteSuperUserWeb([FromForm] ParamGetDetailUser pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.DeleteDataSuperUser(pr, sessionUser);

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


        //Set Admin HCT

        [Authorize]
        [Route("AdminHct")]
        [HttpGet]
        public ActionResult GetAllAdminHctWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllDwopdownAdminHct(sessionUser);
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
        [Route("GetAllDataAdminHct")]
        [HttpPost]
        public ActionResult GetDataTabelAdminHctWeb([FromForm] ParamGetUsertWeb pr)
        {
            try
            {
                sessionUser = SetSession();



                GeneralOutputModel retrn = _dataAccessProvider.GetAllDataAdminHctWeb(sessionUser, pr);

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
        [Route("UpdateAdminHCT")]
        [HttpPost]
        public ActionResult UpdateAdminHCTWeb([FromForm] ParamGetDetailUser pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UpdateDataAdminHct(pr, sessionUser);

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
        [Route("DeleteAdminHct")]
        [HttpPost]
        public ActionResult DeleteAdminHctWeb([FromForm] ParamGetDetailUser pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.DeleteDataAdminHct(pr, sessionUser);

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
       [Route("GetAllDataEmployee")]
       [HttpGet]
        public ActionResult GetAllEmloyeeWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllDwopdownEmloyee(sessionUser);
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
        [Route("GetAllEmpLevel")]
        [HttpGet]
        public ActionResult GetAllEmpLevelHct()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllEmpLevel(sessionUser);


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
        [Route("GetAllUserAdminDivisionHct")]
        [HttpGet]
        public ActionResult GetAllUserAdminDivisionHct()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllUserAdminDivisionHct(sessionUser);


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
        [Route("InsertLevelEmpl")]
        [HttpPost]
        public ActionResult InsertLevelEmployee([FromForm] ParamUpdateLevelemp pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.InsertLevelEmpl(pr, sessionUser);


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
        [Route("DeleteLevelEmpl")]
        [HttpPost]
        public ActionResult DeleteLevelEmp([FromForm] ParamUpdateLevelemp pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.DeleteEplLevel(pr, sessionUser);


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
        [Route("UpdateOneSignalId")]
        [HttpPost]
        public ActionResult UpdateOneSignalIdMobile([FromForm] ParamOneSignal pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UpdateOneSignalId(pr, sessionUser);

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
        [Route("InsertUserSignatureWeb")]
        [HttpPost]
        public ActionResult InsertSignatureUser([FromForm] ImageUploadTTD pr)
        {
            try
            {
                sessionUser = SetSession();
                
                GeneralOutputModel retrn = _dataAccessProvider.InsertDataUserSignature(pr, sessionUser);

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
        [Route("GetOuputSignatureUser")]
        [HttpGet]
        public ActionResult GetAllUserSignatureWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllSignatureUser(sessionUser);
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
        [Route("GetOuputApprovalSignatureUser")]
        [HttpGet]
        public ActionResult GetAllUApprovalserSignatureWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllApprovalSignatureUser(sessionUser);
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
        [Route("GetOuputApprovalRejectSignatureUserWeb")]
        [HttpGet]
        public ActionResult GetOuputApprovalRejectSignatureUser()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetOuputApprovalRejectSignatureUser(sessionUser);
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

        //public ActionResult ApprovalSignatureUser([FromForm] ParamGetApprovalSignature pr)
        [Authorize]
        [Route("ApprovalSignatureUserWeb")]
        [HttpPost]
        public ActionResult ApprovalSignatureUser([FromForm] ParamJsonStirngSiganture pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.ApprovalSigantureDataUser(pr, sessionUser);

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
        [Route("RejectSignatureUserWeb")]
        [HttpPost]
        public ActionResult RejectSignatureUser([FromForm] ParamJsonStirngSiganture pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.RejectSigantureDataUser(pr, sessionUser);

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
        [Route("DeleteUserProfileSignatureWeb")]
        [HttpPost]
        public ActionResult DeleteUserProfileSignature([FromForm] OuputSignature pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.DeleteUserProfileSignature(pr, sessionUser);

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
        [Route("DetailUserProfileSignatureWeb")]
        [HttpPost]
        public ActionResult DetailUserProfileSignature([FromForm] OuputSignature pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.DetailUserProfileSignature(pr, sessionUser);

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
        [Route("DetailApprovalUserProfileSignatureWeb")]
        [HttpPost]
        public ActionResult DetailApprovalUserProfileSignature([FromForm] OuputSignature pr)
        {
            try
            {
                sessionUser = SetSession();
                
                GeneralOutputModel retrn = _dataAccessProvider.DetailApprovalUserProfileSignature(pr, sessionUser);
                

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
        [Route("ApprovalSignatureOneDataWeb")]
        [HttpPost]
        public ActionResult ApprovalSignatureOneDataWeb([FromForm] ParamGetApprovalSignatureOnedata pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.ApprovalSignatureOneDataWeb_(pr, sessionUser);


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
        [Route("RejectSignatureOneDataWeb")]
        [HttpPost]
        public ActionResult RejectSignatureOneDataWeb([FromForm] ParamGetApprovalSignatureOnedata pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.RejectSignatureOneDataWeb_(pr, sessionUser);


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
        [Route("GetDropdwonSetUserApprovalBod")]
        [HttpGet]
        public ActionResult GetDropdwonSettingUserApprovalBod()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetUserDropDownSetUserBod(sessionUser);
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
        [Route("GetAllDataSettingBod")]
        [HttpPost]
        public ActionResult GetAllDataSettingBod([FromForm] ParamGetUsertWeb pr)
        {
            try
            {
                sessionUser = SetSession();



                GeneralOutputModel retrn = _dataAccessProvider.GetDataUserBodAll(sessionUser, pr);

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
        [Route("CreateBodWeb")]
        [HttpPost]
        public ActionResult CreateBodWeb([FromForm] ParamGetDetailUser pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.CreateDataUserBod(pr, sessionUser);

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
        [Route("DeleteSettingApprvlBodWeb")]
        [HttpPost]
        public ActionResult DeleteSettingApprvlWeb([FromForm] ParamGetDetailUser pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.DeleteDataSettingApprovalBod(pr, sessionUser);

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
        [Route("AdminPengadaan")]
        [HttpGet]
        public ActionResult GetAllAdminPengadaanWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllDwopdownAdminPengadaan(sessionUser);
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
        [Route("GetAllDataAdminPengadaan")]
        [HttpPost]
        public ActionResult GetDataTabelAdminPengadaanWeb([FromForm] ParamGetUsertWeb pr)
        {
            try
            {
                sessionUser = SetSession();



                GeneralOutputModel retrn = _dataAccessProvider.GetAllDataAdminPengadaanWeb(sessionUser, pr);

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
        [Route("UpdateAdminPengadaan")]
        [HttpPost]
        public ActionResult UpdateAdminPengadaanWeb([FromForm] ParamGetDetailUser pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UpdateDataAdminPengadaan(pr, sessionUser);

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
        [Route("DeleteAdminPengadaan")]
        [HttpPost]
        public ActionResult DeleteAdminPengadaanWeb([FromForm] ParamGetDetailUser pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.DeleteDataAdminPengadaan(pr, sessionUser);

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
        [Route("SetPengadaan")]
        [HttpGet]
        public ActionResult GetAllSetPengadaanWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllDwopdownSetPengadaan(sessionUser);
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
        [Route("GetAllDataSetPengadaanWeb")]
        [HttpPost]
        public ActionResult GetDataTabelSetPengadaanWeb([FromForm] ParamGetUsertWeb pr)
        {
            try
            {
                sessionUser = SetSession();



                GeneralOutputModel retrn = _dataAccessProvider.GetAllDataSetPengadaanWeb(sessionUser, pr);

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
        [Route("InsertDataPengadaan")]
        [HttpPost]
        public ActionResult InsertDataPengadaanWeb([FromForm] ParamInsertPengadaan pr)
        {
            try
            {
                sessionUser = SetSession();


                GeneralOutputModel retrn = _dataAccessProvider.InsertDataPengadaan(pr, sessionUser);
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
        [Route("SetDelegasi")]
        [HttpGet]
        public ActionResult GetAllSetDelegasiWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetAllDwopdownSetDelegasi(sessionUser);
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
        [Route("GetAllDataSetDelegasiWeb")]
        [HttpPost]
        public ActionResult GetDataTabelSetDelegasiWeb([FromForm] ParamGetUsertWeb pr)
        {
            try
            {
                sessionUser = SetSession();



                GeneralOutputModel retrn = _dataAccessProvider.GetAllDataSetDelegasiWeb(sessionUser, pr);

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
        [Route("InsertDataDelegasi")]
        [HttpPost]
        public ActionResult InsertDataDelegasiWeb([FromForm] ParamInsertDelegasi pr)
        {
            try
            {
                sessionUser = SetSession();


                GeneralOutputModel retrn = _dataAccessProvider.InsertDataDelegasi(pr, sessionUser);
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
        [Route("GetDataPengadaanAllWeb")]
        [HttpGet]
        public ActionResult GetAllDataPengadaanWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetDataPengadaanAll(sessionUser);
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
        [Route("GetDataPengadaanApprovalWeb")]
        [HttpGet]
        public ActionResult GetAllDataPengadaanApprovalWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetDataPengadaanApproval(sessionUser);
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
        [Route("ApprovalJenisPengadaanWeb")]
        [HttpPost]
        public ActionResult ApprovalJenisPengadaan([FromForm] ParamJsonStirngPengadaan pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.ApprovalJenisPengadaan(pr, sessionUser);

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
        [Route("GetDataPengadaanViewWeb")]
        [HttpPost]
        public ActionResult GetDataPengadaanView([FromForm] ParamGetPengadaanView pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetDataPengadaanView(pr, sessionUser);

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
        [Route("GetDataDelegasiWeb")]
        [HttpGet]
        public ActionResult GetAllDataDelegasiWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetDataDelegasi(sessionUser);
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
        [Route("GetDataDelegasiApprovalWeb")]
        [HttpGet]
        public ActionResult GetDataDelegasiApprovalWeb()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetDataDelegasiApproval(sessionUser);
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
        [Route("ApprovalDelegasiUserWeb")]
        [HttpPost]
        public ActionResult ApprovalDelegasiUser([FromForm] ParamJsonStirngDelegasi pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.ApprovalDelegasiUser(pr, sessionUser);

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
        [Route("UpdateResetPasswordWeb")]
        [HttpPost]
        public ActionResult UpdateResetPassword([FromForm] ParamUpdateAdminDivisi pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.UpdateResetPassword(pr, sessionUser);


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


        [AllowAnonymous]
        [Route("UpdatePasswordLoginUser")]
        [HttpPost]
        public ActionResult UpdatePasswordLoginUser([FromForm] ParamUpdatePasswordLogin pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.UpdatePasswordLoginUser_(pr);

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
