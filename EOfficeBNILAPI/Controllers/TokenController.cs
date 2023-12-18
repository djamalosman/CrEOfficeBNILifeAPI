using EOfficeBNILAPI.DataAccess;
using EOfficeBNILAPI.Jwt;
using EOfficeBNILAPI.Models.Table;
using EOfficeBNILAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EOfficeBNILAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration configuration;
        private readonly IDataAccessProvider _dataAccessProvider;
        public TokenController(IConfiguration iConfig, IDataAccessProvider dataAccessProvider)
        {
            configuration = iConfig;
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginParam param)
        {
            GeneralOutputModel output = new GeneralOutputModel();
            try
            {
                LoginResponse user = null;
                LoginResponse finalResult = new LoginResponse();

                var passwordBytes = System.Text.Encoding.UTF8.GetBytes(param.Password);
                var passwordEncode = System.Convert.ToBase64String(passwordBytes);

                LoginResponse resultData = _dataAccessProvider.GetDetailUserLoginToken(param.Nip, passwordEncode);
                if (resultData == null)
                {
                    output.Status = "NG";
                    output.Result = resultData;
                    output.Message = "User not found";
                    return Ok(output);
                }
                //if (resultData.Password == "MTIz")
                //{
                //    output.Status = "updpssd";
                //    output.Result = resultData;
                //    output.Message = "Ganti Password";
                //    return Ok(output);
                //}
                    finalResult.Token = resultData.IdUser.ToString();
                    finalResult.IdUser = resultData.IdUser;
                    finalResult.Fullname = resultData.Fullname;
                    finalResult.Nip = resultData.Nip;
                    finalResult.Password = resultData.Password;
                    finalResult.IdPosition = resultData.IdPosition;
                    finalResult.PositionName = resultData.PositionName;
                    finalResult.parentIdPosition = resultData.parentIdPosition;
                    finalResult.parentIdUser = resultData.parentIdUser;
                    finalResult.IdGroup = resultData.IdGroup;
                    finalResult.IdUnit = resultData.IdUnit;
                    finalResult.UnitName = resultData.UnitName;
                    finalResult.unitCode = resultData.unitCode;
                    finalResult.IdBranch = resultData.IdBranch;
                    finalResult.BranchName = resultData.BranchName;
                    finalResult.email = resultData.email;
                    finalResult.phone = resultData.phone;
                    finalResult.directorIdUnit = resultData.directorIdUnit;
                    finalResult.directorUnitName = resultData.directorUnitName;
                    finalResult.directorUnitCode = resultData.directorUnitCode;

                    user = finalResult;
                    if (user != null)
                    {
                        //SNR: ADD ROLE ID IN USERCLAIM
                        var token = new JwtTokenBuilder()
                                            .AddSecurityKey(JwtSecurityKey.Create("Eoffice1!-secret-key"))
                                            .AddSubject("EOffice")
                                            .AddIssuer("BNILife.EOffice.Bearer")
                                            .AddAudience("BNILife.EOffice.Bearer")
                                            .AddClaim("idUser", user.Token) //ambil userid dari var token
                                            .AddClaim("Nip", user.Nip.ToString())
                                            .AddClaim("Nama", user.Fullname.ToString())
                                            .AddClaim("idUnit", user.IdUnit.ToString())
                                            .AddClaim("idBranch", user.IdBranch.ToString())
                                            .AddClaim("unitCode", user.unitCode.ToString())
                                            .AddClaim("idPosition", user.IdPosition.ToString())
                                            .AddClaim("idGroup", user.IdGroup.ToString())
                                            .AddClaim("parentId", user.parentIdPosition.ToString())
                                            .AddClaim("directorIdUnit", user.directorIdUnit.ToString())
                                            .AddClaim("parentIdUser", user.parentIdUser.ToString())
                                            .AddExpiry(1440)
                                            .Build();

                        var dateTime = token.ValidTo;
                        var dateTimeOffset = new DateTimeOffset(dateTime);
                        var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();

                        user.Token = token.Value;
                        output.Status = "OK";
                        output.Result = user;
                        output.Message = "Login Success";

                        return Ok(output);
                }
                output.Status = "NG";
                output.Result = resultData;
                output.Message = "Username or password invalid";
                return Ok(output);
            }
            catch (Exception ex)
            {
                output.Status = "NG";
                output.Result = null;
                output.Message = ex.ToString();
                return Ok(output);
            }
        }
    }
}
