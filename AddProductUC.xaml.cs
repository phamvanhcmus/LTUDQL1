using Microsoft.Win32;
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
    /// Interaction logic for AddProductUC.xaml
    /// </summary>
	/// 

	
	
    public partial class AddProductUC : UserControl
    {
		MS02Entities db = new MS02Entities();
		public Product ProductAdd { get; set; }
        public AddProductUC()
        {
            InitializeComponent();
        }

		public AddProductUC(int cateID)
		{
			InitializeComponent();
			ProductAdd = new Product()
			{ 
				CateID = cateID
			};
		}

		bool checkValidAllField()
		{
			if (productImage == null || productNameTextBox.Text.isEmpty() ||
					productPriceTextBox.Text.isEmpty() || productQuantityTextBox.Text.isEmpty()
					|| productDescriptionTextBox.Text.isEmpty())
				return false;
			return true;
		}

		private void AddBtn_Click(object sender, RoutedEventArgs e)
		{
			if(checkValidAllField())
			{
				try
				{
					//assign field to property add to database
					ProductAdd.ProductName = productNameTextBox.Text;
					ProductAdd.ProductDescription = productDescriptionTextBox.Text;
					ProductAdd.Product_Quantity = int.Parse(productQuantityTextBox.Text);
					ProductAdd.ProductPrice = int.Parse(productPriceTextBox.Text);
					ProductAdd.Product_createAt = DateTime.Now;
					ProductAdd.Product_updateAt = DateTime.Now;

					db.Products.Add(ProductAdd);
					db.SaveChanges();
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}

				MessageBox.Show("Them sản phẩm thanh cong", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void BrowseBtn_Click(object sender, RoutedEventArgs e)
		{
			var screen = new OpenFileDialog();
			
			if(screen.ShowDialog() == true)
			{
				var fileName = screen.FileName;

				var tokens = fileName.Split('\\');
				var imageFileDir = tokens[tokens.Length - 1];

				productImage.Source = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}Image\\{imageFileDir}", 
												UriKind.Absolute));

				ProductAdd.ProductImage = imageFileDir;

				//move out position of browse button.
				browseBtn.Margin = new Thickness(349,80,31,519);
				browseBtn.Content = "Edit";
				browseBtn.Width = 120;
				browseBtn.Height = 40;
				
			}
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new CategoryList();
		}

		private void ReturnBtn_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new CategoryList();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			var queryCate = (from cate in db.Categories
							where cate.CategoryID == ProductAdd.CateID
							select cate).ToList().FirstOrDefault();

			categoryTextBox.Text = queryCate.CategoryName;
		}
	}
}
