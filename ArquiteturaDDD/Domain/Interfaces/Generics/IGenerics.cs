using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Generics
{
    public interface IGenerics<T> where T : class
    {
        Task Add(T Objeto);
        Task<T> Update(T Objeto);

        Task Delete(T Objeto);

        Task<T> GetById(int id);

        Task<List<T>> GetAll();

    }
}
