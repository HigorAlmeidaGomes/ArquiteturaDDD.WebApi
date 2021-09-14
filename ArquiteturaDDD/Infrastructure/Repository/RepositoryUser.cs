using Domain.Interfaces;
using Entities.Entites;
using Infrastructure.Repository.Generics;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryUser : GenericRepository<AplicationUser>, IUser
    {
        private readonly DbContextOptions<Context> _dbContextBuilder;

        public RepositoryUser()
        {
            _dbContextBuilder = new DbContextOptions<Context>();
        }

        public async Task<bool> AddUser(string email, string password, int age, string cellPhone)
        {
            try
            {
                using (var data = new Context(_dbContextBuilder))
                {
                    var parameters = new AplicationUser { Email = email, PasswordHash = password, Age = age, Cellphone = cellPhone };

                    await data.AplicationUser.AddAsync(parameters);

                    await data.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(paramName: nameof(ex), message: "Erro ao tentar Adicionar Usuário");

                return false;
            }

            return true;
        }
    }
}
