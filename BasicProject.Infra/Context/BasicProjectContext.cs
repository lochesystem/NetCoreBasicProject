using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace BasicProject.Infra.Context
{
    public class BasicProjectContext : DbContext
    {
        public BasicProjectContext(DbContextOptions<BasicProjectContext> options) : base(options) { }

        //public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.GetInterfaces().Any(i => i.IsGenericType
                                                             && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));


            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);

        }
    }
}
