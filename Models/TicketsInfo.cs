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
	public static class TicketsInfo
	{
		public static string Request = @$"https://poloniex.com/public?command=returnTicker";

		public static Dictionary<string, ReturnTicker> Get()
		{
			using HttpClient httpClient = new HttpClient();
			HttpResponseMessage response = httpClient.GetAsync(Request).Result;
			response.EnsureSuccessStatusCode();

			string result = response.Content.ReadAsStringAsync().Result;

			Dictionary<string, ReturnTicker> coinTicketResult = JsonConvert.DeserializeObject<Dictionary<string, ReturnTicker>>(result);

			//return decimal.Parse(coinTicketResult["USDT_BTC"].last, CultureInfo.InvariantCulture);

			return coinTicketResult;
		}
	}

	public class ReturnTicker
	{
		public int id { get; set; }
		public string last { get; set; }
		public string lowestAsk { get; set; }
		public string highestBid { get; set; }
		public string percentChange { get; set; }
		public string baseVolume { get; set; }
		public string quoteVolume { get; set; }
		public string isFrozen { get; set; }
		public string postOnly { get; set; }
		public string high24hr { get; set; }
		public string low24hr { get; set; }
	}
}
