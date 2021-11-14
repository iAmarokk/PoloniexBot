using System.Windows;

namespace PoloniexBot.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string Key
        {
            get { return accountKeyBox.Text; }
        }

        public string Secret
        {
            get { return accountSecretBox.Text; }
        }
    }
}
