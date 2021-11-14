using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace PoloniexBot.Models
{
	public static class CompleteBalances
	{
		private static string request = "returnCompleteBalances";

		public static ObservableCollection<Coin> Get()
		{
			using HttpClient httpClient = new HttpClient();

			httpClient.BaseAddress = new Uri(BaseRequest.PrivateHTTPEndpoint);
			httpClient.DefaultRequestHeaders.Add("Key", BaseRequest.Key);

			KeyValuePair<string, string>[] param = {
					new("command", CompleteBalances.request),
					new("nonce", DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()),
				};

			FormUrlEncodedContent content = new FormUrlEncodedContent(param);
			string body = content.ReadAsStringAsync().Result;
			string sign = BaseRequest.Sign(BaseRequest.Secret, body);
			httpClient.DefaultRequestHeaders.Add("Sign", sign);

			HttpResponseMessage response = httpClient.PostAsync("", content).Result;
			response.EnsureSuccessStatusCode();

			string result = response.Content.ReadAsStringAsync().Result;

			//Dictionary<string, CoinValues> res = JsonConvert.DeserializeObject<Dictionary<string, CoinValues>>(result);

			#region JToken
			JEnumerable<JToken> x = JsonConvert.DeserializeObject<JObject>(result).Children();
			ObservableCollection<Coin> Coins = new ObservableCollection<Coin>();
			foreach (JToken item in x)
			{
				Coin coin = new Coin();
				coin.Name = item.Path;
				coin.Values = JsonConvert.DeserializeObject<CoinValues>(item.First.ToString());
				coin.Values.ParseValues();
				Coins.Add(coin);
			}
			return Coins;
			#endregion

			//return res;
		}
	}
}
