using PoloniexBot.Models;
using PoloniexBot.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

		public decimal PriceBTC { get; set; }
		public decimal TotalAssets { get; set; }

		public ObservableCollection<Coin> Currencies { get; set; }

		public MainWindowViewModel()
		{
			this.PriceBTC = BTCPrice.Get();
			CompleteBalances balances = new CompleteBalances();
			this.Currencies = balances.Get();
			foreach(Coin item in this.Currencies)
			{
				decimal value = decimal.Parse(item.Values.btcValue, CultureInfo.InvariantCulture);

				if (value > 0)
				{
					this.TotalAssets += value * this.PriceBTC;
				}
			}
		}
	}
}
