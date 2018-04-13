using System.Threading.Tasks;

namespace LoginApp.Features.Login
{
    public class AccountService : IAccountService
    {
        public async Task<bool> Login(string username, string password)
        {
            await Task.Delay(1500);

            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }
    }
}