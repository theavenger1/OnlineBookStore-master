using BookStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookStore.Startup))]
namespace BookStore
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();

        }
        public void CreateDefaultRolesAndUsers()
        {
            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManger.RoleExists("Admins"))
            {
                role.Name = "Admins";
                roleManger.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Shora";
                user.Email = "Shora@aa.com";
                user.cityId = 1;
                var Check = userManger.Create(user, "Shora.12345");
                if (Check.Succeeded)
                {
                    userManger.AddToRoles(user.Id, "Admins");
                }
            }
        }
        }
}
