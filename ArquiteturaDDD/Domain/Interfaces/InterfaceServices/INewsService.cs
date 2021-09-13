using Entities.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
