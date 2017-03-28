using Eventos.IO.Domain.Core.Commands;
using Eventos.IO.Domain.Core.Events;

namespace Eventos.IO.Domain.Core.Bus
{
    public interface IBus
    {
        void SendCommand<T>(T theCommand) where T : Command;
        void RaiseEvent<T>(T theEvent) where T : Event;
    }
}
