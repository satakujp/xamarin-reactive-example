
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{

	/// <summary>
	/// アプリケーション全体の管理
	/// すべてのページを所有してページ遷移をコントロールしているのかも？
	/// ### ReactiveObjectの継承
	/// * ReactiveObjectはViewModelのためのクラス、加えてプロパティの変更をthis.WhenAnyValue()などで他のオブジェクトから観測できるようになっている。
	/// </summary>
    public class AppCoordinator : ReactiveObject
    {
        // MainPage.
        public ContentPage _rootPage;
        public ContentPage RootPage
        {
            get { return _rootPage; }

			/// <summary>
			/// ReactiveObject.RaiseAndSetIfChanged()メソッドでプロパティが変更されている場合は、変更をこのオブジェクトを購読しているオブジェクトへ通知するためのイベントを発生させる。
			/// </summary>
			/// <param name="value">Value.</param>
            protected set { this.RaiseAndSetIfChanged(ref _rootPage, value); }
        }

        public AppCoordinator()
        {
            _loginVM = new LoginVM();
            _loginPage = new LoginPage(_loginVM);

            _successPage = new SuccessPage();

			_failureVM = new FailureVM();
			_failurePage = new FailurePage(_failureVM);

            _rootPage = _loginPage;

            setupMGR();
            setupMGRAuth();
            setupMGRAuthTransitions();
        }

		/// <summary>
		/// ユーザー認証サービスを初期化する。
		/// </summary>
        public void setupMGR()
        {
            // _mgrClient = new MGRClient();
            // _mgr = new MGR(_mgrClient);

            /// Print result of the request.
			/// MC.MGR.authorize()でユーザー認証が成功したら、Authプロパティが変更されて、認証情報を標準出力に表示する。
            // this.WhenAnyValue(x => x._mgr.Auth)
			this.WhenAnyValue(x => x._loginVM.Model.Auth) 
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(auth =>
                    {
                        Debug.WriteLine(
                            "AppCoordinator. Authorize(access token: '{0}' refresh token: '{1}')",
                            auth.accessToken,
                            auth.refreshToken);
                    });
        }

		/// <summary>
		/// ユーザー認証中にMC.LoginVMがどのような状態になるか設定する。
		/// </summary>
        void setupMGRAuth()
        {

			this.WhenAnyValue(x => x._failureVM.IsConfirmed)
				.ObserveOn(RxApp.MainThreadScheduler)
				.Subscribe(executing =>
			{
				RootPage = _loginPage;
			});
        }

		/// <summary>
		/// ユーザー認証後にMC.LoginVMがどのような状態になるか設定する。
		/// </summary>
        void setupMGRAuthTransitions()
        {
			/// MC.MGR.AuthStatusがModelRequestStatus.Successになったら、つまり認証に成功したら、成功ページ`SuccessPage.xaml`を表示する。
            /// Go to 'Success' upon successful authorization.
			this.WhenAnyValue(x => x._loginVM.Model.AuthStatus)
                .Where(x => x == ModelRequestStatus.Success)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(status =>
                    {
                        Debug.WriteLine("AppCoordinator. set main page to SuccessPage");
                        RootPage = _successPage;
                    });

			/// MC.MGR.AuthStatusがModelRequestStatus.Failureになったら、つまり認証に失敗したら、失敗ページ`FailurePage.xaml`を表示する。
            /// Go to 'Failure' upon failed authorization.
			this.WhenAnyValue(x => x._loginVM.Model.AuthStatus)
                .Where(x => x == ModelRequestStatus.Failure)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(status =>
                    {
                        Debug.WriteLine("AppCoordinator. set main page to FailurePage");
                        RootPage = _failurePage;
                    });
        }

        // private MGRClient _mgrClient;
		// private LoginModel _mgr;

        private LoginVM _loginVM;
        private LoginPage _loginPage;

        //private SuccessVM _successVM;
        private SuccessPage _successPage;

		private FailureVM _failureVM;
        private FailurePage _failurePage;
    }
}

