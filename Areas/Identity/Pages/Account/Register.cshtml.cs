using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using doan_cnpm.Models;
using doan_cnpm.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace doan_cnpm.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterModel(
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Address")]
            public string Address { get; set; }


            [Required]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Super Admin")]
            public bool IsSuperAdmin { get; set; }

            [Required]
            [Display(Name="Customer Account")]
            public bool IsCustomerEndUser { get; set; }
            [Required]
            [Display(Name="Admin")]
            public bool IsAdmin { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");/// PHÂN QUYỀN HỆ THỐNG 
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                //TRUYỀN TỪ DỮ LIỆU TỪ VIEW TẠO MỘT USRER ACCOUNT MỚI 
                if(User.IsInRole(SD.SuperAdminEndUser))
                {
                    user = new ApplicationAdminUser { UserName = Input.Email, Email = Input.Email, Name = Input.Name, Address = Input.Address, PhoneNumber = Input.PhoneNumber };
                }
                 else
                {
                    user = new ApplicationCustomerAccount { UserName = Input.Email, Email = Input.Email, Name = Input.Name, Address = Input.Address, PhoneNumber = Input.PhoneNumber };
                }
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded) // NẾU TẠO TÀI KHOẢN THÀNH CÔNG THÌ BẮT ĐẦU PHÂN QUYỀN CHO HỆ THỐNG
                {
                    if (!await _roleManager.RoleExistsAsync(SD.AdminEndUser))  // KIỂM TRA HỆ THỐNG ĐÃ CÓ USER CÓ QUYỀN LÀ SUPER ADMIN CHƯA NẾU CHƯA CÓ THÌ TẠO MỘT MÃ ID CẤP QUYỀN SUPER ADMIN
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.AdminEndUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.SuperAdminEndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.SuperAdminEndUser));
                    }
                    if(!await _roleManager.RoleExistsAsync(SD.CustomerEndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.CustomerEndUser));
                    }
                    if (Input.IsSuperAdmin) // NẾU USER VỪA ĐK LÀ QUYỀN GÌ THÌ TẠO TÀI KHOẢN CÓ QUYỀN ĐÓ
                    {
                        await _userManager.AddToRoleAsync(user, SD.SuperAdminEndUser);
                    }
                    
                    else if(Input.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, SD.AdminEndUser);
                    }

                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.CustomerEndUser);
                    }


                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    return RedirectToAction("Index", "HomePages", new { area = "Customer" });

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
