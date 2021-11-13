using PoloniexBot.Models;
using PoloniexBot.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;

namespace PoloniexBot.ViewModels
{
	public class MainWindowViewModel : ViewModel
	{
		private string title = "Poloniex Bot";
		private Coin selectedCoin;

		/// <summary>
		/// Заголовок окна.
		/// </summary>
		public string Title
		{
			get => this.title;
			set => this.Set(ref this.title, value);
		}

		public Coin SelectedCoin
		{
			get => this.selectedCoin;
			set => this.Set(ref this.selectedCoin, value);
		}

		public ObservableCollection<Coin> Currencies { get; set; }

		public MainWindowViewModel()
		{
			decimal priceBTC = BTCPrice.Get();
			CompleteBalances balances = new CompleteBalances();
			this.Currencies = balances.Get();
		}
	}
}
