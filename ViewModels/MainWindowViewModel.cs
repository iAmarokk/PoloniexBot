using PoloniexBot.ViewModels.Base;

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
	}
}
