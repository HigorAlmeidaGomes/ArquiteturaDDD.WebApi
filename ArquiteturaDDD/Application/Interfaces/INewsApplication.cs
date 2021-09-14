using Application.Interfaces.Generics;
using Entities.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INewsApplication : IGenericsApplication<News>
    {
        Task AddNews(News news);

        Task UpdateNews(News news);

        Task<List<News>> GetActiveNews();
    }
}
