using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using doan_cnpm.Models;
using doan_cnpm.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace doan_cnpm.Areas.Identity.Pages.Account
{
    public class AddAdminUserModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AddAdminUserModel(

            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet() {
            if (!await _roleManager.RoleExistsAsync(SD.AdminEndUser))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.AdminEndUser));
            }
            if (!await _roleManager.RoleExistsAsync(SD.SuperAdminEndUser))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.SuperAdminEndUser));
                var userAdmin = new ApplicationUser
                {
                    UserName = "haidemons1663@gmail.com",
                    Email = "haidemons1663@gmail.com",
                    PhoneNumber = "0389667651",
                    Name = "Admin Hai Uom"

                };
                var result = await _userManager.CreateAsync(userAdmin, "Admin123*");
                await _userManager.AddToRoleAsync(userAdmin, SD.SuperAdminEndUser);
            }

   
            return Page();
        }
    }
}
