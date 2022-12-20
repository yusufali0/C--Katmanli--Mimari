using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAB.ToDoAppNTier.DataAccess.Contexts;
using YAB.ToDoAppNTier.DataAccess.Interfaces;
using YAB.ToDoAppNTier.DataAccess.Repositories;
using YAB.ToDoAppNTier.Entities.Domains;

namespace YAB.ToDoAppNTier.DataAccess.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly ToDoContext _context;

        public Uow(ToDoContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
