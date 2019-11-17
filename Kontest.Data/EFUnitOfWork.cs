using Kontest.Infrastructure.Interfaces;

namespace Kontest.Data
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly KontestDbContext _context;
        public EFUnitOfWork(KontestDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
