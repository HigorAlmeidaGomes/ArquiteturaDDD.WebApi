using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class NewsService : INewsService
    {
        private readonly INews _Inews;

        public NewsService(INews news)
        {
            _Inews = news;
        }
        
        /// <summary>
        /// Adicionar Uma nova Noticia 
        /// </summary>
        /// <param name="news"></param>
        /// <returns>Noticia Criada</returns>
        public async Task AddNews(News news)
        {
            var validateTitle = news.ValidateStringProperty(news.Title, "Title");
            var validateInformation = news.ValidateStringProperty(news.Information, "Information");

            if (validateInformation && validateTitle)
            {
                news.DateRegister = DateTime.Now;
                news.DateChange = DateTime.Now;
                news.Active = true;
                await _Inews.Add(news);
            }
        }
        /// <summary>
        /// Obter Todas as notícias
        /// </summary>
        /// <returns>Retorna uma Lista de Noticias</returns>
        public async Task<List<News>> GetActiveNews()
        {
            return await _Inews.ListNews(n => n.Active);
        }
        /// <summary>
        /// Atualizar os noticias
        /// </summary>
        /// <param name="news"></param>
        /// <returns>NoticiaAtualizada</returns>
        public async Task UpdateNews(News news)
        {

            var validateTitle = news.ValidateStringProperty(news.Title, "Title");
            var validateInformation = news.ValidateStringProperty(news.Information, "Information");

            if (validateInformation && validateTitle)
            {
                news.DateRegister = DateTime.Now;
                news.DateChange = DateTime.Now;
                news.Active = true;
                await _Inews.Update(news);
            }
        }
    }
}
