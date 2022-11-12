using System;
using System.IO;
using System.Windows.Forms;

namespace TestProductsFormsApp
{
    public partial class ProductListForm : Form
    {
        int selectedInd;

        public ProductListForm()
        {
            InitializeComponent();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            var filename = Directory.GetCurrentDirectory() + @"\" + "test.xlsx";
            if (!File.Exists(filename))
            {
                MessageBox.Show(String.Format("Файл {0} не найден", filename));
                return;
            }

            var export = new ExcelExport(filename);
            var expProds = export.GetProducts();

            var repository = new ProductsRepository();
            repository.AddProducts(expProds);

            UpdateGrid();
        }

        private void ProductListForm_Load(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1[0, selectedInd].Value);

            dataGridView1.Rows.RemoveAt(selectedInd);

            var repository = new ProductsRepository();
            repository.RemoveProductById(id);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1[0, selectedInd].Value);

            var repository = new ProductsRepository();
            var edProduct = repository.GetProductById(id);

            ProductForm productForm = new ProductForm(edProduct, UpdateGrid);            
            productForm.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                selectedInd = dataGridView1.SelectedCells[0].RowIndex;
            }
            catch
            {
                selectedInd = 0;
            }
        }

        private void UpdateGrid()
        {
            this.productsTableAdapter.Fill(this.productsDbDataSet.Products);
        }
    }
}
