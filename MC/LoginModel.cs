
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MC
{
	/// <summary>
	/// ユーザー認証を実行するサービス
	/// </summary>
    public class LoginModel : ReactiveObject
    {
        // AuthStatus.
        ModelRequestStatus _authStatus;

		/// <summary>
		/// 変更があったらPropertyChangedイベントが発生する。
		/// </summary>
		/// <value>The auth status.</value>
        public ModelRequestStatus AuthStatus
        {
            get { return _authStatus; }
            protected set { this.RaiseAndSetIfChanged(ref _authStatus, value); }
        }

        // Auth.
		AuthToken _auth;

		/// <summary>
		/// 変更があったらPropertyChangedイベントが発生する。
		/// </summary>
		/// <value>The auth.</value>
		public AuthToken Auth
        {
            get { return _auth; }
            protected set { this.RaiseAndSetIfChanged(ref _auth, value); }
        }

		public LoginModel()
        {
			_client = new LoginModelClient();
			_auth = new AuthToken();
            _authStatus = ModelRequestStatus.None;
        }

		/// <summary>
		/// 非同期でユーザー認証する。
		/// 認証結果および認証キーはそれぞれ、`this.AuthStatus`と`this.Auth`が変更されそのイベントを呼び出し側が購読することでViewへ伝わるようになっている。
		/// </summary>
		/// <returns>The authorize.</returns>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
        public async void authorize(string username, string password)
        {
            Debug.WriteLine("MGR. authorize");
            AuthStatus = ModelRequestStatus.Process;
            try
            {
                Auth = await _client.GetAuthAsync(username, password);
                AuthStatus = ModelRequestStatus.Success;
                Debug.WriteLine("MGR. authorize OK: '{0}'", Auth.toJSON());
            }
            catch (Exception e)
            {
                Debug.WriteLine("MGR. authorize ERROR: '{0}'", e.Message);
                AuthStatus = ModelRequestStatus.Failure;
            }
        }

		private LoginModelClient _client;
    }
}

