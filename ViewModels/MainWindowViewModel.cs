using PoloniexBot.Models;
using PoloniexBot.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace PoloniexBot.ViewModels
{
	internal class MainWindowViewModel : ViewModel
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

		public Currencies Currencies { get; set; }

		public MainWindowViewModel()
		{
			CompleteBalances balances = new CompleteBalances();
			this.Currencies = balances.Get();

			Type myType = typeof(Currencies);

			List<MemberInfo> obj = new List<MemberInfo>();
			foreach (MemberInfo mi in myType.GetMembers())
			{
				if(mi.MemberType == MemberTypes.Field)
				{
					obj.Add(mi);
				}
			}
			foreach(var item in obj)
			{

			}
			int i = 0;
		}
	}
}
