namespace BlogSystem.Data.Seed
{
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNetCore.Identity;
    using Models;

    public class AdministratorSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministratorSeeder(
            ApplicationDbContext data,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.data = data;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void Seed()
        {
            if (this.data.Roles.Any())
            {
                return;
            }

            Task
                .Run(async () =>
                {
                    var adminRole = new IdentityRole(DataConstants.AdministratorRoleName);

                    await this.roleManager.CreateAsync(adminRole);

                    var adminUser = new ApplicationUser
                    {
                        UserName = "admin@blog.com",
                        Email = "admin@blog.com",
                        SecurityStamp = "RandomSecurityStamp"
                    };

                    await userManager.CreateAsync(adminUser, "adminpass12");

                    await userManager.AddToRoleAsync(adminUser, DataConstants.AdministratorRoleName);

                    await this.data.SaveChangesAsync();
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
