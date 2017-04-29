using System;
using ReactiveUI;
using ReactiveUI.XamForms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MC
{
	public partial class FailurePage
	{
		public FailurePage()
		{
			InitializeComponent();
		}

		public FailurePage(FailureVM viewModel)
		{
			InitializeComponent();

			ViewModel = viewModel;

			this.BindCommand(ViewModel, vm => vm.Confirm, v => v.Back);

		}
	}
}
