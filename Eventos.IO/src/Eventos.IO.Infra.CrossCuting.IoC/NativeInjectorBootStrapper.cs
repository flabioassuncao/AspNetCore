﻿using AutoMapper;
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
using Eventos.IO.Domain.Organizadores.Commands;
using Eventos.IO.Domain.Organizadores.Events;
using Eventos.IO.Domain.Organizadores.Repository;
using Eventos.IO.Infra.CrossCutting.Identity.Models;
using Eventos.IO.Infra.CrossCutting.Identity.Services;
using Eventos.IO.Infra.Data.Context;
using Eventos.IO.Infra.Data.Repository;
using Eventos.IO.Infra.Data.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Eventos.IO.Infra.CrossCutting.AspNetFilters;

namespace Eventos.IO.Infra.CrossCuting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterRegisterService(IServiceCollection service)
        {
            //ASPNET
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Application
            service.AddSingleton(Mapper.Configuration);
            service.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            service.AddScoped<IEventoAppService, EventoAppService>();

            service.AddScoped<IOrganizadorAppService, OrganizadorAppService>();

            //Domain - Command
            service.AddScoped<IHandler<RegistrarEventoCommand>, EventoCommandHandler>();
            service.AddScoped<IHandler<AtualizarEventoCommand>, EventoCommandHandler>();
            service.AddScoped<IHandler<ExcluirEventoCommand>, EventoCommandHandler>();

            service.AddScoped<IHandler<AtualizarEnderecoEventoCommand>, EventoCommandHandler>();
            service.AddScoped<IHandler<IncluirEnderecoEventoCommand>, EventoCommandHandler>();

            service.AddScoped<IHandler<RegistrarOrganizadorCommand>, OrganizadorCommandHandler>();

            //Domain - Events
            service.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();
            service.AddScoped<IHandler<EventoRegistradoEvent>, EventoEventHandler>();
            service.AddScoped<IHandler<EventoAtualizadoEvent>, EventoEventHandler>();
            service.AddScoped<IHandler<EventoExcluidoEvent>, EventoEventHandler>();

            service.AddScoped<IHandler<EnderecoEventoAtualizadoEvent>, EventoEventHandler>();
            service.AddScoped<IHandler<EnderecoEventoAdicionadoEvent>, EventoEventHandler>();

            service.AddScoped<IHandler<OrganizadorRegistradoEvent>, OrganizadorEventHandler>();

            // Infra - Data
            service.AddScoped<IEventoRepository, EventoRepository>();
            service.AddScoped<IOrganizadorRepository, OrganizadorRepository>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<EventosContext>();

            //Infra - Bus
            service.AddScoped<IBus, Bus.InMemoryBus>();

            //Infra - Identity
            service.AddTransient<IEmailSender, AuthMessageSender>();
            service.AddTransient<ISmsSender, AuthMessageSender>();
            service.AddScoped<IUser, AspNetUser>();

            //Infra - Filtros
            service.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            service.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();

            service.AddScoped<GlobalExceptionHandlingFilter>();

            service.AddScoped<GlobalActionLogger>();
        }
    }
}
