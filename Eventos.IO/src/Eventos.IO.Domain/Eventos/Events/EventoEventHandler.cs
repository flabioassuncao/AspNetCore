using Eventos.IO.Domain.Core.Events.Interface;
using Eventos.IO.Domain.Eventos.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos.EventHandlers
{
    public class EventoEventHandler :
        IHandler<EventoRegistradoEvent>,
        IHandler<EventoAtualizadoEvent>,
        IHandler<EventoExcluidoEvent>,
        IHandler<EnderecoEventoAdicionadoEvent>,
        IHandler<EnderecoEventoAtualizadoEvent>
    {
        public void Handle(EventoRegistradoEvent message)
        {
            // enviar email ou log ou salvar registro no banco
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Registrado com sucesso");
        }

        public void Handle(EventoAtualizadoEvent message)
        {
            // enviar email ou log ou salvar registro no banco
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Atualizado com sucesso");
        }

        public void Handle(EventoExcluidoEvent message)
        {
            // enviar email ou log ou salvar registro no banco
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Excluido com sucesso");
        }

        public void Handle(EnderecoEventoAdicionadoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Endereco do evento adicionado com sucesso");
        }

        public void Handle(EnderecoEventoAtualizadoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Endereco do evento atualizado com sucesso");
        }
    }
}
