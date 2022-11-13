using System;
using System.IO;
using System.Windows.Forms;

namespace TestProductsFormsApp
{
    public partial class ProductListForm : Form
    {
        private int selRow = -1;

        private int selCol = -1;

        private int prevSelRow = -1;

        private int prevSelCol = -1;

        private ProductsRepository repository = new ProductsRepository();

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

            repository.AddProducts(expProds);

            prevSelRow = selRow;
            prevSelCol = selCol;
            UpdateGrid();
        }

        private void ProductListForm_Load(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1[0, selRow].Value);
            repository.RemoveProductById(id);
            
            dataGridView1.Rows.RemoveAt(selRow);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1[0, selRow].Value);

            var edProduct = repository.GetProductById(id);

            prevSelRow = selRow;
            prevSelCol = selCol;

            ProductForm productForm = new ProductForm(edProduct, UpdateGrid);            
            productForm.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = btnEdit.Enabled = true;

            try
            {
                selRow = dataGridView1.CurrentCell.RowIndex;
                selCol = dataGridView1.CurrentCell.ColumnIndex;
            }
            catch
            {
                selRow = selCol = -1;
                btnDelete.Enabled = btnEdit.Enabled = false;
            }
        }

        private void UpdateGrid()
        {
            this.productsTableAdapter.Fill(this.productsDbDataSet.Products);
            if (prevSelRow > -1)
            {
                dataGridView1.Rows[prevSelRow].Cells[prevSelCol].Selected = true;
            }
        }
    }
}
