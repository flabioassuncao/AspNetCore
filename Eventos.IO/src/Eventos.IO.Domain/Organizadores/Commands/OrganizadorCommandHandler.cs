using Eventos.IO.Domain.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.Notifications;
using Eventos.IO.Domain.Interfaces;
using Eventos.IO.Domain.Core.Events.Interface;
using Eventos.IO.Domain.Organizadores.Repository;
using System.Linq;
using Eventos.IO.Domain.Organizadores.Events;

namespace Eventos.IO.Domain.Organizadores.Commands
{
    public class OrganizadorCommandHandler : CommandHandler,
        IHandler<RegistrarOrganizadorCommand>
    {
        private readonly IBus _bus;
        private readonly IOrganizadorRepository _organizadorReposistory;

        public OrganizadorCommandHandler(IUnitOfWork uow, IBus bus, IDomainNotificationHandler<DomainNotification> notifications, IOrganizadorRepository organizadorReposistory) 
            : base(uow, bus, notifications)
        {
            _bus = bus;
            _organizadorReposistory = organizadorReposistory;
        }

        public void Handle(RegistrarOrganizadorCommand message)
        {
            var organizador = new Organizador(message.Id, message.Nome, message.CPF, message.Email);

            if(!organizador.EhValido())
            {
                NotificarValidacoesErro(organizador.ValidationResult);
                return;
            }

            // Validar email e CPF duplicado
            var organizadorExistente = _organizadorReposistory.Buscar(o => o.CPF == organizador.CPF || o.Email == organizador.Email);
            if (organizadorExistente.Any())
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "CPF ou EMAIL já utilizado"));
                //return;
            }

            _organizadorReposistory.Adicionar(organizador);

            //ADD no repositorio

            if (Commit())
            {
                _bus.RaiseEvent(new OrganizadorRegistradoEvent(organizador.Id, organizador.Nome, organizador.CPF, organizador.Email));
            }
        }
    }
}
