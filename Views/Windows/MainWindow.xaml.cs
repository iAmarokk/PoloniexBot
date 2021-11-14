using PoloniexBot.ViewModels;
using PoloniexBot.Views.Windows;
using System.Windows;

namespace PoloniexBot
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainWindowViewModel();
		}

		private void Login_Click(object sender, RoutedEventArgs e)
		{

			AuthWindow passwordWindow = new AuthWindow();

			if (passwordWindow.ShowDialog() == true)
			{

				var k = passwordWindow.Key;
				var s = passwordWindow.Secret;

				if (passwordWindow.Key == "123")
					MessageBox.Show("Авторизация пройдена");
				else
					MessageBox.Show("Неверный пароль");
			}
			else
			{
				MessageBox.Show("Авторизация не пройдена");
			}

		}
	}
}
