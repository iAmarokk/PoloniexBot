using PoloniexBot.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoloniexBot.Models
{
	public class Coin : ViewModel
	{
		public string Name { get; set; }
		public CoinValues Values { get; set; }
		public decimal USDTPrice { get; set; }
	}

	public class CoinValues
	{
		public string available { get; set; }
		public string onOrders { get; set; }
		public string btcValue { get; set; }
		public decimal btcValueDecimal { get; set; }
		public void ParseValues()
		{
			this.btcValueDecimal = decimal.Parse(this.btcValue, CultureInfo.InvariantCulture);
		}
	}
}
