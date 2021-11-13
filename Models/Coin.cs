using PoloniexBot.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoloniexBot.Models
{
	public class Coin : ViewModel
	{
		public string Name { get; set; }
		public CoinValues Values { get; set; }
	}

	public class CoinValues
	{
		public string available { get; set; }
		public string onOrders { get; set; }
		public string btcValue { get; set; }
	}
}
