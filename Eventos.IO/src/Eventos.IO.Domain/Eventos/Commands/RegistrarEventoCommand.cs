using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos.Commands
{
    public class RegistrarEventoCommand : BaseEventoCommand
    {
        public RegistrarEventoCommand(string nome, DateTime dataInicio, DateTime dataFim, bool gratuito,
                        decimal valor, bool online, string nomeEmpresa, Guid organizadorId, Endereco endereco, Categoria categoria)
        {
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;
            OrganizadorId = organizadorId;
            Endereco = endereco;
            Categoria = categoria;
        }
    }
}
