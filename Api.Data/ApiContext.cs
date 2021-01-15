using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Api.Data.Mappers;
using Api.Entidad;

namespace Api.Data
{
    public class ApiContext: DbContext
    {
        public ApiContext() : base("odonto")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            
            

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApiContext, ApiContextConfigurationMigration>());
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Distribuidora> Distribuidora { get; set; }
        public DbSet<Telefono> Telefono { get; set; }
        public DbSet<UsuarioDistribuidora> UsuarioDistribuidora { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Privilegio> Privilegio { get; set; }
        public DbSet<PrivilegioUsuarioDistribuidora> PrivilegioUsuarioDistribuidora{ get; set; }
        public DbSet<LoginUsuario> LoginUsuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMapper());
            modelBuilder.Configurations.Add(new DistribuidoraMapper());
            modelBuilder.Configurations.Add(new TelefonoMapper());
            modelBuilder.Configurations.Add(new UsuarioDistribuidoraMapper());
            modelBuilder.Configurations.Add(new DocumentoMapper());
            modelBuilder.Configurations.Add(new PrivilegioMapper());
            modelBuilder.Configurations.Add(new PrivilegioUsuarioDistribuidoraMapper());
            modelBuilder.Configurations.Add(new LoginUsuarioMapper());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            

            base.OnModelCreating(modelBuilder);
        }
    }

 //   /// <summary>
 //   /// Query with dynamic Include
 //   /// </summary>
 //   /// <typeparam name="T">Entity</typeparam>
 //   /// <param name="context">dbContext</param>
 //   /// <param name="includeProperties">includeProperties with ; delimiters</param>
 //   /// <returns>Constructed query with include properties</returns>
 //   public static IQueryable<T> MyQueryWithDynamicInclude<T>(this ApiContext context, string includeProperties)
 //      where T : class
 //   {
 //       string[] includes = includeProperties.Split(';');
 //       var query = context.Set<T>().AsQueryable();

 //       foreach (string include in includes)
 //           query = query.Include(include);

 //       return query;
 //   }

 //   public static IQueryable<T> MyQuery<T>(this DbContext context)
 //where T : class
 //   {
 //       return context.Set<T>().AsQueryable();
 //   }
}
