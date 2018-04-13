using Android.App;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Content;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Android.Gms.Common;
using Android.Gms.Common;

namespace LoginApp.Features.Login
{
    [Activity(Label = "LoginApp", MainLauncher = true)]
    public class LoginActivity : BaseActivity, ILoginView
    {
        const string TAG = "MainActivity";

        private LoginPresenter _presenter;
        private EditText _usernameEditText;
        private EditText _passwordEditText;
        private Button _loginButton;
        private Button _helpMeButton;

        private RecyclerView _recyclerView;
        private RecyclerView.LayoutManager _layoutManager;
        private LastUsersAdapter _adapter;

        private Dictionary<string, string> _items;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

            UserDialogs.Init(this);

            _items = new Dictionary<string, string>();
            UpdateItemsFromBundle(savedInstanceState);

            _presenter = new LoginPresenter(this, new AccountService());

            BindUiElements();
            SetupRecyclerView();

            BindUiActions();

            CheckGooglePlayServices();

            var logTokenButton = FindViewById<Button>(Resource.Id.logTokenButton);
            logTokenButton.Click += delegate {
                Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
                ShowToast(FirebaseInstanceId.Instance.Token);
            };
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(nameof(_items), Newtonsoft.Json.JsonConvert.SerializeObject(_items));
        }

        public async Task Login(string username, string password)
        {
            AddUserToItems(username, password);
            await _presenter.Login(username, password);
        }

        public void ShowUserIsNotAllowed()
        {
            ShowToast("User is not allowed");
        }

        private void AddUserToItems(string username, string password)
        {
            if (!_items.ContainsKey(username))
            {
                _items.Add(username, password);
            }
        }

        private void BindUiActions()
        {
            _loginButton.Click += async (sender, args) =>
            {
                await Login(_usernameEditText.Text, _passwordEditText.Text);
            };

            _helpMeButton.Click += (sender, args) =>
            {
                HelpMe();
            };

            _adapter.ItemClick += (sender, args) =>
            {
                ItemClick(args);
            };
        }

        private void BindUiElements()
        {
            _usernameEditText = FindViewById<EditText>(Resource.Id.username);
            _passwordEditText = FindViewById<EditText>(Resource.Id.password);
            _loginButton = FindViewById<Button>(Resource.Id.loginButton);
            _helpMeButton = FindViewById<Button>(Resource.Id.helpMeButton);
        }

        private void UpdateItemsFromBundle(Bundle savedInstanceState)
        {
            if (!string.IsNullOrEmpty(savedInstanceState?.GetString(nameof(_items))))
            {
                string _serializedItems = savedInstanceState.GetString(nameof(_items));
                _items = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(_serializedItems);
            }
        }

        private void HelpMe()
        {
            ShowToast($"Help me button clicked! ({_items.Count})");
        }

        private void ItemClick(string email)
        {
            ShowToast($"Username: {email}");
        }

        private void SetupRecyclerView()
        {
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.lastUsersRecyclerView);

            _layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            _recyclerView.SetLayoutManager(_layoutManager);

            _adapter = new LastUsersAdapter(this, _items);

            _recyclerView.SetAdapter(_adapter);
        }

        public void GoToMain()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        public bool CheckGooglePlayServices()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    ShowToast(GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    ShowToast("This device is not supported");
                    //Finish();
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}