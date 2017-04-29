using ReactiveUI;
using System.Reactive.Linq;
using Xamarin.Forms;
using System.Diagnostics;
using System;

namespace MC
{
	/// <summary>
	/// MC.iOS.AppDelegateから呼び出されるクラス
	/// </summary>
    public partial class App : Application
    {
		/// <summary>
		/// コンストラクタ
		/// </summary>
        public App()
        {
			/// ### InitializeComponent()
			/// http://www.atmarkit.co.jp/ait/articles/1006/22/news101.html
			/// ここで、WPFに不慣れな人は以下の2点を疑問に思うかもしれない。
			/// * （1）Mainメソッドがない
			/// * （2）どこにも定義が見当たらないInitializeComponentというメソッドを呼び出している
			/// * InitializeComponentメソッドはXAML(この場合はApp.xaml)から自動生成されたコードの中で定義されているらしい
			/// * Mainメソッドも同様
			/// * InitializeComponentメソッドが見つからないというエラーもよく発生するようだ
            InitializeComponent();

            _coordinator = new AppCoordinator();

			/// ### MainPage
			/// * https://developer.xamarin.com/api/property/Xamarin.Forms.Application.MainPage/	
			/// * this.MainPageはXamarin.Forms.Applicationで定義されたプロパティ
			/// * アプリケーションのメインページを指定する
            MainPage = _coordinator.RootPage;

			/// ### this.WhenAnyValue
			/// * WhenAnyValueはReactiveUIで定義されているようだが、継承している形跡がない。どこで定義されているのか？
			/// * RactiveUI.WhenAnyMixinという静的クラスで定義されたメソッドだった。
			/// * http://reactiveui.net/api/index.html?%2Fapi%2Fpages%2FReactiveUI.WhenAnyMixin.WhenAnyDynamic.html
			/// * どうやらthis.WheAnyValueで定義したプロパティが変更された場合に、Subscribeにチェインした処理が実行されるようだ
			/// * この場合は、MC.AppCordinator.RootPageが変更されたら、Xamrin.Forms.Application.MainPageが変更されるように、つまりページが切り替わるようになっている。
			/// 
			/// ### RxApp.MainThreadScheduler
			/// * http://qiita.com/Temarin/items/a0525d73e8981489dd3e
			/// * ObserveOnで以降のSubscribeの処理を現在と同じスレッドで実行するように指定するようだ。現在はUI処理を実行しているスレッドなので、ページの切り替えも同じスレッドで行うのが好ましいのだろう。
			/// * https://docs.reactiveui.net/en/design-guidelines/ui-thread-and-schedulers.html
			/// * RxApp.MainThreadSchedulerはXamarin.FormsでUI処理を実行するためのスレッドのようだ。
            /// Monitor main page change.
			this.WhenAnyValue(x => x._coordinator.RootPage)
			    .ObserveOn(RxApp.MainThreadScheduler)
			    .Subscribe(page =>
                    {
					    Debug.WriteLine("App. Assign MainPage: '{0}'", page);
						MainPage = page;
                    });

		}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private AppCoordinator _coordinator;
    }
}

