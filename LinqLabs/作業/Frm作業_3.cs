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

namespace MyHomeWork
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }
        void GridAndTreeClear()
        {
            this.treeView1.Nodes.Clear();
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
        }
        int flag;
        IEnumerable<System.IO.FileInfo> FileCell;
        IEnumerable<Products> ProductsCell;
        IEnumerable<Order> OrdersCell;

        private void button4_Click(object sender, EventArgs e)
        {
            GridAndTreeClear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            int count = 0;
            TreeNode node = null;
            foreach (int n in nums)
            {
                if (treeView1.Nodes[MyKey(n)] == null)
                {
                    count = 0;
                    node = treeView1.Nodes.Add(MyKey(n), MyKey(n));
                    node.Nodes.Add(n.ToString());
                    count++;
                    //node.Text = MyKey(n) +"("+ count+")";
                    node.Text = $"{MyKey(n)}({count})";
                }
                else
                {
                    node.Nodes.Add(n.ToString());
                    count++;
                    node.Text = $"{MyKey(n)}({count})";
                }
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            GridAndTreeClear();
            flag = 1;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            this.dataGridView2.DataSource = files.ToList();

            var q = from n in files
                    orderby n.Length descending
                    group n by MyFileLengthGroup(n.Length) into g
                    select new
                    {
                        FileLength = g.Key,
                        Count = g.Count(),
                        Group = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            FileCell = files.Where(f => MyFileLengthGroup(f.Length) == dataGridView1.CurrentRow.Cells[0].Value.ToString()).OrderByDescending(n => n.Length);
            
            foreach (var group in q)
            {
                string s = $"{group.FileLength}({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(group.FileLength.ToString(), s);
                foreach (var item in group.Group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GridAndTreeClear();
            flag = 1;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            this.dataGridView2.DataSource = files.ToList();

            var q = from n in files
                    orderby n.CreationTime.Year descending
                    group n by n.CreationTime.Year into g
                    select new { CreationTime = g.Key, Count = g.Count(), Group = g };
            this.dataGridView1.DataSource = q.ToList();
            FileCell = files.Where(f => f.CreationTime.Year == (int)dataGridView1.CurrentRow.Cells[0].Value);

            foreach (var group in q)
            {
                string s = $"{group.CreationTime}({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(group.CreationTime.ToString(), s);
                foreach (var item in group.Group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }
        private string MyFileLengthGroup(long i)
        {
            if (i < 10000)
            {
                return ("Small");
            }
            if (i < 20000)
            {
                return ("Medium");
            }
            else
            {
                return ("Large");
            }
        }
        private string MyKey(int n)
        {
            if (n < 5)
                return "small";
            else if (n < 10)
                return "Medium";
            else
                return "Large";
        }
        private string MyProductPrice(Products n)
        {
            if (n.UnitPrice < 40)
                return "LowPrice";
            else if (n.UnitPrice < 100)
                return "MediumPrice";
            else
                return "LargePrice";
        }
        NorthwindEntities dbContext = new NorthwindEntities();
        private void button8_Click(object sender, EventArgs e)
        {
            //var q = from p in this.dbContext.Products
            GridAndTreeClear();
            flag = 2;
            var qq = from p in dbContext.Products
                     select p;
            this.dataGridView2.DataSource = qq.ToList();

            var q = from p in dbContext.Products.ToList()
                    orderby p.UnitPrice
                    group p by MyProductPrice(p) into g
                    select new
                    {
                        UnitPrice = g.Key,
                        Count = g.Count(),
                        Group = g
                    };
            dataGridView1.DataSource = q.ToList();
            ProductsCell = dbContext.Products.AsEnumerable().Where(f => MyProductPrice(f) == dataGridView1.CurrentRow.Cells[0].Value.ToString());

            foreach (var group in q)
            {
                string s = $"{group.UnitPrice}({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(group.UnitPrice.ToString(), s);
                foreach (var item in group.Group)
                {
                    node.Nodes.Add(item.UnitPrice.ToString());
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            GridAndTreeClear();
            flag = 3;
            var q = from p in dbContext.Orders
                    select p;
            this.dataGridView2.DataSource = q.ToList();

            var qq = from p in dbContext.Orders
                     group p by p.OrderDate.Value.Year into g
                     orderby g.Key
                     select new { Year = g.Key, Count = g.Count(), Group = g };
            this.dataGridView1.DataSource = qq.ToList();
            OrdersCell = dbContext.Orders.AsEnumerable().Where(f => f.OrderDate.Value.Year == (int)dataGridView1.CurrentRow.Cells[0].Value);
            foreach (var group in qq)
            {
                string s = $"{group.Year}({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(group.Year.ToString(), s);
                foreach (var item in group.Group)
                {
                    node.Nodes.Add(item.OrderDate.ToString());
                }
            }
            //var q = from p in dbContext
            //        where p.UnitPrice > 30
            //        select p;

            //this.dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GridAndTreeClear();
            flag = 3;
            var q = from p in dbContext.Orders
                    select p;
            this.dataGridView2.DataSource = q.ToList();

            var qq = from p in dbContext.Orders
                     group p by new { p.OrderDate.Value.Year, p.OrderDate.Value.Month } into g
                     orderby g.Key.Year, g.Key.Month
                     select new { Year = g.Key.Year, Month = g.Key.Month, Count = g.Count(), Group = g };
            this.dataGridView1.DataSource = qq.ToList();
            OrdersCell = dbContext.Orders.AsEnumerable().Where(f => f.OrderDate.Value.Year == (int)dataGridView1.CurrentRow.Cells[0].Value && f.OrderDate.Value.Month == (int)dataGridView1.CurrentRow.Cells[1].Value);

            foreach (var group in qq)
            {
                string s = $"{group.Year}年{group.Month}月({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(group.Year.ToString(), s);
                foreach (var item in group.Group)
                {
                    node.Nodes.Add(item.OrderDate.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GridAndTreeClear();
            var q = from p in dbContext.Products
                    from od in p.Order_Details
                    orderby p.ProductName
                    select new { ProductName = p.ProductName, TotalPrice = Math.Round(od.Quantity * (1 - od.Discount) * (float)od.UnitPrice, 2) };
            this.dataGridView1.DataSource = q.ToList();

            var qq = from qqq in q
                     select new { TotalPrice = Math.Round(q.Sum(n => n.TotalPrice), 2) };
            this.dataGridView2.DataSource = qq.Take(1).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GridAndTreeClear();
            //var q = from e in dbContext.Employees
            //        from o in e.Orders
            //        orderby
            var q = from emp in this.dbContext.Employees
                    from o in emp.Orders
                    from od in o.Order_Details
                    select new { LastName = emp.LastName, TotalPrice = Math.Round((float)od.UnitPrice * od.Quantity * (1 - od.Discount), 2) };
            dataGridView1.DataSource = q.ToList();

            var qq = from p in q.OrderBy(n => n.TotalPrice).GroupBy(n => n.LastName)
                     select new { LastName = p.Key, SaleAmout = Math.Round(p.Sum(n => n.TotalPrice), 2) };
            var qqq = from pp in qq.OrderByDescending(n => n.SaleAmout)
                      select pp;
            this.dataGridView2.DataSource = qqq.Take(5).ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GridAndTreeClear();
            var qq = from c in dbContext.Categories
                     from p in c.Products
                     orderby p.UnitPrice descending
                     select new { ProductName = p.ProductName, UnitPrice = p.UnitPrice, CategoryName = c.CategoryName };
            this.dataGridView1.DataSource = qq.ToList();
            var q = from c in dbContext.Categories
                    from p in c.Products
                    orderby p.UnitPrice descending
                    select new { ProductName = p.ProductName, UnitPrice = p.UnitPrice, CategoryName = c.CategoryName };
            this.dataGridView2.DataSource = q.Take(5).ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var q = from p in dbContext.Products
                    select p.UnitPrice;
            bool result = q.Any(n => n > 300);
            string w = " ";
            switch (result)
            {
                case true:
                    w = "是";
                    break;
                case false:
                    w = "否";
                    break;
            }
            MessageBox.Show("是否有任何一筆單價大於300？ " + w);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          if(flag == 1)
            {
                this.dataGridView2.DataSource = FileCell.ToList();
            }
            if(flag == 2)
            {
                this.dataGridView2.DataSource = ProductsCell.ToList();
            }
            if(flag == 3)
            {
                this.dataGridView2.DataSource = OrdersCell.ToList();
            }
        }
    }
}
