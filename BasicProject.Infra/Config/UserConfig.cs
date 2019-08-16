using BasicProject.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicProject.Infra.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Nome da Tabela
            builder.ToTable("User");

            //Chave Primaria
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            //Chave Estrangeira
            builder.HasOne(x => x.Address)
                .WithOne(t => t.User)
                .HasForeignKey<Address>(t => t.UserId);
        }
    }
}
