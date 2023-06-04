using System.Linq.Expressions;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task Create(T t);

        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null); //? que no sea obligatorio, si no envia un filtro devolvera todos

        Task<T> GetById(Expression<Func<T, bool>>? filter = null, bool tracked = true);

        Task Remove(T t);

        Task GrabarCambios();
    }
}
