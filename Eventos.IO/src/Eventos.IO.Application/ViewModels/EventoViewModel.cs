using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eventos.IO.Application.ViewModels
{
    public class EventoViewModel
    {

        public EventoViewModel()
        {
            Id = Guid.NewGuid();
            Endereco = new EnderecoViewModel();
            Categoria = new CategoriaViewModel();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é requerido")]
        [MinLength(2, ErrorMessage ="tamanho minimo do nome é {1}")]
        [MaxLength(150, ErrorMessage = "tamanho maximo do Nome é {1}")]
        [Display(Name = "Nome do evento")]
        public string Nome { get; set; }

        [Display(Name ="Descrição Curta do evento")]
        public string DescricaoCurta { get; set; }

        [Display(Name = "Descrição longa do evento")]
        public string DescricaoLonga { get; set; }

        [Display(Name ="Inicio do evento")]
        [Required(ErrorMessage ="Data inicio requerida")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Fim do evento")]
        [Required(ErrorMessage ="Data fim requerida")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataFim { get; set; }

        [Display(Name = "Será gratuito?")]
        public bool Gratuito { get; set; }

        [Display(Name ="Valor")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Valor { get; set; }

        [Display(Name = "Será online?")]
        public bool Online { get; set; }

        [Display(Name = "Empresa / Grupo Organizador")]
        public string NomeEmpresa { get; set; }

        public EnderecoViewModel Endereco { get; set; }
        public CategoriaViewModel Categoria { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid OrganizadorId { get; set; }

    }
}
