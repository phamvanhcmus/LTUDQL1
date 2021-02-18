using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        //MS02Entities db = new MS02Entities();
        public Order()
        {
            InitializeComponent();
        }

        PagingInfo pagingInfoTransaction;
        int rowsPerPage = 5;

        class InfoPurchase
        {
            public string Name { get; set; }
            public int Purchase_ID { get; set; }
            public string Customer_Tel { get; set; }
            public string Tel { get; set; }
            public int Total { get; set; }
            public DateTime? Create_At { get; set; }
            public string Status { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
        }
        MS02Entities db = new MS02Entities(); // Tạo kết nối tới CSDL

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadAllPurchases();
            var allpurchaseStatus = db.PurchaseStateEnums.ToList();
            purchaseDG.Visibility = Visibility.Visible;
            filterDG.Visibility = Visibility.Collapsed;
            purchaseStatesComboBox.ItemsSource = allpurchaseStatus;

        }

        void loadAllPurchases()
        {
            var query = db.Purchases.GroupJoin(db.Customers,
               p => p.Customer_Tel,
               c => c.Customer_Tel,
               (x, y) => new { Purchases = x, Customers = y }
               )
               .SelectMany(
                    x => x.Customers.DefaultIfEmpty(),  
                    (x, y) => new { Purchase = x.Purchases, Customer = y }
                )
                .Select(item => new {
                    item.Purchase.PurchaseID,
                    item.Purchase.Total,
                    item.Purchase.CreateAt,
                    item.Purchase.Status,
                    item.Customer.Customer_Address,
                    item.Customer.Customer_Email,
                    item.Customer.Customer_Name,
                    item.Customer.Customer_Tel
                });
            purchaseDG.Visibility = Visibility.Visible;
            filterDG.Visibility = Visibility.Collapsed;
            purchaseDataGrid.ItemsSource = query.ToList();
        }

        private void addPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Create();
            if (screen.ShowDialog() == true)
            {
                loadAllPurchases();
            }
        }

        void CalculatePagingInfoTransaction()
        {
            var db = new MS02Entities();
            var purchases = db.Purchases.ToList();
            var ListInfoPruchase = new List<InfoPurchase>();
            var StartDate = fromDatePicker.SelectedDate;
            var EndDate = toDatePicker.SelectedDate;
            var Status = purchaseStatesComboBox.SelectedItem;
            if (Status == null && (EndDate == null || StartDate == null))
            {
                foreach (var purchase in purchases)
                {
                    var infoPurchase = new InfoPurchase()
                    {
                        Name = purchase.Customer.Customer_Name,
                        Customer_Tel = purchase.Customer_Tel,
                        Tel = purchase.Customer_Tel,
                        Purchase_ID = purchase.PurchaseID,
                        Create_At = purchase.CreateAt,
                        Total = purchase.Total.Value,
                        Status = db.PurchaseStateEnums.Find(purchase.Status).EnumKey,
                        Email = purchase.Customer.Customer_Email,
                        Address = purchase.Customer.Customer_Address
                    };
                    ListInfoPruchase.Add(infoPurchase);
                }
            }
            else
            {
                if (Status != null && EndDate != null && StartDate != null)
                {
                    if (EndDate == null || StartDate == null)
                        return;
                    if (StartDate.Value.CompareTo(EndDate) == 1)
                    {
                        MessageBox.Show("Ngày tìm kiếm sai quy định.Vui lòng chọn lại.");
                        return;
                    }
                    foreach (var purchase in purchases)
                    {
                        if (purchase.CreateAt.Value.CompareTo(StartDate) >= 0 && purchase.CreateAt.Value.CompareTo(EndDate) <= 0)
                        {
                            var status = purchaseStatesComboBox.SelectedItem as PurchaseStateEnum;
                            if (status.Value == -1 || purchase.Status == status.Value || purchaseStatesComboBox.SelectedIndex < 0)
                            {
                                var infoPurchase = new InfoPurchase()
                                {
                                    Name = purchase.Customer.Customer_Name,
                                    Customer_Tel = purchase.Customer_Tel,
                                    Tel = purchase.Customer.Customer_Tel,
                                    Create_At = purchase.CreateAt,
                                    Total = purchase.Total.Value,
                                    Purchase_ID = purchase.PurchaseID,
                                    Status = db.PurchaseStateEnums.Find(purchase.Status).EnumKey,
                                    Email = purchase.Customer.Customer_Email,
                                    Address = purchase.Customer.Customer_Address
                                };
                                ListInfoPruchase.Add(infoPurchase);
                            }
                        }
                    }
                }
                else
                {
                    if (Status == null && (EndDate != null && StartDate != null))
                    {
                        if (EndDate == null || StartDate == null)
                            return;
                        if (StartDate.Value.CompareTo(EndDate) == 1)
                        {
                            MessageBox.Show("Ngày tìm kiếm sai quy định.Vui lòng chọn lại.");
                            return;
                        }
                        foreach (var purchase in purchases)
                        {
                            if (purchase.CreateAt.Value.CompareTo(StartDate) >= 0 && purchase.CreateAt.Value.CompareTo(EndDate) <= 0)
                            {
                                var infoPurchase = new InfoPurchase()
                                {
                                    Name = purchase.Customer.Customer_Name,
                                    Customer_Tel = purchase.Customer_Tel,
                                    Tel = purchase.Customer.Customer_Tel,
                                    Create_At = purchase.CreateAt,
                                    Total = purchase.Total.Value,
                                    Purchase_ID = purchase.PurchaseID,
                                    Status = db.PurchaseStateEnums.Find(purchase.Status).EnumKey,
                                    Email = purchase.Customer.Customer_Email,
                                    Address = purchase.Customer.Customer_Address
                                };
                                ListInfoPruchase.Add(infoPurchase);
                            }
                        }

                    }
                    else
                    {
                        if (Status != null && (EndDate == null || StartDate == null))
                        {
                            foreach (var purchase in purchases)
                            {
                                var status = purchaseStatesComboBox.SelectedItem as PurchaseStateEnum;
                                if (status.Value == -1 || purchase.Status == status.Value || purchaseStatesComboBox.SelectedIndex < 0)
                                {
                                    var infoPurchase = new InfoPurchase()
                                    {
                                        Name = purchase.Customer.Customer_Name,
                                        Customer_Tel = purchase.Customer_Tel,
                                        Tel = purchase.Customer.Customer_Tel,
                                        Create_At = purchase.CreateAt,
                                        Total = purchase.Total.Value,
                                        Purchase_ID = purchase.PurchaseID,
                                        Status = db.PurchaseStateEnums.Find(purchase.Status).EnumKey,
                                        Email = purchase.Customer.Customer_Email,
                                        Address = purchase.Customer.Customer_Address
                                    };
                                    ListInfoPruchase.Add(infoPurchase);
                                }
                            }
                        }
                    }
                }
            }

            var count = ListInfoPruchase.Count;
            pagingInfoTransaction = new PagingInfo()
            {
                RowsPerPage = rowsPerPage,
                CurrentPage = 1,
                TotalItems = count,
                TotalPages = count / rowsPerPage + (count % rowsPerPage == 0 ? 0 : 1)
            };
            //InfoPage_TextBlock.Text = pagingInfoTransaction.Pages;
        }


        void UpdateTransactionView()
        {
            var db = new MS02Entities();
            var purchases = db.Purchases.ToList();
            var ListInfoPruchase = new List<InfoPurchase>();
            var StartDate = fromDatePicker.SelectedDate;
            var EndDate = toDatePicker.SelectedDate;
            var Status = purchaseStatesComboBox.SelectedItem;
            if (Status == null && (EndDate == null || StartDate == null))
            {
                foreach (var purchase in purchases)
                {
                    var infoPurchase = new InfoPurchase()
                    {
                        Name = purchase.Customer.Customer_Name,
                        Customer_Tel = purchase.Customer_Tel,
                        Tel = purchase.Customer.Customer_Tel,
                        Create_At = purchase.CreateAt,
                        Purchase_ID = purchase.PurchaseID,
                        Total = purchase.Total.Value,
                        Status = db.PurchaseStateEnums.Find(purchase.Status).EnumKey,
                        Email = purchase.Customer.Customer_Email,
                        Address = purchase.Customer.Customer_Address
                    };
                    ListInfoPruchase.Add(infoPurchase);
                }
            }
            else
            {
                if (Status != null && EndDate != null && StartDate != null)
                {
                    if (EndDate == null || StartDate == null)
                        return;
                    if (StartDate.Value.CompareTo(EndDate) == 1)
                    {
                        MessageBox.Show("Ngày tìm kiếm sai quy định.Vui lòng chọn lại.");
                        return;
                    }
                    foreach (var purchase in purchases)
                    {
                        if (purchase.CreateAt.Value.CompareTo(StartDate) >= 0 && purchase.CreateAt.Value.CompareTo(EndDate) <= 0)
                        {
                            var status = purchaseStatesComboBox.SelectedItem as PurchaseStateEnum;
                            if (status.Value == -1 || purchase.Status == status.Value || purchaseStatesComboBox.SelectedIndex < 0)
                            {
                                var infoPurchase = new InfoPurchase()
                                {
                                    Name = purchase.Customer.Customer_Name,
                                    Customer_Tel = purchase.Customer_Tel,
                                    Tel = purchase.Customer.Customer_Tel,
                                    Create_At = purchase.CreateAt,
                                    Purchase_ID = purchase.PurchaseID,
                                    Total = purchase.Total.Value,
                                    Status = db.PurchaseStateEnums.Find(purchase.Status).EnumKey,
                                    Email = purchase.Customer.Customer_Email,
                                    Address = purchase.Customer.Customer_Address
                                };
                                ListInfoPruchase.Add(infoPurchase);
                            }
                        }
                    }
                }
                else
                {
                    if (Status == null && (EndDate != null && StartDate != null))
                    {
                        if (EndDate == null || StartDate == null)
                            return;
                        if (StartDate.Value.CompareTo(EndDate) == 1)
                        {
                            MessageBox.Show("Ngày tìm kiếm sai quy định.Vui lòng chọn lại.");
                            return;
                        }
                        foreach (var purchase in purchases)
                        {
                            if (purchase.CreateAt.Value.CompareTo(StartDate) >= 0 && purchase.CreateAt.Value.CompareTo(EndDate) <= 0)
                            {
                                var infoPurchase = new InfoPurchase()
                                {
                                    Name = purchase.Customer.Customer_Name,
                                    Customer_Tel = purchase.Customer_Tel,
                                    Tel = purchase.Customer.Customer_Tel,
                                    Purchase_ID = purchase.PurchaseID,
                                    Create_At = purchase.CreateAt,
                                    Total = purchase.Total.Value,
                                    Status = db.PurchaseStateEnums.Find(purchase.Status).EnumKey,
                                    Email = purchase.Customer.Customer_Email,
                                    Address = purchase.Customer.Customer_Address
                                };
                                ListInfoPruchase.Add(infoPurchase);
                            }
                        }

                    }
                    else
                    {
                        if (Status != null && (EndDate == null || StartDate == null))
                        {
                            foreach (var purchase in purchases)
                            {
                                var status = purchaseStatesComboBox.SelectedItem as PurchaseStateEnum;
                                if (status.Value == -1 || purchase.Status == status.Value || purchaseStatesComboBox.SelectedIndex < 0)
                                {
                                    var infoPurchase = new InfoPurchase()
                                    {
                                        Name = purchase.Customer.Customer_Name,
                                        Customer_Tel = purchase.Customer_Tel,
                                        Tel = purchase.Customer.Customer_Tel,
                                        Create_At = purchase.CreateAt,
                                        Purchase_ID = purchase.PurchaseID,
                                        Total = purchase.Total.Value,
                                        Status = db.PurchaseStateEnums.Find(purchase.Status).EnumKey,
                                        Email = purchase.Customer.Customer_Email,
                                        Address = purchase.Customer.Customer_Address
                                    };
                                    ListInfoPruchase.Add(infoPurchase);
                                }
                            }
                        }
                    }
                }
            }
            var skip = (pagingInfoTransaction.CurrentPage - 1) * pagingInfoTransaction.RowsPerPage;
            var take = pagingInfoTransaction.RowsPerPage;
            //InfoPage_TextBlock.Text = pagingInfoTransaction.Pages;
            purchaseDG.Visibility = Visibility.Collapsed;
            filterDG.Visibility = Visibility.Visible;
            filterDataGrid.ItemsSource = ListInfoPruchase.Skip(skip).Take(take);

            TotalTransaction_TextBlock.Text = $"Số Đơn Hàng Tìm Thấy :{ListInfoPruchase.Count}";
        }
        private void UpdateTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (NameCustomerPurchase_Textbox.Text == "" || TelCustomerPurchase_Textbox.Text == "" || TotalTransaction_TextBlock.Text == "" || CreateAtPurchase_DatePricker.SelectedDate == null || StatusPurchase_Combobox.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng cập nhập đầu đủ các thông tin của đơn hàng trước khi cập nhập.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.None);
                return;
            }
            var Infopurchase = filterDataGrid.SelectedItem as InfoPurchase;
            var status = StatusPurchase_Combobox.SelectedItem as PurchaseStateEnum;
            if (Infopurchase == null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần cập nhập.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.None);
                return;
            }
            RunLoading();
            var db = new MS02Entities();
            var newTransaction = db.Purchases.Find(Infopurchase.Purchase_ID);
            //Cập Nhập lại Thông Tin Sản Phẩm
            newTransaction.CreateAt = CreateAtPurchase_DatePricker.SelectedDate;
            newTransaction.Status = status.Value;
            db.SaveChanges();
            //Cập nhập lại thông tin khách hàng.
            var newCustomer = db.Customers.Find(Infopurchase.Customer_Tel);
            newCustomer.Customer_Name = NameCustomerPurchase_Textbox.Text;
            db.SaveChanges();
            MessageBox.Show("Bạn đã cập nhập thành công", "Thông Báo");
            ResetInfomationTransaction();
            UpdateTransactionView();
            StopLoading();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var infopurchase = filterDataGrid.SelectedItem as InfoPurchase;
                if (infopurchase == null)
                {
                    MessageBox.Show("Vui lòng chọn đơn hàng cần xoá.", "Thông Báo");
                    return;
                }
                var result = MessageBox.Show($"Bạn có muốn xoá đơn hàng của {infopurchase.Name}", "Thông Báo", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                    return;
                else
                {
                    var db = new MS02Entities();
                    var pr = db.Purchases.Find(infopurchase.Purchase_ID);
                    db.Purchases.Remove(pr);
                    db.SaveChanges();
                    MessageBox.Show("Bạn đã xoá thành công.", "Thông Báo");
                    CalculatePagingInfoTransaction();
                    UpdateTransactionView();
                    ResetInfomationTransaction();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }
        private void KeyworkTel_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void keyworkTeltextbox_gotfocus(object sender, RoutedEventArgs e)
        {
            Keywork_TextBlock.Visibility = Visibility.Collapsed;
        }

        private void keyworkTeltextbox_Lostfocus(object sender, RoutedEventArgs e)
        {
            if (Keywork_textbox.Text == "")
            {
                Keywork_TextBlock.Visibility = Visibility.Visible;
            }
        }

        private void BackPage_LeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RunLoading();
            try
            {
                if (pagingInfoTransaction.CurrentPage <= 1)
                {
                    StopLoading();
                    return;
                }
                pagingInfoTransaction.CurrentPage--;
                UpdateTransactionView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã có lỗi ở BackPage {ex}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            StopLoading();
        }

        private void NextPage_LeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RunLoading();
            try
            {
                if (pagingInfoTransaction.CurrentPage >= pagingInfoTransaction.TotalPages)
                {
                    StopLoading();
                    return;
                }
                pagingInfoTransaction.CurrentPage++;
                UpdateTransactionView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã có lỗi ở NextPage :{ex}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            StopLoading();
        }
        void ResetInfomationTransaction()
        {
            NameCustomerPurchase_Textbox.Text = "";
            TelCustomerPurchase_Textbox.Text = "";
            TotalPurchase_Textbox.Text = "";
            CreateAtPurchase_DatePricker.SelectedDate = null;
            StatusPurchase_Combobox.ItemsSource = "";
            TotalPriceTransaction_TextBlock.Text = "";
            ListProductDaMua_DataGrid.ItemsSource = "";
        }
        private void RunLoading()
        {

            if (LoadingProgressBarTransaction.IsIndeterminate == false)
                LoadingProgressBarTransaction.IsIndeterminate = true;
        }
        private void StopLoading()
        {
            if (LoadingProgressBarTransaction.IsIndeterminate == true)
                LoadingProgressBarTransaction.IsIndeterminate = false;
        }
   
        private void StatusTransaction_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RunLoading();
            try
            {
                CalculatePagingInfoTransaction();
                UpdateTransactionView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã có lỗi ở StatusTransaction Changed :{ex}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            StopLoading();
        }


        private void transaction_DataGird_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            RunLoading();
            try
            {
                var Infopurchase = filterDataGrid.SelectedItem as InfoPurchase;
                if (Infopurchase == null)
                {
                    StopLoading();
                    return;
                }
                var db = new MS02Entities();
                var purchaseDetails = db.Purchases.Find(Infopurchase.Purchase_ID).PurchaseDetails.ToList();
                NameCustomerPurchase_Textbox.Text = Infopurchase.Name;
                TelCustomerPurchase_Textbox.Text = Infopurchase.Tel;
                TotalPurchase_Textbox.Text = String.Format("{0:#,###,###}", Infopurchase.Total);
                CreateAtPurchase_DatePricker.SelectedDate = Infopurchase.Create_At;
                var Status = db.PurchaseStateEnums.ToList();
                StatusPurchase_Combobox.ItemsSource = Status;
                for (int i = 0; i < Status.Count; i++)
                {
                    if (Status[i].EnumKey == Infopurchase.Status)
                        StatusPurchase_Combobox.SelectedIndex = i;
                }
                var querys = from detail in purchaseDetails
                             select new
                             {
                                 Name = db.Products.Find(detail.ProductID).ProductName,
                                 Price = detail.Price,
                                 Quantity = detail.Quantity,
                                 Total = detail.Total
                             };
                ListProductDaMua_DataGrid.ItemsSource = querys;
                //InfomationTransaction_DockPanel.Visibility = Visibility.Visible;
                int TongTien = 0;
                foreach (var qr in purchaseDetails)
                    TongTien += qr.Total.Value;
                TotalPriceTransaction_TextBlock.Text = String.Format("{0:#,###,###}", TongTien);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
            StopLoading();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            loadAllPurchases();
        }
    }
}
