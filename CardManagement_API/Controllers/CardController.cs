using CardManagement.Application.Dtos.Card;
using CardManagement.Application.Dtos.SysUser;
using CardManagement.Application.Service.Interface;
using CardManagement.Application.Utils;
using CardManagement.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tesseract;

namespace CardManagement_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly Helper _helper;

        public CardController(ICardService cardService, Helper helper) =>
           (_cardService, _helper) = (cardService ?? throw new ArgumentNullException(nameof(cardService)),
                                        helper ?? throw new ArgumentNullException(nameof(helper)));



        [HttpPost("CardApplication")]
        public async Task<IActionResult> CardApplication([FromForm] CardApplicationRequest request)
        {
            try
            {
                if (await ValidateIDPhoto(request.IDPhoto))
                {
                    var UserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var result = await _cardService.CardApplicationAsync(request, UserID);

                    if (!string.IsNullOrEmpty(result))
                    {
                        return Ok(new GeneralResponse<object>("200", null));
                    }

                    return BadRequest(new GeneralResponse<object>("404", null));
                }
                else
                {
                    return BadRequest(new GeneralResponse<object>("Photo Not Recongnized as ID Photo : 404", null));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse<bool>("Error Exception : 500"));
            }
        }

        [HttpPut("SetCardPin")]
        public async Task<IActionResult> SetCardPin([FromBody] SetPinRequest request)
        {
            try
            {
                
                var result = await _cardService.SetCardPinAsync(request);

                if (!string.IsNullOrEmpty(result))
                {
                    return Ok(new GeneralResponse<object>("200", null));
                }

                return BadRequest(new GeneralResponse<object>("404", null));
               
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse<bool>("Error Exception : 500"));
            }
        }

        [HttpPut("AddBalance")]
        public async Task<IActionResult> AddBalance([FromBody] AddBalanceRequest request)
        {
            try
            {

                var result = await _cardService.AddBalanceAsync(request);

                if (!string.IsNullOrEmpty(result))
                {
                    return Ok(new GeneralResponse<object>("200", null));
                }

                return BadRequest(new GeneralResponse<object>("404", null));

            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse<bool>("Error Exception : 500"));
            }
        }

        [HttpPut("BlockCard")]
        public async Task<IActionResult> BlockCard(string CardNumber)
        {
            try
            {

                var result = await _cardService.BlockCardAsync(CardNumber);

                if (!string.IsNullOrEmpty(result))
                {
                    return Ok(new GeneralResponse<object>("200", null));
                }

                return BadRequest(new GeneralResponse<object>("404", null));

            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse<bool>("Error Exception : 500"));
            }
        }

        [HttpPut("TransferBalance")]
        public async Task<IActionResult> TransferBalance([FromBody] TransferBalanceRequest request)
        {
            try
            {

                var result = await _cardService.TransferCardBalanceAsync(request);

                if (!string.IsNullOrEmpty(result))
                {
                    return Ok(new GeneralResponse<object>("200", null));
                }

                return BadRequest(new GeneralResponse<object>("404", null));

            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse<bool>("Error Exception : 500"));
            }
        }

        #region Private Method

        private async Task<bool> ValidateIDPhoto(IFormFile file)
        {
            try
            {
                string ExtractedText = string.Empty;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                   
                    using (var engine = new TesseractEngine(@"./TessData", "eng", EngineMode.Default))
                    {
                        using (var img = Pix.LoadFromMemory(memoryStream.ToArray()))
                        {
                            using (var page = engine.Process(img))
                            {
                                ExtractedText = page.GetText();
                            }
                        }
                    }
                }
                string CleanedText = _helper.CleanExtraction(ExtractedText);

                var EmiratesID = _helper.ExtractEmiratesID(CleanedText);
                var expiryDate = _helper.ExtractExpiryDate(CleanedText);

                if (string.IsNullOrEmpty(EmiratesID) && string.IsNullOrEmpty(expiryDate))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #endregion
    }
}
