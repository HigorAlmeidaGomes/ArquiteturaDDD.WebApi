using Domain.Interfaces;
using Entities.Entites;
using Infrastructure.Repository.Generics;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public async Task<bool> ThereIsUser(string email, string password)
        {
            try
            {
                using (var data = new Context(_dbContextBuilder))
                {
                    return await data.AplicationUser.Where(x => x.Email.Equals(email) && x.PasswordHash.Equals(password)).AsNoTracking().AnyAsync();
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(paramName: nameof(ex), message: "Erro ao tentar encontrar o usuário");

                return false;
            }

        }

        public async Task<string> UserIdReturn(string email)
        {
            try
            {
                using var data = new Context(_dbContextBuilder);

                var user = await data.AplicationUser.Where(x => x.Email.Equals(email)).AsNoTracking().FirstOrDefaultAsync();

                return user.Id;

            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(paramName: nameof(ex), message: "Erro ao tentar retornar o Id do usuário. ");
            }
        }
    }
}
