using Eventos.IO.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}
