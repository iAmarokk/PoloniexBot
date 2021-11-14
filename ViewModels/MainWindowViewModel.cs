using PoloniexBot.Models;
using PoloniexBot.Services;
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
		/// <summary>
		/// Заголовок окна.
		/// </summary>
		public string Title
		{
			get => this.title;
			set => this.Set(ref this.title, value);
		}

		private Coin selectedCoin;
		public Coin SelectedCoin
		{
			get => this.selectedCoin;
			set => this.Set(ref this.selectedCoin, value);
		}

		public decimal PriceBTC { get; set; }

		private decimal totalAssets = 0;
		public decimal TotalAssets 
		{
			get => this.totalAssets;
			set => this.Set(ref this.totalAssets, value);
		}

		private ObservableCollection<Coin> сurrencies = new ObservableCollection<Coin>();
		public ObservableCollection<Coin> Currencies
		{
			get => this.сurrencies;
			set => this.Set(ref this.сurrencies, value);
		}

		Dictionary<string, ReturnTicker> CoinTicket { get; set; }

		public MainWindowViewModel()
		{
			this.CoinTicket = TicketsInfo.Get();
			this.PriceBTC = decimal.Parse(this.CoinTicket["USDT_BTC"].last, CultureInfo.InvariantCulture);
			//this.UpdateCoinsAndTotalBalances();
			//this.NotNullCoins();
		}

		private void UpdateCoinsAndTotalBalances()
		{
			this.Currencies = CompleteBalances.Get();
			foreach (Coin item in this.Currencies)
			{
				if(this.CoinTicket.TryGetValue(string.Format("USDT_{0}", item.Name), out ReturnTicker ticker))
				{
					item.USDTPrice = decimal.Parse(ticker.last, CultureInfo.InvariantCulture);
				}

				if (item.Values.btcValueDecimal > 0)
				{
					this.TotalAssets += item.Values.btcValueDecimal * this.PriceBTC;
				}
			}
			//this.Currencies = new ObservableCollection<Coin>(this.сurrencies);
		}

		private void NotNullCoins()
		{
			List<Coin> result = new List<Coin>();
			foreach (Coin item in this.Currencies)
			{
				if (item.Values.btcValueDecimal > 0)
				{
					result.Add(item);
				}
			}
			this.Currencies = new ObservableCollection<Coin>(result);
			//this.Currencies.CollectionChanged += result;
		}

		// команда обновления балансов монет
		private RelayCommand updateBalancesCommand;
		public RelayCommand UpdateBalancesCommand => this.updateBalancesCommand ??
				  (this.updateBalancesCommand = new RelayCommand(obj =>
				  {
					  this.UpdateCoinsAndTotalBalances();
					  this.Title = "Update Coins And Total Balances";
				  }));

		// команда убрать балансы нулевых монет
		private RelayCommand notNullBalancesCommand;
		public RelayCommand NotNullBalancesCommand => this.notNullBalancesCommand ??
				  (this.notNullBalancesCommand = new RelayCommand(obj =>
				  {
					  this.NotNullCoins();
					  this.Title = "Not Null Coins";
				  }));
	}
}
