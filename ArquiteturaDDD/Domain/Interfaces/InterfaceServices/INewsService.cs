using Entities.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface INewsService 
    {
        Task AddNews(News news);

        Task UpdateNews(News news);

        Task<List<News>> GetActiveNews();
    }
}
