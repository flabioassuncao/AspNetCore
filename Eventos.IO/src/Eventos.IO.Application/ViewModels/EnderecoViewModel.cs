using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eventos.IO.Application.ViewModels
{
    public class EnderecoViewModel
    {
        public EnderecoViewModel()
        {
            Id = Guid.NewGuid();
        }

        public SelectList Estados()
        {
            return new SelectList(EstadoViewModel.ListarEstados(), "UF", "Nome");
        }


        [Key]
        public Guid Id { get; set; }
        public string Logradouro { get;  set; }
        public string Numero { get;  set; }
        public string Complemento { get;  set; }
        public string Bairro { get;  set; }
        public string CEP { get;  set; }
        public string Cidade { get;  set; }
        public string Estado { get;  set; }

        public override string ToString()
        {
            return Logradouro + ", " + Numero + ", " + Bairro + ", " + Cidade + " - " + Estado;
        }
    }
}
