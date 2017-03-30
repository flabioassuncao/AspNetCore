using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Application.ViewModels
{
    public class EstadoViewModel
    {
        public string UF { get; set; }
        public string Nome { get; set; }

        public static List<EstadoViewModel> ListarEstados()
        {
            return new List<EstadoViewModel>()
            {
                new EstadoViewModel() {UF="AC", Nome="Acre"},
                new EstadoViewModel() {UF="AL", Nome="Alagoas"},
                new EstadoViewModel() {UF="AP", Nome="Amapa"},
                new EstadoViewModel() {UF="AM", Nome="Amazonas"},
                new EstadoViewModel() {UF="BA", Nome="Bahia"},
                new EstadoViewModel() {UF="CE", Nome="Ceara"},
                new EstadoViewModel() {UF="DF", Nome="Distrito Federal"},
                new EstadoViewModel() {UF="ES", Nome="Espirito Santo"},
                new EstadoViewModel() {UF="GO", Nome="Goias"},
            };
        }
    }
}
