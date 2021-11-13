using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PoloniexBot.Models
{
	public class CompleteBalances : BaseRequest
	{
		public string request = "returnCompleteBalances";

		public string Coin { get; set; }
		public decimal Balance { get; set; }

		public List<Coin> Get()
		{

			using HttpClient httpClient = new HttpClient();

			httpClient.BaseAddress = new Uri(this.PrivateHTTPEndpoint);
			httpClient.DefaultRequestHeaders.Add("Key", this.key);

			KeyValuePair<string, string>[] param = {
					new("command", this.request),
					new("nonce", DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()),
				};

			FormUrlEncodedContent content = new FormUrlEncodedContent(param);
			string body = content.ReadAsStringAsync().Result;
			string sign = BaseRequest.Sign(this.secret, body);
			httpClient.DefaultRequestHeaders.Add("Sign", sign);

			HttpResponseMessage response = httpClient.PostAsync("", content).Result;
			response.EnsureSuccessStatusCode();

			string result = response.Content.ReadAsStringAsync().Result;

			//Currencies currencies = JsonConvert.DeserializeObject<Currencies>(result);

			//<Dictionary<string, string>> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

			JEnumerable<JToken> x = JsonConvert.DeserializeObject<JObject>(result).Children();

			List<Coin> Coins = new List<Coin>();

			foreach (JToken item in x)
			{
				Coin coin = new Coin();
				coin.Name = item.Path;
				coin.Name = ((JProperty)item).Name;
				coin.Values = JsonConvert.DeserializeObject<CoinValues>(item.First.ToString());
			}

			return Coins;
		}
	}

	public class JSONCoin
	{

	}
}
