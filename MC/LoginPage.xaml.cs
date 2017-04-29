using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
    public partial class LoginPage
    {
		public LoginPage()
		{
			InitializeComponent();
		}

        public LoginPage(LoginVM loginVM)
        {
            InitializeComponent();

            ViewModel = loginVM;
            this.Bind(ViewModel, vm => vm.Username, v => v.Username.Text);
            this.Bind(ViewModel, vm => vm.Password, v => v.Password.Text);
            this.BindCommand(ViewModel, vm => vm.Login, v => v.Login);

			/// LoginVM.IsLoadingがtrueの場合はテキストボックスやボタンを非活動にしたり、スピナーを表示したりする。
            /// Display spinner while loading.
            this.WhenAnyValue(x => x.ViewModel.IsLoading)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(busy =>
                    {
                        Username.IsEnabled = !busy;
                        Password.IsEnabled = !busy;
                        Processing.IsVisible = busy;
                        Main.IsVisible = !busy;
                    });
        }
    }
}

