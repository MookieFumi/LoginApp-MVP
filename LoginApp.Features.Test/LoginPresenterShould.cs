using System.Collections.Generic;
using System.Threading.Tasks;
using LoginApp.Features.Login;
using Moq;
using Xunit;

namespace LoginApp.Features.Test
{
    public class LoginPresenterShould
    {
        private readonly Mock<ILoginView> _view;
        private readonly Mock<IAccountService> _service;
        private ILoginPresenter _sut;

        public LoginPresenterShould()
        {
            _view = new Mock<ILoginView>();
            _service = new Mock<IAccountService>();
            _sut = new LoginPresenter(_view.Object, _service.Object);
        }

        [Fact]
        public void Login_Call_Busy_Method_2_Times()
        {
            _sut.Login("asdas", "asdas");

            _view.Verify(m => m.Busy(true), Times.Once);
            _view.Verify(m => m.Busy(false), Times.Once);
        }

        [Fact]
        public void Login_Call_Login_Service_Method()
        {
            _sut.Login(GetValidUser().Key, GetValidUser().Value);

            _service.Verify(m=>m.Login(GetValidUser().Key, GetValidUser().Value), Times.Once);
        }

        [Fact]
        public void Not_Allowed_Login_Calls_ShowUserIsNotAllowed_Method()
        {
            _service.Setup(a => a.Login(GetValidUser().Key, GetValidUser().Value)).Returns(Task.FromResult(false));

            _sut.Login(GetValidUser().Key, GetValidUser().Value);

            _view.Verify(m => m.ShowUserIsNotAllowed(), Times.Once);
        }

        private KeyValuePair<string, string> GetValidUser()
        {
            return new KeyValuePair<string, string>("username1", "username");
        }
    }

}
