using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Food_Recipe
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
	/// 

    public partial class LoginWindow : Window
    {
		MS02Entities db = new MS02Entities();
        public LoginWindow()
        {
            InitializeComponent();
        }

		private void ExitBtn_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void UserNameTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			userNameLabel.Visibility = Visibility.Collapsed;
		}

		private void UserNameTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if(userNameTextBox.Text.isEmpty())
			{
				userNameLabel.Visibility = Visibility.Visible;
			}
		}

		private void PasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			passwordLabel.Visibility = Visibility.Hidden;
		}

		private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if(passwordTextBox.Password.Length == 0)
			{
				passwordLabel.Visibility = Visibility.Visible;
			}
		}

		bool CheckInvalidFields()
		{
			return passwordTextBox.Password.Length == 0 || userNameTextBox.Text.isEmpty();
		}

		private void LoginBtn_Click(object sender, RoutedEventArgs e)
		{
            if (!CheckInvalidFields())
            {
                var queryFindAccount = (from account in db.Logins
                                        where userNameTextBox.Text == account.UserName && passwordTextBox.Password == account.Password
                                        select account).ToList();

                if (queryFindAccount.Count > 0)
                {
                    var screen = new SplashScreenWindow();
                    this.Hide();
                    screen.ShowDialog();
                }
                else
                    MessageBox.Show("Wrong user name or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
	}
}
