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
    /// Interaction logic for EditCategoryUC.xaml
    /// </summary>
    public partial class EditCategoryUC : UserControl
    {
		public Category EditCategory { get; set; }
		MS02Entities db = new MS02Entities();

        public EditCategoryUC()
        {
            InitializeComponent();
        }

		public EditCategoryUC(dynamic category)
		{
			InitializeComponent();
			EditCategory = new Category()
			{
				CategoryID = category.CategoryID,
				CategoryName = category.CategoryName,
				CategoryImage = category.CategoryImage,
				CategoryDescription = category.CategoryDescription
			};
			
			CateImage.Source = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\{EditCategory.CategoryImage}", UriKind.Absolute));
			cateNameTextBox.Text = EditCategory.CategoryName;
			cateDescriptionTextBox.Text = EditCategory.CategoryDescription;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void ReturnBtn_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new CategoryList();
		}

		private void BrowseBtn_Click(object sender, RoutedEventArgs e)
		{
			var screen = new OpenFileDialog();
			
			if(screen.ShowDialog() == true)
			{
				var fileName = screen.FileName;

				var tokens = fileName.Split('\\');
				var imageFileName = tokens[tokens.Length - 1];

				EditCategory.CategoryImage = imageFileName;
				CateImage.Source = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\{imageFileName}", UriKind.Absolute));
			}
			
		}

		private void SaveBtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var CateChange = (from cate in db.Categories
								  where cate.CategoryID == EditCategory.CategoryID
								  select cate).ToList().FirstOrDefault();

				CateChange.CategoryImage = EditCategory.CategoryImage;
				CateChange.CategoryName = cateNameTextBox.Text;
				CateChange.CategoryDescription = cateDescriptionTextBox.Text;
				CateChange.Category_UpdateAt = DateTime.Now;

				db.SaveChanges();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			MessageBox.Show("Cap nhat thanh cong", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new CategoryList();
		}
	}
}
