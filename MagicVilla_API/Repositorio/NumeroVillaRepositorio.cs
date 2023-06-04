using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repositorio.IRepositorio;

namespace MagicVilla_API.Repositorio
{
    public class NumeroVillaRepositorio : Repositorio<NumeroVilla>, INumeroVillaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public NumeroVillaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<NumeroVilla> Update(NumeroVilla t)
        {
            t.UpdatedDate = DateTime.Now;
            _db.NumeroVillas.Update(t);
            await _db.SaveChangesAsync();
            return t;
        }
    }
}
