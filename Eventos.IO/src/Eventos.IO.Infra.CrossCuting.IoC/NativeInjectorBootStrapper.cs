using AutoMapper;
using Eventos.IO.Application.Interfaces;
using Eventos.IO.Application.Services;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.Events.Interface;
using Eventos.IO.Domain.Core.Notifications;
using Eventos.IO.Domain.Eventos.CommandHandlers;
using Eventos.IO.Domain.Eventos.Commands;
using Eventos.IO.Domain.Eventos.EventHandlers;
using Eventos.IO.Domain.Eventos.Events;
using Eventos.IO.Domain.Eventos.Repository;
using Eventos.IO.Domain.Interfaces;
using Eventos.IO.Infra.Data.Repository;
using Eventos.IO.Infra.Data.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace Eventos.IO.Infra.CrossCuting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterRegisterService(IServiceCollection service)
        {
            //Application
            service.AddSingleton(Mapper.Configuration);
            service.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            service.AddScoped<IEventoAppService, EventoAppService>();

            //Domain - Command
            service.AddScoped<IHandler<RegistrarEventoCommand>, EventoCommandHandler>();
            service.AddScoped<IHandler<AtualizarEventoCommand>, EventoCommandHandler>();
            service.AddScoped<IHandler<ExcluirEventoCommand>, EventoCommandHandler>();

            //Domain - Events
            service.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();
            service.AddScoped<IHandler<EventoRegistradoEvent>, EventoEventHandler>();
            service.AddScoped<IHandler<EventoAtualizadoEvent>, EventoEventHandler>();
            service.AddScoped<IHandler<EventoExcluidoEvent>, EventoEventHandler>();

            // Infra - Data
            service.AddScoped<IEventoRepository, EventoRepository>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            //Infra - Bus
            service.AddScoped<IBus, Bus.InMemoryBus>();
        }
    }
}
