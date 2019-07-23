using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProjFinalEcho1.Models;

[assembly: OwinStartupAttribute(typeof(ProjFinalEcho1.Startup))]
namespace ProjFinalEcho1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // executar o método para configurar a aplicação
            iniciaAplicacao();
        }




        private void iniciaAplicacao()
        {

            // identifica a base de dados de serviço à aplicação
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'GestorMultas'
            if (!roleManager.RoleExists("Administrador"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Administrador";
                roleManager.Create(role);
            }



            // criar um utilizador 'Administrador'
            var user = new ApplicationUser();

            user.UserName = "admin@admin.admin";
            user.Email = "admin@admin.admin";
            string userPWD = "123456Aa_";
            var chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Administrador
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Administrador");
            }

            // criar um utilizador 'Administrador'
            var user2 = new ApplicationUser();

            user2.UserName = "admin1@admin.admin";
            user2.Email = "admin1@admin.admin";
            string userPWD2 = "123456Aa_";
            var chkUser2 = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Administrador
            if (chkUser2.Succeeded)
            {
                var result1 = userManager.AddToRole(user2.Id, "Administrador");
            }

            var user3 = new ApplicationUser();

            user3.UserName = "User@User.User";
            user3.Email = "User@User.User";
            string userPWD3 = "123456Aa_";
            var chkUser3 = userManager.Create(user, userPWD);

        }

    }
}
