using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AgriWiki_Project.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgriWiki_Project.Pages
{
    public class Auth : PageModel
    {
        private readonly ILogger<Auth> _logger;
        // private readonly IAuthenticationBusiness _authBusiness;
        private readonly UserBusiness _userBusiness;

        public SigninInputModel SigninInput { get; set; } = new();

        public SignupInputModel SignupInput { get; set; } = new();
        public bool isSignupActive { get; set; } = false;

        public Auth(ILogger<Auth> logger, UserBusiness userBusiness)
        {
            _logger = logger;
            // _authBusiness = authenticationBusiness;
            _userBusiness = userBusiness;
        }

        public async Task<IActionResult> OnPostSignin([FromForm] SigninInputModel SigninInput)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Incorrect email or password.";
                return Page();
            }
            await _userBusiness.Login(SigninInput.Email, SigninInput.Password);
            // var user = await _authBusiness.Authenticate(SigninInputModel.Email, SigninInputModel.Password);
            // if (!(SigninInput.Email == "khang@gmail.com" && SigninInput.Password == "123"))
            // {
            //     TempData["ErrorMessage"] = "Incorrect email or password";
            //     _logger.LogInformation("Incorrect email or password.");
            //     return Page();
            // }

            // var claims = new List<Claim>
            //     {
            //         new Claim(ClaimTypes.Name, user.FullName),
            //         new Claim(ClaimTypes.Role, user.Role),
            //         new Claim("UserId", user.UserId.ToString())
            //     };

            // var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            // var principal = new ClaimsPrincipal(claimsIdentity);
            // await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            TempData["SuccessMessage"] = "Sign-in successfully.";
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostSignup([FromForm] SignupInputModel SignupInput)
        {
            isSignupActive = true;
            if (!TryValidateModel(SignupInput))
            {
                TempData["ErrorMessage"] = "Error while sign-up.";
                return Page();
            }
            var user = await _userBusiness.Create(SignupInput.Email);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Error while sign-up.";
                return Page();
            }
            // var user = new User
            // {
            //     Password = SignupInput.Password,
            //     FullName = SignupInput.FullName,
            //     Email = SignupInput.Email,
            //     Role = "ENDUSER",
            //     CreatedAt = DateTime.Now
            // };

            // await _userBusiness.Create(user);
            TempData["SuccessMessage"] = "Registration successful! You can log in now.";
            isSignupActive = false;
            return RedirectToPage("Auth");
        }

        public async Task<IActionResult> OnPostSignout()
        {
            // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "Sign-out successfully.";
            return RedirectToPage("/Account/Auth");
        }

        public class SigninInputModel
        {
            [Required(ErrorMessage = "Email is required")]
            [Display(Name = "Email")]
            [EmailAddress(ErrorMessage = "Invalid email format")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember login")]
            public bool RememberMe { get; set; }
        }

        public class SignupInputModel
        {

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Confirm password is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Confirm password does not match.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Full name is required")]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email format")]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }
    }
}