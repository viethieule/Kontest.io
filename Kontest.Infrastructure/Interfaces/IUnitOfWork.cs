using System;

namespace Kontest.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit transaction
        /// </summary>
        void Commit();
    }
}
