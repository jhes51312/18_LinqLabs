using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();
            Console.Write("xxxx...opep()..select * from products....close()..........");
            this.dbContext.Database.Log = Console.Write;
        }
          NorthwindEntities dbContext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            var q = from p in dbContext.Products
                    where p.UnitPrice > 30
                    select p;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.dbContext.Categories.First().Products.ToList();
            MessageBox.Show(this.dbContext.Products.First().Category.CategoryName);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.dbContext.Sales_by_Year(new DateTime(1997, 1, 1), DateTime.Now);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products.AsEnumerable()
                    orderby p.UnitsInStock descending, p.ProductID descending
                    select new
                    {
                        p.ProductID,
                        p.ProductName,
                        p.UnitPrice,
                        p.UnitsInStock,
                        TotalPrice = $"{p.UnitPrice * p.UnitsInStock:C2}"
                    };
            this.dataGridView1.DataSource = q.ToList();
            //==========================================
            var q2 = this.dbContext.Products.OrderByDescending(p => p.UnitsInStock).ThenByDescending(p => p.ProductID);
            this.dataGridView2.DataSource = q2.ToList();
        }
        //class MyComparer : IComparer<Product>
        //{
        //    public int compare(Products x, Products y)
        //    {
        //        if (x.UnitPrice < y.UnitPrice)
        //        {
        //            return -1;
        //        }
        //        else if (x.UnitPrice < y.UnitPrice)
        //        {
        //            return 1;
        //        }
        //        else
        //            return string.Compare(x.ProductName.ToString(), y.ProductName[0].ToString(), true);
        //    }
        //}        
        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    select new { p.CategoryID, p.Category.CategoryName, p.ProductName, p.UnitPrice };
            this.dataGridView3.DataSource = q.ToList();

            var q2 = from c in this.dbContext.Categories
                     from p in this.dbContext.Products
                     select new { c.CategoryID, c.CategoryName, p.ProductID, p.UnitPrice };
            MessageBox.Show("q2.count() = " + q2.Count());
            this.dataGridView2.DataSource = q2.ToList();

        }

        private void button21_Click(object sender, EventArgs e)
        {
            var q = from c in this.dbContext.Categories
                    from p in c.Products
                    select new { c.CategoryID, c.CategoryName, p.ProductName, p.UnitPrice };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    group p by p.Category.CategoryName into g
                    select new { CategoryName = g.Key, AvgUnitPrice = g.Average(p => p.UnitPrice) };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool? b;
            b = true;
            b = false;
            b = null;

            var q = from o in this.dbContext.Orders
                    group o by o.OrderDate.Value.Year into g
                    select new { Year = g.Key, Count = g.Count() };
            this.dataGridView1.DataSource = q.ToList();
            //=====================================================

            var q2 = from p in this.dbContext.Orders
                     group p by new { p.OrderDate.Value.Year, p.OrderDate.Value.Month } into g
                     select new { Year = g.Key, Count = g.Count() };
            this.dataGridView2.DataSource = q2.ToList();


        }
        private void button55_Click(object sender, EventArgs e)
        {
            //Product prod = new Product { ProductName = DateTime.Now.ToLongTimeString(), Discountinued = ture };
            //this.dbContext.Products.Add(prod);
            //this.dbContext.SaveChanges();
        }
    }
           
}

