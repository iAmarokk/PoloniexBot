using PoloniexBot.Models;
using PoloniexBot.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace PoloniexBot.ViewModels
{
	internal class MainWindowViewModel : ViewModel
	{
		private string title = "Poloniex Bot";

		/// <summary>
		/// Заголовок окна.
		/// </summary>
		public string Title
		{
			get => this.title;
			set => this.Set(ref this.title, value);
		}

		public List<Coin> Currencies { get; set; }

		public MainWindowViewModel()
		{
			CompleteBalances balances = new CompleteBalances();
			this.Currencies = balances.Get();

			int i = 0;
		}
	}
}
