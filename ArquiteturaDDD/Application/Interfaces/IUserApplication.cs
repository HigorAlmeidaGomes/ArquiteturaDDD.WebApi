using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserApplication
    {
        Task<bool> AddUser(string email, string password, int age, string cellPhone);

        Task<bool> ThereIsUser(string email, string password);
    }
}
