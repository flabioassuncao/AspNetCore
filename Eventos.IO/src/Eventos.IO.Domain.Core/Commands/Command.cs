using Eventos.IO.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Core.Commands
{
    public class Command : Message
    {
        public DateTime Timestamp { get; set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
