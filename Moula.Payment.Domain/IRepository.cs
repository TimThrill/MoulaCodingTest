using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Payment.Domain
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
