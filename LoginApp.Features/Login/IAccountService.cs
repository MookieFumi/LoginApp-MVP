using System.Threading.Tasks;

namespace LoginApp.Features.Login
{
    public interface IAccountService
    {
        Task<bool> Login(string username, string password);
    }
}