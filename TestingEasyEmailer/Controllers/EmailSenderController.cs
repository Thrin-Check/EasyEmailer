using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThrinCheck.EasyEmailer;

namespace TestingEasyEmailer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly EasyEmailer _easyEmailer;
        public EmailSenderController(EasyEmailer easyEmailer)
        {
            _easyEmailer = easyEmailer;
        }


        [HttpPost("WithCode")]
        public async Task<IActionResult> SendEmailConfirmationCode(string receiverEmail)
        {
            try
            {
                var emailBody = _easyEmailer.PrepareEmailVerificationWithCode("345867", Constants.FacebookLink, Constants.LinkedInLink, Constants.TwitterLink, Constants.InstagramLink, Constants.CompanyEmail, "08100220011", Constants.CompanyHexColorCode);

                var sendMail = await _easyEmailer.SendEmailAsync(Constants.OutLookEmail, Constants.OutLookPassword, receiverEmail, "Email Verification with code", emailBody);
                if (sendMail)
                {
                    return Ok("Email Sent Successfully");
                }
                return BadRequest("Fail to send");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("WithLink")]
        public async Task<IActionResult> SendEmailConfirmationLink(string receiverEmail)
        {
            try
            {
                var emailBody = _easyEmailer.PrepareEmailVerificationWithLink("https://trest.com/verify", Constants.FacebookLink, Constants.LinkedInLink, Constants.TwitterLink, Constants.InstagramLink, Constants.CompanyEmail, "08100220011", Constants.CompanyHexColorCode);

                var sendMail = await _easyEmailer.SendEmailAsync(Constants.OutLookEmail, Constants.OutLookPassword, receiverEmail, "Email Verification with Link", emailBody);
                if (sendMail)
                {
                    return Ok("Email Sent Successfully");
                }
                return BadRequest("Fail to send");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PasswordReset")]
        public async Task<IActionResult> SendPasswordResetMail(string receiverEmail)
        {
            try
            {
                var emailBody = _easyEmailer.PreparePasswordResetEmail("https://trest.com/password-reset", Constants.FacebookLink, Constants.LinkedInLink, Constants.TwitterLink, Constants.InstagramLink, Constants.CompanyEmail, Constants.CompanyHexColorCode);

                var sendMail = await _easyEmailer.SendEmailAsync(Constants.OutLookEmail, Constants.OutLookPassword, receiverEmail, "Password Reset", emailBody);
                if (sendMail)
                {
                    return Ok("Email Sent Successfully");
                }
                return BadRequest("Fail to send");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
