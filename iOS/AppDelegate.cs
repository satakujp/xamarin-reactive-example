using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace MC.iOS
{
	/// <summary>
	/// iOS向けアプリケーションの本体
	/// * global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegateの継承
	///   * global::はグローバル名前空間、https://msdn.microsoft.com/en-us/library/cc713620.aspx 
	///   * global名前空間はあらかじめ定義された無名名前空間へのエイリアスで、名前空間を指定しなかったクラスや全ての名前空間が含まれる。
	/// </summary>
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		/// <summary>
		/// * http://matatabi-ux.hateblo.jp/entry/2015/03/13/120000
		/// * https://developer.xamarin.com/api/member/MonoTouch.UIKit.UIApplicationDelegate.FinishedLaunching/p/MonoTouch.UIKit.UIApplication/MonoTouch.Foundation.NSDictionary/
		/// * FinishedLaucingは起動プロセスがほぼ終了した後、アプリの起動準備ができた状態で呼び出される。
		/// </summary>
		/// <returns><c>true</c>, if launching was finisheded, <c>false</c> otherwise.</returns>
		/// <param name="app">App.</param>
		/// <param name="options">Options.</param>
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			/// ## LoadApplicationメソッド
			/// iOSで言うところの`root view controller`をセットして、`Xamarin.Forms`のアプリケーションをスタートする。
			/// MC.Appクラスに委譲するので、ここから先はiOSとAndroid共通の部分になる
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
