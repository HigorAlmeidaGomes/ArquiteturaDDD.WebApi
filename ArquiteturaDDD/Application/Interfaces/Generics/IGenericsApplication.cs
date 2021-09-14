using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Generics
{
    public interface IGenericsApplication<T> where T : class
    {
        Task Add(T Objeto);
        Task Update(T Objeto);

        Task Delete(T Objeto);

        Task<T> GetById(int id);

        Task<List<T>> GetAll();
    }
}
