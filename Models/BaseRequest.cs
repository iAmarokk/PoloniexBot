using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PoloniexBot.Models
{
	public static class BaseRequest
	{
		public static string PrivateHTTPEndpoint = $@"https://poloniex.com/tradingApi";
		public static string PublicHTTPEndpoint = $@"https://poloniex.com/public";

		public static string Key;
		public static string Secret;

		static BaseRequest()
		{
			BaseRequest.Key = BaseRequest.ReadFromConfig("accountKey");
			BaseRequest.Secret = BaseRequest.ReadFromConfig("accountSecret");
		}

		public static string Sign(string secret, string body)
		{
			byte[] keyBytes = Encoding.UTF8.GetBytes(secret);
			using HMACSHA512 hmac = new HMACSHA512(keyBytes);

			byte[] data = Encoding.UTF8.GetBytes(body);
			byte[] hash = hmac.ComputeHash(data);
			string sign = BinaryToHex(hash);

			return sign;
		}

		private static string BinaryToHex(byte[] data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in data)
			{
				stringBuilder.Append(b.ToString("x2"));
			}

			return stringBuilder.ToString();
		}

		private static string ReadFromConfig(string settingKey)
		{
			string setting = ConfigurationManager.AppSettings[settingKey];
			if (string.IsNullOrWhiteSpace(setting))
			{
				throw new ArgumentNullException(nameof(setting), "Не заполнены параметры конфигурации");
			}

			return setting;
		}
	}
}
