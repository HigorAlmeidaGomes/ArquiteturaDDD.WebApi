using Application.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class NewsApplication : INewsApplication
    {
        INews _Inews;
        INewsService _INewsService;

        public NewsApplication(INews news, INewsService newsService)
        {
            _Inews = news;
            _INewsService = newsService;
        }

        public async Task AddNews(News news)
        {
           await _INewsService.AddNews(news);
        }

        public async Task UpdateNews(News news)
        {
            await _INewsService.UpdateNews(news);
        }
        public async Task<List<News>> GetActiveNews()
        {
            return await _INewsService.GetActiveNews();
        }

        public async Task Add(News Objeto)
        {
            await _Inews.Add(Objeto);
        }
        public async Task Delete(News Objeto)
        {
            await _Inews.Delete(Objeto);
        }

        public async Task Update(News Objeto)
        {
            await _Inews.Update(Objeto);
        }
        public async Task<News> GetById(int id)
        {
            return await _Inews.GetById(id);
        }

        public async Task<List<News>> GetAll()
        {
            return await _Inews.GetAll();
        }
    }
}
