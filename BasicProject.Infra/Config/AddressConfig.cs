using BasicProject.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicProject.Infra.Config
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            //Nome da Tabela
            builder.ToTable("Address");

            //Chave Primaria
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
        }
    }
}
