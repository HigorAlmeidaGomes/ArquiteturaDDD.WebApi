using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUser
    {
        Task<bool> AddUser(string email, string password, int age, string cellPhone);

        Task<bool> ThereIsUser(string email, string password);

        Task<string> UserIdReturn(string email);
    }
}
