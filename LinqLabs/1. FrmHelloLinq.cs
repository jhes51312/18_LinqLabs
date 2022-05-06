using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
            productsTableAdapter1.Fill(this.dataSet11.Products);
            ordersTableAdapter1.Fill(this.dataSet11.Orders);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            //public interface IEnumerable<T>
            //System.Collections.Generic 的成員

            //摘要:
            //公開支援指定類型集合上簡單反覆運算的列舉值。

            //類型參數:
            //T: 要列舉之物件的類型。
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            foreach (int n in nums)
            {
                this.listBox1.Items.Add(n);
            }
            this.listBox1.Items.Add("==========================");
            System.Collections.IEnumerator en = nums.GetEnumerator();

            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            foreach (int n in list)
            {
                this.listBox1.Items.Add(n);
            }
            //int n = 100;
            //var n1 = 200;
            this.listBox1.Items.Add("=======================");
            List<int>.Enumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Step 1: define Data Source
            //Step 2: define query
            //Step 3: excute query
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                 where (n >= 5 && n <= 8) && (n % 2 == 0)
                                 select n;
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                 where IsEven(n)
                                 select n;
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }
        bool IsEven(int n)
        {
            //if(n % 2 == 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return n % 2 == 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<Point> q = from w in nums
                                   where w > 5
                                   select new Point(w, w * w);
            foreach (Point pt in q)
            {
                this.listBox1.Items.Add(pt.X + " , " + pt.Y);
            }
            //==================================
            List<Point> list = q.ToList();
            this.dataGridView1.DataSource = list;
            //==================================

            this.chart1.DataSource = list;
            this.chart1.Series[0].XValueMember = "X";
            this.chart1.Series[0].YValueMembers = "Y";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].Color = Color.Red;
            this.chart1.Series[0].BorderWidth = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] words = { "aaa", "Apple", "pineApple", "xxxxxapple" };
            IEnumerable<string> q = from w in words
                                    where w.ToLower().Contains("apple") && w.Length > 5
                                    select w;
            foreach (string s in q)
            {
                this.listBox1.Items.Add(s);
            }
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.DataSource = this.dataSet11.Products;

            IEnumerable<global::LinqLabs.DataSet1.ProductsRow> q = from p in this.dataSet11.Products
                                                                   where !p.IsUnitPriceNull() && p.UnitPrice > 30 && p.UnitPrice < 50 && p.ProductName.StartsWith("M")
                                                                   select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //var q = from p in this.dataSet11.Orders
            //        where !p.IsOrderDateNull() && p.OrderDate.Year == 1997 && p.OrderDate.Month < 4
            //        orderby p.OrderDate
            //        select p;
            var q = from p in this.dataSet11.Orders
                    group p by p.OrderDate.Year into Year /*&& !p.IsOrderDateNull()*/
                    orderby Year.Key
                    select Year;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
