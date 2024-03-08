using ApiRestaurante.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Infraestructure.Identity.Seeds
{
    public static class SeedsConfiguration
    {
        public async static Task AddIdentitySeedsInfraestructure(this IServiceProvider service)
        {
            #region "Seeds"
            using (var scope = service.CreateScope())
            {
                var serviceScope = scope.ServiceProvider;
                try
                {
                    var userManager = serviceScope.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = serviceScope.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(roleManager);
                    await DefaultWaiterUser.SeddAsyns(userManager);
                    await DefaultAdminUser.SeddAsyns(userManager);
                    await DefaultSuperAdminUser.SeddAsyns(userManager, roleManager);

                }
                catch (Exception ex)
                {
                
                }
            }
            #endregion
        }
    }
}
