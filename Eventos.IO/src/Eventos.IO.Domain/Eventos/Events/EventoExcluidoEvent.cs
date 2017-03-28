using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos.Events
{
    public class EventoExcluidoEvent : BaseEventoEvent
    {
        public EventoExcluidoEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
