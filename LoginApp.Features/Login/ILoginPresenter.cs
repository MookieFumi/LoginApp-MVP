using System.Threading.Tasks;

namespace LoginApp.Features.Login
{
    public interface ILoginPresenter
    {
        Task Login(string username, string password);
    }
}

