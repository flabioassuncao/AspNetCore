using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Core.Events.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Core.Notifications
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
    {
        bool HasNotifications();
        List<T> GetNotifications();
    }
}
