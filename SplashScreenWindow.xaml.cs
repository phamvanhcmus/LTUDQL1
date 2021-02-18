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
using System.Configuration;
using System.Windows.Threading;

namespace Food_Recipe
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        public SplashScreenWindow()
        {
            InitializeComponent();
        }

		Random rng = new Random();
		List<string> quotes = new List<string> { "The term electronic commerce or e-commerce refers to any sort of business transaction that involves the transfer of information through the internet",
													 "E-commerce means using the Internet and the web for business transactions",
													 "By definition it covers a variety of business activities",
													 "E-business applications turn into e-commerce precisely",
													 "The history of E-commerce begins with the invention of the telephone at the end of last century",
													 "The Internet was conceived in 1969, when the Advanced Research Projects Agency "};

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var config = ConfigurationManager.AppSettings["ShowSplash"];
			
			if(config.ToLower() == "false")
			{
				this.Hide();
				var homeScreen = new MainWindow();
				homeScreen.Show();
			}

			DataContext = quotes[rng.Next(quotes.Count())];

			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(3);
			timer.Tick += Timer_Tick;
			timer.Start();
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			DataContext = quotes[rng.Next(quotes.Count())];
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.AppSettings.Settings["ShowSplash"].Value = "False";
			config.Save(ConfigurationSaveMode.Modified);
		}

		private void NavigateHomeWindow_Click(object sender, RoutedEventArgs e)
		{
			var screen = new MainWindow();
			screen.Show();
			this.Close();
		}
	}
}
