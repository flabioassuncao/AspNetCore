using Eventos.IO.Domain.Core.Notifications;
using Eventos.IO.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Eventos.IO.Site.Controllers
{
    public class BaseController : Controller
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notification;
        private readonly IUser _user;

        public Guid OrganizadroId { get; set; }

        public BaseController(IDomainNotificationHandler<DomainNotification> notification, IUser user)
        {
            _notification = notification;
            _user = user;

            if (user.IsAuthenticated())
            {
                OrganizadroId = _user.GetUserId();
            }
        }

        protected bool OperacaoValida()
        {
            return (!_notification.HasNotifications());
        }


    }
}
