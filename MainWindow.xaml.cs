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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Food_Recipe
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 
	public static class StringExtension
	{
		public delegate void ReturnToAnother(bool isClicked);

		public static bool IsNotEmpty(this string data)
		{
			bool result = data.Length != 0;
			return result;
		}
	}

	public partial class MainWindow : Window
	{
		bool isOpen = true;
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//List<Product> queryFavorFood = null;
			//List<Product> queryAllFood = null;
			//loadingProgressBar.IsIndeterminate = true;

			//await Task.Run(() =>
			//{
			//	System.Threading.Thread.Sleep(3000);
			//	queryFavorFood = new List<Product>( (from product in db.Products
			//							    where product.Product_isFavor == true
			//							    select product).ToList());

			//	queryAllFood = new List<Product>((from product in db.Products
			//									  select product));
			//});

			//loadingProgressBar.IsIndeterminate = false;
			

			//FavorProductListView.ItemsSource = queryFavorFood;
			//AllFoodListView.ItemsSource = queryAllFood;
			//StatusBarTextBlock.Text = "All products load complete";

		}

		void HideAllUC()
		{
			homeUC.Visibility = Visibility.Collapsed;
			categoryListUC.Visibility = Visibility.Collapsed;
		}

		private void HomeBtn_Click(object sender, RoutedEventArgs e)
		{
			HideAllUC();
			categoryListUC.Visibility = Visibility.Visible;
		}

		private void MenuBtn_Click(object sender, RoutedEventArgs e)
		{
			const int WIDTH_OPEN = 210;
			const int WIDTH_CLOSE = 47;
			

			if (!isOpen)
			{
				//menuStackPanel.Show();
				menuStackPanel.Width = WIDTH_OPEN;

				logoBrandImage.Visibility = Visibility.Visible;
			}
			else
			{
				//menuStackPanel.Collapse();
				menuStackPanel.Width = WIDTH_CLOSE;

				logoBrandImage.Visibility = Visibility.Hidden;
			}

			isOpen = !isOpen;
		}

		private void EditBtn_Click(object sender, RoutedEventArgs e)
		{
			var scr = new Order();
			scr.Show();
			this.Close();
		}

        private void ChartBtn_Click(object sender, RoutedEventArgs e)
        {
			MainWindow w = new MainWindow();
			w.Content = new ReportText();
			w.Show();
		}
    }
}
