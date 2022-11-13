using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProductsFormsApp
{
    public class ProductsRepository
    {
        private TestProductContext context = new TestProductContext();

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products.ToList();
        }

        public void AddProduct(Product item)
        {
            context.Products.Add(item);
            context.SaveChanges();
        }

        public void AddProducts(IEnumerable<Product> items)
        {
            context.Products.AddRange(items);
            context.SaveChanges();
        }

        public void RemoveProduct(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void RemoveProductById(int id)
        {
            var product = context.Products
                .Where(p => p.Id == id)
                .FirstOrDefault();

            context.Products.Remove(product);
            context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return context.Products.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
