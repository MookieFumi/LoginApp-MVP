using System.Threading.Tasks;

namespace LoginApp.Features.Login
{
    public interface ILoginView
    {
        Task Login(string username, string password);
        void Busy(bool value = true);
        void ShowUserIsNotAllowed();
    }
}

