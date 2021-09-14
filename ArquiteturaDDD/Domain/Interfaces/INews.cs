using Domain.Interfaces.Generics;
using Entities.Entites;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface INews : IGenerics<News>
    {
        Task<List<News>> ListNews(Expression<Func<News, bool>> exNews);
    }
}
