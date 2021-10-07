using Application.Interfaces;
using Domain.Interfaces;
using Entities.Entites;
using System;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class UserApplication : IUserApplication
    {
        private readonly IUser _user;

        public UserApplication(IUser user)
        {
            _user = user;
        }

        public async Task<bool> AddUser(string email, string password, int age, string cellPhone)
        {
            //var parameters = new AplicationUser { Email = email, PasswordHash = password, Age = age, Cellphone = cellPhone };

            return await _user.AddUser(email, password, age, cellPhone);
        }

        public async Task<bool> ThereIsUser(string email, string password)
        {
            return await _user.ThereIsUser(email, password);
        }

        public async Task<string> UserIdReturn(string email)
        {
            return await _user.UserIdReturn(email);
        }
    }
}
