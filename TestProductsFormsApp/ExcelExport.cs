using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace TestProductsFormsApp
{
    public class ExcelExport
    {
        private string filename;

        public ExcelExport(string filename)
        {
            this.filename = filename;
        }

        public IEnumerable<Product> GetProducts()
        {
            HashSet<Product> result = new HashSet<Product>();

            Excel.Application objExcel = null;
            Excel.Workbook objBook = null;
            Excel.Worksheet objSheet = null;

            try
            {
                objExcel = new Excel.Application();
                objBook = objExcel.Workbooks.Open(filename);
                objSheet = (Excel.Worksheet)objBook.Sheets[1];
                var lastCell = objSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
                var lastRow = (int)lastCell.Row;

                for (var i = 2; i <= lastRow; i++)
                {
                    var item = new Product()
                    {
                        Article = objSheet.Cells[i, 1].Text.ToString(),
                        Name = objSheet.Cells[i, 2].Text.ToString(),
                        Price = Convert.ToDecimal(objSheet.Cells[i, 3].Text.ToString()),
                        Quantity = Convert.ToInt32(objSheet.Cells[i, 4].Text.ToString())
                    };
                    result.Add(item);
                }
            }
            finally
            {
                objBook.Close(false, Type.Missing, Type.Missing);
                objExcel.Quit();
                GC.Collect();
            }

            return result;
        }
    }
}
