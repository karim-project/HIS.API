using HIS.API.DTOs.Request;
using HIS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HIS.API.Areas.identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("identity")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRepository<ApplicationUserOTP> _applicationUserOTPRepository;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<ApplicationUser> userManager,IEmailSender emailSender ,SignInManager<ApplicationUser> signInManager, IRepository<ApplicationUserOTP> applicationUserOTPRepository, ITokenService tokenService) 
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _applicationUserOTPRepository = applicationUserOTPRepository;
            _tokenService = tokenService;
        }
        [HttpPost("Registor")]
        public async Task<IActionResult> Registor(RegistorRequest registorRequest)
        {
            var user = new ApplicationUser()
            {
                    FirstName = registorRequest.FirstName,
                    LastName = registorRequest.LastName,
                    Email = registorRequest.Email,
                    UserName = registorRequest.UserName,
            };

            var result =await _userManager.CreateAsync(user , registorRequest.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var token =await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var link = Url.Action(nameof(ConfirmEmail), "Account", new { area = "Identity", token, userId = user.Id }, Request.Scheme);

            await _emailSender.SendEmailAsync(registorRequest.Email, "HIS - Comfirm your email", $"<h1>Confirm Your Email By Clicking <a href='{link}'>Here</a></h1>");

           await _userManager.AddToRoleAsync(user, SD.Customer_Role);

            return Ok(new
            {
                msg = "Create Account Successfully"
            });
        }
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId , string token)
        {
            var user =await _userManager.FindByIdAsync(userId);

            if (user is null)
                return NotFound(new
                {
                    msg = "Invalid User"
                });

            var result =await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    msg = "Invalid or expired token"
                });
            }
            else
            {
                return Ok(new
                {
                    msg = "Email Confirmed Successfully"
                });
            }

              
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {


            var user = await _userManager.FindByNameAsync(loginRequest.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(loginRequest.UserNameOrEmail);


            if (user is null)
            {
                return NotFound(new ErrorModelResponse
                {
                    Code = "Invalid Cred",
                    Description = "Invalid User Name / Email OR Password"
                });

            }
            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, loginRequest.RememberMe, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    return BadRequest(new ErrorModelResponse
                    {
                        Code = "Too many attemps",
                        Description = "Too many attemps, try again after 5 min"
                    });
                else if (!user.EmailConfirmed)
                    return BadRequest(new ErrorModelResponse
                    {
                        Code = "Confirm Your Email",
                        Description = "Please Confirm Your Email First!!"
                    });
                else
                    return NotFound(new ErrorModelResponse
                    {
                        Code = "Invalid Cred.",
                        Description = "Invalid User Name / Email OR Password"
                    });
            }
            //generate Token
            var userRole = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
             {
                  new Claim(ClaimTypes.Name, user.UserName!),
                  new Claim(ClaimTypes.Email, user.Email!),
                  new Claim(ClaimTypes.NameIdentifier, user.Id),
                  new Claim(ClaimTypes.Role, String.Join( ", ",       userRole)),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };





            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                AccessToken = accessToken,
                ValidTo = "30 min",
                RefreshToken = refreshToken,
                RefreshTokenExpiration = "7 day"
            });

        }
        [HttpPost("ResendComfirmEmail")]
        public async Task<IActionResult> ResendComfirmEmail(ResendComfirmEmailRequset resendComfirmEmailRequset)
        {
            var user =await _userManager.FindByEmailAsync(resendComfirmEmailRequset.UserNameOrEmail) ??await _userManager.FindByNameAsync(resendComfirmEmailRequset.UserNameOrEmail);

            if (user is null)
                return NotFound(new ErrorModelResponse
                {
                    Code = "Invalid Cred",
                    Description = "Invalid User Name / Email OR Password"
                });

            if(user.EmailConfirmed)
                return NotFound(new ErrorModelResponse
                {
                    Code = "Invalid Cred",
                    Description = "Invalid User Name / Email OR Password"
                });
             
            var token =await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(nameof(ConfirmEmail), "Account", new { area = "Identity", token, userId = user.Id }, Request.Scheme);


            await _emailSender.SendEmailAsync(user.Email!, "ECommerc519 - Resend Comfirm your email", $"<h1>Confirm Your Email By Clicking <a href='{link}'>Here</a></h1>");

            return Ok(new
            {
                msg = "Send msg successfully"
            });
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest forgetPasswordRequest)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordRequest.UserNameOrEmail) ?? await _userManager.FindByNameAsync(forgetPasswordRequest.UserNameOrEmail);

            if (user is null)
                return NotFound(new ErrorModelResponse
                {
                    Code = "Invalid Cred",
                    Description = "Invalid User Name / Email OR Password"
                });

            if (user.EmailConfirmed)
                return NotFound(new ErrorModelResponse
                {
                    Code = "Invalid Cred",
                    Description = "Invalid User Name / Email OR Password"
                });

            var userOTP =await _applicationUserOTPRepository.GetAsync(e => e.ApplicationUserId == user.Id);

            var totalOtps = userOTP.Count(e => (DateTime.UtcNow - e!.CreateAt).TotalHours < 24);
            if(totalOtps > 3)
            {
                return BadRequest(new ErrorModelResponse
                {
                    Code = "Too Many Attemp",
                    Description = "Too many attemps, try again later"
                });
            }

            var otp = new Random().Next(1000, 9999).ToString();

            await _applicationUserOTPRepository.AddAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                ApplicationUserId = user.Id,
                CreateAt = DateTime.UtcNow,
                IsValid = true,
                OTP = otp,
                ValidTO = DateTime.UtcNow.AddDays(1),
            });
           await _applicationUserOTPRepository.CommitAsync();

            await _emailSender.SendEmailAsync(user.Email!, "HIS - Reset your password", $"<h1>Use This OTP: {otp} To Reset Your Account. Don't share it.</h1>");

            return CreatedAtAction("ValidateOTP" , new {userId = user.Id});
        }
        [HttpPost("ValidateOTP")]
        public async Task<IActionResult> ValidateOTP(ValidateOTPRequset validateOTPRequset)
        {
            var result =await _applicationUserOTPRepository.GetOneAsync(e => e.ApplicationUserId == validateOTPRequset.ApplicationUserId && e.OTP == validateOTPRequset.OTP && e.IsValid);

            if(result is null)
            {
                return CreatedAtAction("ValidateOTP", new { userId = validateOTPRequset.ApplicationUserId });
            }
            return CreatedAtAction("ValidateOTP", new { userId = validateOTPRequset.ApplicationUserId });
        }
        [HttpPost("NewPassword")]
        public async Task<IActionResult> NewPassword(NewPasswordRequset newPasswordRequset)
        {
            var user = await _userManager.FindByIdAsync(newPasswordRequset.ApplicationUserId);

            if (user is null)
            {
                return NotFound(new ErrorModelResponse
                {
                    Code = "Invalid Cred",
                    Description = "Invalid User Name / Email OR Password"
                });
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, newPasswordRequset.Password);

            if (!result.Succeeded)
            {
                //foreach (var item in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, item.Code);
                //}

                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost]
        [Route("Refresh")]
        public async Task<IActionResult> Refresh(TokenApiRequest tokenApiRequest)
        {
            if (tokenApiRequest is null || tokenApiRequest.RefreshToken is null || tokenApiRequest.AccessToken is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiRequest.AccessToken;
            string refreshToken = tokenApiRequest.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userName = principal.Identity.Name;
            var user = _userManager.Users.FirstOrDefault(e => e.UserName == userName);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                AccessToken = newAccessToken,
                ValidTo = "30 min",
                RefreshToken = newRefreshToken,
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;
            var user = _userManager.Users.FirstOrDefault(e => e.UserName == username);
            if (user == null) return BadRequest();
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
            return NoContent();
        }
    }
}
