using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace MC.iOS
{
	/// <summary>
	/// iOS向けアプリケーションクラス
	/// </summary>
	public class Application
	{
		/// <summary>
		/// iOS向けアプリケーションのメインエントリポイント
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main(string[] args)
		{
			/// if you want to use a different Application Delegate class from "AppDelegate"
			/// you can specify it here.
			/// メインの処理はMC.iOS.AppDelegateクラスに投げられるようだ。
			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}
