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
    public static class DefaultWaiterUser
    {
        public static async Task SeddAsyns(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new();

            defaultUser.FirstName = "Cleimes";

            defaultUser.lastName = "Lara";

            defaultUser.PhoneNumber = "809-000-0000";

            defaultUser.Email = "waiter@gmail.com";

            defaultUser.EmailConfirmed = true;

            defaultUser.UserName = "Waiter";

            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var usuario = userManager.FindByEmailAsync(defaultUser.Email);

                if (usuario != null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$work");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Waiter.ToString());
                }
            }

        }
    }
}
