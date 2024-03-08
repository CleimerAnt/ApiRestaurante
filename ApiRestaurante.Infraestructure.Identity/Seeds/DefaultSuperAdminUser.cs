using ApiRestaurante.Core.Application.Enums;
using ApiRestaurante.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Infraestructure.Identity.Seeds
{
    public static class DefaultSuperAdminUser
    {
        public static async Task SeddAsyns(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();

            defaultUser.FirstName = "Clei";

            defaultUser.lastName = "Lorenzo";

            defaultUser.Email = "SuperAdmin@gmail.com";

            defaultUser.PhoneNumber = "809-000-0000";

            defaultUser.EmailConfirmed = true;

            defaultUser.UserName = "SuperAdmin";

            defaultUser.PhoneNumberConfirmed = true;

            if(userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var usuario = userManager.FindByEmailAsync(defaultUser.Email);

                if(usuario != null) 
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$work");
                    await userManager.AddToRoleAsync(defaultUser,Roles.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Waiter.ToString());
                }
            }

        }
    }
}
