using Microsoft.EntityFrameworkCore;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 

namespace CursoEFCore.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
                builder.ToTable("Clientes");
                builder.HasKey(builder => builder.Id);
                builder.Property(builder => builder.Nome).HasColumnType("VARCHAR(80)").IsRequired();
                builder.Property(builder => builder.Telefone).HasColumnType("CHAR(11)");
                builder.Property(builder => builder.Cep).HasColumnType("CHAR(8)").IsRequired();
                builder.Property(builder => builder.Estado).HasColumnType("CHAR(2)").IsRequired();
                builder.Property(builder => builder.Cidade).HasMaxLength(60).IsRequired();

                builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
        }
    }
}