using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MC
{
	/// <summary>
	/// LoginPage.xaml用のViewModel
	/// </summary>
    public class LoginVM : ReactiveObject
    {
		/// <summary>
		/// 書きかけ
		/// Login, IsLogging. 
		/// </summary>
		/// <value>The login.</value>
        public ReactiveCommand Login { get; protected set; }

		/// <summary>
		/// 書きかけ
		/// </summary>
        readonly ObservableAsPropertyHelper<bool> _isLogging;
        public bool IsLogging
        {
            get { return _isLogging.Value; }
        }

        // IsLoading.
        bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { this.RaiseAndSetIfChanged(ref _isLoading, value); }
        }

        // Username.
        string _username;
        public string Username
        {
            get { return _username; }
            set { this.RaiseAndSetIfChanged(ref _username, value); }
        }

        // Password.
        string _password;
        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        public LoginVM()
        {
			/// IObservable()オブジェクトを変数に格納する。
			/// 中身はUsernameプロパティとPasswordプロパティの両方が入力されたらtrueを返す処理
            var canLogin =
                this.WhenAnyValue(
                    x => x.Username,
                    x => x.Password,
                    (us, pa) =>
                        {
                            bool IsUsernameValid = !String.IsNullOrWhiteSpace(us);
                            bool IsPasswordValid = !String.IsNullOrWhiteSpace(pa);
                            return IsUsernameValid && IsPasswordValid;
                        });

			/// 
            Login = ReactiveCommand.Create(() => { }, canLogin);

			/// 
            Login.IsExecuting.ToProperty(this, x => x.IsLogging, out _isLogging);
        }
    }
}

