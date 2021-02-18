using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {

        public Create()
        {
            InitializeComponent();
        }

        string CustomerTel = " ";
        PagingInfo pagingInfo;
        int rowsperpage = 5;
        BindingList<object> listProductSelected = new BindingList<object>();

        MS02Entities db = new MS02Entities();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var customers = db.Customers.ToList();
            Customers_DataGrid.ItemsSource = customers;
            var categories = db.Categories.ToList();
            Categorys_Combobox.ItemsSource = categories;
            Categorys_Combobox.SelectedIndex = 0;
            CalculatePaging();
            UpdatePaging();
        }

        private void ToolBar_Border_MouseEnter(object sender, MouseEventArgs e)
        {
            ToolBar_Border.Opacity = 1;
        }

        private void ToolBar_Border_MouseLeave(object sender, MouseEventArgs e)
        {
            ToolBar_Border.Opacity = .5;
        }

        private void Info_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Info_Button.Opacity = 1;
        }

        private void Info_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Info_Button.Opacity = .5;
        }

        private void Cart_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Cart_Button.Opacity = 1;
        }

        private void Cart_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Cart_Button.Opacity = .5;
        }

        private void Info_Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (InfoCustomer_StackPanel.Visibility == Visibility.Collapsed)
                InfoCustomer_StackPanel.Visibility = Visibility.Visible;
            if (Cart_StackPanel.Visibility == Visibility.Visible)
                Cart_StackPanel.Visibility = Visibility.Collapsed;
            if (Customer_Docpanel.Visibility == Visibility.Visible)
                Customer_Docpanel.Visibility = Visibility.Collapsed;
        }

        private void Cart_Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (InfoCustomer_StackPanel.Visibility == Visibility.Visible)
                InfoCustomer_StackPanel.Visibility = Visibility.Collapsed;
            if (Cart_StackPanel.Visibility == Visibility.Collapsed)
                Cart_StackPanel.Visibility = Visibility.Visible;
            if (Customer_Docpanel.Visibility == Visibility.Visible)
                Customer_Docpanel.Visibility = Visibility.Collapsed;
        }

        private void DocPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InfoCustomer_StackPanel.Width = DocPanel.ActualWidth;
            Cart_StackPanel.Width = DocPanel.ActualWidth;
            InfoCustomer_StackPanel.Height = DocPanel.ActualHeight;
            Cart_StackPanel.Height = DocPanel.ActualHeight;
            Customer_Docpanel.Width = DocPanel.ActualWidth;
            Customer_Docpanel.Height = DocPanel.ActualHeight;
        }

        private void DisplayListCustomers_MouseEnter(object sender, MouseEventArgs e)
        {
            DisplayListCustomers.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void DisplayListCustomers_MouseLeave(object sender, MouseEventArgs e)
        {
            DisplayListCustomers.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void DisplayListCustomers_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (InfoCustomer_StackPanel.Visibility == Visibility.Visible)
                InfoCustomer_StackPanel.Visibility = Visibility.Collapsed;
            if (Cart_StackPanel.Visibility == Visibility.Visible)
                Cart_StackPanel.Visibility = Visibility.Collapsed;
            if (Customer_Docpanel.Visibility == Visibility.Collapsed)
                Customer_Docpanel.Visibility = Visibility.Visible;
        }

        private void ResetInfo_Click(object sender, RoutedEventArgs e)
        {
            TelCustomer_Textbox.Text = "";
            NameCusstomer_Textbox.Text = "";
            Address_Textbox.Text = "";
            EmailCustomer_Textbox.Text = "";
            CustomerTel = " ";
        }

        private void Customers_DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var customer = Customers_DataGrid.SelectedItem as Customer;
            CustomerTel = customer.Customer_Tel;
            TelCustomer_Textbox.Text = customer.Customer_Tel.ToString();
            NameCusstomer_Textbox.Text = customer.Customer_Name.ToString();
            Address_Textbox.Text = customer.Customer_Address.ToString();
            EmailCustomer_Textbox.Text = customer.Customer_Email.ToString();
            if (InfoCustomer_StackPanel.Visibility == Visibility.Collapsed)
                InfoCustomer_StackPanel.Visibility = Visibility.Visible;
            if (Cart_StackPanel.Visibility == Visibility.Visible)
                Cart_StackPanel.Visibility = Visibility.Collapsed;
            if (Customer_Docpanel.Visibility == Visibility.Visible)
                Customer_Docpanel.Visibility = Visibility.Collapsed;
        }

        void CalculatePaging()
        {
            var categorie = Categorys_Combobox.SelectedItem as Category;
            if (categorie == null)
                return;
            var products = db.Categories.Find(categorie.CategoryID).Products.ToList();
            var query = from pd in products
                        select new
                        {
                            Name = pd.ProductName,
                            Price = pd.ProductPrice,
                            SKU = pd.Product_isFavor,
                            Quantity = pd.Product_Quantity,
                            Description = pd.ProductDescription,
                            DataImage = pd.ProductImage
                        };
            pagingInfo = new PagingInfo()
            {
                CurrentPage = 1,
                RowsPerPage = rowsperpage,
                TotalItems = query.Count(),
                TotalPages = query.Count() / rowsperpage + (query.Count() % rowsperpage == 0 ? 0 : 1)
            };
        }

        void UpdatePaging()
        {
            var categorie = Categorys_Combobox.SelectedItem as Category;
            if (categorie == null)
                return;
            var products = db.Categories.Find(categorie.CategoryID).Products.ToList();
            var query = from pd in products
                        select new
                        {
                            ID = pd.ProductID,
                            Name = pd.ProductName,
                            Price = pd.ProductPrice,
                            SKU = pd.Product_isFavor,
                            Quantity = pd.Product_Quantity,
                            Description = pd.ProductDescription,
                            DataImage = pd.ProductImage
                        };
            Products_DataGird.ItemsSource = query.Skip((pagingInfo.CurrentPage - 1) * pagingInfo.RowsPerPage).Take(pagingInfo.RowsPerPage);
            //Page_Textbox.Text = pagingInfo.Pages;
        }

        private void Categorys_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculatePaging();
            UpdatePaging();
        }

        //Button chuyển trang.
        private void Back_Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (pagingInfo.CurrentPage == 1)
                return;
            pagingInfo.CurrentPage--;
            UpdatePaging();
        }

        private void Next_Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (pagingInfo.CurrentPage == pagingInfo.TotalPages)
                return;
            pagingInfo.CurrentPage++;
            UpdatePaging();
        }

        private void Back_Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Back_Image.Opacity = 1;
        }

        private void Back_Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Back_Image.Opacity = .5;
        }

        private void Next_Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Next_Image.Opacity = 1;
        }

        private void Next_Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Next_Image.Opacity = .5;
        }
        int Total = 0;
        private void Products_DataGrid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            dynamic pd = Products_DataGird.SelectedItem;
            for (var i = 0; i < listProductSelected.Count; i++)
            {
                dynamic p = listProductSelected[i];
                if (p.ID == pd.ID)
                {
                    var newProduct = new
                    {
                        ID = pd.ID,
                        Name = pd.Name,
                        Price = pd.Price,
                        DataImage = pd.DataImage,
                        Quantity = p.Quantity + 1,
                        Total = pd.Price * (p.Quantity + 1)
                    };
                    listProductSelected.Remove(listProductSelected[i]);
                    listProductSelected.Insert(i, newProduct);
                    Total = 0;
                    foreach (dynamic pduct in listProductSelected)
                    {
                        Total += pduct.Total;
                    }
                    Total_Textblock.Text = String.Format("{0:#,###,###}", Total);
                    return;
                }
            }
            listProductSelected.Add(new
            {
                ID = pd.ID,
                Name = pd.Name,
                Price = pd.Price,
                DataImage = pd.DataImage,
                Quantity = 1,
                Total = pd.Price
            });
            Total = 0;
            foreach (dynamic pduct in listProductSelected)
            {
                Total += pduct.Total;
            }
            Total_Textblock.Text = String.Format("{0:#,###,###}", Total);
            ListProductSelected_Datagrid.ItemsSource = listProductSelected;
            if (listProductSelected.Count != 0)
                Order_Button.Background = Brushes.Turquoise;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChooseProduct_Dockpanel.Width = this.ActualWidth * 0.4;
        }

        private void QuantityChange_Textbox(object sender, TextChangedEventArgs e)
        {
            dynamic pd = ListProductSelected_Datagrid.SelectedItem;
            if (pd == null)
                return;
            var value = (TextBox)sender;
            int newQuantity = Int16.Parse(value.Text);
            for (var i = 0; i < listProductSelected.Count; i++)
            {
                dynamic p = listProductSelected[i];
                if (p.ID == pd.ID)
                {
                    if (newQuantity == 0)
                    {
                        listProductSelected.Remove(listProductSelected[i]);
                        return;
                    }
                    var newProduct = new
                    {
                        ID = pd.ID,
                        Name = pd.Name,
                        Price = pd.Price,
                        DataImage = pd.DataImage,
                        Quantity = newQuantity,
                        Total = pd.Price * (newQuantity)
                    };
                    listProductSelected.Remove(listProductSelected[i]);
                    listProductSelected.Insert(i, newProduct);
                    Total = 0;
                    foreach (dynamic pduct in listProductSelected)
                    {
                        Total += pduct.Total;
                    }
                    Total_Textblock.Text = String.Format("{0:#,###,###}", Total);
                    return;
                }
            }
        }

        //Hàm đặt hàng.
        //Hàm xử lý khi chết
        public delegate void DeathHandler();
        public event DeathHandler Dying;

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TelCustomer_Textbox.Text == "Nhập số điện thoại" || NameCusstomer_Textbox.Text == "Nhập họ và tên" || Address_Textbox.Text == "Nhập địa chỉ" || EmailCustomer_Textbox.Text == "Nhập email")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng trước khi order.", "Thông Báo");
                    if (InfoCustomer_StackPanel.Visibility == Visibility.Collapsed)
                        InfoCustomer_StackPanel.Visibility = Visibility.Visible;
                    if (Cart_StackPanel.Visibility == Visibility.Visible)
                        Cart_StackPanel.Visibility = Visibility.Collapsed;
                    if (Customer_Docpanel.Visibility == Visibility.Visible)
                        Customer_Docpanel.Visibility = Visibility.Collapsed;
                    return;
                }
                if (listProductSelected.Count == 0)
                {
                    MessageBox.Show("Giỏ hàng đang rỗng.", "Thông Báo");
                    if (InfoCustomer_StackPanel.Visibility == Visibility.Visible)
                        InfoCustomer_StackPanel.Visibility = Visibility.Collapsed;
                    if (Cart_StackPanel.Visibility == Visibility.Collapsed)
                        Cart_StackPanel.Visibility = Visibility.Visible;
                    if (Customer_Docpanel.Visibility == Visibility.Visible)
                        Customer_Docpanel.Visibility = Visibility.Collapsed;
                    return;
                }
                var result = MessageBox.Show("Bạn có đồng ý đặt hàng đơn hàng này.", "Xác nhận đơn hàng", MessageBoxButton.OKCancel);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        //Nếu customer là mới thì thêm mới vào db
                        if (CustomerTel == " " )
                        {
                            var customers = db.Customers.ToList();
                            var newcustomer = new Customer()
                            {
                                Customer_Name = NameCusstomer_Textbox.Text,
                                Customer_Tel = TelCustomer_Textbox.Text,
                                Customer_Address = Address_Textbox.Text,
                                Customer_Email = EmailCustomer_Textbox.Text
                            };
                            db.Customers.Add(newcustomer);
                            db.SaveChanges();
                            customers = db.Customers.ToList();
                            foreach (var ctm in customers)
                            {
                                if (ctm.Customer_Tel == TelCustomer_Textbox.Text)
                                    CustomerTel = ctm.Customer_Tel;
                            }
                        }
                        //Thêm mới purchase
                        var DateTime_Now = DateTime.Now;
                        var newpurchases = new Purchase()
                        {
                            CreateAt = DateTime_Now,
                            Status = 1,
                            Customer_Tel = CustomerTel,
                            Total = Total
                        };
                        db.Purchases.Add(newpurchases);
                        db.SaveChanges();
                        var idNewpurchase = 0;
                        foreach (var purchase in db.Purchases.ToList())
                        {
                            if (purchase.CreateAt == DateTime_Now)
                                idNewpurchase = purchase.PurchaseID;
                        }
                        //thêm từng purchasedetail vào db
                        foreach (dynamic product in listProductSelected)
                        {
                            var purchaseDetail = new PurchaseDetail()
                            {
                                PurchaseID = idNewpurchase,
                                ProductID = product.ID,
                                Price = product.Price,
                                Quantity = product.Quantity,
                                Total = product.Total
                            };
                            db.PurchaseDetails.Add(purchaseDetail);
                        }
                        db.SaveChanges();

                        MessageBox.Show("Bạn Đã Order Thành Công", "Thông Báo");

                        if (Dying != null)
                        {
                            Dying.Invoke();
                        }
                        this.Close();
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                        //  case MessageBoxResult.OK:
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (Dying != null)
            {
                Dying.Invoke();
            }
            this.Close();
        }

        private void TelCustomer_Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TelCustomer_Textbox.Text == "Nhập số điện thoại..")
                TelCustomer_Textbox.Text = "";
        }

        private void TelCustomer_Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TelCustomer_Textbox.Text == "")
                TelCustomer_Textbox.Text = "Nhập số điện thoại..";
        }

        private void NameCusstomer_Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameCusstomer_Textbox.Text == "Nhập họ và tên..")
                NameCusstomer_Textbox.Text = "";
        }

        private void NameCusstomer_Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameCusstomer_Textbox.Text == "")
                NameCusstomer_Textbox.Text = "Nhập họ và tên..";
        }

        private void Address_Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Address_Textbox.Text == "Nhập địa chỉ..")
                Address_Textbox.Text = "";
        }

        private void Address_Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Address_Textbox.Text == "")
                Address_Textbox.Text = "Nhập địa chỉ..";
        }

        private void EmailCustomer_Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailCustomer_Textbox.Text == "Nhập email..")
                EmailCustomer_Textbox.Text = "";
        }

        private void EmailCustomer_Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EmailCustomer_Textbox.Text == "")
                EmailCustomer_Textbox.Text = "Nhập email..";
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            dynamic pd = ListProductSelected_Datagrid.SelectedItem;
            if (pd == null)
                return;
            for (var i = 0; i < listProductSelected.Count; i++)
            {
                dynamic p = listProductSelected[i];
                if (p.ID == pd.ID)
                {
                    listProductSelected.Remove(listProductSelected[i]);
                }
            }
        }
        private void Products_DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
