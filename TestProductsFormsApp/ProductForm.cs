using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProductsFormsApp
{
    public partial class ProductForm : Form
    {
        Product product;

        Action updDataGrid;

        public ProductForm()
        {
            InitializeComponent();
        }

        public ProductForm(Product edProduct, Action callback)
        {
            InitializeComponent();
            product = edProduct;
            updDataGrid = callback;
            tbArticle.Text = product.Article;
            tbName.Text = product.Name;
            tbPrice.Text = product.Price.ToString();
            tbQuantity.Text = product.Quantity.ToString();
            var filename = Directory.GetCurrentDirectory() + @"\pic\" + product.Article + ".jpg";
            if (File.Exists(filename))
            {
                pcbPicture.ImageLocation = filename;
            }
            pcbPicture.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            product.Article = tbArticle.Text;
            product.Name = tbName.Text;
            product.Price = Convert.ToDecimal(tbPrice.Text.ToString());
            product.Quantity = Convert.ToInt32(tbQuantity.Text.ToString());
            var repository = new ProductsRepository();
            repository.UpdateProduct(product);
            updDataGrid();
            this.Close();
        }
    }
}
