using Aspose.Cells;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
	/// Interaction logic for CategoryList.xaml
	/// </summary>
	public partial class CategoryList : UserControl
	{
		MS02Entities db = new MS02Entities();
		public int TotalProductInCate { get; set; }
		public CategoryList()
		{
			InitializeComponent();
		}

		private void ImportExcelBtn_Click(object sender, RoutedEventArgs e)
		{
			var screen = new OpenFileDialog();
			bool theFirstPage = true;
			if (screen.ShowDialog() == true)
			{
				var fileName = screen.FileName;
				var sheets = new Workbook(fileName);
				var tabs = sheets.Worksheets;

				foreach (var tab in tabs)
				{
					var row = 3;
					//The first page is product list
					if (theFirstPage == true)
					{

						var cell = tab.Cells[$"C3"];

						while (cell.Value != null || cell.StringValue.IsNotEmpty())
						{
							var _name = tab.Cells[$"C{row}"].StringValue;
							var _image = tab.Cells[$"D{row}"].StringValue;
							var _description = tab.Cells[$"E{row}"].StringValue;
							var _createAt = tab.Cells[$"F{row}"].DateTimeValue;
							var _updateAt = tab.Cells[$"G{row}"].DateTimeValue;

							var category = new Category()
							{
								CategoryName = _name,
								CategoryImage = _image,
								CategoryDescription = _description,
								Category_CreateAt = _createAt,
								Category_UpdateAt = _updateAt
							};

							db.Categories.Add(category);
							db.SaveChanges();

							row++;
							cell = tab.Cells[$"C{row}"];
						}

						theFirstPage = false;       //mark that all read the category list.
					}

					//Read Product
					else
					{
						try
						{
							//get category with same name with the tab name.
							var category = (from cate in db.Categories
											where cate.CategoryName == tab.Name
											select cate).ToList().FirstOrDefault();     //get first result.

							var cell = tab.Cells[$"C3"];

							while (cell.Value != null || cell.StringValue.IsNotEmpty())
							{
								var _name = tab.Cells[$"C{row}"].StringValue;
								var _image = tab.Cells[$"D{row}"].StringValue;
								var _description = tab.Cells[$"E{row}"].StringValue;
								var _price = tab.Cells[$"F{row}"].IntValue;
								var _quantity = tab.Cells[$"G{row}"].IntValue;
								var _isFavor = tab.Cells[$"H{row}"].BoolValue;
								var _createAt = tab.Cells[$"I{row}"].DateTimeValue;
								var _updateAt = tab.Cells[$"J{row}"].DateTimeValue;

								var product = new Product()
								{
									CateID = (int)category.CategoryID,
									ProductName = _name,
									ProductImage = _image,
									ProductPrice = _price,
									Product_Quantity = _quantity,
									Product_isFavor = _isFavor,
									ProductDescription = _description,
									Product_createAt = _createAt,
									Product_updateAt = _updateAt
								};

								category.Products.Add(product);
								db.SaveChanges();

								row++;
								cell = tab.Cells[$"C{row}"];
							}
						}
						catch (DbEntityValidationException ex)
						{
							foreach (var eve in ex.EntityValidationErrors)
							{
								MessageBox.Show($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
								foreach (var ve in eve.ValidationErrors)
								{
									MessageBox.Show($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}");
								}
							}
							throw;
						}
					}
				}
				MessageBox.Show("Import Sucessfully", "Congratulation");
			}
		}

		private void LoadExcelBtn_Click(object sender, RoutedEventArgs e)
		{
			dynamic queryLoadCate = (from category in db.Categories
									 select new
									 {
										 category.CategoryID,
										 category.CategoryName,
										 category.CategoryImage,
										 category.CategoryDescription,
										 TotalProductInCate = (from product in db.Products
															   where product.CateID == category.CategoryID
															   select product).ToList().Count
									 }).ToList();
			var queryLoadAllProduct = (from product in db.Products
									   select product).ToList();
			var queryLoadProduct = (from product in db.Products
									where product.Product_isFavor == true
									select product).ToList();



			categoryListView.ItemsSource = queryLoadCate;
			favorProductListView.ItemsSource = queryLoadProduct;
		}

		private void CategoryItemBtn_Click(object sender, RoutedEventArgs e)
		{
			dynamic selectedCateItem = categoryListView.SelectedItem;
			int selectedCateID = selectedCateItem.CategoryID;

			this.Content = new HomeUC(selectedCateID);

		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			RefreshPage();
		}

		void RefreshPage()
		{
			dynamic queryLoadCate = (from category in db.Categories
									 select new
									 {
										 category.CategoryID,
										 category.CategoryName,
										 category.CategoryImage,
										 category.CategoryDescription,
										 TotalProductInCate = (from product in db.Products
															   where product.CateID == category.CategoryID
															   select product).ToList().Count
									 }).ToList();
			var queryLoadAllProduct = (from product in db.Products
									   select product).ToList();
			var queryLoadProduct = (from product in db.Products
									where product.Product_isFavor == true
									select product).ToList();



			categoryListView.ItemsSource = queryLoadCate;
			favorProductListView.ItemsSource = queryLoadProduct;
		}

		private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void CategoryItemBtn_Click_1(object sender, RoutedEventArgs e)
		{

		}

		private void EditBtn_Click(object sender, RoutedEventArgs e)
		{
			dynamic selectedItem = categoryListView.SelectedItem;
			this.Content = new EditCategoryUC(selectedItem);
		}

		private void DeleteBtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				dynamic selectedItem = categoryListView.SelectedItem;           //get selected item and set it to anonymous type because
																				// we have modify category and binding in listview and
																				//again we select it -> now it not type Category anymore
																				//because it have custom field TotalProductInCate.

				var selectedSpecifyCategory = new Category()                    //clone the category with input data is anonymous type and
																				//explicit cast it to original Category type.
				{
					CategoryID = selectedItem.CategoryID,
					CategoryName = selectedItem.CategoryName,
					CategoryImage = selectedItem.CategoryImage,
					CategoryDescription = selectedItem.CategoryDescription
				};

				var cateFind = db.Categories.Find(selectedSpecifyCategory.CategoryID);          //return category correspond with id.
				db.Categories.Remove(cateFind);
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			MessageBox.Show("Xoa thanh cong", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
			RefreshPage();
		}

		private void AddProductBtn_Click(object sender, RoutedEventArgs e)
		{
			dynamic selectedCateItem = categoryListView.SelectedItem;
			int selectedCateID = selectedCateItem.CategoryID;

			this.Content = new AddProductUC(selectedCateID);
		}
	}
}
