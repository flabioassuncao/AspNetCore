using Eventos.IO.Domain.Eventos;
using Eventos.IO.Infra.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Eventos.IO.Infra.Data.Mappings
{
    public class EventoMapping : EntityTypeConfiguration<Evento>
    {
        public override void Map(EntityTypeBuilder<Evento> builder)
        {
            builder
                .Property(e => e.Nome)
                .HasColumnName("varchar(150)")
                .IsRequired();

            builder
                .Property(e => e.DescricaoCurta)
                .HasColumnName("varchar(150)");

            builder
                .Property(e => e.DescricaoCurta)
                .HasColumnName("varchar(max)");

            builder
                .Property(e => e.NomeEmpresa)
                .HasColumnName("varchar(150)")
                .IsRequired();

            builder
                .Ignore(e => e.ValidationResult);

            builder
                .Ignore(e => e.Tags);

            builder
                .Ignore(e => e.CascadeMode);

            builder
                .ToTable("Eventos");

            builder
                .HasOne(e => e.Organizador)
                .WithMany(o => o.Eventos)
                .HasForeignKey(e => e.OrganizadorId);

            builder
                .HasOne(e => e.Categoria)
                .WithMany(o => o.Eventos)
                .HasForeignKey(e => e.CategoriaId)
                .IsRequired(false);
        }
    }
}
