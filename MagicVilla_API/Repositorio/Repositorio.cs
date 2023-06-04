using MagicVilla_API.Data;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this._dbSet = _db.Set<T>(); //convirtiendo la T en una entidad
        }

        public async Task Create(T t)
        {
            await _db.AddAsync(t);
            await GrabarCambios();
        }

        public async Task GrabarCambios()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();

        }

        public async Task Remove(T t)
        {
            _dbSet.Remove(t);
            await GrabarCambios();
        }
    }
}
