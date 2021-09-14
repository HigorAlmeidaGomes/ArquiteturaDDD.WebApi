using Domain.Interfaces;
using Entities.Entites;
using Infrastructure.Repository.Generics;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryNews : GenericRepository<News>, INews
    {
        private readonly DbContextOptions<Context> _dbContextBuilder;

        public RepositoryNews()
        {
            _dbContextBuilder = new DbContextOptions<Context>();
        }
        public async Task<List<News>> ListNews(Expression<Func<News, bool>> exNews)
        {
            using (var banco = new Context(_dbContextBuilder))
            {
                try
                {
                    return await banco.News.Where(exNews).AsNoTracking().ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException(paramName: nameof(ex), message: "Erro ao tentar obter a notícia");
                }

            }
        }
    }
}
