using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationUser
    {
        Task<bool> AddUser(string email, string password, int age, string cellPhone);
    }
}
