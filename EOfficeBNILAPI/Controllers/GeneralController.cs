using EOfficeBNILAPI.DataAccess;
using EOfficeBNILAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Text;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Net;
using QRCoder;
using System.Drawing;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
using Rectangle = iTextSharp.text.Rectangle;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Paragraph = iTextSharp.text.Paragraph;
using Path = System.IO.Path;
using Document = iTextSharp.text.Document;
using PageSize = iTextSharp.text.PageSize;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace EOfficeBNILAPI.Controllers
{


    public static class BitmapExtension
    {
        public static byte[] ConvertBitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        GeneralOutputModel output = new GeneralOutputModel();
        SessionUser sessionUser = new SessionUser();
        private readonly IHostingEnvironment _environment;
        public GeneralController(IDataAccessProvider dataAccessProvider, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _dataAccessProvider = dataAccessProvider;
            _environment=environment;
        }

        [HttpGet(Name = "SetSessionGenreal")]
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
        [Route("GetStringmap")]
        [HttpPost]
        public ActionResult GetStringmap([FromForm] ParamGetStringmap pr)
        {
            try
            {

                GeneralOutputModel retrn = _dataAccessProvider.GetStringMap(pr);

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
        [Route("GetDataPenerima")]
        [HttpPost]
        public ActionResult GetDataPenerima([FromForm] string keyword)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetDataPenerima(keyword, sessionUser);

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
        [Route("GetDashboardContent")]
        [HttpGet]
        public ActionResult GetDashboardContent()
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetDashboardContent(sessionUser);

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
        [Route("GetDataPemeriksaDivWeb")]
        [HttpPost]
        public ActionResult GetDataPemeriksaDiv([FromForm] string keyword)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetDataPemeriksaDiv_(keyword, sessionUser);

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
        [Route("GetDataPemeriksaDivLainyaWeb")]
        [HttpPost]
        public ActionResult GetDataPemeriksaDivLainya([FromForm] string keyword)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetDataPemeriksaDivLainya(keyword, sessionUser);

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
        [Route("GetStringMapSearchInboxWM")]
        [HttpPost]
        public ActionResult GetStringMapSearchInbox([FromForm] ParamGetStringmap pr)
        {
            try
            {

                GeneralOutputModel retrn = _dataAccessProvider.GetStringMapSearchInboxWM_(pr);

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
        [Route("GetStringMapSearchOutboxWM")]
        [HttpPost]
        public ActionResult GetStringMapSearchOutbox([FromForm] ParamGetStringmap pr)
        {
            try
            {

                GeneralOutputModel retrn = _dataAccessProvider.GetStringMapSearchOutbox_(pr);

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
        [Route("GetStringMapMemoSrchMemoWM")]
        [HttpPost]
        public ActionResult GetStringMapMemoSrch([FromForm] ParamGetStringmap pr)
        {
            try
            {
                sessionUser = SetSession();
                GeneralOutputModel retrn = _dataAccessProvider.GetStringMapMemoSrch_(pr, sessionUser);

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
        [Route("GetStringmapLevelStgJbtnWeb")]
        [HttpPost]
        public ActionResult GetStringmapLevelStgJbtn([FromForm] ParamGetStringmap pr)
        {
            try
            {

                GeneralOutputModel retrn = _dataAccessProvider.GetStringmapLevelStgJbtn_(pr);

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
        [Route("UpdateStatusNotifikasi")]
        [HttpPost]
        public ActionResult UpdateStatusNotif([FromForm] ParamUpdateNotif pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.UpdateStatusNotif(pr, sessionUser);

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
        [Route("PushNotifikasiToMobile")]
        [HttpPost]
        public async Task<ActionResult> PushNotifikasiToMobileAsync([FromForm] ParamPushNotifikasi pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.PushNotifikasiToMobile_(pr, sessionUser);

                List<GetDataPushNotifikasi> PushNotifikasi = new List<GetDataPushNotifikasi>();
                var jsonApiResponseSerializee = JsonConvert.SerializeObject(retrn.Result);
                PushNotifikasi = JsonConvert.DeserializeObject<List<GetDataPushNotifikasi>>(jsonApiResponseSerializee);
                
                foreach (var item in PushNotifikasi)
                {
                    var pesan = "";
                    //if (item.lettertypeCode == 1)

                    //{
                    //     pesan = " Terdapat Surat Masuk baru perihal " + item.about + " yang membutuhkan Approval anda";
                    //}
                    if(item.lettertypeCode == 2)
                    {
                        pesan = " Terdapat Surat Keluar baru perihal " + item.about + " yang membutuhkan Approval anda";
                    }
                    else if (item.lettertypeCode == 3)
                    {
                        pesan = " Terdapat Surat Masuk Memo baru perihal " + item.about + " yang membutuhkan Approval anda";
                    }
                    else if (item.lettertypeCode == 4)
                    {
                        pesan = " Terdapat Surat Masuk Memo Pengadaan baru perihal " + item.about + " yang membutuhkan Approval anda";
                    }
                    else if (item.lettertypeCode == 5)
                    {
                        pesan = " Terdapat Surat Masuk Memo Lainnya baru perihal " + item.about + " yang membutuhkan Approval anda";
                    }
                    else if (item.lettertypeCode == 6)
                    {
                        pesan = " Terdapat Surat Keluar Lainnya baru perihal " + item.about + " yang membutuhkan Approval anda";
                    }
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "https://onesignal.com/api/v1/notifications");
                    request.Headers.Add("Authorization", "Bearer OWEwMDg1YzctOWNiMi00MjYwLTgzMmMtMDYzOWMyZGViM2M4");
                     var content = new StringContent($@"{{
                    ""include_player_ids"": [""{item.idOneSignal}""],
                    ""app_id"": ""f6e0ae51-3bb9-4084-b080-0651eb2a3870"",
                    ""contents"": {{
                         ""en"": ""{pesan}""
                    }},
                    ""headings"": {{
                        ""en"": [""{item.fullname}""]
                    }},
                    ""ios_badgeType"": ""SetTo"",
                    ""ios_badgeCount"": 1,
                    ""priority"": 10
                }}", null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                }
                //var appConfig = new Configuration();
                //appConfig.BasePath = "https://onesignal.com/api/v1";
                //appConfig.AccessToken = "OWEwMDg1YzctOWNiMi00MjYwLTgzMmMtMDYzOWMyZGViM2M4";
                //var appInstance = new DefaultApi(appConfig);

                //foreach (var item in PushNotifikasi)
                //{
                //    // Create and send notification to all subscribed users
                //    var notification = new Notification(appId: "f6e0ae51-3bb9-4084-b080-0651eb2a3870")
                //    {
                //        Contents = new StringMap(en: "Hello World from .NET!"),
                //        IncludedSegments = new List<string> { item.idOneSignal.ToString() }
                //    };
                //    var response = await appInstance.CreateNotificationAsync(notification);
                //}


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
        [Route("GetDataPriviewPdfMobile")]
        [HttpPost]
        public async Task<ActionResult> GetDataPriviewMobile([FromForm] ParamPriviewMobile pr)
        {
            try
            {
                sessionUser = SetSession();
                if (pr.lettertypeCode == 2 || pr.lettertypeCode == 6)
                {
                    ParamGetDetailLetter prLetter = new ParamGetDetailLetter();
                    prLetter.idLetter = pr.idLetter;

                    GeneralOutputModel retrn = _dataAccessProvider.GetDetailLetterSK(prLetter, sessionUser);
                    if (retrn.Status == "OK")
                    {
                        OutputDetailLetter letterDetail = new OutputDetailLetter();
                        var jsonApiResponseSerialize = JsonConvert.SerializeObject(retrn.Result);
                        letterDetail = JsonConvert.DeserializeObject<OutputDetailLetter>(jsonApiResponseSerialize);

                        var letternumber = letterDetail.letter.letterNumber;
                        letternumber = letternumber.Replace(".", "_");
                        var fileName = letternumber + ".pdf";
                        if (letterDetail.letter.statusCode != 4)
                        {
                            fileName = "Draft_" + letterDetail.letter.about + ".pdf";
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                        StringReader sr = new StringReader(sb.ToString());
                        //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                        Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                            pdfDoc.Open();
                            //Font
                            var fontName = "Calibri";
                            //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                            //FontFactory.Register(fontPath);

                            Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                            Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                            Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                            Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                            PdfPTable table = new PdfPTable(3);
                            float[] width = new float[] { 0.3f, 0.1f, 2f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_RIGHT;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 0f;
                            table.SpacingAfter = 0f;

                            PdfPCell cell = new PdfPCell();
                            cell.Border = 0;
                            Paragraph paragraph = new Paragraph("No", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(letterDetail.letter.letterNumber, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Perihal", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(letterDetail.letter.about, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Lampiran", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            var lampiran = letterDetail.letter.attachmentDesc == null ? "-" : letterDetail.letter.attachmentDesc;
                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(lampiran, bold);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //Add table to document
                            pdfDoc.Add(table);

                            table = new PdfPTable(1);
                            width = new float[] { 2f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 20f;
                            table.SpacingAfter = 0f;

                            cell = new PdfPCell();
                            table.AddCell(new Phrase("Jakarta, " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normal));
                            pdfDoc.Add(table);

                            table = new PdfPTable(1);
                            width = new float[] { 2f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 20f;
                            table.SpacingAfter = 0f;

                            cell = new PdfPCell();
                            table.AddCell(new Phrase("Kepada Yth, ", normal));
                            table.AddCell(new Phrase(letterDetail.outgoingRecipient[0].recipientCompany, bold));
                            table.AddCell(new Phrase(letterDetail.outgoingRecipient[0].recipientAddress, normal));
                            pdfDoc.Add(table);

                            table = new PdfPTable(1);
                            width = new float[] { 2f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 20f;
                            table.SpacingAfter = 0f;

                            cell = new PdfPCell();
                            int no = 1;
                            foreach (var item in letterDetail.outgoingRecipient)
                            {
                                if (no == 1)
                                {
                                    if (letterDetail.outgoingRecipient.Count() > 1)
                                    {
                                        table.AddCell(new Phrase("Up    " + no + ". " + item.recipientName, bold));
                                    }
                                    else
                                    {
                                        table.AddCell(new Phrase("Up    " + item.recipientName, bold));
                                    }

                                }
                                else
                                {
                                    table.AddCell(new Phrase("         " + no + ". " + item.recipientName, bold));
                                }
                                no++;
                            }
                            pdfDoc.Add(table);

                            //table = new PdfPTable(1);
                            //width = new float[] { 2f };
                            //table.WidthPercentage = 100;
                            //table.SetWidths(width);
                            //table.HorizontalAlignment = Element.ALIGN_LEFT;
                            //table.DefaultCell.Border = 0;
                            //table.SpacingBefore = 20f;
                            //table.SpacingAfter = 0f;

                            //cell = new PdfPCell();
                            //table.AddCell(new Phrase("Dengan hormat, ", normal));
                            //pdfDoc.Add(table);

                            //ADD HTML CONTENT
                            htmlparser.Parse(sr);

                            table = new PdfPTable(1);
                            width = new float[] { 2f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 20f;
                            table.SpacingAfter = 0f;

                            //cell = new PdfPCell();
                            //table.AddCell(new Phrase("Demikian kami sampaikan, Atas perhatian dan kerjasamanya, kami ucapkan terima kasih.", normal));
                            //pdfDoc.Add(table);

                            //table = new PdfPTable(1);
                            //width = new float[] { 2f };
                            //table.WidthPercentage = 100;
                            //table.SetWidths(width);
                            //table.HorizontalAlignment = Element.ALIGN_LEFT;
                            //table.DefaultCell.Border = 0;
                            //table.SpacingBefore = 20f;
                            //table.SpacingAfter = 0f;

                            //cell = new PdfPCell();
                            //table.AddCell(new Phrase("Hormat kami", normal));
                            //table.AddCell(new Phrase("PT BNI Life Insurance", normal));
                            //pdfDoc.Add(table);

                            table = new PdfPTable(1);
                            width = new float[] { 2f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;
                            table.DefaultCell.PaddingBottom = 8;
                            table.SpacingBefore = 10f;
                            table.SpacingAfter = 20f;

                            cell = new PdfPCell();
                            table.AddCell(new Phrase(" ", normal));
                            pdfDoc.Add(table);


                            string filename = letterDetail.sender[0].nip;
                            OutputNotifikasiLainnya qrR = new OutputNotifikasiLainnya();
                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                QRCodeModel myQRCode = new QRCodeModel();
                                myQRCode.QRCodeText = "NIP : " + letterDetail.sender[0].nip + "\nNAMA : " + letterDetail.sender[0].fullname + "\nJabatan : " + letterDetail.sender[0].positionName;
                                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                                using (QRCodeData qRCodeData = qrGenerator.CreateQrCode(
                                myQRCode.QRCodeText, QRCodeGenerator.ECCLevel.Q))

                                using (QRCode qRCode = new QRCode(qRCodeData))
                                {
                                    Bitmap qrCodeImage = qRCode.GetGraphic(3);
                                    byte[] BitmapArray = qrCodeImage.ConvertBitmapToByteArray();
                                    string urlImgQrcode = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
                                    qrR.NOTIFIKASI = urlImgQrcode;
                                }

                                if (letterDetail.letter.signatureType == 1)
                                {
                                    table = new PdfPTable(1);
                                    width = new float[] { 1f };
                                    table.WidthPercentage = 20;
                                    table.SetWidths(width);
                                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                                    table.DefaultCell.Border = 0;
                                    table.DefaultCell.PaddingBottom = 8;

                                    string filePath = Path.Combine("wwwroot\\uploads\\imgsignature\\" + filename + ".png");
                                    string filettd = Path.GetFileName(filePath);
                                    string pathttd = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/uploads/imgsignature/" + filettd;

                                    cell.Border = 0;
                                    Image imagettd = Image.GetInstance(pathttd);
                                    imagettd.ScaleToFit(2F, 2F);//Set width and height in float   
                                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    cell.AddElement(imagettd);
                                    table.AddCell(imagettd);
                                    pdfDoc.Add(table);
                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {
                                    table = new PdfPTable(1);
                                    width = new float[] { 1f };
                                    table.WidthPercentage = 20;
                                    table.SetWidths(width);
                                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                                    table.DefaultCell.Border = 0;
                                    table.DefaultCell.PaddingBottom = 8;

                                    Regex regex = new Regex(@"^data:image/(?<mediaType>[^;]+);base64,(?<data>.*)");
                                    Match match = regex.Match(qrR.NOTIFIKASI);
                                    Image imagettdqrcode = Image.GetInstance(
                                        Convert.FromBase64String(match.Groups["data"].Value)
                                    );
                                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    cell.AddElement(imagettdqrcode);
                                    table.AddCell(imagettdqrcode);
                                    pdfDoc.Add(table);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    table = new PdfPTable(2);
                                    width = new float[] { 1f, 1f };
                                    table.WidthPercentage = 20;
                                    table.SetWidths(width);
                                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                                    table.DefaultCell.Border = 0;
                                    table.DefaultCell.PaddingBottom = 8;

                                    string filePath = Path.Combine("wwwroot\\uploads\\imgsignature\\" + filename + ".png");
                                    string filettd = Path.GetFileName(filePath);
                                    string pathttd = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/uploads/imgsignature/" + filettd;
                                    //cell.Border = 0;
                                    Image imagettd = Image.GetInstance(pathttd);
                                    imagettd.ScaleToFit(2F, 2F);//Set width and height in float   
                                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    cell.AddElement(imagettd);
                                    table.AddCell(imagettd);

                                    Regex regex = new Regex(@"^data:image/(?<mediaType>[^;]+);base64,(?<data>.*)");
                                    Match match = regex.Match(qrR.NOTIFIKASI);
                                    Image imagettdqrcode = Image.GetInstance(
                                        Convert.FromBase64String(match.Groups["data"].Value)
                                    );
                                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    cell.AddElement(imagettdqrcode);
                                    table.AddCell(imagettdqrcode);
                                    pdfDoc.Add(table);
                                }
                            }

                            table = new PdfPTable(1);
                            width = new float[] { 2f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(letterDetail.sender[0].fullname, underlineBold);
                            //table.AddCell(new Phrase(letterDetail.sender[0].fullname, underlineBold));
                            cell.AddElement(paragraph);
                            table.AddCell(cell);
                            table.AddCell(new Phrase(letterDetail.sender[0].positionName, bold));
                            pdfDoc.Add(table);

                            pdfDoc.Close();

                            var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                            var letterNumber = letterDetail.letter.letterNumber;
                            byte[] bytess = ms.ToArray();
                            byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Surat Keluar");
                            ms.Close();

                            return File(bytes.ToArray(), "application/pdf", fileName);
                        }
                        //return Ok(retrn);


                    }
                    return BadRequest(retrn);
                }
                else if (pr.lettertypeCode == 3 || pr.lettertypeCode == 5)
                {
                    //GeneralOutputModel retrn2 = _dataAccessProvider.GetDataPriviewMobile_(pr, sessionUser);
                    return Ok("djamal ganteng");
                    //if (retrn2.Status == "OK")
                    //{
                    //    OutputDetailMemo letterDetail = new OutputDetailMemo();
                    //    var jsonApiResponseSerialize = JsonConvert.SerializeObject(retrn2.Result);
                    //    letterDetail = JsonConvert.DeserializeObject<OutputDetailMemo>(jsonApiResponseSerialize);

                    //    var letternumber = letterDetail.letter.letterNumber;
                    //    letternumber = letternumber.Replace(".", "_");
                    //    var fileName = letternumber + ".pdf";
                    //    //Surat Keputusan Direksi (Done)


                    //}
                }
                return BadRequest("Gagal");
            }
            catch (Exception ex)
            {
                output.Status = "NG";
                output.Message = ex.ToString();

                return BadRequest(output);
            }

        }


        #region Print Direksi
            //Done
            private FileContentResult PrivSuratKeputusanDireksi(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {
                #region Surat Keputusan Direksi

                StringBuilder sb = new StringBuilder();
                sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                StringReader sr = new StringReader(sb.ToString());
                //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 66.5f, 99.225f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

            
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                    pdfDoc.Open();
                    //Font
                    int no;
                    var fontName = "Calibri";
                    //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                    //FontFactory.Register(fontPath);

                    Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                    Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                    Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                    Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                    Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                    Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                    Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                    // Colors
                    BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                    BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));

                        PdfPTable table = new PdfPTable(2);
                        float[] width = new float[] { 0.667f, 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        PdfPCell cell = new PdfPCell();
                        Paragraph paragraph = new Paragraph();

                        #region header
                                table = new PdfPTable(1);
                                width = new float[] { 2f };
                                table.WidthPercentage = 100;
                                table.SetWidths(width);
                                table.HorizontalAlignment = Element.ALIGN_LEFT;
                                table.DefaultCell.Border = 0;
                                table.SpacingBefore = 00f;
                                table.SpacingAfter = 0f;

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(new Phrase("KEPUTUSAN DIREKSI PT BNI LIFE INSURANCE", bold));
                                paragraph.Alignment = Element.ALIGN_CENTER;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);
                                pdfDoc.Add(table);

                                table = new PdfPTable(1);
                                width = new float[] { 10f };
                                table.WidthPercentage = 100;
                                table.SetWidths(width);
                                table.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.DefaultCell.Border = 0;
                                table.SpacingBefore = 00f;
                                table.SpacingAfter = 0f;

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(new Phrase("NOMOR : " + letterDetail.letter.letterNumber, bold));
                                paragraph.Alignment = Element.ALIGN_CENTER;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);
                                pdfDoc.Add(table);


                                table = new PdfPTable(1);
                                width = new float[] { 10f };
                                table.WidthPercentage = 100;
                                table.SetWidths(width);
                                table.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.DefaultCell.Border = 0;
                                table.SpacingBefore = 00f;
                                table.SpacingAfter = 0f;

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(new Phrase("TANGGAL " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), bold));
                                paragraph.Alignment = Element.ALIGN_CENTER;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);
                                pdfDoc.Add(table);

                                table = new PdfPTable(1);
                                width = new float[] { 10f };
                                table.WidthPercentage = 100;
                                table.SetWidths(width);
                                table.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.DefaultCell.Border = 0;
                                table.SpacingBefore = 0f;
                                table.SpacingAfter = 0f;

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(new Phrase("TENTANG", bold));
                                paragraph.Alignment = Element.ALIGN_CENTER;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);
                                pdfDoc.Add(table);

                                table = new PdfPTable(1);
                                width = new float[] { 10f };
                                table.WidthPercentage = 100;
                                table.SetWidths(width);
                                table.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.DefaultCell.Border = 0;
                                table.SpacingBefore = 00f;
                                table.SpacingAfter = 0f;

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(new Phrase(letterDetail.letter.about, underlineBold));
                                paragraph.Alignment = Element.ALIGN_CENTER;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);
                                pdfDoc.Add(table);

                                    table = new PdfPTable(1);
                                    width = new float[] { 2f };
                                    table.WidthPercentage = 100;
                                    table.SetWidths(width);
                                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                                    table.DefaultCell.Border = 0;
                                    table.SpacingBefore = 00f;
                                    table.SpacingAfter = 0f;

                                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                                line.SpacingBefore = 0f;
                                line.SetLeading(2F, 0.5F);
                                pdfDoc.Add(line);
                                Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                                lines.SpacingBefore = 0f;
                                line.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(line);

                                #endregion
                        
                        
                        //ADD HTML CONTENT
                        htmlparser.Parse(sr);

                        #region Tanda Tangan

                        //Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        //a.SpacingBefore = 0f;
                        //a.SetLeading(0.5F, 0.5F);
                        //pdfDoc.Add(a);



                        cell = new PdfPCell();
                        table.AddCell(new Phrase("", normal));

                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                        //table.AddCell(new Phrase("Best Regards", normal));
                        pdfDoc.Add(table);



                        string filename = letterDetail.sender[0].nip;


                        // Tanda tangan
                        //var dirChecker = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                         var dirChecker = letterDetail.checker.Where(p => p.idLevelChecker == 2).OrderBy(p => p.idLevelChecker).ToList();

                        table = new PdfPTable(dirChecker.Count());
                        width = new float[dirChecker.Count()];
                        for (int i = 0; i < dirChecker.Count(); i++)
                        {
                            width[i] = 1f;
                        }
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        var no1 = 0;


                        //cell = new PdfPCell();
                        //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                        foreach (var item in dirChecker)
                        {
                            cell = new PdfPCell();
                            cell.Border = 0;




                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                    }

                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {


                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                    cell.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia1.SpacingBefore = 0f;
                                    linessia1.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia1);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell.AddElement(imagettd);
                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd != null)
                                    //{
                                    //    cell.AddElement(imagettd);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);
                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                                no1++;
                            }

                            paragraph = new Paragraph(item.fullname, underlineBold);
                            cell.AddElement(paragraph);
                            paragraph = new Paragraph(item.positionName, bold);
                            cell.AddElement(paragraph);

                            table.AddCell(cell);
                        }
                        // end tanda tangan
                        pdfDoc.Add(table);

                        #endregion

                        pdfDoc.NewPage();

                        #region Lampiran

                        PdfPTable table1 = new PdfPTable(2);
                        float[] width1 = new float[] { 0.667f, 2f };
                        table1.WidthPercentage = 100;
                        table1.SetWidths(width1);
                        table1.HorizontalAlignment = Element.ALIGN_LEFT;
                        table1.DefaultCell.Border = 0;
                        table1.SpacingBefore = 0f;
                        table1.SpacingAfter = 0f;

                        PdfPCell cell1 = new PdfPCell();
                        Paragraph paragraph1 = new Paragraph();

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        pdfDoc.Add(table1);

                        //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                        var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                        foreach (var item in checkerList)
                        {
                            var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                            var comment = "";
                            string approveDate = "";
                            if (getLogComment != null)
                            {
                                comment = getLogComment.comment;
                                approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                            }
                            table1 = new PdfPTable(3);
                            width = new float[] { 1f, 2f, 1f };
                            table1.WidthPercentage = 100;
                            table1.SetWidths(width);
                            table1.HorizontalAlignment = Element.ALIGN_LEFT;
                            table1.DefaultCell.Border = 0;
                            table1.SpacingBefore = 0f;
                            table1.SpacingAfter = 0f;

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            if (letterDetail.letter.statusCode == 2)
                            {
                                paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                            }
                            else if (letterDetail.letter.statusCode == 5)
                            {
                                paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                            }
                            else
                            {
                                if (item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                {
                                    paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                }
                                else
                                {
                                    paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);

                                }
                            }

                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(comment, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 40f;
                            paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            //cell1.AddElement(paragraph);
                            paragraph1 = new Paragraph(approveDate, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {
                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        
                                        cell1.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph1 = new Paragraph(img, normal);
                                        cell1.AddElement(paragraph1);
                                    }

                                    //string[] signatures = new string[] { item.idUserChecker.ToString() };
                                    //int count = 1;
                                    //foreach (var signature in signatures)
                                    //{

                                    //    Image imagettdd = GetSignatureImage(item.nip);
                                    //    //var pixelData = barcodeWriter.Write(signature);
                                    //    //Image img = Image.GetInstance(pixelData.Pixels, pixelData.Width, pixelData.Height);
                                    //    pdfDoc.Add(imagettdd);
                                    //    if (count == 1)
                                    //    {
                                    //        pdfDoc.Add(new Paragraph("\n"));
                                    //    }
                                    //    count++;
                                    //}
                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {
                                    //innertable1 = new PdfPtable1(2);
                                    //float[] widthinner = new float[] { 0.3f, 0.3f };
                                    //innertable1.WidthPercentage = 100;
                                    //innertable1.SetWidths(widthinner);
                                    //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                    //innertable1.DefaultCell.Border = 1;
                                    //innertable1.DefaultCell.PaddingBottom = 8;

                                    //innercell = new PdfPCell();
                                    //innercell.BorderWidthLeft = 1f;
                                    //innercell.BorderWidthRight = 1f;
                                    //innercell.BorderWidthTop = 1f;
                                    //innercell.BorderWidthBottom = 1f;




                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell1.AddElement(imagettd);

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd !=null)
                                    //{
                                    //    cell.AddElement(imagettd);

                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);

                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                            }

                            paragraph1 = new Paragraph(item.fullname, bold);
                            paragraph1.Alignment = Element.ALIGN_CENTER;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);


                            pdfDoc.Add(table1);
                        }


                        #endregion

                        pdfDoc.Close();

                        var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                        var letterNumber = letterDetail.letter.letterNumber;
                        byte[] bytess = ms.ToArray();
                        byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                        ms.Close();

                        return File(bytes.ToArray(), "application/pdf", fileName);
                }

                #endregion
            }

            // Done (Masi Ikuti Template Memo Direksi)
            private FileContentResult PrivSuratEdaranDireksi(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {
                #region Surat Edaran Direksi

                StringBuilder sb = new StringBuilder();
                sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                StringReader sr = new StringReader(sb.ToString());
                //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 66.5f, 99.225f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();
                        //Font
                        int no;
                        var fontName = "Calibri";
                        //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                        //FontFactory.Register(fontPath);

                        Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                        Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                        Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                        Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                        Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        // Colors
                        BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                        BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));

                

                        PdfPTable table = new PdfPTable(2);
                        float[] width = new float[] { 0.667f, 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        PdfPCell cell = new PdfPCell();
                        Paragraph paragraph = new Paragraph();


                        #region header
                        //add new page

                        //table = new PdfPTable(1);
                        //width = new float[] { 2f };
                        //table.WidthPercentage = 100;
                        //table.SetWidths(width);
                        //table.HorizontalAlignment = Element.ALIGN_LEFT;
                        //table.DefaultCell.Border = 0;
                        //table.SpacingBefore = 00f;
                        //table.SpacingAfter = 0f;


                        //table.AddCell(new Phrase("MEMO", bold));
                        //pdfDoc.Add(table);

                            table = new PdfPTable(2);
                            width = new float[] { 0.2f, 1f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_RIGHT;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 0f;
                            table.SpacingAfter = 0f;

                            cell.Border = 0;
                            paragraph = new Paragraph("Nomor", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+letterDetail.letter.letterNumber, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Tanggal", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Kepada", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            no = 1;
                            foreach (var item in letterDetail.receiver)
                            {
                                if (letterDetail.receiver.Count() > 1)
                                {
                                    paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                else
                                {
                                    paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                no++;
                            }
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Tembusan", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            no = 1;
                            foreach (var item in letterDetail.copy)
                            {
                                if (letterDetail.copy.Count() > 1)
                                {
                                    paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                else
                                {
                                    paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                no++;
                            }
                            if (letterDetail.copy.Count() == 0)
                            {
                                paragraph = new Paragraph("-", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Dari", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+letterDetail.sender[0].fullname + " - " + letterDetail.sender[0].positionName, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Perihal", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+letterDetail.letter.about, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Lampiran", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            var lampiran = letterDetail.letter.attachmentDesc == null ? "-" : letterDetail.letter.attachmentDesc;
                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+lampiran, bold);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //Add table to document
                            pdfDoc.Add(table);
              

                        Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                        line.SpacingBefore = 0f;
                        line.SetLeading(2F, 0.5F);
                        pdfDoc.Add(line);
                        Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                        lines.SpacingBefore = 0f;
                        line.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(line);

                        Paragraph liness = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        liness.SpacingBefore = 0f;
                        liness.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(liness);

                        //Paragraph linesss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        //linesss.SpacingBefore = 0f;
                        //linesss.SetLeading(0.5F, 0.5F);
                        //pdfDoc.Add(linesss);


                        table = new PdfPTable(1);
                        width = new float[] { 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 00f;
                        table.SpacingAfter = 0f;


                        //table.AddCell(new Phrase("Dengan hormat", normal));
                        //pdfDoc.Add(table);

                        Paragraph linessss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        linessss.SpacingBefore = 0f;
                        linessss.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(linessss);

                        Paragraph lin = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        lin.SpacingBefore = 0f;
                        lin.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(lin);

                        #endregion

                        //ADD HTML CONTENT
                        htmlparser.Parse(sr);


                        #region Tanda Tangan

                        Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        a.SpacingBefore = 0f;
                        a.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(a);



                        cell = new PdfPCell();
                        table.AddCell(new Phrase("", normal));

                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                        //table.AddCell(new Phrase("Best Regards", normal));
                        pdfDoc.Add(table);



                        string filename = letterDetail.sender[0].nip;


                        // Tanda tangan
                        var dirChecker = letterDetail.sender.OrderBy(p => p.idUserSender).ToList();
                        //var dirChecker = letterDetail.checker.Where(p => p.idLevelChecker == 2).ToList();


                        table = new PdfPTable(dirChecker.Count());
                        width = new float[dirChecker.Count()];
                        for (int i = 0; i < dirChecker.Count(); i++)
                        {
                            width[i] = 1f;
                        }
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        var no1 = 0;


                        //cell = new PdfPCell();
                        //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                        foreach (var item in dirChecker)
                        {
                            cell = new PdfPCell();
                            cell.Border = 0;

                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                    }

                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {


                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                    cell.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    liness.SpacingBefore = 0f;
                                    liness.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell.AddElement(imagettd);
                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd != null)
                                    //{
                                    //    cell.AddElement(imagettd);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);
                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                                no1++;
                            }

                            paragraph = new Paragraph(item.fullname, underlineBold);
                            cell.AddElement(paragraph);
                            paragraph = new Paragraph(item.positionName, bold);
                            cell.AddElement(paragraph);

                            table.AddCell(cell);
                        }
                        // end tanda tangan
                        pdfDoc.Add(table);

                        #endregion

                        pdfDoc.NewPage();

                        #region Lampiran

                        PdfPTable table1 = new PdfPTable(2);
                        float[] width1 = new float[] { 0.667f, 2f };
                        table1.WidthPercentage = 100;
                        table1.SetWidths(width1);
                        table1.HorizontalAlignment = Element.ALIGN_LEFT;
                        table1.DefaultCell.Border = 0;
                        table1.SpacingBefore = 0f;
                        table1.SpacingAfter = 0f;

                        PdfPCell cell1 = new PdfPCell();
                        Paragraph paragraph1 = new Paragraph();

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        pdfDoc.Add(table1);

                        //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                        var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                        foreach (var item in checkerList)
                        {
                            var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                            var comment = "";
                            string approveDate = "";
                            if (getLogComment != null)
                            {
                                comment = getLogComment.comment;
                                approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                            }
                            table1 = new PdfPTable(3);
                            width = new float[] { 1f, 2f, 1f };
                            table1.WidthPercentage = 100;
                            table1.SetWidths(width);
                            table1.HorizontalAlignment = Element.ALIGN_LEFT;
                            table1.DefaultCell.Border = 0;
                            table1.SpacingBefore = 0f;
                            table1.SpacingAfter = 0f;

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            if (letterDetail.letter.statusCode == 2)
                                {
                                    paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                                }
                                else if (letterDetail.letter.statusCode == 5)
                                {
                                    paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                                }
                                else
                                {
                                   if(item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                   { 
                                       paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                   }
                                   else
                                   {
                                       paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);
                                       
                                   }
                                }

                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(comment, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 40f;
                            paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            //cell1.AddElement(paragraph);
                            paragraph1 = new Paragraph(approveDate, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {
                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell1.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph1 = new Paragraph(img, normal);
                                        cell1.AddElement(paragraph1);
                                    }
                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    liness.SpacingBefore = 0f;
                                    liness.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {
                                    //innertable1 = new PdfPtable1(2);
                                    //float[] widthinner = new float[] { 0.3f, 0.3f };
                                    //innertable1.WidthPercentage = 100;
                                    //innertable1.SetWidths(widthinner);
                                    //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                    //innertable1.DefaultCell.Border = 1;
                                    //innertable1.DefaultCell.PaddingBottom = 8;

                                    //innercell = new PdfPCell();
                                    //innercell.BorderWidthLeft = 1f;
                                    //innercell.BorderWidthRight = 1f;
                                    //innercell.BorderWidthTop = 1f;
                                    //innercell.BorderWidthBottom = 1f;




                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell1.AddElement(imagettd);

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd !=null)
                                    //{
                                    //    cell.AddElement(imagettd);

                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);

                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                            }

                            paragraph1 = new Paragraph(item.fullname, bold);
                            paragraph1.Alignment = Element.ALIGN_CENTER;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);


                            pdfDoc.Add(table1);
                        }


                        #endregion
                        
                        pdfDoc.Close();

                        var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                        var letterNumber = letterDetail.letter.letterNumber;
                        byte[] bytess = ms.ToArray();
                        byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                        ms.Close();

                        return File(bytes.ToArray(), "application/pdf", fileName);
                    }

                #endregion
             }

            // Done (Masi Ikuti Template Memo Direksi)
            private FileContentResult PrivSuratPernyataanDireksi(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {
                #region Surat Pernyataan Direksi
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                    StringReader sr = new StringReader(sb.ToString());
                    //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                    //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                    Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 66.5f, 99.225f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();
                        //Font
                        int no;
                        var fontName = "Calibri";
                        //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                        //FontFactory.Register(fontPath);

                        Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                        Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                        Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                        Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                        Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        // Colors
                        BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                        BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));



                        PdfPTable table = new PdfPTable(2);
                        float[] width = new float[] { 0.667f, 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        PdfPCell cell = new PdfPCell();
                        Paragraph paragraph = new Paragraph();


                        #region header
                        //add new page

                        //table = new PdfPTable(1);
                        //width = new float[] { 2f };
                        //table.WidthPercentage = 100;
                        //table.SetWidths(width);
                        //table.HorizontalAlignment = Element.ALIGN_LEFT;
                        //table.DefaultCell.Border = 0;
                        //table.SpacingBefore = 00f;
                        //table.SpacingAfter = 0f;


                        //table.AddCell(new Phrase("MEMO", bold));
                        //pdfDoc.Add(table);

                            table = new PdfPTable(2);
                            width = new float[] { 0.2f, 1f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_RIGHT;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 0f;
                            table.SpacingAfter = 0f;

                            cell.Border = 0;
                            paragraph = new Paragraph("Nomor", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+letterDetail.letter.letterNumber, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Tanggal", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Kepada", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            no = 1;
                            foreach (var item in letterDetail.receiver)
                            {
                                if (letterDetail.receiver.Count() > 1)
                                {
                                    paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                else
                                {
                                    paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                no++;
                            }
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Tembusan", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            no = 1;
                            foreach (var item in letterDetail.copy)
                            {
                                if (letterDetail.copy.Count() > 1)
                                {
                                    paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                else
                                {
                                    paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                no++;
                            }
                            if (letterDetail.copy.Count() == 0)
                            {
                                paragraph = new Paragraph("-", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Dari", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+letterDetail.sender[0].fullname + " - " + letterDetail.sender[0].positionName, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Perihal", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+letterDetail.letter.about, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Lampiran", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            var lampiran = letterDetail.letter.attachmentDesc == null ? "-" : letterDetail.letter.attachmentDesc;
                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+lampiran, bold);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //Add table to document
                            pdfDoc.Add(table);


                        Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                        line.SpacingBefore = 0f;
                        line.SetLeading(2F, 0.5F);
                        pdfDoc.Add(line);
                        Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                        lines.SpacingBefore = 0f;
                        line.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(line);

                        Paragraph liness = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        liness.SpacingBefore = 0f;
                        liness.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(liness);

                        //Paragraph linesss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        //linesss.SpacingBefore = 0f;
                        //linesss.SetLeading(0.5F, 0.5F);
                        //pdfDoc.Add(linesss);


                        table = new PdfPTable(1);
                        width = new float[] { 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 00f;
                        table.SpacingAfter = 0f;


                        //table.AddCell(new Phrase("Dengan hormat", normal));
                        //pdfDoc.Add(table);

                        Paragraph linessss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        linessss.SpacingBefore = 0f;
                        linessss.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(linessss);

                        Paragraph lin = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        lin.SpacingBefore = 0f;
                        lin.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(lin);

                        #endregion

                        //ADD HTML CONTENT
                        htmlparser.Parse(sr);


                        #region Tanda Tangan

                        Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        a.SpacingBefore = 0f;
                        a.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(a);



                        cell = new PdfPCell();
                        table.AddCell(new Phrase("", normal));

                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                        //table.AddCell(new Phrase("Best Regards", normal));
                        pdfDoc.Add(table);



                        string filename = letterDetail.sender[0].nip;


                        // Tanda tangan
                        var dirChecker = letterDetail.sender.OrderBy(p => p.idUserSender).ToList();
                        //var dirChecker = letterDetail.checker.Where(p => p.idLevelChecker == 2).ToList();

                        table = new PdfPTable(dirChecker.Count());
                        width = new float[dirChecker.Count()];
                        for (int i = 0; i < dirChecker.Count(); i++)
                        {
                            width[i] = 1f;
                        }
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        var no1 = 0;


                        //cell = new PdfPCell();
                        //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                        foreach (var item in dirChecker)
                        {
                            cell = new PdfPCell();
                            cell.Border = 0;




                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                    }

                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {


                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                    cell.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell.AddElement(imagettd);
                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd != null)
                                    //{
                                    //    cell.AddElement(imagettd);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);
                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                                no1++;
                            }

                            paragraph = new Paragraph(item.fullname, underlineBold);
                            cell.AddElement(paragraph);
                            paragraph = new Paragraph(item.positionName, bold);
                            cell.AddElement(paragraph);

                            table.AddCell(cell);
                        }
                        // end tanda tangan
                        pdfDoc.Add(table);

                        #endregion

                        pdfDoc.NewPage();

                        #region Lampiran

                        PdfPTable table1 = new PdfPTable(2);
                        float[] width1 = new float[] { 0.667f, 2f };
                        table1.WidthPercentage = 100;
                        table1.SetWidths(width1);
                        table1.HorizontalAlignment = Element.ALIGN_LEFT;
                        table1.DefaultCell.Border = 0;
                        table1.SpacingBefore = 0f;
                        table1.SpacingAfter = 0f;

                        PdfPCell cell1 = new PdfPCell();
                        Paragraph paragraph1 = new Paragraph();

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        pdfDoc.Add(table1);

                        //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                        var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                        foreach (var item in checkerList)
                        {
                            var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                            var comment = "";
                            string approveDate = "";
                            if (getLogComment != null)
                            {
                                comment = getLogComment.comment;
                                approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                            }
                            table1 = new PdfPTable(3);
                            width = new float[] { 1f, 2f, 1f };
                            table1.WidthPercentage = 100;
                            table1.SetWidths(width);
                            table1.HorizontalAlignment = Element.ALIGN_LEFT;
                            table1.DefaultCell.Border = 0;
                            table1.SpacingBefore = 0f;
                            table1.SpacingAfter = 0f;

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            if (letterDetail.letter.statusCode == 2)
                                {
                                    paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                                }
                                else if (letterDetail.letter.statusCode == 5)
                                {
                                    paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                                }
                                else
                                {
                                   if(item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                   { 
                                       paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                   }
                                   else
                                   {
                                       paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);
                                       
                                   }
                                }

                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(comment, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 40f;
                            paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            //cell1.AddElement(paragraph);
                            paragraph1 = new Paragraph(approveDate, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {
                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell1.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph1 = new Paragraph(img, normal);
                                        cell1.AddElement(paragraph1);
                                    }
                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {
                                    //innertable1 = new PdfPtable1(2);
                                    //float[] widthinner = new float[] { 0.3f, 0.3f };
                                    //innertable1.WidthPercentage = 100;
                                    //innertable1.SetWidths(widthinner);
                                    //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                    //innertable1.DefaultCell.Border = 1;
                                    //innertable1.DefaultCell.PaddingBottom = 8;

                                    //innercell = new PdfPCell();
                                    //innercell.BorderWidthLeft = 1f;
                                    //innercell.BorderWidthRight = 1f;
                                    //innercell.BorderWidthTop = 1f;
                                    //innercell.BorderWidthBottom = 1f;




                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell1.AddElement(imagettd);

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd !=null)
                                    //{
                                    //    cell.AddElement(imagettd);

                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);

                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                            }

                            paragraph1 = new Paragraph(item.fullname, bold);
                            paragraph1.Alignment = Element.ALIGN_CENTER;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);


                            pdfDoc.Add(table1);
                        }


                        #endregion

                        pdfDoc.Close();

                        var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                        var letterNumber = letterDetail.letter.letterNumber;
                        byte[] bytess = ms.ToArray();
                        byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                        ms.Close();

                        return File(bytes.ToArray(), "application/pdf", fileName);
                    }

                #endregion
            }

            // done 
            private FileContentResult PrivSuratKeteranganDireksi(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {
                #region Surat Keterangan Direksi

                StringBuilder sb = new StringBuilder();
                sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                StringReader sr = new StringReader(sb.ToString());
                //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 66.5f, 99.225f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                    pdfDoc.Open();
                    //Font
                    int no;
                    var fontName = "Calibri";
                    //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                    //FontFactory.Register(fontPath);

                    Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                    Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                    Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                    Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                    Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                    Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                    Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                    // Colors
                    BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                    BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));

                    PdfPTable table = new PdfPTable(2);
                    float[] width = new float[] { 0.667f, 2f };
                    table.WidthPercentage = 100;
                    table.SetWidths(width);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.DefaultCell.Border = 0;
                    table.SpacingBefore = 0f;
                    table.SpacingAfter = 0f;

                    PdfPCell cell = new PdfPCell();
                    Paragraph paragraph = new Paragraph();


                    #region header

                    table = new PdfPTable(1);
                    width = new float[] { 2f };
                    table.WidthPercentage = 100;
                    table.SetWidths(width);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.DefaultCell.Border = 0;
                    table.SpacingBefore = 00f;
                    table.SpacingAfter = 0f;

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph(new Phrase("Surat Keterangan", bold));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    pdfDoc.Add(table);

                    table = new PdfPTable(1);
                    width = new float[] { 10f };
                    table.WidthPercentage = 100;
                    table.SetWidths(width);
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.DefaultCell.Border = 0;
                    table.SpacingBefore = 00f;
                    table.SpacingAfter = 0f;

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph(new Phrase("NOMOR : " + letterDetail.letter.letterNumber, normal));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    pdfDoc.Add(table);


                    Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                    line.SpacingBefore = 0f;
                    line.SetLeading(2F, 0.5F);
                    pdfDoc.Add(line);
                    Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                    lines.SpacingBefore = 0f;
                    line.SetLeading(0.5F, 0.5F);
                    pdfDoc.Add(line);

                    Paragraph liness = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                    liness.SpacingBefore = 0f;
                    liness.SetLeading(0.5F, 0.5F);
                    pdfDoc.Add(liness);

                    //Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                    //a.SpacingBefore = 0f;
                    //a.SetLeading(0.5F, 0.5F);
                    //pdfDoc.Add(a);

                    //Paragraph aaw = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                    //aaw.SpacingBefore = 0f;
                    //aaw.SetLeading(0.5F, 0.5F);
                    //pdfDoc.Add(aaw);
                    
                    #endregion



                    //ADD HTML CONTENT
                    htmlparser.Parse(sr);

                    #region Tanda Tangan

                    Paragraph ac = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                    ac.SpacingBefore = 0f;
                    ac.SetLeading(0.5F, 0.5F);
                    pdfDoc.Add(ac);



                    cell = new PdfPCell();
                    table.AddCell(new Phrase("", normal));

                    cell = new PdfPCell();
                    table.AddCell(new Phrase(" ", normal));
                    cell = new PdfPCell();
                    table.AddCell(new Phrase(" ", normal));
                    cell = new PdfPCell();
                    table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                    table.AddCell(new Phrase("Best Regards", normal));
                    pdfDoc.Add(table);



                    string filename = letterDetail.sender[0].nip;


                    // Tanda tangan
                    var dirChecker = letterDetail.sender.OrderBy(p => p.idUserSender).ToList();
                    //var dirChecker = letterDetail.checker.Where(p => p.idLevelChecker == 2 ).ToList();

                    table = new PdfPTable(dirChecker.Count());
                    width = new float[dirChecker.Count()];
                    for (int i = 0; i < dirChecker.Count(); i++)
                    {
                        width[i] = 1f;
                    }
                    table.WidthPercentage = 100;
                    table.SetWidths(width);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.DefaultCell.Border = 0;
                    var no1 = 0;


                    //cell = new PdfPCell();
                    //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                    foreach (var item in dirChecker)
                    {
                        cell = new PdfPCell();
                        cell.Border = 0;




                        if (letterDetail.letter.letterNumber != "NO_LETTER")
                        {
                            if (letterDetail.letter.signatureType == 1)
                            {

                                Image imagettd = GetSignatureImage(item.nip);
                                //cell.AddElement(imagettd);
                                if (imagettd != null)
                                {
                                    cell.AddElement(imagettd);
                                }
                                else
                                {
                                    var img = "File tanda tangan tidak ditemukan";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                }

                            }
                            else if (letterDetail.letter.signatureType == 2)
                            {


                                Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                cell.AddElement(QRCodeSignatureApprover);
                            }
                            else if (letterDetail.letter.signatureType == 3)
                            {
                                var img = "";
                                paragraph = new Paragraph(img, normal);
                                cell.AddElement(paragraph);
                                Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessia.SpacingBefore = 0f;
                                linessia.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessia);

                                Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessiaa.SpacingBefore = 0f;
                                linessiaa.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessiaa);

                                Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessiaaa.SpacingBefore = 0f;
                                linessiaaa.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessiaaa);
                            }
                            else
                            {

                                Image imagettd = GetSignatureImage(item.nip);
                                cell.AddElement(imagettd);
                                Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                cell.AddElement(QRCodeSignatureApprover);
                                //if (imagettd != null)
                                //{
                                //    cell.AddElement(imagettd);
                                //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                //    cell.AddElement(QRCodeSignatureApprover);
                                //}
                                //else
                                //{
                                //    var img = "File tanda tangan tidak di temukan";
                                //    paragraph = new Paragraph(img, normal);
                                //    cell.AddElement(paragraph);
                                //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                //    cell.AddElement(QRCodeSignatureApprover);
                                //}

                            }
                            no1++;
                        }

                        paragraph = new Paragraph(item.fullname, underlineBold);
                        cell.AddElement(paragraph);
                        paragraph = new Paragraph(item.positionName, bold);
                        cell.AddElement(paragraph);

                        table.AddCell(cell);
                    }
                    // end tanda tangan
                    pdfDoc.Add(table);

                    #endregion

                    pdfDoc.NewPage();

                    #region Lampiran

                    PdfPTable table1 = new PdfPTable(2);
                    float[] width1 = new float[] { 0.667f, 2f };
                    table1.WidthPercentage = 100;
                    table1.SetWidths(width1);
                    table1.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.DefaultCell.Border = 0;
                    table1.SpacingBefore = 0f;
                    table1.SpacingAfter = 0f;

                    PdfPCell cell1 = new PdfPCell();
                    Paragraph paragraph1 = new Paragraph();

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 1f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 1f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 30f;
                    paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    table1.AddCell(cell1);

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 0f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 1f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 30f;
                    paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    table1.AddCell(cell1);

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 1f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 0f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 100f;
                    paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    table1.AddCell(cell1);

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 0f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 0f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 100f;
                    paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    table1.AddCell(cell1);

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 1f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 0f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 100f;
                    paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    no = 1;
                    foreach (var item in letterDetail.delibration)
                    {
                        if (letterDetail.receiver.Count() > 1)
                        {
                            paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                        }
                        else
                        {
                            paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                        }
                        no++;
                    }
                    table1.AddCell(cell1);

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 0f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 0f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 100f;
                    paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    no = 1;
                    foreach (var item in letterDetail.delibration)
                    {
                        if (letterDetail.receiver.Count() > 1)
                        {
                            paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                        }
                        else
                        {
                            paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                        }
                        no++;
                    }
                    table1.AddCell(cell1);

                    pdfDoc.Add(table1);

                    //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                    var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                    foreach (var item in checkerList)
                    {
                        var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                        var comment = "";
                        string approveDate = "";
                        if (getLogComment != null)
                        {
                            comment = getLogComment.comment;
                            approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                        }
                        table1 = new PdfPTable(3);
                        width = new float[] { 1f, 2f, 1f };
                        table1.WidthPercentage = 100;
                        table1.SetWidths(width);
                        table1.HorizontalAlignment = Element.ALIGN_LEFT;
                        table1.DefaultCell.Border = 0;
                        table1.SpacingBefore = 0f;
                        table1.SpacingAfter = 0f;

                        cell1 = new PdfPCell();
                        cell1.Rowspan = 2;
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 120f;
                        if (letterDetail.letter.statusCode == 2)
                        {
                            paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                        }
                        else if (letterDetail.letter.statusCode == 5)
                        {
                            paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                        }
                        else
                        {
                            if (item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                            {
                                paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                            }
                            else
                            {
                                paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);

                            }
                        }

                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.Rowspan = 2;
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 120f;
                        paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        paragraph1 = new Paragraph(comment, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 40f;
                        paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        //cell1.AddElement(paragraph);
                        paragraph1 = new Paragraph(approveDate, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        if (letterDetail.letter.letterNumber != "NO_LETTER")
                        {
                            if (letterDetail.letter.signatureType == 1)
                            {
                                Image imagettd = GetSignatureImage(item.nip);
                                //cell.AddElement(imagettd);
                                if (imagettd != null)
                                {
                                    cell1.AddElement(imagettd);
                                }
                                else
                                {
                                    var img = "File tanda tangan tidak ditemukan";
                                    paragraph1 = new Paragraph(img, normal);
                                    cell1.AddElement(paragraph1);
                                }
                            }
                            else if (letterDetail.letter.signatureType == 2)
                            {

                                Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                cell1.AddElement(QRCodeSignatureApprover);
                            }
                            else if (letterDetail.letter.signatureType == 3)
                            {
                                var img = "";
                                paragraph = new Paragraph(img, normal);
                                cell.AddElement(paragraph);
                                Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessia.SpacingBefore = 0f;
                                linessia.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessia);

                                Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessiaa.SpacingBefore = 0f;
                                linessiaa.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessiaa);

                                Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessiaaa.SpacingBefore = 0f;
                                linessiaaa.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessiaaa);
                            }
                            else
                            {
                                //innertable1 = new PdfPtable1(2);
                                //float[] widthinner = new float[] { 0.3f, 0.3f };
                                //innertable1.WidthPercentage = 100;
                                //innertable1.SetWidths(widthinner);
                                //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                //innertable1.DefaultCell.Border = 1;
                                //innertable1.DefaultCell.PaddingBottom = 8;

                                //innercell = new PdfPCell();
                                //innercell.BorderWidthLeft = 1f;
                                //innercell.BorderWidthRight = 1f;
                                //innercell.BorderWidthTop = 1f;
                                //innercell.BorderWidthBottom = 1f;




                                Image imagettd = GetSignatureImage(item.nip);
                                cell1.AddElement(imagettd);

                                Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                cell1.AddElement(QRCodeSignatureApprover);
                                //if (imagettd !=null)
                                //{
                                //    cell.AddElement(imagettd);

                                //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                //    cell.AddElement(QRCodeSignatureApprover);
                                //}
                                //else
                                //{
                                //    var img = "File tanda tangan tidak di temukan";
                                //    paragraph = new Paragraph(img, normal);

                                //    cell.AddElement(paragraph);
                                //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                //    cell.AddElement(QRCodeSignatureApprover);
                                //}

                            }
                        }

                        paragraph1 = new Paragraph(item.fullname, bold);
                        paragraph1.Alignment = Element.ALIGN_CENTER;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);


                        pdfDoc.Add(table1);
                    }


                    #endregion

                    pdfDoc.Close();

                    var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                    var letterNumber = letterDetail.letter.letterNumber;
                    byte[] bytess = ms.ToArray();
                    byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                    ms.Close();

                    return File(bytes.ToArray(), "application/pdf", fileName);

                }
                
                #endregion
            }

            // Done (Masi Ikuti Template Memo Direksi)
            private FileContentResult PrivSuratKuasaOperasional(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {
                #region Surat Kuasa Operasional
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                    StringReader sr = new StringReader(sb.ToString());
                    //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                    //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                    Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 66.5f, 99.225f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                    using (MemoryStream ms = new MemoryStream())
                    {
                            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                            pdfDoc.Open();
                            //Font
                            int no;
                            var fontName = "Calibri";
                            //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                            //FontFactory.Register(fontPath);

                            Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                            Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                            Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                            Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                            Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                            Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                            Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                            // Colors
                            BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                            BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));



                            PdfPTable table = new PdfPTable(2);
                            float[] width = new float[] { 0.667f, 2f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 0f;
                            table.SpacingAfter = 0f;

                            PdfPCell cell = new PdfPCell();
                            Paragraph paragraph = new Paragraph();


                            #region header
                            //add new page

                            //table = new PdfPTable(1);
                            //width = new float[] { 2f };
                            //table.WidthPercentage = 100;
                            //table.SetWidths(width);
                            //table.HorizontalAlignment = Element.ALIGN_LEFT;
                            //table.DefaultCell.Border = 0;
                            //table.SpacingBefore = 00f;
                            //table.SpacingAfter = 0f;


                            //table.AddCell(new Phrase("MEMO", bold));
                            //pdfDoc.Add(table);

                            table = new PdfPTable(2);
                            width = new float[] { 0.2f, 1f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_RIGHT;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 0f;
                            table.SpacingAfter = 0f;

                            cell.Border = 0;
                            paragraph = new Paragraph("Nomor", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+letterDetail.letter.letterNumber, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Tanggal", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Kepada", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            no = 1;
                            foreach (var item in letterDetail.receiver)
                            {
                                if (letterDetail.receiver.Count() > 1)
                                {
                                    paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                else
                                {
                                    paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                no++;
                            }
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Tembusan", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            no = 1;
                            foreach (var item in letterDetail.copy)
                            {
                                if (letterDetail.copy.Count() > 1)
                                {
                                    paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                else
                                {
                                    paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                no++;
                            }
                            if (letterDetail.copy.Count() == 0)
                            {
                                paragraph = new Paragraph("-", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Dari", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+letterDetail.sender[0].fullname + " - " + letterDetail.sender[0].positionName, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Perihal", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+letterDetail.letter.about, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph("Lampiran", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //cell = new PdfPCell();
                            //cell.Border = 0;
                            //paragraph = new Paragraph(":", normal);
                            //paragraph.Alignment = Element.ALIGN_RIGHT;
                            //cell.AddElement(paragraph);
                            //table.AddCell(cell);

                            var lampiran = letterDetail.letter.attachmentDesc == null ? "-" : letterDetail.letter.attachmentDesc;
                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(":"+" "+lampiran, bold);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);

                            //Add table to document
                            pdfDoc.Add(table);


                            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                            line.SpacingBefore = 0f;
                            line.SetLeading(2F, 0.5F);
                            pdfDoc.Add(line);
                            Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                            lines.SpacingBefore = 0f;
                            line.SetLeading(0.5F, 0.5F);
                            pdfDoc.Add(line);

                            Paragraph liness = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                            liness.SpacingBefore = 0f;
                            liness.SetLeading(0.5F, 0.5F);
                            pdfDoc.Add(liness);

                            //Paragraph linesss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                            //linesss.SpacingBefore = 0f;
                            //linesss.SetLeading(0.5F, 0.5F);
                            //pdfDoc.Add(linesss);


                            table = new PdfPTable(1);
                            width = new float[] { 2f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 00f;
                            table.SpacingAfter = 0f;


                            //table.AddCell(new Phrase("Dengan hormat", normal));
                            //pdfDoc.Add(table);

                            Paragraph linessss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                            linessss.SpacingBefore = 0f;
                            linessss.SetLeading(0.5F, 0.5F);
                            pdfDoc.Add(linessss);

                            Paragraph lin = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                            lin.SpacingBefore = 0f;
                            lin.SetLeading(0.5F, 0.5F);
                            pdfDoc.Add(lin);

                            #endregion

                            //ADD HTML CONTENT
                            htmlparser.Parse(sr);

                            
                            #region Tanda Tangan

                            Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                            a.SpacingBefore = 0f;
                            a.SetLeading(0.5F, 0.5F);
                            pdfDoc.Add(a);



                            cell = new PdfPCell();
                            table.AddCell(new Phrase("", normal));

                            cell = new PdfPCell();
                            table.AddCell(new Phrase(" ", normal));
                            cell = new PdfPCell();
                            table.AddCell(new Phrase(" ", normal));
                            cell = new PdfPCell();
                            table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                            //table.AddCell(new Phrase("Best Regards", normal));
                            pdfDoc.Add(table);



                            string filename = letterDetail.sender[0].nip;


                            // Tanda tangan
                            //var dirChecker = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                            var dirChecker = letterDetail.checker.Where(p => p.idLevelChecker == 2 ).ToList();

                            table = new PdfPTable(dirChecker.Count());
                            width = new float[dirChecker.Count()];
                            for (int i = 0; i < dirChecker.Count(); i++)
                            {
                                width[i] = 1f;
                            }
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;
                            var no1 = 0;


                            //cell = new PdfPCell();
                            //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                            foreach (var item in dirChecker)
                            {
                                cell = new PdfPCell();
                                cell.Border = 0;




                                if (letterDetail.letter.letterNumber != "NO_LETTER")
                                {
                                    if (letterDetail.letter.signatureType == 1)
                                    {

                                        Image imagettd = GetSignatureImage(item.nip);
                                        //cell.AddElement(imagettd);
                                        if (imagettd != null)
                                        {
                                            cell.AddElement(imagettd);
                                        }
                                        else
                                        {
                                            var img = "File tanda tangan tidak ditemukan";
                                            paragraph = new Paragraph(img, normal);
                                            cell.AddElement(paragraph);
                                        }

                                    }
                                    else if (letterDetail.letter.signatureType == 2)
                                    {


                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                        cell.AddElement(QRCodeSignatureApprover);
                                    }
                                    else if (letterDetail.letter.signatureType == 3)
                                    {
                                        var img = "";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                        Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessia.SpacingBefore = 0f;
                                        linessia.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessia);

                                        Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaa.SpacingBefore = 0f;
                                        linessiaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaa);

                                        Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaaa.SpacingBefore = 0f;
                                        linessiaaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaaa);
                                    }
                                    else
                                    {

                                        Image imagettd = GetSignatureImage(item.nip);
                                        cell.AddElement(imagettd);
                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        cell.AddElement(QRCodeSignatureApprover);
                                        //if (imagettd != null)
                                        //{
                                        //    cell.AddElement(imagettd);
                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}
                                        //else
                                        //{
                                        //    var img = "File tanda tangan tidak di temukan";
                                        //    paragraph = new Paragraph(img, normal);
                                        //    cell.AddElement(paragraph);
                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}

                                    }
                                    no1++;
                                }

                                paragraph = new Paragraph(item.fullname, underlineBold);
                                cell.AddElement(paragraph);
                                paragraph = new Paragraph(item.positionName, bold);
                                cell.AddElement(paragraph);

                                table.AddCell(cell);
                            }
                            // end tanda tangan
                            pdfDoc.Add(table);

                            #endregion

                            pdfDoc.NewPage();

                            #region Lampiran

                            PdfPTable table1 = new PdfPTable(2);
                            float[] width1 = new float[] { 0.667f, 2f };
                            table1.WidthPercentage = 100;
                            table1.SetWidths(width1);
                            table1.HorizontalAlignment = Element.ALIGN_LEFT;
                            table1.DefaultCell.Border = 0;
                            table1.SpacingBefore = 0f;
                            table1.SpacingAfter = 0f;

                            PdfPCell cell1 = new PdfPCell();
                            Paragraph paragraph1 = new Paragraph();

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 1f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 30f;
                            paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 1f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 30f;
                            paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            no = 1;
                            foreach (var item in letterDetail.delibration)
                            {
                                if (letterDetail.receiver.Count() > 1)
                                {
                                    paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                else
                                {
                                    paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                no++;
                            }
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            no = 1;
                            foreach (var item in letterDetail.delibration)
                            {
                                if (letterDetail.receiver.Count() > 1)
                                {
                                    paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                else
                                {
                                    paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                no++;
                            }
                            table1.AddCell(cell1);

                            pdfDoc.Add(table1);

                            //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                            var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                            foreach (var item in checkerList)
                            {
                                var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                                var comment = "";
                                string approveDate = "";
                                if (getLogComment != null)
                                {
                                    comment = getLogComment.comment;
                                    approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                                }
                                table1 = new PdfPTable(3);
                                width = new float[] { 1f, 2f, 1f };
                                table1.WidthPercentage = 100;
                                table1.SetWidths(width);
                                table1.HorizontalAlignment = Element.ALIGN_LEFT;
                                table1.DefaultCell.Border = 0;
                                table1.SpacingBefore = 0f;
                                table1.SpacingAfter = 0f;

                                cell1 = new PdfPCell();
                                cell1.Rowspan = 2;
                                cell1.BorderWidthLeft = 1f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                cell1.MinimumHeight = 120f;
                                if (letterDetail.letter.statusCode == 2)
                                {
                                    paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                                }
                                else if (letterDetail.letter.statusCode == 5)
                                {
                                    paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                                }
                                else
                                {
                                   if(item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                   { 
                                       paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                   }
                                   else
                                   {
                                       paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);
                                       
                                   }
                                }

                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);

                                cell1 = new PdfPCell();
                                cell1.Rowspan = 2;
                                cell1.BorderWidthLeft = 0f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                cell1.MinimumHeight = 120f;
                                paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                paragraph1 = new Paragraph(comment, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);

                                cell1 = new PdfPCell();
                                cell1.BorderWidthLeft = 0f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                cell1.MinimumHeight = 40f;
                                paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                //cell1.AddElement(paragraph);
                                paragraph1 = new Paragraph(approveDate, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);

                                cell1 = new PdfPCell();
                                cell1.BorderWidthLeft = 0f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                if (letterDetail.letter.letterNumber != "NO_LETTER")
                                {
                                    if (letterDetail.letter.signatureType == 1)
                                    {
                                        Image imagettd = GetSignatureImage(item.nip);
                                        //cell.AddElement(imagettd);
                                        if (imagettd != null)
                                        {
                                            cell1.AddElement(imagettd);
                                        }
                                        else
                                        {
                                            var img = "File tanda tangan tidak ditemukan";
                                            paragraph1 = new Paragraph(img, normal);
                                            cell1.AddElement(paragraph1);
                                        }
                                    }
                                    else if (letterDetail.letter.signatureType == 2)
                                    {

                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        cell1.AddElement(QRCodeSignatureApprover);
                                    }
                                    else if (letterDetail.letter.signatureType == 3)
                                    {
                                        var img = "";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                        Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessia.SpacingBefore = 0f;
                                        linessia.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessia);

                                        Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaa.SpacingBefore = 0f;
                                        linessiaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaa);

                                        Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaaa.SpacingBefore = 0f;
                                        linessiaaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaaa);
                                    }
                                    else
                                    {
                                        //innertable1 = new PdfPtable1(2);
                                        //float[] widthinner = new float[] { 0.3f, 0.3f };
                                        //innertable1.WidthPercentage = 100;
                                        //innertable1.SetWidths(widthinner);
                                        //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                        //innertable1.DefaultCell.Border = 1;
                                        //innertable1.DefaultCell.PaddingBottom = 8;

                                        //innercell = new PdfPCell();
                                        //innercell.BorderWidthLeft = 1f;
                                        //innercell.BorderWidthRight = 1f;
                                        //innercell.BorderWidthTop = 1f;
                                        //innercell.BorderWidthBottom = 1f;




                                        Image imagettd = GetSignatureImage(item.nip);
                                        cell1.AddElement(imagettd);

                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        cell1.AddElement(QRCodeSignatureApprover);
                                        //if (imagettd !=null)
                                        //{
                                        //    cell.AddElement(imagettd);

                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}
                                        //else
                                        //{
                                        //    var img = "File tanda tangan tidak di temukan";
                                        //    paragraph = new Paragraph(img, normal);

                                        //    cell.AddElement(paragraph);
                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}

                                    }
                                }

                                paragraph1 = new Paragraph(item.fullname, bold);
                                paragraph1.Alignment = Element.ALIGN_CENTER;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);


                                pdfDoc.Add(table1);
                            }


                            #endregion

                            pdfDoc.Close();

                            var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                            var letterNumber = letterDetail.letter.letterNumber;
                            byte[] bytess = ms.ToArray();
                            byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                            ms.Close();

                            return File(bytes.ToArray(), "application/pdf", fileName);
                    }

                #endregion
            }

        #endregion

        #region Print Dewan Komisaris

            //Done(Masi Ikuti Template Memo Direksi)
            private FileContentResult PrivSuratKuasaDewanKom(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {

                #region Surat Kuasa Operasional

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                    StringReader sr = new StringReader(sb.ToString());
                    //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                    //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                    Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 66.5f, 99.225f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();
                        //Font
                        int no;
                        var fontName = "Calibri";
                        //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                        //FontFactory.Register(fontPath);

                        Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                        Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                        Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                        Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                        Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        // Colors
                        BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                        BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));



                        PdfPTable table = new PdfPTable(2);
                        float[] width = new float[] { 0.667f, 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        PdfPCell cell = new PdfPCell();
                        Paragraph paragraph = new Paragraph();


                        #region header
                        //add new page

                        //table = new PdfPTable(1);
                        //width = new float[] { 2f };
                        //table.WidthPercentage = 100;
                        //table.SetWidths(width);
                        //table.HorizontalAlignment = Element.ALIGN_LEFT;
                        //table.DefaultCell.Border = 0;
                        //table.SpacingBefore = 00f;
                        //table.SpacingAfter = 0f;


                        //table.AddCell(new Phrase("MEMO", bold));
                        //pdfDoc.Add(table);

                        table = new PdfPTable(2);
                        width = new float[] { 0.2f, 1f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        cell.Border = 0;
                        paragraph = new Paragraph("Nomor", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+letterDetail.letter.letterNumber, normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Tanggal", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Kepada", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        no = 1;
                        foreach (var item in letterDetail.receiver)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            else
                            {
                                paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            no++;
                        }
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Tembusan", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        no = 1;
                        foreach (var item in letterDetail.copy)
                        {
                            if (letterDetail.copy.Count() > 1)
                            {
                                paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            else
                            {
                                paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            no++;
                        }
                        if (letterDetail.copy.Count() == 0)
                        {
                            paragraph = new Paragraph("-", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                        }
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Dari", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+letterDetail.sender[0].fullname + " - " + letterDetail.sender[0].positionName, normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Perihal", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+letterDetail.letter.about, normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Lampiran", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        var lampiran = letterDetail.letter.attachmentDesc == null ? "-" : letterDetail.letter.attachmentDesc;
                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+lampiran, bold);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //Add table to document
                        pdfDoc.Add(table);


                        Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                        line.SpacingBefore = 0f;
                        line.SetLeading(2F, 0.5F);
                        pdfDoc.Add(line);
                        Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                        lines.SpacingBefore = 0f;
                        line.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(line);

                        Paragraph liness = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        liness.SpacingBefore = 0f;
                        liness.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(liness);

                        //Paragraph linesss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        //linesss.SpacingBefore = 0f;
                        //linesss.SetLeading(0.5F, 0.5F);
                        //pdfDoc.Add(linesss);


                        table = new PdfPTable(1);
                        width = new float[] { 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 00f;
                        table.SpacingAfter = 0f;


                        //table.AddCell(new Phrase("Dengan hormat", normal));
                        //pdfDoc.Add(table);

                        Paragraph linessss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        linessss.SpacingBefore = 0f;
                        linessss.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(linessss);

                        Paragraph lin = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        lin.SpacingBefore = 0f;
                        lin.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(lin);

                        #endregion

                        //ADD HTML CONTENT
                        htmlparser.Parse(sr);


                        #region Tanda Tangan

                        Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        a.SpacingBefore = 0f;
                        a.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(a);



                        cell = new PdfPCell();
                        table.AddCell(new Phrase("", normal));

                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                        //table.AddCell(new Phrase("Best Regards", normal));
                        pdfDoc.Add(table);



                        string filename = letterDetail.sender[0].nip;


                        // Tanda tangan
                         var dirChecker = letterDetail.sender.OrderBy(p => p.idUserSender).ToList();
                         //var dirChecker = letterDetail.checker.Where(p =>  p.idLevelChecker == 1).ToList();

                        table = new PdfPTable(dirChecker.Count());
                        width = new float[dirChecker.Count()];
                        for (int i = 0; i < dirChecker.Count(); i++)
                        {
                            width[i] = 1f;
                        }
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        var no1 = 0;


                        //cell = new PdfPCell();
                        //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                        foreach (var item in dirChecker)
                        {
                            cell = new PdfPCell();
                            cell.Border = 0;




                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                    }

                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {


                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                    cell.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell.AddElement(imagettd);
                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd != null)
                                    //{
                                    //    cell.AddElement(imagettd);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);
                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                                no1++;
                            }

                            paragraph = new Paragraph(item.fullname, underlineBold);
                            cell.AddElement(paragraph);
                            paragraph = new Paragraph(item.positionName, bold);
                            cell.AddElement(paragraph);

                            table.AddCell(cell);
                        }
                        // end tanda tangan
                        pdfDoc.Add(table);

                        #endregion

                        pdfDoc.NewPage();

                        #region Lampiran

                        PdfPTable table1 = new PdfPTable(2);
                        float[] width1 = new float[] { 0.667f, 2f };
                        table1.WidthPercentage = 100;
                        table1.SetWidths(width1);
                        table1.HorizontalAlignment = Element.ALIGN_LEFT;
                        table1.DefaultCell.Border = 0;
                        table1.SpacingBefore = 0f;
                        table1.SpacingAfter = 0f;

                        PdfPCell cell1 = new PdfPCell();
                        Paragraph paragraph1 = new Paragraph();

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        pdfDoc.Add(table1);

                        //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                        var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                        foreach (var item in checkerList)
                        {
                            var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                            var comment = "";
                            string approveDate = "";
                            if (getLogComment != null)
                            {
                                comment = getLogComment.comment;
                                approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                            }
                            table1 = new PdfPTable(3);
                            width = new float[] { 1f, 2f, 1f };
                            table1.WidthPercentage = 100;
                            table1.SetWidths(width);
                            table1.HorizontalAlignment = Element.ALIGN_LEFT;
                            table1.DefaultCell.Border = 0;
                            table1.SpacingBefore = 0f;
                            table1.SpacingAfter = 0f;

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            if (letterDetail.letter.statusCode == 2)
                                {
                                    paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                                }
                                else if (letterDetail.letter.statusCode == 5)
                                {
                                    paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                                }
                                else
                                {
                                   if(item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                   { 
                                       paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                   }
                                   else
                                   {
                                       paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);
                                       
                                   }
                                }

                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(comment, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 40f;
                            paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            //cell1.AddElement(paragraph);
                            paragraph1 = new Paragraph(approveDate, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {
                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell1.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph1 = new Paragraph(img, normal);
                                        cell1.AddElement(paragraph1);
                                    }
                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {
                                    //innertable1 = new PdfPtable1(2);
                                    //float[] widthinner = new float[] { 0.3f, 0.3f };
                                    //innertable1.WidthPercentage = 100;
                                    //innertable1.SetWidths(widthinner);
                                    //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                    //innertable1.DefaultCell.Border = 1;
                                    //innertable1.DefaultCell.PaddingBottom = 8;

                                    //innercell = new PdfPCell();
                                    //innercell.BorderWidthLeft = 1f;
                                    //innercell.BorderWidthRight = 1f;
                                    //innercell.BorderWidthTop = 1f;
                                    //innercell.BorderWidthBottom = 1f;




                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell1.AddElement(imagettd);

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd !=null)
                                    //{
                                    //    cell.AddElement(imagettd);

                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);

                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                            }

                            paragraph1 = new Paragraph(item.fullname, bold);
                            paragraph1.Alignment = Element.ALIGN_CENTER;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);


                            pdfDoc.Add(table1);
                        }


                        #endregion

                        pdfDoc.Close();

                        var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                        var letterNumber = letterDetail.letter.letterNumber;
                        byte[] bytess = ms.ToArray();
                        byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                        ms.Close();

                        return File(bytes.ToArray(), "application/pdf", fileName);
                }

                #endregion

            }

            //Done(Masi Ikuti Template Memo Direksi)
            private FileContentResult PrivSuratPernyataanDewanKom(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {

               #region Surat Kuasa Operasional

               StringBuilder sb = new StringBuilder();
               sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
               StringReader sr = new StringReader(sb.ToString());
               //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
               //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
               Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 66.5f, 99.225f);
               HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();
                        //Font
                        int no;
                        var fontName = "Calibri";
                        //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                        //FontFactory.Register(fontPath);

                        Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                        Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                        Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                        Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                        Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        // Colors
                        BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                        BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));



                        PdfPTable table = new PdfPTable(2);
                        float[] width = new float[] { 0.667f, 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        PdfPCell cell = new PdfPCell();
                        Paragraph paragraph = new Paragraph();


                        #region header
                        //add new page

                        //table = new PdfPTable(1);
                        //width = new float[] { 2f };
                        //table.WidthPercentage = 100;
                        //table.SetWidths(width);
                        //table.HorizontalAlignment = Element.ALIGN_LEFT;
                        //table.DefaultCell.Border = 0;
                        //table.SpacingBefore = 00f;
                        //table.SpacingAfter = 0f;


                        //table.AddCell(new Phrase("MEMO", bold));
                        //pdfDoc.Add(table);

                        table = new PdfPTable(2);
                        width = new float[] { 0.2f, 1f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        cell.Border = 0;
                        paragraph = new Paragraph("Nomor", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+letterDetail.letter.letterNumber, normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Tanggal", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Kepada", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        no = 1;
                        foreach (var item in letterDetail.receiver)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            else
                            {
                                paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            no++;
                        }
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Tembusan", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        no = 1;
                        foreach (var item in letterDetail.copy)
                        {
                            if (letterDetail.copy.Count() > 1)
                            {
                                paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            else
                            {
                                paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            no++;
                        }
                        if (letterDetail.copy.Count() == 0)
                        {
                            paragraph = new Paragraph("-", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                        }
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Dari", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+letterDetail.sender[0].fullname + " - " + letterDetail.sender[0].positionName, normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Perihal", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+letterDetail.letter.about, normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Lampiran", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        var lampiran = letterDetail.letter.attachmentDesc == null ? "-" : letterDetail.letter.attachmentDesc;
                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+lampiran, bold);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //Add table to document
                        pdfDoc.Add(table);

                        Paragraph linessss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        linessss.SpacingBefore = 0f;
                        linessss.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(linessss);

                        //Paragraph lin = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        //lin.SpacingBefore = 0f;
                        //lin.SetLeading(0.5F, 0.5F);
                        //pdfDoc.Add(lin);

                        #endregion

                        //ADD HTML CONTENT
                        htmlparser.Parse(sr);


                        #region Tanda Tangan

                        Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        a.SpacingBefore = 0f;
                        a.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(a);



                        cell = new PdfPCell();
                        table.AddCell(new Phrase("", normal));

                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                        //table.AddCell(new Phrase("Best Regards", normal));
                        pdfDoc.Add(table);



                        string filename = letterDetail.sender[0].nip;


                        // Tanda tangan
                        var dirChecker = letterDetail.sender.OrderBy(p => p.idUserSender).ToList();
                        //var dirChecker = letterDetail.checker.Where(p => p.idLevelChecker == 1).ToList();

                        table = new PdfPTable(dirChecker.Count());
                        width = new float[dirChecker.Count()];
                        for (int i = 0; i < dirChecker.Count(); i++)
                        {
                            width[i] = 1f;
                        }
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        var no1 = 0;


                        //cell = new PdfPCell();
                        //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                        foreach (var item in dirChecker)
                        {
                            cell = new PdfPCell();
                            cell.Border = 0;




                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                    }

                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {


                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                    cell.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell.AddElement(imagettd);
                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd != null)
                                    //{
                                    //    cell.AddElement(imagettd);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);
                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                                no1++;
                            }

                            paragraph = new Paragraph(item.fullname, underlineBold);
                            cell.AddElement(paragraph);
                            paragraph = new Paragraph(item.positionName, bold);
                            cell.AddElement(paragraph);

                            table.AddCell(cell);
                        }
                        // end tanda tangan
                        pdfDoc.Add(table);

                        #endregion

                        pdfDoc.NewPage();

                        #region Lampiran

                        PdfPTable table1 = new PdfPTable(2);
                        float[] width1 = new float[] { 0.667f, 2f };
                        table1.WidthPercentage = 100;
                        table1.SetWidths(width1);
                        table1.HorizontalAlignment = Element.ALIGN_LEFT;
                        table1.DefaultCell.Border = 0;
                        table1.SpacingBefore = 0f;
                        table1.SpacingAfter = 0f;

                        PdfPCell cell1 = new PdfPCell();
                        Paragraph paragraph1 = new Paragraph();

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        pdfDoc.Add(table1);

                        //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                        var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                        foreach (var item in checkerList)
                        {
                            var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                            var comment = "";
                            string approveDate = "";
                            if (getLogComment != null)
                            {
                                comment = getLogComment.comment;
                                approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                            }
                            table1 = new PdfPTable(3);
                            width = new float[] { 1f, 2f, 1f };
                            table1.WidthPercentage = 100;
                            table1.SetWidths(width);
                            table1.HorizontalAlignment = Element.ALIGN_LEFT;
                            table1.DefaultCell.Border = 0;
                            table1.SpacingBefore = 0f;
                            table1.SpacingAfter = 0f;

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            if (letterDetail.letter.statusCode == 2)
                                {
                                    paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                                }
                                else if (letterDetail.letter.statusCode == 5)
                                {
                                    paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                                }
                                else
                                {
                                   if(item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                   { 
                                       paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                   }
                                   else
                                   {
                                       paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);
                                       
                                   }
                                }

                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(comment, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 40f;
                            paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            //cell1.AddElement(paragraph);
                            paragraph1 = new Paragraph(approveDate, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {
                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell1.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph1 = new Paragraph(img, normal);
                                        cell1.AddElement(paragraph1);
                                    }
                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {
                                    //innertable1 = new PdfPtable1(2);
                                    //float[] widthinner = new float[] { 0.3f, 0.3f };
                                    //innertable1.WidthPercentage = 100;
                                    //innertable1.SetWidths(widthinner);
                                    //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                    //innertable1.DefaultCell.Border = 1;
                                    //innertable1.DefaultCell.PaddingBottom = 8;

                                    //innercell = new PdfPCell();
                                    //innercell.BorderWidthLeft = 1f;
                                    //innercell.BorderWidthRight = 1f;
                                    //innercell.BorderWidthTop = 1f;
                                    //innercell.BorderWidthBottom = 1f;




                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell1.AddElement(imagettd);

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd !=null)
                                    //{
                                    //    cell.AddElement(imagettd);

                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);

                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                            }

                            paragraph1 = new Paragraph(item.fullname, bold);
                            paragraph1.Alignment = Element.ALIGN_CENTER;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);


                            pdfDoc.Add(table1);
                        }


                        #endregion

                        pdfDoc.Close();

                        var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                        var letterNumber = letterDetail.letter.letterNumber;
                        byte[] bytess = ms.ToArray();
                        byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                        ms.Close();

                        return File(bytes.ToArray(), "application/pdf", fileName);
                    }

               #endregion

            }

            //done
            private FileContentResult PrivSuratKeputusanDewanKom (string letternumber, OutputDetailMemo letterDetail, string fileName)
            {

                    #region Surat Keputusan Dewan Komisaris

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                    StringReader sr = new StringReader(sb.ToString());
                    //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                    //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                    Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 66.5f, 99.225f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();
                        //Font
                        int no;
                        var fontName = "Calibri";
                        //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                        //FontFactory.Register(fontPath);

                        Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                        Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                        Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                        Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                        Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        // Colors
                        BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                        BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));

                        PdfPTable table = new PdfPTable(2);
                        float[] width = new float[] { 0.667f, 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        PdfPCell cell = new PdfPCell();
                        Paragraph paragraph = new Paragraph();


                            #region header

                            table = new PdfPTable(1);
                            width = new float[] { 2f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 00f;
                            table.SpacingAfter = 0f;

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(new Phrase("KEPUTUSAN DEWAN KOMISARIS PT BNI LIFE INSURANCE", bold));
                            paragraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);
                            pdfDoc.Add(table);

                            table = new PdfPTable(1);
                            width = new float[] { 10f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 00f;
                            table.SpacingAfter = 0f;

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(new Phrase("NOMOR : " + letterDetail.letter.letterNumber, bold));
                            paragraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);
                            pdfDoc.Add(table);


                            table = new PdfPTable(1);
                            width = new float[] { 10f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 00f;
                            table.SpacingAfter = 0f;

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(new Phrase("TANGGAL " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), bold));
                            paragraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);
                            pdfDoc.Add(table);

                            table = new PdfPTable(1);
                            width = new float[] { 10f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 0f;
                            table.SpacingAfter = 0f;

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(new Phrase("TENTANG", bold));
                            paragraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);
                            pdfDoc.Add(table);

                            table = new PdfPTable(1);
                            width = new float[] { 10f };
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.DefaultCell.Border = 0;
                            table.SpacingBefore = 00f;
                            table.SpacingAfter = 0f;

                            cell = new PdfPCell();
                            cell.Border = 0;
                            paragraph = new Paragraph(new Phrase(letterDetail.letter.about, underlineBold));
                            paragraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(paragraph);
                            table.AddCell(cell);
                            pdfDoc.Add(table);


                            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                            line.SpacingBefore = 0f;
                            line.SetLeading(2F, 0.5F);
                            pdfDoc.Add(line);
                            Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                            lines.SpacingBefore = 0f;
                            line.SetLeading(0.5F, 0.5F);
                            pdfDoc.Add(line);

                            #endregion

                            //ADD HTML CONTENT
                            htmlparser.Parse(sr);


                            #region Tanda Tangan

                            Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                            a.SpacingBefore = 0f;
                            a.SetLeading(0.5F, 0.5F);
                            pdfDoc.Add(a);



                            cell = new PdfPCell();
                            table.AddCell(new Phrase("", normal));

                            cell = new PdfPCell();
                            table.AddCell(new Phrase(" ", normal));
                            cell = new PdfPCell();
                            table.AddCell(new Phrase(" ", normal));
                            cell = new PdfPCell();
                            table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                            //table.AddCell(new Phrase("Best Regards", normal));
                            pdfDoc.Add(table);



                            string filename = letterDetail.sender[0].nip;


                            // Tanda tangan
                            //var dirChecker = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                            var dirChecker = letterDetail.checker.Where(p => p.idLevelChecker == 1).OrderBy(p => p.idLevelChecker).ToList();

                            table = new PdfPTable(dirChecker.Count());
                            width = new float[dirChecker.Count()];
                            for (int i = 0; i < dirChecker.Count(); i++)
                            {
                                width[i] = 1f;
                            }
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;
                            var no1 = 0;


                            //cell = new PdfPCell();
                            //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                            foreach (var item in dirChecker)
                            {
                                cell = new PdfPCell();
                                cell.Border = 0;




                                if (letterDetail.letter.letterNumber != "NO_LETTER")
                                {
                                    if (letterDetail.letter.signatureType == 1)
                                    {

                                        Image imagettd = GetSignatureImage(item.nip);
                                        //cell.AddElement(imagettd);
                                        if (imagettd != null)
                                        {
                                            cell.AddElement(imagettd);
                                        }
                                        else
                                        {
                                            var img = "File tanda tangan tidak ditemukan";
                                            paragraph = new Paragraph(img, normal);
                                            cell.AddElement(paragraph);
                                        }

                                    }
                                    else if (letterDetail.letter.signatureType == 2)
                                    {


                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                        cell.AddElement(QRCodeSignatureApprover);
                                    }
                                    else if (letterDetail.letter.signatureType == 3)
                                    {
                                        var img = "";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                        Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessia.SpacingBefore = 0f;
                                        linessia.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessia);

                                        Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaa.SpacingBefore = 0f;
                                        linessiaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaa);

                                        Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaaa.SpacingBefore = 0f;
                                        linessiaaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaaa);
                                    }
                                    else
                                    {

                                        Image imagettd = GetSignatureImage(item.nip);
                                        cell.AddElement(imagettd);
                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        cell.AddElement(QRCodeSignatureApprover);
                                        //if (imagettd != null)
                                        //{
                                        //    cell.AddElement(imagettd);
                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}
                                        //else
                                        //{
                                        //    var img = "File tanda tangan tidak di temukan";
                                        //    paragraph = new Paragraph(img, normal);
                                        //    cell.AddElement(paragraph);
                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}

                                    }
                                    no1++;
                                }

                                paragraph = new Paragraph(item.fullname, underlineBold);
                                cell.AddElement(paragraph);
                                paragraph = new Paragraph(item.positionName, bold);
                                cell.AddElement(paragraph);

                                table.AddCell(cell);
                            }
                                // end tanda tangan
                                pdfDoc.Add(table);

                                #endregion

                            pdfDoc.NewPage();

                            #region Lampiran

                            PdfPTable table1 = new PdfPTable(2);
                            float[] width1 = new float[] { 0.667f, 2f };
                            table1.WidthPercentage = 100;
                            table1.SetWidths(width1);
                            table1.HorizontalAlignment = Element.ALIGN_LEFT;
                            table1.DefaultCell.Border = 0;
                            table1.SpacingBefore = 0f;
                            table1.SpacingAfter = 0f;

                            PdfPCell cell1 = new PdfPCell();
                            Paragraph paragraph1 = new Paragraph();

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 1f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 30f;
                            paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 1f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 30f;
                            paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            no = 1;
                            foreach (var item in letterDetail.delibration)
                            {
                                if (letterDetail.receiver.Count() > 1)
                                {
                                    paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                else
                                {
                                    paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                no++;
                            }
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            no = 1;
                            foreach (var item in letterDetail.delibration)
                            {
                                if (letterDetail.receiver.Count() > 1)
                                {
                                    paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                else
                                {
                                    paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                no++;
                            }
                            table1.AddCell(cell1);

                            pdfDoc.Add(table1);

                            //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                            var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                            foreach (var item in checkerList)
                            {
                                var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                                var comment = "";
                                string approveDate = "";
                                if (getLogComment != null)
                                {
                                    comment = getLogComment.comment;
                                    approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                                }
                                table1 = new PdfPTable(3);
                                width = new float[] { 1f, 2f, 1f };
                                table1.WidthPercentage = 100;
                                table1.SetWidths(width);
                                table1.HorizontalAlignment = Element.ALIGN_LEFT;
                                table1.DefaultCell.Border = 0;
                                table1.SpacingBefore = 0f;
                                table1.SpacingAfter = 0f;

                                cell1 = new PdfPCell();
                                cell1.Rowspan = 2;
                                cell1.BorderWidthLeft = 1f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                cell1.MinimumHeight = 120f;
                                if (letterDetail.letter.statusCode == 2)
                                {
                                    paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                                }
                                else if (letterDetail.letter.statusCode == 5)
                                {
                                    paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                                }
                                else
                                {
                                   if(item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                   { 
                                       paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                   }
                                   else
                                   {
                                       paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);
                                       
                                   }
                                }

                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);

                                cell1 = new PdfPCell();
                                cell1.Rowspan = 2;
                                cell1.BorderWidthLeft = 0f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                cell1.MinimumHeight = 120f;
                                paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                paragraph1 = new Paragraph(comment, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);

                                cell1 = new PdfPCell();
                                cell1.BorderWidthLeft = 0f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                cell1.MinimumHeight = 40f;
                                paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                //cell1.AddElement(paragraph);
                                paragraph1 = new Paragraph(approveDate, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);

                                cell1 = new PdfPCell();
                                cell1.BorderWidthLeft = 0f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                if (letterDetail.letter.letterNumber != "NO_LETTER")
                                {
                                    if (letterDetail.letter.signatureType == 1)
                                    {
                                        Image imagettd = GetSignatureImage(item.nip);
                                        //cell.AddElement(imagettd);
                                        if (imagettd != null)
                                        {
                                            cell1.AddElement(imagettd);
                                        }
                                        else
                                        {
                                            var img = "File tanda tangan tidak ditemukan";
                                            paragraph1 = new Paragraph(img, normal);
                                            cell1.AddElement(paragraph1);
                                        }
                                    }
                                    else if (letterDetail.letter.signatureType == 2)
                                    {

                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        cell1.AddElement(QRCodeSignatureApprover);
                                    }
                                    else if (letterDetail.letter.signatureType == 3)
                                    {
                                        var img = "";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                        Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessia.SpacingBefore = 0f;
                                        linessia.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessia);

                                        Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaa.SpacingBefore = 0f;
                                        linessiaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaa);

                                        Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaaa.SpacingBefore = 0f;
                                        linessiaaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaaa);
                                    }
                                    else
                                    {
                                        //innertable1 = new PdfPtable1(2);
                                        //float[] widthinner = new float[] { 0.3f, 0.3f };
                                        //innertable1.WidthPercentage = 100;
                                        //innertable1.SetWidths(widthinner);
                                        //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                        //innertable1.DefaultCell.Border = 1;
                                        //innertable1.DefaultCell.PaddingBottom = 8;

                                        //innercell = new PdfPCell();
                                        //innercell.BorderWidthLeft = 1f;
                                        //innercell.BorderWidthRight = 1f;
                                        //innercell.BorderWidthTop = 1f;
                                        //innercell.BorderWidthBottom = 1f;




                                        Image imagettd = GetSignatureImage(item.nip);
                                        cell1.AddElement(imagettd);

                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        cell1.AddElement(QRCodeSignatureApprover);
                                        //if (imagettd !=null)
                                        //{
                                        //    cell.AddElement(imagettd);

                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}
                                        //else
                                        //{
                                        //    var img = "File tanda tangan tidak di temukan";
                                        //    paragraph = new Paragraph(img, normal);

                                        //    cell.AddElement(paragraph);
                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}

                                    }
                                }

                                paragraph1 = new Paragraph(item.fullname, bold);
                                paragraph1.Alignment = Element.ALIGN_CENTER;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);


                                pdfDoc.Add(table1);
                            }


                #endregion

                            pdfDoc.Close();

                            var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                            var letterNumber = letterDetail.letter.letterNumber;
                            byte[] bytess = ms.ToArray();
                            byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                            ms.Close();

                            return File(bytes.ToArray(), "application/pdf", fileName);
            }

                    #endregion

            }

        #endregion

        #region Print Divisi

            //Done(Masi Ikuti Template Memo Direksi)
            private FileContentResult PrivSuratKuasa(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {
                #region Surat Kuasa Divisi

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                    StringReader sr = new StringReader(sb.ToString());
                    //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                    //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                    Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 66.5f, 99.225f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();
                        //Font
                        int no;
                        var fontName = "Calibri";
                        //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                        //FontFactory.Register(fontPath);

                        Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                        Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                        Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                        Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                        Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        // Colors
                        BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                        BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));



                        PdfPTable table = new PdfPTable(2);
                        float[] width = new float[] { 0.667f, 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        PdfPCell cell = new PdfPCell();
                        Paragraph paragraph = new Paragraph();


                        #region header
                        //add new page

                        //table = new PdfPTable(1);
                        //width = new float[] { 2f };
                        //table.WidthPercentage = 100;
                        //table.SetWidths(width);
                        //table.HorizontalAlignment = Element.ALIGN_LEFT;
                        //table.DefaultCell.Border = 0;
                        //table.SpacingBefore = 00f;
                        //table.SpacingAfter = 0f;


                        //table.AddCell(new Phrase("MEMO", bold));
                        //pdfDoc.Add(table);

                        table = new PdfPTable(2);
                        width = new float[] { 0.2f, 1f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        cell.Border = 0;
                        paragraph = new Paragraph("Nomor", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+letterDetail.letter.letterNumber, normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Tanggal", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Kepada", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        no = 1;
                        foreach (var item in letterDetail.receiver)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            else
                            {
                                paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            no++;
                        }
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Tembusan", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        no = 1;
                        foreach (var item in letterDetail.copy)
                        {
                            if (letterDetail.copy.Count() > 1)
                            {
                                paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            else
                            {
                                paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                            }
                            no++;
                        }
                        if (letterDetail.copy.Count() == 0)
                        {
                            paragraph = new Paragraph("-", normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                        }
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Dari", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+letterDetail.sender[0].fullname + " - " + letterDetail.sender[0].positionName, normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Perihal", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+letterDetail.letter.about, normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph("Lampiran", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //cell = new PdfPCell();
                        //cell.Border = 0;
                        //paragraph = new Paragraph(":", normal);
                        //paragraph.Alignment = Element.ALIGN_RIGHT;
                        //cell.AddElement(paragraph);
                        //table.AddCell(cell);

                        var lampiran = letterDetail.letter.attachmentDesc == null ? "-" : letterDetail.letter.attachmentDesc;
                        cell = new PdfPCell();
                        cell.Border = 0;
                        paragraph = new Paragraph(":"+" "+lampiran, bold);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                        table.AddCell(cell);

                        //Add table to document
                        pdfDoc.Add(table);


                        Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                        line.SpacingBefore = 0f;
                        line.SetLeading(2F, 0.5F);
                        pdfDoc.Add(line);
                        Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                        lines.SpacingBefore = 0f;
                        line.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(line);

                        Paragraph liness = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        liness.SpacingBefore = 0f;
                        liness.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(liness);

                        //Paragraph linesss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        //linesss.SpacingBefore = 0f;
                        //linesss.SetLeading(0.5F, 0.5F);
                        //pdfDoc.Add(linesss);


                        table = new PdfPTable(1);
                        width = new float[] { 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 00f;
                        table.SpacingAfter = 0f;


                        //table.AddCell(new Phrase("Dengan hormat", normal));
                        //pdfDoc.Add(table);

                        Paragraph linessss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        linessss.SpacingBefore = 0f;
                        linessss.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(linessss);

                        Paragraph lin = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        lin.SpacingBefore = 0f;
                        lin.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(lin);

                        #endregion

                        //ADD HTML CONTENT
                        htmlparser.Parse(sr);


                        #region Tanda Tangan

                        Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        a.SpacingBefore = 0f;
                        a.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(a);



                        cell = new PdfPCell();
                        table.AddCell(new Phrase("", normal));

                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                        //table.AddCell(new Phrase("Best Regards", normal));
                        pdfDoc.Add(table);



                        string filename = letterDetail.sender[0].nip;


                        // Tanda tangan
                        var dirChecker = letterDetail.sender.OrderBy(p => p.idUserSender).ToList();
                        //var dirChecker = letterDetail.checker.Where(p => p.idLevelChecker == 2 || p.idLevelChecker == 1).ToList();

                        

                        table = new PdfPTable(dirChecker.Count());
                        width = new float[dirChecker.Count()];
                        for (int i = 0; i < dirChecker.Count(); i++)
                        {
                            width[i] = 1f;
                        }
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        var no1 = 0;


                        //cell = new PdfPCell();
                        //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                        foreach (var item in dirChecker)
                        {
                            cell = new PdfPCell();
                            cell.Border = 0;




                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                    }

                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {


                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                    cell.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell.AddElement(imagettd);
                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd != null)
                                    //{
                                    //    cell.AddElement(imagettd);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);
                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                                no1++;
                            }

                            paragraph = new Paragraph(item.fullname, underlineBold);
                            cell.AddElement(paragraph);
                            paragraph = new Paragraph(item.positionName, bold);
                            cell.AddElement(paragraph);

                            table.AddCell(cell);
                        }
                        // end tanda tangan
                        pdfDoc.Add(table);

                        #endregion

                        pdfDoc.NewPage();

                        #region Lampiran

                        PdfPTable table1 = new PdfPTable(2);
                        float[] width1 = new float[] { 0.667f, 2f };
                        table1.WidthPercentage = 100;
                        table1.SetWidths(width1);
                        table1.HorizontalAlignment = Element.ALIGN_LEFT;
                        table1.DefaultCell.Border = 0;
                        table1.SpacingBefore = 0f;
                        table1.SpacingAfter = 0f;

                        PdfPCell cell1 = new PdfPCell();
                        Paragraph paragraph1 = new Paragraph();

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        pdfDoc.Add(table1);

                        //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                        var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                        foreach (var item in checkerList)
                        {
                            var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                            var comment = "";
                            string approveDate = "";
                            if (getLogComment != null)
                            {
                                comment = getLogComment.comment;
                                approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                            }
                            table1 = new PdfPTable(3);
                            width = new float[] { 1f, 2f, 1f };
                            table1.WidthPercentage = 100;
                            table1.SetWidths(width);
                            table1.HorizontalAlignment = Element.ALIGN_LEFT;
                            table1.DefaultCell.Border = 0;
                            table1.SpacingBefore = 0f;
                            table1.SpacingAfter = 0f;

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            if (letterDetail.letter.statusCode == 2)
                                {
                                    paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                                }
                                else if (letterDetail.letter.statusCode == 5)
                                {
                                    paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                                }
                                else
                                {
                                   if(item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                   { 
                                       paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                   }
                                   else
                                   {
                                       paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);
                                       
                                   }
                                }

                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(comment, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 40f;
                            paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            //cell1.AddElement(paragraph);
                            paragraph1 = new Paragraph(approveDate, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {
                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell1.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph1 = new Paragraph(img, normal);
                                        cell1.AddElement(paragraph1);
                                    }
                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {
                                    //innertable1 = new PdfPtable1(2);
                                    //float[] widthinner = new float[] { 0.3f, 0.3f };
                                    //innertable1.WidthPercentage = 100;
                                    //innertable1.SetWidths(widthinner);
                                    //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                    //innertable1.DefaultCell.Border = 1;
                                    //innertable1.DefaultCell.PaddingBottom = 8;

                                    //innercell = new PdfPCell();
                                    //innercell.BorderWidthLeft = 1f;
                                    //innercell.BorderWidthRight = 1f;
                                    //innercell.BorderWidthTop = 1f;
                                    //innercell.BorderWidthBottom = 1f;




                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell1.AddElement(imagettd);

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd !=null)
                                    //{
                                    //    cell.AddElement(imagettd);

                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);

                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                            }

                            paragraph1 = new Paragraph(item.fullname, bold);
                            paragraph1.Alignment = Element.ALIGN_CENTER;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);


                            pdfDoc.Add(table1);
                        }


                        #endregion

                        pdfDoc.Close();

                        var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                        var letterNumber = letterDetail.letter.letterNumber;
                        byte[] bytess = ms.ToArray();
                        byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                        ms.Close();

                        return File(bytes.ToArray(), "application/pdf", fileName);
                    }

                #endregion
            }

            //Done(Masi Ikuti Template Memo Direksi)
            private FileContentResult PrivSuratPernyataan(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {
                #region Surat Pernyataan Divisi

                StringBuilder sb = new StringBuilder();
                sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                StringReader sr = new StringReader(sb.ToString());
                //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 66.5f, 99.225f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                    pdfDoc.Open();
                    //Font
                    int no;
                    var fontName = "Calibri";
                    //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                    //FontFactory.Register(fontPath);

                    Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                    Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                    Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                    Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                    Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                    Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                    Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                    // Colors
                    BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                    BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));



                    PdfPTable table = new PdfPTable(2);
                    float[] width = new float[] { 0.667f, 2f };
                    table.WidthPercentage = 100;
                    table.SetWidths(width);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.DefaultCell.Border = 0;
                    table.SpacingBefore = 0f;
                    table.SpacingAfter = 0f;

                    PdfPCell cell = new PdfPCell();
                    Paragraph paragraph = new Paragraph();


                    #region header
                    //add new page

                    //table = new PdfPTable(1);
                    //width = new float[] { 2f };
                    //table.WidthPercentage = 100;
                    //table.SetWidths(width);
                    //table.HorizontalAlignment = Element.ALIGN_LEFT;
                    //table.DefaultCell.Border = 0;
                    //table.SpacingBefore = 00f;
                    //table.SpacingAfter = 0f;


                    //table.AddCell(new Phrase("MEMO", bold));
                    //pdfDoc.Add(table);

                    table = new PdfPTable(2);
                    width = new float[] { 0.2f, 1f };
                    table.WidthPercentage = 100;
                    table.SetWidths(width);
                    table.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.DefaultCell.Border = 0;
                    table.SpacingBefore = 0f;
                    table.SpacingAfter = 0f;

                    cell.Border = 0;
                    paragraph = new Paragraph("Nomor", normal);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    //cell = new PdfPCell();
                    //cell.Border = 0;
                    //paragraph = new Paragraph(":", normal);
                    //paragraph.Alignment = Element.ALIGN_RIGHT;
                    //cell.AddElement(paragraph);
                    //table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph(":"+" "+letterDetail.letter.letterNumber, normal);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph("Tanggal", normal);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    //cell = new PdfPCell();
                    //cell.Border = 0;
                    //paragraph = new Paragraph(":", normal);
                    //paragraph.Alignment = Element.ALIGN_RIGHT;
                    //cell.AddElement(paragraph);
                    //table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph(":"+" "+Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normal);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph("Kepada", normal);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    //cell = new PdfPCell();
                    //cell.Border = 0;
                    //paragraph = new Paragraph(":", normal);
                    //paragraph.Alignment = Element.ALIGN_RIGHT;
                    //cell.AddElement(paragraph);
                    //table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    no = 1;
                    foreach (var item in letterDetail.receiver)
                    {
                        if (letterDetail.receiver.Count() > 1)
                        {
                            paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                        }
                        else
                        {
                            paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                        }
                        no++;
                    }
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph("Tembusan", normal);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    //cell = new PdfPCell();
                    //cell.Border = 0;
                    //paragraph = new Paragraph(":", normal);
                    //paragraph.Alignment = Element.ALIGN_RIGHT;
                    //cell.AddElement(paragraph);
                    //table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    no = 1;
                    foreach (var item in letterDetail.copy)
                    {
                        if (letterDetail.copy.Count() > 1)
                        {
                            paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                        }
                        else
                        {
                            paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                            paragraph.Alignment = Element.ALIGN_LEFT;
                            cell.AddElement(paragraph);
                        }
                        no++;
                    }
                    if (letterDetail.copy.Count() == 0)
                    {
                        paragraph = new Paragraph("-", normal);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        cell.AddElement(paragraph);
                    }
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph("Dari", normal);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    //cell = new PdfPCell();
                    //cell.Border = 0;
                    //paragraph = new Paragraph(":", normal);
                    //paragraph.Alignment = Element.ALIGN_RIGHT;
                    //cell.AddElement(paragraph);
                    //table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph(":"+" "+letterDetail.sender[0].fullname + " - " + letterDetail.sender[0].positionName, normal);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph("Perihal", normal);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    //cell = new PdfPCell();
                    //cell.Border = 0;
                    //paragraph = new Paragraph(":", normal);
                    //paragraph.Alignment = Element.ALIGN_RIGHT;
                    //cell.AddElement(paragraph);
                    //table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph(":"+" "+letterDetail.letter.about, normal);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph("Lampiran", normal);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    //cell = new PdfPCell();
                    //cell.Border = 0;
                    //paragraph = new Paragraph(":", normal);
                    //paragraph.Alignment = Element.ALIGN_RIGHT;
                    //cell.AddElement(paragraph);
                    //table.AddCell(cell);

                    var lampiran = letterDetail.letter.attachmentDesc == null ? "-" : letterDetail.letter.attachmentDesc;
                    cell = new PdfPCell();
                    cell.Border = 0;
                    paragraph = new Paragraph(":"+" "+lampiran, bold);
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);

                    //Add table to document
                    pdfDoc.Add(table);


                    Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                    line.SpacingBefore = 0f;
                    line.SetLeading(2F, 0.5F);
                    pdfDoc.Add(line);
                    Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                    lines.SpacingBefore = 0f;
                    line.SetLeading(0.5F, 0.5F);
                    pdfDoc.Add(line);

                    Paragraph liness = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                    liness.SpacingBefore = 0f;
                    liness.SetLeading(0.5F, 0.5F);
                    pdfDoc.Add(liness);

                    //Paragraph linesss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                    //linesss.SpacingBefore = 0f;
                    //linesss.SetLeading(0.5F, 0.5F);
                    //pdfDoc.Add(linesss);


                    table = new PdfPTable(1);
                    width = new float[] { 2f };
                    table.WidthPercentage = 100;
                    table.SetWidths(width);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.DefaultCell.Border = 0;
                    table.SpacingBefore = 00f;
                    table.SpacingAfter = 0f;


                    //table.AddCell(new Phrase("Dengan hormat", normal));
                    //pdfDoc.Add(table);

                    Paragraph linessss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                    linessss.SpacingBefore = 0f;
                    linessss.SetLeading(0.5F, 0.5F);
                    pdfDoc.Add(linessss);

                    Paragraph lin = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                    lin.SpacingBefore = 0f;
                    lin.SetLeading(0.5F, 0.5F);
                    pdfDoc.Add(lin);

                    #endregion

                    //ADD HTML CONTENT
                    htmlparser.Parse(sr);


                    #region Tanda Tangan

                    Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                    a.SpacingBefore = 0f;
                    a.SetLeading(0.5F, 0.5F);
                    pdfDoc.Add(a);



                    cell = new PdfPCell();
                    table.AddCell(new Phrase("", normal));

                    cell = new PdfPCell();
                    table.AddCell(new Phrase(" ", normal));
                    cell = new PdfPCell();
                    table.AddCell(new Phrase(" ", normal));
                    cell = new PdfPCell();
                    table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                    //table.AddCell(new Phrase("Best Regards", normal));
                    pdfDoc.Add(table);



                    string filename = letterDetail.sender[0].nip;


                    // Tanda tangan
                    var dirChecker = letterDetail.sender.OrderBy(p => p.idUserSender).ToList();
                    //var dirChecker = letterDetail.checker.Where(p => p.idLevelChecker == 2 || p.idLevelChecker == 1).ToList();
                   
                    table = new PdfPTable(dirChecker.Count());
                    width = new float[dirChecker.Count()];
                    for (int i = 0; i < dirChecker.Count(); i++)
                    {
                        width[i] = 1f;
                    }
                    table.WidthPercentage = 100;
                    table.SetWidths(width);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.DefaultCell.Border = 0;
                    var no1 = 0;


                    //cell = new PdfPCell();
                    //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                    foreach (var item in dirChecker)
                    {
                        cell = new PdfPCell();
                        cell.Border = 0;




                        if (letterDetail.letter.letterNumber != "NO_LETTER")
                        {
                            if (letterDetail.letter.signatureType == 1)
                            {

                                Image imagettd = GetSignatureImage(item.nip);
                                //cell.AddElement(imagettd);
                                if (imagettd != null)
                                {
                                    cell.AddElement(imagettd);
                                }
                                else
                                {
                                    var img = "File tanda tangan tidak ditemukan";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                }

                            }
                            else if (letterDetail.letter.signatureType == 2)
                            {


                                Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                cell.AddElement(QRCodeSignatureApprover);
                            }
                            else if (letterDetail.letter.signatureType == 3)
                            {
                                var img = "";
                                paragraph = new Paragraph(img, normal);
                                cell.AddElement(paragraph);
                                Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessia.SpacingBefore = 0f;
                                linessia.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessia);

                                Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessiaa.SpacingBefore = 0f;
                                linessiaa.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessiaa);

                                Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessiaaa.SpacingBefore = 0f;
                                linessiaaa.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessiaaa);
                            }
                            else
                            {

                                Image imagettd = GetSignatureImage(item.nip);
                                cell.AddElement(imagettd);
                                Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                cell.AddElement(QRCodeSignatureApprover);
                                //if (imagettd != null)
                                //{
                                //    cell.AddElement(imagettd);
                                //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                //    cell.AddElement(QRCodeSignatureApprover);
                                //}
                                //else
                                //{
                                //    var img = "File tanda tangan tidak di temukan";
                                //    paragraph = new Paragraph(img, normal);
                                //    cell.AddElement(paragraph);
                                //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                //    cell.AddElement(QRCodeSignatureApprover);
                                //}

                            }
                            no1++;
                        }

                        paragraph = new Paragraph(item.fullname, underlineBold);
                        cell.AddElement(paragraph);
                        paragraph = new Paragraph(item.positionName, bold);
                        cell.AddElement(paragraph);

                        table.AddCell(cell);
                    }
                    // end tanda tangan
                    pdfDoc.Add(table);

                    #endregion

                    pdfDoc.NewPage();

                    #region Lampiran

                    PdfPTable table1 = new PdfPTable(2);
                    float[] width1 = new float[] { 0.667f, 2f };
                    table1.WidthPercentage = 100;
                    table1.SetWidths(width1);
                    table1.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.DefaultCell.Border = 0;
                    table1.SpacingBefore = 0f;
                    table1.SpacingAfter = 0f;

                    PdfPCell cell1 = new PdfPCell();
                    Paragraph paragraph1 = new Paragraph();

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 1f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 1f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 30f;
                    paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    table1.AddCell(cell1);

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 0f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 1f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 30f;
                    paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    table1.AddCell(cell1);

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 1f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 0f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 100f;
                    paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    table1.AddCell(cell1);

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 0f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 0f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 100f;
                    paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    table1.AddCell(cell1);

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 1f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 0f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 100f;
                    paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    no = 1;
                    foreach (var item in letterDetail.delibration)
                    {
                        if (letterDetail.receiver.Count() > 1)
                        {
                            paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                        }
                        else
                        {
                            paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                        }
                        no++;
                    }
                    table1.AddCell(cell1);

                    cell1 = new PdfPCell();
                    cell1.BorderWidthLeft = 0f;
                    cell1.BorderWidthRight = 1f;
                    cell1.BorderWidthTop = 0f;
                    cell1.BorderWidthBottom = 1f;
                    cell1.MinimumHeight = 100f;
                    paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    cell1.AddElement(paragraph1);
                    no = 1;
                    foreach (var item in letterDetail.delibration)
                    {
                        if (letterDetail.receiver.Count() > 1)
                        {
                            paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                        }
                        else
                        {
                            paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                        }
                        no++;
                    }
                    table1.AddCell(cell1);

                    pdfDoc.Add(table1);

                    //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                    var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                    foreach (var item in checkerList)
                    {
                        var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                        var comment = "";
                        string approveDate = "";
                        if (getLogComment != null)
                        {
                            comment = getLogComment.comment;
                            approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                        }
                        table1 = new PdfPTable(3);
                        width = new float[] { 1f, 2f, 1f };
                        table1.WidthPercentage = 100;
                        table1.SetWidths(width);
                        table1.HorizontalAlignment = Element.ALIGN_LEFT;
                        table1.DefaultCell.Border = 0;
                        table1.SpacingBefore = 0f;
                        table1.SpacingAfter = 0f;

                        cell1 = new PdfPCell();
                        cell1.Rowspan = 2;
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 120f;
                        if (letterDetail.letter.statusCode == 2)
                                {
                                    paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                                }
                                else if (letterDetail.letter.statusCode == 5)
                                {
                                    paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                                }
                                else
                                {
                                   if(item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                   { 
                                       paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                   }
                                   else
                                   {
                                       paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);
                                       
                                   }
                                }

                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.Rowspan = 2;
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 120f;
                        paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        paragraph1 = new Paragraph(comment, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 40f;
                        paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        //cell1.AddElement(paragraph);
                        paragraph1 = new Paragraph(approveDate, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        if (letterDetail.letter.letterNumber != "NO_LETTER")
                        {
                            if (letterDetail.letter.signatureType == 1)
                            {
                                Image imagettd = GetSignatureImage(item.nip);
                                //cell.AddElement(imagettd);
                                if (imagettd != null)
                                {
                                    cell1.AddElement(imagettd);
                                }
                                else
                                {
                                    var img = "File tanda tangan tidak ditemukan";
                                    paragraph1 = new Paragraph(img, normal);
                                    cell1.AddElement(paragraph1);
                                }
                            }
                            else if (letterDetail.letter.signatureType == 2)
                            {

                                Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                cell1.AddElement(QRCodeSignatureApprover);
                            }
                            else if (letterDetail.letter.signatureType == 3)
                            {
                                var img = "";
                                paragraph = new Paragraph(img, normal);
                                cell.AddElement(paragraph);
                                Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessia.SpacingBefore = 0f;
                                linessia.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessia);

                                Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessiaa.SpacingBefore = 0f;
                                linessiaa.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessiaa);

                                Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                linessiaaa.SpacingBefore = 0f;
                                linessiaaa.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(linessiaaa);
                            }
                            else
                            {
                                //innertable1 = new PdfPtable1(2);
                                //float[] widthinner = new float[] { 0.3f, 0.3f };
                                //innertable1.WidthPercentage = 100;
                                //innertable1.SetWidths(widthinner);
                                //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                //innertable1.DefaultCell.Border = 1;
                                //innertable1.DefaultCell.PaddingBottom = 8;

                                //innercell = new PdfPCell();
                                //innercell.BorderWidthLeft = 1f;
                                //innercell.BorderWidthRight = 1f;
                                //innercell.BorderWidthTop = 1f;
                                //innercell.BorderWidthBottom = 1f;




                                Image imagettd = GetSignatureImage(item.nip);
                                cell1.AddElement(imagettd);

                                Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                cell1.AddElement(QRCodeSignatureApprover);
                                //if (imagettd !=null)
                                //{
                                //    cell.AddElement(imagettd);

                                //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                //    cell.AddElement(QRCodeSignatureApprover);
                                //}
                                //else
                                //{
                                //    var img = "File tanda tangan tidak di temukan";
                                //    paragraph = new Paragraph(img, normal);

                                //    cell.AddElement(paragraph);
                                //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                //    cell.AddElement(QRCodeSignatureApprover);
                                //}

                            }
                        }

                        paragraph1 = new Paragraph(item.fullname, bold);
                        paragraph1.Alignment = Element.ALIGN_CENTER;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);


                        pdfDoc.Add(table1);
                    }


                    #endregion

                    pdfDoc.Close();

                    var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                    var letterNumber = letterDetail.letter.letterNumber;
                    byte[] bytess = ms.ToArray();
                    byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                    ms.Close();

                    return File(bytes.ToArray(), "application/pdf", fileName);
                }


                #endregion
            }

            // Done
            private FileContentResult PrivSuratMemoDivisi(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {
                #region Surat Memo Divisi

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                    StringReader sr = new StringReader(sb.ToString());
                    //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                    //Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 113.4f, 99.225f);
                    Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 113.4f, 99.225f);
                    
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfWriter.PageEvent = new PDFHeaderEvent();
                        //pdfWriter.PageEvent = new PDFHeaderEvent2();
                        pdfDoc.Open();
                        //Font
                        int no;
                        var fontName = "Calibri";
                        //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                        //FontFactory.Register(fontPath);

                        Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                        Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                        Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        Font boldMemo = FontFactory.GetFont(fontName, 14, Font.BOLD, BaseColor.BLACK);

                        Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                        Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        // Colors
                        BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                        BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));
                        
                

                        PdfPTable table = new PdfPTable(2);
                        float[] width = new float[] { 0.667f, 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        PdfPCell cell = new PdfPCell();
                        Paragraph paragraph = new Paragraph();


                       #region header

                       //cell = new PdfPCell();
                       //cell.Border = Rectangle.NO_BORDER;
                       //cell.HorizontalAlignment= Element.ALIGN_LEFT;
                       //cell.Phrase = new Phrase("", bold);
                       //table.AddCell(cell);

                       //Image logo = GetLogoImage("logobni");
                       //logo.ScalePercent(55);
                       //cell = new PdfPCell(logo);
                       //cell.Border = Rectangle.NO_BORDER;
                       //cell.HorizontalAlignment= Element.ALIGN_RIGHT;
                       //table.AddCell(cell);
                       //pdfDoc.Add(table);

                       //table = new PdfPTable(1);
                       //width = new float[] { 2f };
                       //table.WidthPercentage = 120;
                       //table.SetWidths(width);
                       //table.HorizontalAlignment = Element.ALIGN_LEFT;
                       //table.DefaultCell.Border = 0;
                       //table.SpacingBefore = 00f;
                       //table.SpacingAfter = 0f;


                       //table.AddCell(new Phrase("MEMO", boldMemo));
                       //pdfDoc.Add(table);
                       
                       table = new PdfPTable(2);
                       width = new float[] { 0.2f, 1f };
                       table.WidthPercentage = 100;
                       table.SetWidths(width);
                       table.HorizontalAlignment = Element.ALIGN_RIGHT;
                       table.DefaultCell.Border = 0;
                       table.SpacingBefore = 0f;
                       table.SpacingAfter = 0f;

                       cell.Border = 0;
                       paragraph = new Paragraph("Nomor", normal);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       //cell = new PdfPCell();
                       //cell.Border = 0;
                       //paragraph = new Paragraph(":", normal);
                       //paragraph.Alignment = Element.ALIGN_RIGHT;
                       //cell.AddElement(paragraph);
                       //table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       paragraph = new Paragraph(":"+" "+letterDetail.letter.letterNumber, normal);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       paragraph = new Paragraph("Tanggal", normal);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       //cell = new PdfPCell();
                       //cell.Border = 0;
                       //paragraph = new Paragraph(":", normal);
                       //paragraph.Alignment = Element.ALIGN_RIGHT;
                       //cell.AddElement(paragraph);
                       //table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       paragraph = new Paragraph(":"+" "+Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normal);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       paragraph = new Paragraph("Kepada", normal);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       //cell = new PdfPCell();
                       //cell.Border = 0;
                       //paragraph = new Paragraph(":", normal);
                       //paragraph.Alignment = Element.ALIGN_RIGHT;
                       //cell.AddElement(paragraph);
                       //table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       no = 1;
                       foreach (var item in letterDetail.receiver)
                       {
                           if (letterDetail.receiver.Count() > 1)
                           {
                               paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                               paragraph.Alignment = Element.ALIGN_LEFT;
                               cell.AddElement(paragraph);
                           }
                           else
                           {
                               paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                               paragraph.Alignment = Element.ALIGN_LEFT;
                               cell.AddElement(paragraph);
                           }
                           no++;
                       }
                       table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       paragraph = new Paragraph("Tembusan", normal);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       //cell = new PdfPCell();
                       //cell.Border = 0;
                       //paragraph = new Paragraph(":", normal);
                       //paragraph.Alignment = Element.ALIGN_RIGHT;
                       //cell.AddElement(paragraph);
                       //table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       no = 1;
                       foreach (var item in letterDetail.copy)
                       {
                           if (letterDetail.copy.Count() > 1)
                           {
                               paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                               paragraph.Alignment = Element.ALIGN_LEFT;
                               cell.AddElement(paragraph);
                           }
                           else
                           {
                               paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                               paragraph.Alignment = Element.ALIGN_LEFT;
                               cell.AddElement(paragraph);
                           }
                           no++;
                       }
                       if (letterDetail.copy.Count() == 0)
                       {
                           paragraph = new Paragraph("-", normal);
                           paragraph.Alignment = Element.ALIGN_LEFT;
                           cell.AddElement(paragraph);
                       }
                       table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       paragraph = new Paragraph("Dari", normal);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       //cell = new PdfPCell();
                       //cell.Border = 0;
                       //paragraph = new Paragraph(":", normal);
                       //paragraph.Alignment = Element.ALIGN_RIGHT;
                       //cell.AddElement(paragraph);
                       //table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       paragraph = new Paragraph(":"+" "+letterDetail.sender[0].fullname + " - " + letterDetail.sender[0].positionName, normal);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       paragraph = new Paragraph("Perihal", normal);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       //cell = new PdfPCell();
                       //cell.Border = 0;
                       //paragraph = new Paragraph(":", normal);
                       //paragraph.Alignment = Element.ALIGN_RIGHT;
                       //cell.AddElement(paragraph);
                       //table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       paragraph = new Paragraph(":"+" "+letterDetail.letter.about, normal);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       cell = new PdfPCell();
                       cell.Border = 0;
                       paragraph = new Paragraph("Lampiran", normal);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       //cell = new PdfPCell();
                       //cell.Border = 0;
                       //paragraph = new Paragraph(":", normal);
                       //paragraph.Alignment = Element.ALIGN_RIGHT;
                       //cell.AddElement(paragraph);
                       //table.AddCell(cell);

                       var lampiran = letterDetail.letter.attachmentDesc == null ? "-" : letterDetail.letter.attachmentDesc;
                       cell = new PdfPCell();
                       cell.Border = 0;
                       paragraph = new Paragraph(":"+" "+lampiran, bold);
                       paragraph.Alignment = Element.ALIGN_LEFT;
                       cell.AddElement(paragraph);
                       table.AddCell(cell);

                       //Add table to document
                       pdfDoc.Add(table);

                       

                       Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                       line.SpacingBefore = 0f;
                       line.SetLeading(2F, 0.5F);
                       pdfDoc.Add(line);
                       Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                       lines.SpacingBefore = 0f;
                       line.SetLeading(0.5F, 0.5F);
                       pdfDoc.Add(line);

                       Paragraph liness = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                       liness.SpacingBefore = 0f;
                       liness.SetLeading(0.5F, 0.5F);
                       pdfDoc.Add(liness);

                       //Paragraph linesss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                       //linesss.SpacingBefore = 0f;
                       //linesss.SetLeading(0.5F, 0.5F);
                       //pdfDoc.Add(linesss);

                       //Paragraph linessss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                       //linessss.SpacingBefore = 0f;
                       //linessss.SetLeading(0.5F, 0.5F);
                       //pdfDoc.Add(linesss);

                       table = new PdfPTable(1);
                       width = new float[] { 2f };
                       table.WidthPercentage = 100;
                       table.SetWidths(width);
                       table.HorizontalAlignment = Element.ALIGN_LEFT;
                       table.DefaultCell.Border = 0;
                       table.SpacingBefore = 00f;
                       table.SpacingAfter = 0f;


                       table.AddCell(new Phrase("", normal));
                       pdfDoc.Add(table);


                       Paragraph lin = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                       lin.SpacingBefore = 0f;
                       lin.SetLeading(0.5F, 0.5F);
                       pdfDoc.Add(lin);
                       #endregion
                       
                       //ADD HTML CONTENT
                       htmlparser.Parse(sr);



                       #region Tanda Tangan

                       Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                       a.SpacingBefore = 0f;
                       a.SetLeading(0.5F, 0.5F);
                       pdfDoc.Add(a);



                       cell = new PdfPCell();
                       table.AddCell(new Phrase("", normal));

                       cell = new PdfPCell();
                       table.AddCell(new Phrase(" ", normal));
                       cell = new PdfPCell();
                       table.AddCell(new Phrase(" ", normal));
                       cell = new PdfPCell();
                       table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                       //table.AddCell(new Phrase("Best Regards", normal));
                       pdfDoc.Add(table);



                       string filename = letterDetail.sender[0].nip;


                       // Tanda tangan
                       var dirChecker = letterDetail.sender.OrderBy(p => p.idUserSender).ToList();
                
                       //senderList
                       //var dirChecker = letterDetail.checker.OrderBy(p => p.ordernumber == 1).ToList();

                       table = new PdfPTable(dirChecker.Count());
                       width = new float[dirChecker.Count()];
                       for (int i = 0; i < dirChecker.Count(); i++)
                       {
                           width[i] = 1f;
                       }
                       table.WidthPercentage = 100;
                       table.SetWidths(width);
                       table.HorizontalAlignment = Element.ALIGN_LEFT;
                       table.DefaultCell.Border = 0;
                       var no1 = 0;


                       //cell = new PdfPCell();
                       //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                       foreach (var item in dirChecker)
                       {
                           cell = new PdfPCell();
                           cell.Border = 0;




                           if (letterDetail.letter.letterNumber != "NO_LETTER")
                           {
                               if (letterDetail.letter.signatureType == 1)
                               {

                                   Image imagettd = GetSignatureImage(item.nip);
                                   //cell.AddElement(imagettd);
                                   if (imagettd != null)
                                   {
                                        //imagettd.ScaleToFit(90f,90f);
                                       //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                       cell.AddElement(imagettd);
                                   }
                                   else
                                   {
                                       var img = "File tanda tangan tidak ditemukan";
                                       paragraph = new Paragraph(img, normal);
                                       cell.AddElement(paragraph);
                                   }

                               }
                               else if (letterDetail.letter.signatureType == 2)
                               {


                                   Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                   QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                   cell.AddElement(QRCodeSignatureApprover);

                               }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                               {

                                   Image imagettd = GetSignatureImage(item.nip);
                                   cell.AddElement(imagettd);
                                   Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                   cell.AddElement(QRCodeSignatureApprover);
                                   //if (imagettd != null)
                                   //{
                                   //    cell.AddElement(imagettd);
                                   //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                   //    cell.AddElement(QRCodeSignatureApprover);
                                   //}
                                   //else
                                   //{
                                   //    var img = "File tanda tangan tidak di temukan";
                                   //    paragraph = new Paragraph(img, normal);
                                   //    cell.AddElement(paragraph);
                                   //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                   //    cell.AddElement(QRCodeSignatureApprover);
                                   //}

                               }
                               no1++;
                           }

                           paragraph = new Paragraph(item.fullname, underlineBold);
                           cell.AddElement(paragraph);
                           paragraph = new Paragraph(item.positionName, bold);
                           cell.AddElement(paragraph);

                           table.AddCell(cell);
                       }
                       // end tanda tangan
                       pdfDoc.Add(table);

                       #endregion

                       pdfDoc.NewPage();

                       #region Lampiran

                       PdfPTable table1 = new PdfPTable(2);
                       float[] width1 = new float[] { 0.667f, 2f };
                       table1.WidthPercentage = 100;
                       table1.SetWidths(width1);
                       table1.HorizontalAlignment = Element.ALIGN_LEFT;
                       table1.DefaultCell.Border = 0;
                       table1.SpacingBefore = 0f;
                       table1.SpacingAfter = 0f;

                       PdfPCell cell1 = new PdfPCell();
                       Paragraph paragraph1 = new Paragraph();

                       cell1 = new PdfPCell();
                       cell1.BorderWidthLeft = 1f;
                       cell1.BorderWidthRight = 1f;
                       cell1.BorderWidthTop = 1f;
                       cell1.BorderWidthBottom = 1f;
                       cell1.MinimumHeight = 30f;
                       paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                       paragraph1.Alignment = Element.ALIGN_LEFT;
                       cell1.AddElement(paragraph1);
                       table1.AddCell(cell1);

                       cell1 = new PdfPCell();
                       cell1.BorderWidthLeft = 0f;
                       cell1.BorderWidthRight = 1f;
                       cell1.BorderWidthTop = 1f;
                       cell1.BorderWidthBottom = 1f;
                       cell1.MinimumHeight = 30f;
                       paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                       paragraph1.Alignment = Element.ALIGN_LEFT;
                       cell1.AddElement(paragraph1);
                       table1.AddCell(cell1);

                       cell1 = new PdfPCell();
                       cell1.BorderWidthLeft = 1f;
                       cell1.BorderWidthRight = 1f;
                       cell1.BorderWidthTop = 0f;
                       cell1.BorderWidthBottom = 1f;
                       cell1.MinimumHeight = 100f;
                       paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                       paragraph1.Alignment = Element.ALIGN_LEFT;
                       cell1.AddElement(paragraph1);
                       table1.AddCell(cell1);

                       cell1 = new PdfPCell();
                       cell1.BorderWidthLeft = 0f;
                       cell1.BorderWidthRight = 1f;
                       cell1.BorderWidthTop = 0f;
                       cell1.BorderWidthBottom = 1f;
                       cell1.MinimumHeight = 100f;
                       paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                       paragraph1.Alignment = Element.ALIGN_LEFT;
                       cell1.AddElement(paragraph1);
                       table1.AddCell(cell1);

                       cell1 = new PdfPCell();
                       cell1.BorderWidthLeft = 1f;
                       cell1.BorderWidthRight = 1f;
                       cell1.BorderWidthTop = 0f;
                       cell1.BorderWidthBottom = 1f;
                       cell1.MinimumHeight = 100f;
                       paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                       paragraph1.Alignment = Element.ALIGN_LEFT;
                       cell1.AddElement(paragraph1);
                       no = 1;
                       foreach (var item in letterDetail.delibration)
                       {
                           if (letterDetail.receiver.Count() > 1)
                           {
                               paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                               paragraph1.Alignment = Element.ALIGN_LEFT;
                               cell1.AddElement(paragraph1);
                           }
                           else
                           {
                               paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                               paragraph1.Alignment = Element.ALIGN_LEFT;
                               cell1.AddElement(paragraph1);
                           }
                           no++;
                       }
                       table1.AddCell(cell1);

                       cell1 = new PdfPCell();
                       cell1.BorderWidthLeft = 0f;
                       cell1.BorderWidthRight = 1f;
                       cell1.BorderWidthTop = 0f;
                       cell1.BorderWidthBottom = 1f;
                       cell1.MinimumHeight = 100f;
                       paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                       paragraph1.Alignment = Element.ALIGN_LEFT;
                       cell1.AddElement(paragraph1);
                       no = 1;
                       foreach (var item in letterDetail.delibration)
                       {
                           if (letterDetail.receiver.Count() > 1)
                           {
                               paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                               paragraph1.Alignment = Element.ALIGN_LEFT;
                               cell1.AddElement(paragraph1);
                           }
                           else
                           {
                               paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                               paragraph1.Alignment = Element.ALIGN_LEFT;
                               cell1.AddElement(paragraph1);
                           }
                           no++;
                       }
                       table1.AddCell(cell1);

                       pdfDoc.Add(table1);

                       //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                       var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                       foreach (var item in checkerList)
                       {
                           var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                           var comment = "";
                           string approveDate = "";
                           if (getLogComment != null)
                           {
                               comment = getLogComment.comment;
                               approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                           }
                           table1 = new PdfPTable(3);
                           width = new float[] { 1f, 2f, 1f };
                           table1.WidthPercentage = 100;
                           table1.SetWidths(width);
                           table1.HorizontalAlignment = Element.ALIGN_LEFT;
                           table1.DefaultCell.Border = 0;
                           table1.SpacingBefore = 0f;
                           table1.SpacingAfter = 0f;

                           cell1 = new PdfPCell();
                           cell1.Rowspan = 2;
                           cell1.BorderWidthLeft = 1f;
                           cell1.BorderWidthRight = 1f;
                           cell1.BorderWidthTop = 0f;
                           cell1.BorderWidthBottom = 1f;
                           cell1.MinimumHeight = 120f;
                           if (letterDetail.letter.statusCode == 2)
                                {
                                    paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                                }
                                else if (letterDetail.letter.statusCode == 5)
                                {
                                    paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                                }
                                else
                                {
                                   if(item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                   { 
                                       paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                   }
                                   else
                                   {
                                       paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);
                                       
                                   }
                                }

                           paragraph1.Alignment = Element.ALIGN_LEFT;
                           cell1.AddElement(paragraph1);
                           paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                           paragraph1.Alignment = Element.ALIGN_LEFT;
                           cell1.AddElement(paragraph1);
                           table1.AddCell(cell1);

                           cell1 = new PdfPCell();
                           cell1.Rowspan = 2;
                           cell1.BorderWidthLeft = 0f;
                           cell1.BorderWidthRight = 1f;
                           cell1.BorderWidthTop = 0f;
                           cell1.BorderWidthBottom = 1f;
                           cell1.MinimumHeight = 120f;
                           paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                           paragraph1.Alignment = Element.ALIGN_LEFT;
                           cell1.AddElement(paragraph1);
                           paragraph1 = new Paragraph(comment, normalDeliberation);
                           paragraph1.Alignment = Element.ALIGN_LEFT;
                           cell1.AddElement(paragraph1);
                           table1.AddCell(cell1);

                           cell1 = new PdfPCell();
                           cell1.BorderWidthLeft = 0f;
                           cell1.BorderWidthRight = 1f;
                           cell1.BorderWidthTop = 0f;
                           cell1.BorderWidthBottom = 1f;
                           cell1.MinimumHeight = 40f;
                           paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                           paragraph1.Alignment = Element.ALIGN_LEFT;
                           //cell1.AddElement(paragraph1);
                           paragraph1 = new Paragraph(approveDate, normalDeliberation);
                           paragraph1.Alignment = Element.ALIGN_LEFT;
                           cell1.AddElement(paragraph1);
                           table1.AddCell(cell1);

                           cell1 = new PdfPCell();
                           cell1.BorderWidthLeft = 0f;
                           cell1.BorderWidthRight = 1f;
                           cell1.BorderWidthTop = 0f;
                           cell1.BorderWidthBottom = 1f;
                           paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                           paragraph1.Alignment = Element.ALIGN_LEFT;
                           cell1.AddElement(paragraph1);
                            
                           if (letterDetail.letter.letterNumber != "NO_LETTER")
                           {
                                
                               if (letterDetail.letter.signatureType == 1)
                               {
                                   Image imagettd = GetSignatureImage(item.nip);
                                   //cell.AddElement(imagettd);
                                   if (imagettd != null)
                                   {
                                       //cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                                       cell1.AddElement(imagettd);
                                   }
                                   else
                                   {
                                       var img = "File tanda tangan tidak ditemukan";
                                       paragraph1 = new Paragraph(img, normal);
                                       cell1.AddElement(paragraph1);
                                   }
                               }
                               else if (letterDetail.letter.signatureType == 2)
                               {

                                   Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                   cell1.AddElement(QRCodeSignatureApprover);
                               }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                               else
                               {





                                   Image imagettd = GetSignatureImage(item.nip);
                                   cell1.AddElement(imagettd);

                                   Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    
                                   cell1.AddElement(QRCodeSignatureApprover);


                               }
                           }

                                    paragraph1 = new Paragraph(item.fullname, bold);
                                    paragraph1.Alignment = Element.ALIGN_CENTER;
                                    cell1.AddElement(paragraph1);
                                    table1.AddCell(cell1);
                                    

                                    pdfDoc.Add(table1);
                        }


                       #endregion

                       pdfDoc.Close();

                       var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                       var letterNumber = letterDetail.letter.letterNumber;
                       byte[] bytess = ms.ToArray();
                       byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                       
                       ms.Close();

                       return File(bytes.ToArray(), "application/pdf", fileName);
                    }

                #endregion
            }

            //Done
            private FileContentResult PrivSuratMemoDepartement(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {
                #region Surat Memo Departement

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                    StringReader sr = new StringReader(sb.ToString());
                    //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                    //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                    //Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 113.4f, 99.225f);
                    Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 90.4f, 99.225f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfWriter.PageEvent = new PDFHeaderEvent();
                        //pdfWriter.PageEvent = new PDFHeaderEvent2();
                        pdfDoc.Open();
                        //Font
                        int no;
                        var fontName = "Calibri";
                        //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                        //FontFactory.Register(fontPath);

                        Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                        Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                        Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        Font boldMemo = FontFactory.GetFont(fontName, 14, Font.BOLD, BaseColor.BLACK);

                        Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                        Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        // Colors
                        BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                        BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));

                        PdfPTable table = new PdfPTable(2);
                        float[] width = new float[] { 0.667f, 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        PdfPCell cell = new PdfPCell();
                        Paragraph paragraph = new Paragraph();


                                #region header
                
                                //cell = new PdfPCell();
                                //cell.Border = Rectangle.NO_BORDER;
                                //cell.HorizontalAlignment= Element.ALIGN_LEFT;
                                //cell.Phrase = new Phrase("", bold);
                                //table.AddCell(cell);

                                //Image logo = GetLogoImage("logobni");
                                //logo.ScalePercent(55);
                                //cell = new PdfPCell(logo);
                                //cell.Border = Rectangle.NO_BORDER;
                                //cell.HorizontalAlignment= Element.ALIGN_RIGHT;
                                //table.AddCell(cell);
                                //pdfDoc.Add(table);

                                //table = new PdfPTable(1);
                                //width = new float[] { 2f };
                                //table.WidthPercentage = 100;
                                //table.SetWidths(width);
                                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                                //table.DefaultCell.Border = 0;
                                //table.SpacingBefore = 00f;
                                //table.SpacingAfter = 0f;


                                //table.AddCell(new Phrase("MEMO", boldMemo));
                                //pdfDoc.Add(table);

                                table = new PdfPTable(2);
                                width = new float[] { 0.2f, 1f };
                                table.WidthPercentage = 100;
                                table.SetWidths(width);
                                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                                table.DefaultCell.Border = 0;
                                table.SpacingBefore = 0f;
                                table.SpacingAfter = 0f;

                                cell.Border = 0;
                                paragraph = new Paragraph("Nomor", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(":"+" "+letterDetail.letter.letterNumber, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Tanggal", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(":"+" "+Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Kepada", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                no = 1;
                                foreach (var item in letterDetail.receiver)
                                {
                                    if (letterDetail.receiver.Count() > 1)
                                    {
                                        paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                        paragraph.Alignment = Element.ALIGN_LEFT;
                                        cell.AddElement(paragraph);
                                    }
                                    else
                                    {
                                        paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                        paragraph.Alignment = Element.ALIGN_LEFT;
                                        cell.AddElement(paragraph);
                                    }
                                    no++;
                                }
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Tembusan", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                no = 1;
                                foreach (var item in letterDetail.copy)
                                {
                                    if (letterDetail.copy.Count() > 1)
                                    {
                                        paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                        paragraph.Alignment = Element.ALIGN_LEFT;
                                        cell.AddElement(paragraph);
                                    }
                                    else
                                    {
                                        paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                        paragraph.Alignment = Element.ALIGN_LEFT;
                                        cell.AddElement(paragraph);
                                    }
                                    no++;
                                }
                                if (letterDetail.copy.Count() == 0)
                                {
                                    paragraph = new Paragraph("-", normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Dari", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(":"+" "+letterDetail.sender[0].fullname + " - " + letterDetail.sender[0].positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Perihal", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(":"+" "+letterDetail.letter.about, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Lampiran", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                var lampiran = letterDetail.letter.attachmentDesc == null ? "-" : letterDetail.letter.attachmentDesc;
                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(":"+" "+lampiran, bold);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //Add table to document
                                pdfDoc.Add(table);

                        

                                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                                line.SpacingBefore = 0f;
                                line.SetLeading(2F, 0.5F);
                                pdfDoc.Add(line);
                                Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                                lines.SpacingBefore = 0f;
                                line.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(line);

                                Paragraph liness = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                liness.SpacingBefore = 0f;
                                liness.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(liness);

                                //Paragraph linesss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                //linesss.SpacingBefore = 0f;
                                //linesss.SetLeading(0.5F, 0.5F);
                                //pdfDoc.Add(linesss);

                                //Paragraph linessss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                //linessss.SpacingBefore = 0f;
                                //linessss.SetLeading(0.5F, 0.5F);
                                //pdfDoc.Add(linesss);

                                table = new PdfPTable(1);
                                width = new float[] { 2f };
                                table.WidthPercentage = 100;
                                table.SetWidths(width);
                                table.HorizontalAlignment = Element.ALIGN_LEFT;
                                table.DefaultCell.Border = 0;
                                table.SpacingBefore = 00f;
                                table.SpacingAfter = 0f;


                                table.AddCell(new Phrase("Dengan hormat", normal));
                                pdfDoc.Add(table);


                                Paragraph lin = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                lin.SpacingBefore = 0f;
                                lin.SetLeading(0.5F, 0.5F);
                                pdfDoc.Add(lin);

                                #endregion

                            //ADD HTML CONTENT
                            htmlparser.Parse(sr);

                            #region Tanda Tangan

                            Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                            a.SpacingBefore = 0f;
                            a.SetLeading(0.5F, 0.5F);
                            pdfDoc.Add(a);



                            cell = new PdfPCell();
                            table.AddCell(new Phrase("", normal));

                            cell = new PdfPCell();
                            table.AddCell(new Phrase(" ", normal));
                            cell = new PdfPCell();
                            table.AddCell(new Phrase(" ", normal));
                            cell = new PdfPCell();
                            table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                            //table.AddCell(new Phrase("Best Regards", normal));
                            pdfDoc.Add(table);



                            string filename = letterDetail.sender[0].nip;


                            // Tanda tangan
                            var dirChecker = letterDetail.sender.OrderBy(p => p.idUserSender).ToList();
                            //var dirChecker = letterDetail.checker.Where(p => p.idLevelChecker == 2 || p.idLevelChecker == 1).ToList();
                            //var dirChecker = letterDetail.checker.Where(p => p.ordernumber == 1).ToList();

                            table = new PdfPTable(dirChecker.Count());
                            width = new float[dirChecker.Count()];
                            for (int i = 0; i < dirChecker.Count(); i++)
                            {
                                width[i] = 1f;
                            }
                            table.WidthPercentage = 100;
                            table.SetWidths(width);
                            table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCell.Border = 0;
                            var no1 = 0;


                            //cell = new PdfPCell();
                            //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                            foreach (var item in dirChecker)
                            {
                                cell = new PdfPCell();
                                cell.Border = 0;




                                if (letterDetail.letter.letterNumber != "NO_LETTER")
                                {
                                    if (letterDetail.letter.signatureType == 1)
                                    {

                                        Image imagettd = GetSignatureImage(item.nip);
                                        //cell.AddElement(imagettd);
                                        if (imagettd != null)
                                        {
                                            cell.AddElement(imagettd);
                                        }
                                        else
                                        {
                                            var img = "File tanda tangan tidak ditemukan";
                                            paragraph = new Paragraph(img, normal);
                                            cell.AddElement(paragraph);
                                        }

                                    }
                                    else if (letterDetail.letter.signatureType == 2)
                                    {


                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                        cell.AddElement(QRCodeSignatureApprover);
                                    }
                                    else if (letterDetail.letter.signatureType == 3)
                                    {
                                        var img = "";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                        Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessia.SpacingBefore = 0f;
                                        linessia.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessia);

                                        Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaa.SpacingBefore = 0f;
                                        linessiaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaa);

                                        Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaaa.SpacingBefore = 0f;
                                        linessiaaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaaa);
                                    }
                                    else
                                    {

                                        Image imagettd = GetSignatureImage(item.nip);
                                        cell.AddElement(imagettd);
                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        cell.AddElement(QRCodeSignatureApprover);
                                        //if (imagettd != null)
                                        //{
                                        //    cell.AddElement(imagettd);
                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}
                                        //else
                                        //{
                                        //    var img = "File tanda tangan tidak di temukan";
                                        //    paragraph = new Paragraph(img, normal);
                                        //    cell.AddElement(paragraph);
                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}

                                    }
                                    no1++;
                                }

                                paragraph = new Paragraph(item.fullname, underlineBold);
                                cell.AddElement(paragraph);
                                paragraph = new Paragraph(item.positionName, bold);
                                cell.AddElement(paragraph);

                                table.AddCell(cell);
                            }
                            // end tanda tangan
                            pdfDoc.Add(table);

                            #endregion

                            pdfDoc.NewPage();

                            #region Lampiran

                            PdfPTable table1 = new PdfPTable(2);
                            float[] width1 = new float[] { 0.667f, 2f };
                            table1.WidthPercentage = 100;
                            table1.SetWidths(width1);
                            table1.HorizontalAlignment = Element.ALIGN_LEFT;
                            table1.DefaultCell.Border = 0;
                            table1.SpacingBefore = 0f;
                            table1.SpacingAfter = 0f;

                            PdfPCell cell1 = new PdfPCell();
                            Paragraph paragraph1 = new Paragraph();

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 1f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 30f;
                            paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 1f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 30f;
                            paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            no = 1;
                            foreach (var item in letterDetail.delibration)
                            {
                                if (letterDetail.receiver.Count() > 1)
                                {
                                    paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                else
                                {
                                    paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                no++;
                            }
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 100f;
                            paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            no = 1;
                            foreach (var item in letterDetail.delibration)
                            {
                                if (letterDetail.receiver.Count() > 1)
                                {
                                    paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                else
                                {
                                    paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                                    paragraph1.Alignment = Element.ALIGN_LEFT;
                                    cell1.AddElement(paragraph1);
                                }
                                no++;
                            }
                            table1.AddCell(cell1);

                            pdfDoc.Add(table1);

                            //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                            var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                            foreach (var item in checkerList)
                            {
                                var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                                var comment = "";
                                string approveDate = "";
                                if (getLogComment != null)
                                {
                                    comment = getLogComment.comment;
                                    approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                                }
                                table1 = new PdfPTable(3);
                                width = new float[] { 1f, 2f, 1f };
                                table1.WidthPercentage = 100;
                                table1.SetWidths(width);
                                table1.HorizontalAlignment = Element.ALIGN_LEFT;
                                table1.DefaultCell.Border = 0;
                                table1.SpacingBefore = 0f;
                                table1.SpacingAfter = 0f;

                                cell1 = new PdfPCell();
                                cell1.Rowspan = 2;
                                cell1.BorderWidthLeft = 1f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                cell1.MinimumHeight = 120f;
                                if (letterDetail.letter.statusCode == 2)
                                {
                                    paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                                }
                                else if (letterDetail.letter.statusCode == 5)
                                {
                                    paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                                }
                                else
                                {
                                   if(item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                   { 
                                       paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                   }
                                   else
                                   {
                                       paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);
                                       
                                   }
                                }

                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);

                                cell1 = new PdfPCell();
                                cell1.Rowspan = 2;
                                cell1.BorderWidthLeft = 0f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                cell1.MinimumHeight = 120f;
                                paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                paragraph1 = new Paragraph(comment, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);

                                cell1 = new PdfPCell();
                                cell1.BorderWidthLeft = 0f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                cell1.MinimumHeight = 40f;
                                paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                //cell1.AddElement(paragraph);
                                paragraph1 = new Paragraph(approveDate, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);

                                cell1 = new PdfPCell();
                                cell1.BorderWidthLeft = 0f;
                                cell1.BorderWidthRight = 1f;
                                cell1.BorderWidthTop = 0f;
                                cell1.BorderWidthBottom = 1f;
                                paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                                if (letterDetail.letter.letterNumber != "NO_LETTER")
                                {
                                    if (letterDetail.letter.signatureType == 1)
                                    {
                                        Image imagettd = GetSignatureImage(item.nip);
                                        //cell.AddElement(imagettd);
                                        if (imagettd != null)
                                        {
                                            cell1.AddElement(imagettd);
                                        }
                                        else
                                        {
                                            var img = "File tanda tangan tidak ditemukan";
                                            paragraph1 = new Paragraph(img, normal);
                                            cell1.AddElement(paragraph1);
                                        }
                                    }
                                    else if (letterDetail.letter.signatureType == 2)
                                    {

                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        cell1.AddElement(QRCodeSignatureApprover);
                                    }
                                    else if (letterDetail.letter.signatureType == 3)
                                    {
                                        var img = "";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                        Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessia.SpacingBefore = 0f;
                                        linessia.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessia);

                                        Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaa.SpacingBefore = 0f;
                                        linessiaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaa);

                                        Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                        linessiaaa.SpacingBefore = 0f;
                                        linessiaaa.SetLeading(0.5F, 0.5F);
                                        pdfDoc.Add(linessiaaa);
                                    }
                                    else
                                    {
                                        //innertable1 = new PdfPtable1(2);
                                        //float[] widthinner = new float[] { 0.3f, 0.3f };
                                        //innertable1.WidthPercentage = 100;
                                        //innertable1.SetWidths(widthinner);
                                        //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                        //innertable1.DefaultCell.Border = 1;
                                        //innertable1.DefaultCell.PaddingBottom = 8;

                                        //innercell = new PdfPCell();
                                        //innercell.BorderWidthLeft = 1f;
                                        //innercell.BorderWidthRight = 1f;
                                        //innercell.BorderWidthTop = 1f;
                                        //innercell.BorderWidthBottom = 1f;




                                        Image imagettd = GetSignatureImage(item.nip);
                                        cell1.AddElement(imagettd);

                                        Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        cell1.AddElement(QRCodeSignatureApprover);
                                        //if (imagettd !=null)
                                        //{
                                        //    cell.AddElement(imagettd);

                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}
                                        //else
                                        //{
                                        //    var img = "File tanda tangan tidak di temukan";
                                        //    paragraph = new Paragraph(img, normal);

                                        //    cell.AddElement(paragraph);
                                        //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                        //    cell.AddElement(QRCodeSignatureApprover);
                                        //}

                                    }
                                }

                                paragraph1 = new Paragraph(item.fullname, bold);
                                paragraph1.Alignment = Element.ALIGN_CENTER;
                                cell1.AddElement(paragraph1);
                                table1.AddCell(cell1);


                                pdfDoc.Add(table1);
                            }


                            #endregion


                            pdfDoc.Close();

                        var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                        var letterNumber = letterDetail.letter.letterNumber;
                        byte[] bytess = ms.ToArray();
                        byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                        ms.Close();

                        return File(bytes.ToArray(), "application/pdf", fileName);
                    }

                #endregion
            }

            //Done(Masi Ikuti Template Memo Direksi)
            private FileContentResult PrivSuratKeterangan(string letternumber, OutputDetailMemo letterDetail, string fileName)
            {
                #region Surat Keterangan Divisi

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div style='padding-left:2px;'>" + letterDetail.letterContent.letterContent + "</div>");
                    StringReader sr = new StringReader(sb.ToString());
                    //Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                    //Document pdfDoc = new Document(PageSize.A4, 56, 56, 113, 97);
                    Document pdfDoc = new Document(PageSize.A4, 56.7f, 56.7f, 66.5f, 99.225f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();
                        //Font
                        int no;
                        var fontName = "Calibri";
                        //string fontPath = Path.Combine(_environment.WebRootPath, "fonts/Calibri.ttf");
                        //FontFactory.Register(fontPath);

                        Font normal = FontFactory.GetFont(fontName, 12, Font.NORMAL, BaseColor.BLACK);
                        Font bold = FontFactory.GetFont(fontName, 12, Font.BOLD, BaseColor.BLACK);
                        Font underline = FontFactory.GetFont(fontName, 12, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineBold = FontFactory.GetFont(fontName, 12, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);

                        Font normalDeliberation = FontFactory.GetFont(fontName, 10, Font.NORMAL, BaseColor.BLACK);
                        Font underlineDeliberation = FontFactory.GetFont(fontName, 10, Font.UNDERLINE, BaseColor.BLACK);
                        Font underlineDeliberationBold = FontFactory.GetFont(fontName, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        // Colors
                        BaseColor colorBlack = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"));
                        BaseColor colorWhite = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));



                        PdfPTable table = new PdfPTable(2);
                        float[] width = new float[] { 0.667f, 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 0f;
                        table.SpacingAfter = 0f;

                        PdfPCell cell = new PdfPCell();
                        Paragraph paragraph = new Paragraph();


                        #region header
                        //add new page

                        //table = new PdfPTable(1);
                        //width = new float[] { 2f };
                        //table.WidthPercentage = 100;
                        //table.SetWidths(width);
                        //table.HorizontalAlignment = Element.ALIGN_LEFT;
                        //table.DefaultCell.Border = 0;
                        //table.SpacingBefore = 00f;
                        //table.SpacingAfter = 0f;


                        //table.AddCell(new Phrase("MEMO", bold));
                        //pdfDoc.Add(table);

                        table = new PdfPTable(2);
                                width = new float[] { 0.2f, 1f };
                                table.WidthPercentage = 100;
                                table.SetWidths(width);
                                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                                table.DefaultCell.Border = 0;
                                table.SpacingBefore = 0f;
                                table.SpacingAfter = 0f;

                                cell.Border = 0;
                                paragraph = new Paragraph("Nomor", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(":"+" "+letterDetail.letter.letterNumber, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Tanggal", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(":"+" "+Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Kepada", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                no = 1;
                                foreach (var item in letterDetail.receiver)
                                {
                                    if (letterDetail.receiver.Count() > 1)
                                    {
                                        paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                        paragraph.Alignment = Element.ALIGN_LEFT;
                                        cell.AddElement(paragraph);
                                    }
                                    else
                                    {
                                        paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                        paragraph.Alignment = Element.ALIGN_LEFT;
                                        cell.AddElement(paragraph);
                                    }
                                    no++;
                                }
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Tembusan", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                no = 1;
                                foreach (var item in letterDetail.copy)
                                {
                                    if (letterDetail.copy.Count() > 1)
                                    {
                                        paragraph = new Paragraph(":"+" "+no + ". " + item.fullname + " - " + item.positionName, normal);
                                        paragraph.Alignment = Element.ALIGN_LEFT;
                                        cell.AddElement(paragraph);
                                    }
                                    else
                                    {
                                        paragraph = new Paragraph(":"+" "+item.fullname + " - " + item.positionName, normal);
                                        paragraph.Alignment = Element.ALIGN_LEFT;
                                        cell.AddElement(paragraph);
                                    }
                                    no++;
                                }
                                if (letterDetail.copy.Count() == 0)
                                {
                                    paragraph = new Paragraph("-", normal);
                                    paragraph.Alignment = Element.ALIGN_LEFT;
                                    cell.AddElement(paragraph);
                                }
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Dari", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(":"+" "+letterDetail.sender[0].fullname + " - " + letterDetail.sender[0].positionName, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Perihal", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(":"+" "+letterDetail.letter.about, normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph("Lampiran", normal);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //cell = new PdfPCell();
                                //cell.Border = 0;
                                //paragraph = new Paragraph(":", normal);
                                //paragraph.Alignment = Element.ALIGN_RIGHT;
                                //cell.AddElement(paragraph);
                                //table.AddCell(cell);

                                var lampiran = letterDetail.letter.attachmentDesc == null ? "-" : letterDetail.letter.attachmentDesc;
                                cell = new PdfPCell();
                                cell.Border = 0;
                                paragraph = new Paragraph(":"+" "+lampiran, bold);
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                cell.AddElement(paragraph);
                                table.AddCell(cell);

                                //Add table to document
                                pdfDoc.Add(table);


                        Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                        line.SpacingBefore = 0f;
                        line.SetLeading(2F, 0.5F);
                        pdfDoc.Add(line);
                        Paragraph lines = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorBlack, Element.ALIGN_CENTER, 1)));
                        lines.SpacingBefore = 0f;
                        line.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(line);

                        Paragraph liness = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        liness.SpacingBefore = 0f;
                        liness.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(liness);

                        //Paragraph linesss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        //linesss.SpacingBefore = 0f;
                        //linesss.SetLeading(0.5F, 0.5F);
                        //pdfDoc.Add(linesss);


                        table = new PdfPTable(1);
                        width = new float[] { 2f };
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        table.SpacingBefore = 00f;
                        table.SpacingAfter = 0f;


                        //table.AddCell(new Phrase("Dengan hormat", normal));
                        //pdfDoc.Add(table);

                        Paragraph linessss = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        linessss.SpacingBefore = 0f;
                        linessss.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(linessss);

                        Paragraph lin = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        lin.SpacingBefore = 0f;
                        lin.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(lin);

                        #endregion

                        //ADD HTML CONTENT
                        htmlparser.Parse(sr);


                        #region Tanda Tangan

                        Paragraph a = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                        a.SpacingBefore = 0f;
                        a.SetLeading(0.5F, 0.5F);
                        pdfDoc.Add(a);



                        cell = new PdfPCell();
                        table.AddCell(new Phrase("", normal));

                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase(" ", normal));
                        cell = new PdfPCell();
                        table.AddCell(new Phrase("Hormat kami, \n" + "PT BNI Life Insurance", normal));
                        //table.AddCell(new Phrase("Best Regards", normal));
                        pdfDoc.Add(table);



                        string filename = letterDetail.sender[0].nip;


                        // Tanda tangan
                        var dirChecker = letterDetail.sender.OrderBy(p => p.idUserSender).ToList();
                        //var dirChecker = letterDetail.checker.Where(p => p.idLevelChecker == 2 || p.idLevelChecker == 1).ToList();
                        //var dirChecker = letterDetail.checker.Where(p => p.ordernumber == 1).ToList();

                        table = new PdfPTable(dirChecker.Count());
                        width = new float[dirChecker.Count()];
                        for (int i = 0; i < dirChecker.Count(); i++)
                        {
                            width[i] = 1f;
                        }
                        table.WidthPercentage = 100;
                        table.SetWidths(width);
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.DefaultCell.Border = 0;
                        var no1 = 0;


                        //cell = new PdfPCell();
                        //table.AddCell(new Phrase("Direksi PT BNI Life Insurance", normal));
                        foreach (var item in dirChecker)
                        {
                            cell = new PdfPCell();
                            cell.Border = 0;




                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph = new Paragraph(img, normal);
                                        cell.AddElement(paragraph);
                                    }

                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {


                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    QRCodeSignatureApprover.ScaleAbsoluteHeight(10f);
                                    cell.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {

                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell.AddElement(imagettd);
                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd != null)
                                    //{
                                    //    cell.AddElement(imagettd);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);
                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                                no1++;
                            }

                            paragraph = new Paragraph(item.fullname, underlineBold);
                            cell.AddElement(paragraph);
                            paragraph = new Paragraph(item.positionName, bold);
                            cell.AddElement(paragraph);

                            table.AddCell(cell);
                        }
                        // end tanda tangan
                        pdfDoc.Add(table);

                        #endregion

                        pdfDoc.NewPage();

                        #region Lampiran

                        PdfPTable table1 = new PdfPTable(2);
                        float[] width1 = new float[] { 0.667f, 2f };
                        table1.WidthPercentage = 100;
                        table1.SetWidths(width1);
                        table1.HorizontalAlignment = Element.ALIGN_LEFT;
                        table1.DefaultCell.Border = 0;
                        table1.SpacingBefore = 0f;
                        table1.SpacingAfter = 0f;

                        PdfPCell cell1 = new PdfPCell();
                        Paragraph paragraph1 = new Paragraph();

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Nomor Registrasi Memo/Registration Number:" + letterDetail.letter.letterDeliberationNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 1f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 30f;
                        paragraph1 = new Paragraph("Tanggal Registrasi Memo/Date of Memo Registration : " + Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy"), normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Nomor Memo : " + letterDetail.letter.letterNumber, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("(Ringkasan Memo/Summary of the proposal) \n" + letterDetail.letterContent.summary, normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 1f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Deliberation:", normalDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.fullname + " - " + item.positionName, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        cell1 = new PdfPCell();
                        cell1.BorderWidthLeft = 0f;
                        cell1.BorderWidthRight = 1f;
                        cell1.BorderWidthTop = 0f;
                        cell1.BorderWidthBottom = 1f;
                        cell1.MinimumHeight = 100f;
                        paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                        paragraph1.Alignment = Element.ALIGN_LEFT;
                        cell1.AddElement(paragraph1);
                        no = 1;
                        foreach (var item in letterDetail.delibration)
                        {
                            if (letterDetail.receiver.Count() > 1)
                            {
                                paragraph1 = new Paragraph(no + ". " + item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            else
                            {
                                paragraph1 = new Paragraph(item.commentdlbrt, normalDeliberation);
                                paragraph1.Alignment = Element.ALIGN_LEFT;
                                cell1.AddElement(paragraph1);
                            }
                            no++;
                        }
                        table1.AddCell(cell1);

                        pdfDoc.Add(table1);

                        //var checkerList = letterDetail.checker.Where(p => p.ordernumber != 1).OrderBy(p => p.idLevelChecker).ToList();
                        var checkerList = letterDetail.checker.OrderBy(p => p.idLevelChecker).ToList();
                        foreach (var item in checkerList)
                        {
                            var getLogComment = letterDetail.log.Where(p => p.idUserLog == item.idUserChecker && p.description.Contains("pemeriksa ke")).OrderByDescending(p => p.createdOn).FirstOrDefault();
                            var comment = "";
                            string approveDate = "";
                            if (getLogComment != null)
                            {
                                comment = getLogComment.comment;
                                approveDate = Convert.ToDateTime(getLogComment.createdOn).ToString("dd MMMM yyyy");
                            }
                            table1 = new PdfPTable(3);
                            width = new float[] { 1f, 2f, 1f };
                            table1.WidthPercentage = 100;
                            table1.SetWidths(width);
                            table1.HorizontalAlignment = Element.ALIGN_LEFT;
                            table1.DefaultCell.Border = 0;
                            table1.SpacingBefore = 0f;
                            table1.SpacingAfter = 0f;

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 1f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            if (letterDetail.letter.statusCode == 2)
                                {
                                    paragraph1 = new Paragraph("Belum diputuskan / Undecided \n", normalDeliberation);
                                }
                                else if (letterDetail.letter.statusCode == 5)
                                {
                                    paragraph1 = new Paragraph("Ditolak / Reject \n", normalDeliberation);
                                }
                                else
                                {
                                   if(item.idLevelChecker == 2 ||item.idLevelChecker == 1)
                                   { 
                                       paragraph1 = new Paragraph("Disetujui / Approved \n", normalDeliberation);
                                   }
                                   else
                                   {
                                       paragraph1 = new Paragraph("Mengetahui / Understand \n", normalDeliberation);
                                       
                                   }
                                }

                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(item.positionName, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.Rowspan = 2;
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 120f;
                            paragraph1 = new Paragraph("Catatan / Notes : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            paragraph1 = new Paragraph(comment, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            cell1.MinimumHeight = 40f;
                            paragraph1 = new Paragraph("Tanggal : ", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            //cell1.AddElement(paragraph);
                            paragraph1 = new Paragraph(approveDate, normalDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);

                            cell1 = new PdfPCell();
                            cell1.BorderWidthLeft = 0f;
                            cell1.BorderWidthRight = 1f;
                            cell1.BorderWidthTop = 0f;
                            cell1.BorderWidthBottom = 1f;
                            paragraph1 = new Paragraph("Tanda Tangan : \n", underlineDeliberation);
                            paragraph1.Alignment = Element.ALIGN_LEFT;
                            cell1.AddElement(paragraph1);
                            if (letterDetail.letter.letterNumber != "NO_LETTER")
                            {
                                if (letterDetail.letter.signatureType == 1)
                                {
                                    Image imagettd = GetSignatureImage(item.nip);
                                    //cell.AddElement(imagettd);
                                    if (imagettd != null)
                                    {
                                        cell1.AddElement(imagettd);
                                    }
                                    else
                                    {
                                        var img = "File tanda tangan tidak ditemukan";
                                        paragraph1 = new Paragraph(img, normal);
                                        cell1.AddElement(paragraph1);
                                    }
                                }
                                else if (letterDetail.letter.signatureType == 2)
                                {

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                }
                                else if (letterDetail.letter.signatureType == 3)
                                {
                                    var img = "";
                                    paragraph = new Paragraph(img, normal);
                                    cell.AddElement(paragraph);
                                    Paragraph linessia = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessia.SpacingBefore = 0f;
                                    linessia.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessia);

                                    Paragraph linessiaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaa.SpacingBefore = 0f;
                                    linessiaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaa);

                                    Paragraph linessiaaa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2F, 100.0F, colorWhite, Element.ALIGN_CENTER, 1)));
                                    linessiaaa.SpacingBefore = 0f;
                                    linessiaaa.SetLeading(0.5F, 0.5F);
                                    pdfDoc.Add(linessiaaa);
                                }
                                else
                                {
                                    //innertable1 = new PdfPtable1(2);
                                    //float[] widthinner = new float[] { 0.3f, 0.3f };
                                    //innertable1.WidthPercentage = 100;
                                    //innertable1.SetWidths(widthinner);
                                    //innertable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                    //innertable1.DefaultCell.Border = 1;
                                    //innertable1.DefaultCell.PaddingBottom = 8;

                                    //innercell = new PdfPCell();
                                    //innercell.BorderWidthLeft = 1f;
                                    //innercell.BorderWidthRight = 1f;
                                    //innercell.BorderWidthTop = 1f;
                                    //innercell.BorderWidthBottom = 1f;




                                    Image imagettd = GetSignatureImage(item.nip);
                                    cell1.AddElement(imagettd);

                                    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    cell1.AddElement(QRCodeSignatureApprover);
                                    //if (imagettd !=null)
                                    //{
                                    //    cell.AddElement(imagettd);

                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}
                                    //else
                                    //{
                                    //    var img = "File tanda tangan tidak di temukan";
                                    //    paragraph = new Paragraph(img, normal);

                                    //    cell.AddElement(paragraph);
                                    //    Image QRCodeSignatureApprover = GetBarcodeSignature("NIP : " + item.nip + "\nNAMA : " + item.fullname + "\nJabatan : " + item.positionName);
                                    //    cell.AddElement(QRCodeSignatureApprover);
                                    //}

                                }
                            }

                            paragraph1 = new Paragraph(item.fullname, bold);
                            paragraph1.Alignment = Element.ALIGN_CENTER;
                            cell1.AddElement(paragraph1);
                            table1.AddCell(cell1);


                            pdfDoc.Add(table1);
                        }


                        #endregion

                        pdfDoc.Close();

                        var dateLetterFormat = Convert.ToDateTime(letterDetail.letter.letterDate).ToString("dd MMMM yyyy");
                        var letterNumber = letterDetail.letter.letterNumber;
                        byte[] bytess = ms.ToArray();
                        byte[] bytes = AddPageNumbers(bytess, dateLetterFormat, letterNumber, "Memo");
                        ms.Close();

                        return File(bytes.ToArray(), "application/pdf", fileName);
                    }

                    #endregion
            }

        #endregion

     
        // Kelas untuk membuat header
        public class PDFHeaderEvent : PdfPageEventHelper
        {
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                var fontName = "Calibri";
                Font boldMemo = FontFactory.GetFont(fontName, 16, Font.BOLD, BaseColor.BLACK);
                // Buat tabel dengan dua kolom
                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.SetWidths(new float[] { table.TotalWidth - 60f });


                // Tambahkan logo ke kolom pertama
                using (var client = new WebClient())
                {
                    byte[] imageBytes = client.DownloadData("http://10.22.13.34/EOFFICEBNILWEB/download/logo/logobni.png");

                    using (var stream = new MemoryStream(imageBytes))
                    {
                        Image logo = Image.GetInstance(stream);
                        logo.ScaleToFit(90f, 90f);
                        PdfPCell logoCell = new PdfPCell(logo);
                        logoCell.Border = 0;
                        logoCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table.AddCell(logoCell);
                    }
                }

                //Tambahkan nama ke kolom kedua
                PdfPCell nameCell = new PdfPCell(new Phrase("MEMO", boldMemo));
                nameCell.Border = 0;
                nameCell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(nameCell);

                // Tempatkan tabel di atas dokumen
                table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 8, writer.DirectContent);



            }
        }

        public class PDFHeaderEvent2 : PdfPageEventHelper
        {
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                // Buat tabel dengan dua kolom
                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.SetWidths(new float[] { table.TotalWidth - 60f });


                // Buat tabel dengan dua kolom
                PdfPTable table1 = new PdfPTable(1);
                table1.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                table1.DefaultCell.Border = Rectangle.NO_BORDER;
                table1.SetWidths(new float[] { table.TotalWidth - 60f });




                //Tambahkan nama ke kolom kedua
                PdfPCell nameCell1 = new PdfPCell(new Phrase("Nama Anda"));
                nameCell1.Border = 1;
                nameCell1.HorizontalAlignment = Element.ALIGN_LEFT;
                table1.AddCell(nameCell1);

                // Tempatkan tabel di atas dokumen
                table1.WriteSelectedRows(0, -2, document.LeftMargin, document.PageSize.Height - 7, writer.DirectContent);



            }
        }

        public static byte[] AddPageNumbers(byte[] pdf, string dateLetterFormat, string letterNumber, string letterType)
        {
            MemoryStream ms = new MemoryStream();
            // we create a reader for a certain document
            PdfReader reader = new PdfReader(pdf);
            // we retrieve the total number of pages
            int n = reader.NumberOfPages;
            // we retrieve the size of the first page
            Rectangle psize = reader.GetPageSize(1);

            // step 1: creation of a document-object
            Document document = new Document(psize, 50, 50, 50, 150);
            // step 2: we create a writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            // step 3: we open the document

            document.Open();
            // step 4: we add content
            PdfContentByte cb = writer.DirectContent;

            int p = 0;
            //Console.WriteLine("There are " + n + " pages in the document.");
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                document.NewPage();
                p++;

                PdfImportedPage importedPage = writer.GetImportedPage(reader, page);
                cb.AddTemplate(importedPage, 0, 0);

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                cb.MoveTo(420, 10);
                cb.LineTo(420, 30);
                cb.Stroke();
                cb.MoveTo(420, 10);
                cb.LineTo(580, 10);
                cb.Stroke();
                cb.MoveTo(580, 30);
                cb.LineTo(420, 30);
                cb.Stroke();
                cb.MoveTo(500, 10);
                cb.LineTo(500, 30);
                cb.Stroke();
                cb.MoveTo(580, 10);
                cb.LineTo(580, 30);
                cb.Stroke();

                cb.BeginText();
                cb.SetFontAndSize(bf, 10);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, letterType + " No. " + letterNumber + " tanggal " + dateLetterFormat, 10, 10, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Page " + p + " of " + n, 490, 15, 0);

                cb.EndText();
            }
            // step 5: we close the document
            document.Close();
            return ms.ToArray();
        }


        private Image GetBarcodeSignature(string text)
        {
            string QRCode = "";
            QRCodeModel myQRCode = new QRCodeModel();
            myQRCode.QRCodeText = text;
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qRCodeData = qrGenerator.CreateQrCode(
                myQRCode.QRCodeText, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qRCode = new QRCode(qRCodeData))
            {
                Bitmap qrCodeImage = qRCode.GetGraphic(1);
                byte[] BitmapArray = qrCodeImage.ConvertBitmapToByteArray();
                string urlImgQrcode = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
                QRCode = urlImgQrcode;
            }

            Regex regex = new Regex(@"^data:image/(?<mediaType>[^;]+);base64,(?<data>.*)");
            Match match = regex.Match(QRCode);
            Image imagettdqrcode = Image.GetInstance(
                Convert.FromBase64String(match.Groups["data"].Value)
            );

            return imagettdqrcode;
        }
        private Image GetSignatureImage(string filename)
        {

            string filePath = Path.Combine("wwwroot\\uploads\\imgsignature\\" + filename + ".png");
            string filettd = Path.GetFileName(filePath);

            //string pathttd = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/uploads/imgsignature/" + filettd;
            string pathttd = "http://10.1.19.26/uploads/imgsignature/" + filettd;
            //cell.Border = 0;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(pathttd);
            request.Method = "HEAD";

            bool exists;
            try
            {
                request.GetResponse();
                exists = true;

            }
            catch
            {
                return null;
            }



            Image imagettd = Image.GetInstance(pathttd);
            float newWidth = 120;
            float newHeight = 150;

            // Atur lebar dan tinggi gambar
            imagettd.ScaleToFit(newWidth, newHeight);

            // Atau Anda bisa menentukan skala persentase
            imagettd.ScalePercent(35); // skala 50%
            return imagettd;
        }

        [Authorize]
        [Route("GetAttachmentMobile")]
        [HttpPost]
        public ActionResult GetAttachment([FromForm] ParamPriviewMobile pr)
        {
            try
            {
                sessionUser = SetSession();

                GeneralOutputModel retrn = _dataAccessProvider.GetDataPriviewMobile_(pr, sessionUser);

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
