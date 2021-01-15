using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace Api.Data
{
    class ApiContextConfigurationMigration : DbMigrationsConfiguration<ApiContext>
    {
        public ApiContextConfigurationMigration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(ApiContext context)
        {
            //var u = context.Usuario.(u => u. "admin");
            //if (u == null)
            //{
            //    context.Usuario.Add(new Entities.Usuario()
            //    {
            //        RutUsuario = "1-9",
            //        NombreUsuario = "Administrador",
            //        Email = "admin",
            //        Password = Util.Cryptojs.EncryptToString("AdminPortal2019"),
            //        FechaRegistro = System.DateTime.Now,
            //        EsAdmin = true
            //    });
            //}
            base.Seed(context);
        }
    }
}
