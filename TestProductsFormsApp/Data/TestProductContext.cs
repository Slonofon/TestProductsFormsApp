﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TestProductsFormsApp
{
    internal class TestProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
