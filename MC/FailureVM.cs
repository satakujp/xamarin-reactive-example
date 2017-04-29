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
	public class FailureVM : ReactiveObject
	{
		/// <summary>
		/// 書きかけ
		/// Login, IsLogging. 
		/// </summary>
		/// <value>The login.</value>
		public ReactiveCommand Confirm { get; protected set; }

		/// <summary>
		/// 書きかけ
		/// </summary>
		readonly ObservableAsPropertyHelper<bool> _isconfirmed;
		public bool IsConfirmed
		{
			get { return _isconfirmed.Value; }
		}

		public FailureVM()
		{

			/// 
			Confirm = ReactiveCommand.Create(() => { });

			/// 
			Confirm.IsExecuting.ToProperty(this, x => x.IsConfirmed, out _isconfirmed);
		}
	}
}

