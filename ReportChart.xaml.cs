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
using LiveCharts;
using LiveCharts.Wpf;

namespace Food_Recipe
{
    /// <summary>
    /// Interaction logic for ReportChart.xaml
    /// </summary>
    enum Filter1 { Revenue, HotSaleProduct };
    public class RevenueStatistic1
    {
        public int Month { get; set; }
        public int Total { get; set; }
    }
    public partial class ReportChart : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public SeriesCollection SeriesCollection1 { get; set; }
        public string[] Labels1 { get; set; }
        public Func<double, string> Formatter1 { get; set; }
        const int DATE_FILTER = 60;

        MS02Entities db = new MS02Entities();
        public ReportChart()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Hot Sale Product",
                    Values = new ChartValues<double> { 8, 13}
                }
            };

            Labels = new[] { "Apple", "Asus" };
            Formatter = value => value.ToString("N");
            
            SeriesCollection1 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Revenue Status",
                    Values = new ChartValues<double> { 650000000, 67000000,0, 0, 0, 0, 0, 0, 0,30000000, 157000000, 0}
                }
            };

            Labels1 = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            Formatter1 = value => value.ToString("N");
            DataContext = this;
        }
        private void order_Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (dockpanel_hsp.Visibility == Visibility.Collapsed)
                dockpanel_hsp.Visibility = Visibility.Visible;
            if (dockpanel_re.Visibility == Visibility.Visible)
                dockpanel_re.Visibility = Visibility.Collapsed;
            
        }

        private void order_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            order_Button.Opacity = 1;
        }

        private void order_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            order_Button.Opacity = .5;
        }

        private void revenue_Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (dockpanel_hsp.Visibility == Visibility.Visible)
                dockpanel_hsp.Visibility = Visibility.Collapsed;
            if (dockpanel_re.Visibility == Visibility.Collapsed)
                dockpanel_re.Visibility = Visibility.Visible;

        }

        private void revenue_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            revenue_Button.Opacity = 1;
        }

        private void revenue_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            revenue_Button.Opacity = .5;
        }

        private void typeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = typeCombobox.SelectedIndex;
            DateTime today = DateTime.Now;
            DateTime fromTodayToBefore = DateTime.Now.AddDays(-DATE_FILTER);

            switch (selectedIndex)
            {
                case (int)Filter1.HotSaleProduct:
                    var products = (from purchaseDetail in db.PurchaseDetails
                                    join product in db.Products on purchaseDetail.ProductID equals product.ProductID
                                    orderby purchaseDetail.Quantity descending
                                    select new
                                    {
                                        product.ProductName,
                                        product.Product_Quantity,
                                        product.ProductPrice
                                    }).ToList().Take(2);

                    listReportItem_DataGrid1.ItemsSource = products;
                    dockpanel_hsp.Visibility = Visibility.Visible;
                    dockpanel_re.Visibility = Visibility.Collapsed;
                    break;

                case (int)Filter1.Revenue:
                    List<RevenueStatistic1> revenueInMonth = new List<RevenueStatistic1>();
                    int index = 0;

                    for (int month = 1; month <= 12; ++month)
                    {
                        RevenueStatistic1 revenueStatistic = new RevenueStatistic1();

                        revenueStatistic.Month = month;
                        var data = (from purchase in db.Purchases
                                    //where purchase.CreateAt >= fromTodayToBefore && purchase.CreateAt <= today
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

                    listReportItem_DataGrid1.ItemsSource = revenueInMonth;
                    dockpanel_re.Visibility = Visibility.Visible;
                    dockpanel_hsp.Visibility = Visibility.Collapsed;
                    break;
            }
        }

    }
}
