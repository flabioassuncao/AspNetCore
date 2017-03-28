using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Eventos.IO.Infra.Data.Context;

namespace Eventos.IO.Infra.Data.Migrations
{
    [DbContext(typeof(EventosContext))]
    [Migration("20170328143638_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Eventos.IO.Domain.Eventos.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("categorias");
                });

            modelBuilder.Entity("Eventos.IO.Domain.Eventos.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro");

                    b.Property<string>("CEP");

                    b.Property<string>("Cidade");

                    b.Property<string>("Complemento");

                    b.Property<string>("Estado");

                    b.Property<Guid?>("EventoId");

                    b.Property<string>("Logradouro");

                    b.Property<string>("Numero");

                    b.HasKey("Id");

                    b.HasIndex("EventoId")
                        .IsUnique();

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Eventos.IO.Domain.Eventos.Evento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CategoriaId");

                    b.Property<DateTime>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<string>("DescricaoCurta")
                        .HasColumnName("varchar(max)");

                    b.Property<string>("DescricaoLonga");

                    b.Property<Guid?>("EnderecoId");

                    b.Property<bool>("Excluido");

                    b.Property<bool>("Gratuito");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("varchar(150)");

                    b.Property<string>("NomeEmpresa")
                        .IsRequired()
                        .HasColumnName("varchar(150)");

                    b.Property<bool>("Online");

                    b.Property<Guid>("OrganizadorId");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("OrganizadorId");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("Eventos.IO.Domain.Organizadores.Organizador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CPF");

                    b.Property<string>("Email");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Organizadores");
                });

            modelBuilder.Entity("Eventos.IO.Domain.Eventos.Endereco", b =>
                {
                    b.HasOne("Eventos.IO.Domain.Eventos.Evento", "Evento")
                        .WithOne("Endereco")
                        .HasForeignKey("Eventos.IO.Domain.Eventos.Endereco", "EventoId");
                });

            modelBuilder.Entity("Eventos.IO.Domain.Eventos.Evento", b =>
                {
                    b.HasOne("Eventos.IO.Domain.Eventos.Categoria", "Categoria")
                        .WithMany("Eventos")
                        .HasForeignKey("CategoriaId");

                    b.HasOne("Eventos.IO.Domain.Organizadores.Organizador", "Organizador")
                        .WithMany("Eventos")
                        .HasForeignKey("OrganizadorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
