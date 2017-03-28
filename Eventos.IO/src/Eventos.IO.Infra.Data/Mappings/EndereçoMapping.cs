using Eventos.IO.Domain.Eventos;
using Eventos.IO.Infra.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Eventos.IO.Infra.Data.Mappings
{
    public class EndereçoMapping : EntityTypeConfiguration<Endereco>
    {
        public override void Map(EntityTypeBuilder<Endereco> builder)
        {
            builder
                .Property(e => e.Logradouro)
                    .HasMaxLength(150)
                    .HasColumnType("varchar(150)");

            builder
                .Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("varchar(20)");

            builder
                .Property(e => e.Bairro)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

            builder
                .Property(e => e.CEP)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnType("varchar(8)");

            builder
                .Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

            builder
                .Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");
            

            builder
                .HasOne(c => c.Evento)
                .WithOne(c => c.Endereco)
                .HasForeignKey<Endereco>(c => c.EventoId)
                .IsRequired(false);

            builder
                .Ignore(c => c.ValidationResult);

            builder
                .Ignore(c => c.CascadeMode);

            builder
                .ToTable("Enderecos");
        }
    }
}
