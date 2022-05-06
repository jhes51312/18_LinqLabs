using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13 };
            IEnumerable<IGrouping<string, int>> q = from n in nums
                                                    group n by n % 2 == 0 ? "偶數" : "奇數";
            this.dataGridView1.DataSource = q.ToList();

            //==================================

            foreach (var group in q)
            {
                TreeNode node = this.treeView1.Nodes.Add(group.Key.ToString());
                foreach (var item in group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }

            //==================================
            foreach (var group in q)
            {
                ListViewGroup lvg = this.listView1.Groups.Add(group.Key.ToString(), group.Key.ToString());
                foreach (var item in group)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13 };
            var q = from n in nums
                    group n by n % 2 == 0 ? "偶數" : "奇數" into g
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count(),
                        MyMin = g.Min(),
                        MyAvg = g.Average(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            //==================================

            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(group.MyKey.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13 };
            var q = from n in nums
                    group n by MyKey(n) into g
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count(),
                        MyMin = g.Min(),
                        MyAvg = g.Average(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            //==================================

            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(group.MyKey.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //=============================
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = "MyKey";
            this.chart1.Series[1].YValueMembers = "MyAvg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }
        private string MyKey(int n)
        {
            if (n < 5)
            {
                return "small";
            }
            else if (n < 10)
            {
                return "medium";
            }
            else
            {
                return "large";
            }

        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"C:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from n in files
                    group n by n.Extension into g  /*.Extension.Contains("log")*/
                    orderby g.Count() descending
                    select new { Extention = g.Key, MyCount = g.Count() };
            this.dataGridView2.DataSource = q.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.dataSet11.Orders);
            var q = from n in dataSet11.Orders
                    group n by n.OrderDate.Year into g
                    select new { g.Key, MyCount = g.Count() };
            this.dataGridView2.DataSource = q.ToList();

            //============================================

            int count = (from o in this.dataSet11.Orders
                         where o.OrderDate.Year == 1997
                         select o).Count();
            MessageBox.Show("count = " + count);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windwos");
            System.IO.FileInfo[] files = dir.GetFiles();

            int count = (from f in files
                         let s = f.Extension
                         where f.Extension == ".exe"
                         select f).Count();
            MessageBox.Show("Count = " + count);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = "This is a book. this is a pen.     this is an apple.";
            char[] chars = { ' ', ',', '?', '.' };
            string[] words = s.Split(chars,StringSplitOptions.RemoveEmptyEntries);
            var q = from n in words
                    group n by n.ToUpper() into g
                    select new { MyKey = g.Key, MyCount = g.Count() };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 1, 2, 3, 5, 11, 2 };
            int[] nums2 = { 1, 3, 66, 77, 111 };

            IEnumerable<int> q;

            q = nums1.Intersect(nums2);
            q = nums1.Distinct();
            q = nums1.Union(nums2);

        }

        private void button15_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 1, 2, 3, 5, 11, 2 };
            int[] nums2 = { 1, 3, 66, 77, 111 };

            IEnumerable<int> q;

            q = nums1.Intersect(nums2);
            q = nums1.Distinct();
            q = nums1.Union(nums2);
            q = nums1.Take(2);

            bool result;
            result = nums1.Any(n => n > 3);
            result = nums1.All(n => n >= 1);

            int n1;
            n1 = nums1.First();
            n1 = nums1.Last();
            //n1 = nums1.ElementAt(11);
            n1 = nums1.ElementAtOrDefault(13);
            //======================================

            var q1 = Enumerable.Range(1, 1000).Select(n => new { N = n });
            this.dataGridView1.DataSource = q1.ToList();

            var q2 = Enumerable.Repeat(60, 1000).Select(n => new { n });
            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.dataSet11.Products);
            this.categoriesTableAdapter1.Fill(this.dataSet11.Categories);

            var q = from p in this.dataSet11.Products
                    group p by p.CategoryID into g
                    select new { CategoryID = g.Key, MyAVG = g.Average(p => p.UnitPrice) };

            this.dataGridView1.DataSource = q.ToList();

            var q2 = from n in this.dataSet11.Categories
                     join m in this.dataSet11.Products
                     on n.CategoryID equals m.CategoryID
                     group m by n.CategoryName into g
                     select new { CategoryName = g.Key, MyAVG = g.Average(n => n.UnitPrice) };

            this.dataGridView2.DataSource = q2.ToList();
        }
    }
}
