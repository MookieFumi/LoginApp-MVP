﻿using System.Threading.Tasks;

namespace LoginApp.Features.Login
{
    public class LoginPresenter : ILoginPresenter
    {
        private readonly ILoginView _view;
        private readonly IAccountService _accountService;

        public LoginPresenter(ILoginView view, IAccountService accountService)
        {
            _view = view;
            _accountService = accountService;
        }

        public async Task Login(string username, string password)
        {
            _view.Busy(true);

            var result = await _accountService.Login(username, password);

            _view.Busy(false);

            if (result)
            {
                _view.GoToMain();
            }
            else
            {
                _view.ShowUserIsNotAllowed();
            }
        }
    }
}

