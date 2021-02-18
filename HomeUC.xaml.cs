using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Food_Recipe
{
	/// <summary>
	/// Interaction logic for HomeUC.xaml
	/// </summary>
	/// 
	public partial class HomeUC : UserControl
	{
		MS02Entities db = new MS02Entities();
		PagingInfo _pagingInfo;
		int rowsPerPage = 2;
		static public int CateIDSelected { get; set; }

		public HomeUC()
		{
			InitializeComponent();
		}

		public HomeUC(int cateID)
		{
			InitializeComponent();

			CateIDSelected = cateID;
		}
		
		void RefreshProductPage()
		{
			var products = (from product in db.Products
							where product.CateID == CateIDSelected
							select product).ToList();
			productListView.ItemsSource = products;
		}

		void CalculationPaging()
		{
			var query = (from product in db.Products
						select product).ToList();


			int count = query.Count();
			_pagingInfo = new PagingInfo()
			{
				RowsPerPage = rowsPerPage,
				CurrentPage = 1,
				TotalItems = count,
				TotalPages = count / rowsPerPage + (count % rowsPerPage == 0 ? 0 : 1)
			};

			pagingCombobox.ItemsSource = _pagingInfo.Pages;
			pagingCombobox.SelectedIndex = 0;
		}

		void UpdateProductView()
		{

			var query = (from product in db.Products
						 select product).ToList();

			var skip = (_pagingInfo.CurrentPage - 1) * _pagingInfo.RowsPerPage;
			var take = _pagingInfo.RowsPerPage;

			productListView.ItemsSource = query.Skip(skip).Take(take);
		}

		private void CategoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CalculationPaging();
			UpdateProductView();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			RefreshProductPage();
		}

		private void EditBtn_Click(object sender, RoutedEventArgs e)
		{
			var selectedItem = productListView.SelectedItem as Product;

			this.Content = new EditProductUC(selectedItem);
		}

		private void DeleteBtn_Click(object sender, RoutedEventArgs e)
		{
			var selectedProductItem = productListView.SelectedItem as Product;
			db.Products.Remove(selectedProductItem);
			db.SaveChanges();

			RefreshProductPage();
		}

		private void PreviousBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void NextBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void AddToFavorFoodBtn_Click(object sender, RoutedEventArgs e)
		{
			var selectedItem = productListView.SelectedItem as Product;
			var queryGetProductSelectInDB = (from product in db.Products
											 where product.ProductID == selectedItem.ProductID
											 select product).ToList().FirstOrDefault();

			if (queryGetProductSelectInDB.Product_isFavor == true)
			{
				MessageBox.Show($"Sản phẩm {queryGetProductSelectInDB.ProductName} đã nằm trong danh sách món ưa thích rồi", "Error",
											MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				try
				{
					queryGetProductSelectInDB.Product_isFavor = true;
					db.SaveChanges();
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Khong the them {selectedItem.ProductName} vao sản phẩm ua thich", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}

				//success
				MessageBox.Show($"{selectedItem.ProductName} da duoc them vao sản phẩm ua thich", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
				Image img = ((Button)sender).Content as Image;
				img.Source = new BitmapImage(new Uri("Image\\heart.jpg", UriKind.Relative));
			}
		}

		private void LoadBtn_Click(object sender, RoutedEventArgs e)
		{
			var queryLoadProduct = (from product in db.Products
									select product).ToList();

			productListView.ItemsSource = queryLoadProduct;
		}

		private void ReturnBtn_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new CategoryList();
		}

		private void PagingCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int nextPage = pagingCombobox.SelectedIndex + 1;
			_pagingInfo.CurrentPage = nextPage;

			UpdateProductView();
		}
	}
}

