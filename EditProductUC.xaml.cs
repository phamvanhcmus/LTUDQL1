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
	/// Interaction logic for EditProductUC.xaml
	/// </summary>
	public partial class EditProductUC : UserControl
	{
		MS02Entities db = new MS02Entities();
		public Product EditProduct { get; set; }

		public EditProductUC()
		{
			InitializeComponent();
		}

		public EditProductUC(Product product)
		{
			InitializeComponent();
			EditProduct = new Product()
			{
				ProductID = product.ProductID,
				CateID = product.CateID,
				ProductName = product.ProductName,
				ProductImage = product.ProductImage,
				ProductDescription = product.ProductDescription,
				Product_isFavor = product.Product_isFavor,
				ProductPrice = product.ProductPrice,
				Product_Quantity = product.Product_Quantity
			};

			productImage.Source = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}Image\\{EditProduct.ProductImage}",
																UriKind.Absolute));
			productNameTextBox.Text = EditProduct.ProductName;
			productDescriptionTextBox.Text = EditProduct.ProductDescription;
			productPriceTextBox.Text = EditProduct.ProductPrice.ToString();
			productQuantityTextBox.Text = EditProduct.Product_Quantity.ToString();
		}
		
		private void ReturnBtn_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new HomeUC();
		}

		private void SaveBtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//var productFind = db.Products.Find(EditProduct.ProductID);
				var productFind = (from product in db.Products
								   where product.ProductID == EditProduct.ProductID
								   select product).ToList().FirstOrDefault();

				productFind.ProductName = productNameTextBox.Text;
				productFind.ProductDescription = productDescriptionTextBox.Text;
				productFind.ProductImage = EditProduct.ProductImage;
				productFind.ProductPrice = int.Parse(productPriceTextBox.Text);
				productFind.Product_Quantity = int.Parse(productQuantityTextBox.Text);
				productFind.Product_updateAt = DateTime.Now;

				db.SaveChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			MessageBox.Show("Cập nhật sản phẩm thành công", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new HomeUC(HomeUC.CateIDSelected);
		}

		private void BrowseBtn_Click(object sender, RoutedEventArgs e)
		{
			var screen = new OpenFileDialog();

			if (screen.ShowDialog() == true)
			{
				var fileName = screen.FileName;

				var tokens = fileName.Split('\\');

				var imageFileName = tokens[tokens.Length - 1];

				productImage.Source = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\{imageFileName}", UriKind.Absolute));
				EditProduct.ProductImage = imageFileName;
				MessageBox.Show("Change image successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}
