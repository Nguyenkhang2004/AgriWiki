using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgriWiki_Project.Pages
{
    public class Auth : PageModel
    {
        [BindProperty]
        public SigninInput signinInput { get; set; } = new();
        public SignupInput signupInput { get; set; } = new();

        public string? Message { get; set; }

        public class SigninInput
        {
            [Required(ErrorMessage = "Email is required")]
            public string Email { get; set; } = "";

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = "";

            public bool RememberMe { get; set; }
        }

        public class SignupInput
        {
            [Required(ErrorMessage = "Email is required")]
            public string Email { get; set; } = "";

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = "";
            [Required(ErrorMessage = "Confirm Password is required")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; } = "";
            [Required(ErrorMessage = "Full Name is required")]
            public string FullName { get; set; } = "";
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostSignin()
        {
            if (!ModelState.IsValid) return Page();

            if (signinInput.Email == "admin" && signinInput.Password == "1234")
            {
                return RedirectToPage("/Index"); // đăng nhập thành công
            }

            Message = "Đăng nhập thất bại!";
            return Page();
        }

        public IActionResult OnPostSignup()
        {
            if (!ModelState.IsValid) return Page();

            // TODO: xử lý lưu tài khoản vào DB
            Message = "Đăng ký thành công!";
            return Page();
        }
    }
}