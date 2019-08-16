using BasicProject.Infra.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicProject.Infra.Config
{
    public class LogConfig : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            //Nome da Tabela
            builder.ToTable("Log");

            //Chave Primaria
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
        }
    }
}
