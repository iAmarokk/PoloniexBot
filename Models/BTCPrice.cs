using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PoloniexBot.Models
{
	public class BTCPrice : BaseRequest
	{
		public static string Request = @$"https://poloniex.com/public?command=returnTicker";

		public static decimal Get()
		{
			using HttpClient httpClient = new HttpClient();
			HttpResponseMessage response = httpClient.GetAsync(Request).Result;
			response.EnsureSuccessStatusCode();

			string result = response.Content.ReadAsStringAsync().Result;

			Dictionary<string, CoinTicket> coinTicketResult = JsonConvert.DeserializeObject<Dictionary<string, CoinTicket>>(result);

			return decimal.Parse(coinTicketResult["USDT_BTC"].last, CultureInfo.InvariantCulture);
		}
	}


}
