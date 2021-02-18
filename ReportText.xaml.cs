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
    /// Interaction logic for ReportText.xaml
    /// </summary>
    enum Filter { NewestPurchases, NewestCustomer, HotSaleProduct, Revenue, OutOfStockProducts };
    public class RevenueStatistic
    {
		public int Month { get; set; }
		public int Total { get; set; }
	}
    public partial class ReportText : UserControl
    {
		const int DATE_FILTER = 60;

		MS02Entities db = new MS02Entities();
		public ReportText()
        {
            InitializeComponent();
        }
		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			typeCombobox.SelectedIndex = 0;
		}

		private void typeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int selectedIndex = typeCombobox.SelectedIndex;
			DateTime today = DateTime.Now;
			DateTime fromTodayToBefore = DateTime.Now.AddDays(-DATE_FILTER);

			switch (selectedIndex)
			{
				case (int)Filter.NewestPurchases:
					var purchases = (from purchase in db.Purchases
									 join customer in db.Customers on purchase.Customer_Tel equals customer.Customer_Tel
									 join purchaseStateEnum in db.PurchaseStateEnums on purchase.Status equals purchaseStateEnum.Value
									 where purchase.CreateAt >= fromTodayToBefore && purchase.CreateAt <= today
									 select new
									 {
										 customer.Customer_Name,
										 purchaseStateEnum.EnumKey
									 }).ToList();
					listReportItem_DataGrid.ItemsSource = purchases;
					break;

				case (int)Filter.NewestCustomer:
					var customers = (from customer in db.Customers
									 where customer.CreateAt >= fromTodayToBefore && customer.CreateAt <= today
									 select new
									 {
										 customer.Customer_Name,
										 customer.Customer_Tel,
										 customer.Customer_Email,
										 customer.Customer_Address,
									 }).ToList();
					listReportItem_DataGrid.ItemsSource = customers;
					break;

				case (int)Filter.HotSaleProduct:
					var products = (from purchaseDetail in db.PurchaseDetails
									join product in db.Products on purchaseDetail.ProductID equals product.ProductID
									orderby purchaseDetail.Quantity descending
									select new
									{
										product.ProductDescription,
										product.Product_Quantity,
										product.ProductPrice
									}).ToList().Take(2);

					listReportItem_DataGrid.ItemsSource = products;
					break;

				case (int)Filter.Revenue:
					List<RevenueStatistic> revenueInMonth = new List<RevenueStatistic>();
					int index = 0;

					for (int month = 1; month <= 12; ++month)
					{
						RevenueStatistic revenueStatistic = new RevenueStatistic();

						revenueStatistic.Month = month;
						var data = (from purchase in db.Purchases
									join purchaseDetail in db.PurchaseDetails on purchase.PurchaseID equals purchaseDetail.PurchaseID
									where purchase.CreateAt.Value.Month == month
									select new
									{
										purchaseDetail.Quantity,
										purchaseDetail.Price,
									}).ToList();
						var rateSum = data.Sum(d => (d.Quantity * d.Price));

						revenueStatistic.Total = Convert.ToInt32(rateSum);
						revenueInMonth.Add(revenueStatistic);
						index = index + 1;
					}

					listReportItem_DataGrid.ItemsSource = revenueInMonth;
					break;
				case (int)Filter.OutOfStockProducts:
					var queryGetOutOfStockProducts = (from product in db.Products
													  where product.Product_Quantity < 10
													  select new
													  {
														  product.ProductDescription,
														  product.Product_Quantity,
														  product.ProductPrice
													  }).ToList();

					listReportItem_DataGrid.ItemsSource = queryGetOutOfStockProducts;
					break;
			}
		}

        private void chartbtn_Click(object sender, RoutedEventArgs e)
        {
			var scr = new ReportChart();
			scr.Show();
        }
    }
}
