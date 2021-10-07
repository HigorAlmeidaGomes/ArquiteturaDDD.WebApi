using Application.Interfaces;
using Entities.Entites;
using Entities.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsApplication _InewsApplication;

        public NewsController(INewsApplication InewsApplication)
        {
            _InewsApplication = InewsApplication;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetNews")]
        public async Task<List<News>> GetNews() 
        {
            return await _InewsApplication.GetActiveNews();
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/AddNews")]
        public async Task<List<Notify>> AddNews(NewsModel newsModel) 
        {
            var newNews = new News{ Title = newsModel.Title, Information = newsModel.Information, UserId =  await ReturnUserLogged()};

            await _InewsApplication.AddNews(newNews);

            return newNews.Notification;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/UpdateNews")]
        public async Task<List<Notify>> UpdateNews(NewsModel newsModel)
        {
            var newNews = await _InewsApplication.GetById(newsModel.IdNews);
            newNews.Title = newsModel.Title;
            newNews.Information = newsModel.Information;
            newNews.UserId = await ReturnUserLogged();

            await _InewsApplication.UpdateNews(newNews);

            return newNews.Notification;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/DeleteNews")]
        public async Task<List<Notify>> DeleteNews(NewsModel newsModel)
        {
            var newNews = await _InewsApplication.GetById(newsModel.IdNews);

            await _InewsApplication.Delete(newNews);

            return newNews.Notification;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetById{id}")]
        public async Task<Notify> GetById(NewsModel newsModel)
        {
            var newNews = await _InewsApplication.GetById(newsModel.IdNews);
            
            return newNews;
        }


        private Task<string> ReturnUserLogged() 
        {
            if (User != null)
            {
                var userId = User.FindFirst("userId");

                return Task.FromResult(userId.Value);
            }
            else return Task.FromResult(string.Empty);
        }
    }
}
