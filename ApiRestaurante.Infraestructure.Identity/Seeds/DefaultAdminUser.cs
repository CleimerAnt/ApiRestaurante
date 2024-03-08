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
    public static class DefaultAdminUser
    {
        public static async Task SeddAsyns(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new();

            defaultUser.FirstName = "Manuel";

            defaultUser.lastName = "Lara";

            defaultUser.PhoneNumber = "809-000-0000";

            defaultUser.Email = "admin@gmail.com.com";

            defaultUser.EmailConfirmed = true;

            defaultUser.UserName = "Admin";

            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var usuario = userManager.FindByEmailAsync(defaultUser.Email);

                if (usuario != null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$work");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }

        }
    }
}
