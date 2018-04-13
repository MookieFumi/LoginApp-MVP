using Acr.UserDialogs;
using Android.Support.V7.App;

namespace LoginApp
{
    public class BaseActivity : AppCompatActivity
    {
        public void Busy(bool value = true)
        {
            if (value)
            {
                UserDialogs.Instance.ShowLoading();
            }
            else
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        public void ShowToast(string text)
        {
            UserDialogs.Instance.Toast(text);
        }
    }
}

