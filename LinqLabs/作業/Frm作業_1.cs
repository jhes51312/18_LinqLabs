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
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            ordersTableAdapter1.Fill(dataSet11.Orders);
            order_DetailsTableAdapter1.Fill(dataSet11.Order_Details);
            productsTableAdapter1.Fill(dataSet11.Products);
            ComboboxCreation();
        }
        private void ComboboxCreation()
        {
            //var q = from p in this.dataSet11.Orders
            //        group p by p.OrderDate.Year into Year /*&& !p.IsOrderDateNull()*/
            //        orderby Year.Key
            //        select Year;
            var q = from p in this.dataSet11.Orders
                    select p.OrderDate.Year;

            foreach (var Year in q.Distinct())
            {
                comboBox1.Items.Add($"{Year/*.Key*/}");
            }
        }
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from n in files
                    where n.Extension.Contains("log")
                    select n;

            this.dataGridView1.DataSource = q.ToList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from n in files
                    where n.CreationTime.Year.Equals(2019)
                    select n;

            this.dataGridView1.DataSource = q.ToList();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from n in files
                    where n.Length > 65000
                    select n;
            this.dataGridView1.DataSource = q.ToList();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.dataSet11.Orders;
            ordersTableAdapter1.Fill(this.dataSet11.Orders);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var q = from n in dataSet11.Orders
                    where n.OrderDate.Year.ToString() == comboBox1.Text && !n.IsShippedDateNull() && !n.IsShipRegionNull()
                    select n;
            this.dataGridView1.DataSource = q.ToList();

            var qq = from n in dataSet11.Orders
                     join m in dataSet11.Order_Details on n.OrderID equals m.OrderID
                     where n.OrderDate.Year.ToString() == comboBox1.Text
                     select new { n.OrderID, m.ProductID, m.UnitPrice, m.Discount };
            this.dataGridView2.DataSource = qq.ToList();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Flag & count == 0)
            {
                int orderID = (int)this.dataGridView1.CurrentRow.Cells["OrderID"].Value;
                var q = from o in dataSet11.Order_Details
                        where o.OrderID == orderID
                        select o;
                this.dataGridView2.DataSource = q.ToList();
            }
            else
            {
                int productID = (int)this.dataGridView1.CurrentRow.Cells["ProductID"].Value;
                var q = from o in dataSet11.Products
                        where o.ProductID == productID
                        select o;
                this.dataGridView2.DataSource = q.ToList();
            }
        }
        int num;
        int count = 0;
        int end = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            var q = from o in dataSet11.Products
                    select o;
            this.dataGridView1.DataSource = q.ToList();
            end = q.Count();
            count = 0;
        }
        bool Flag = true;
        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)
            //Distinct()
            bool isNum = int.TryParse(textBox1.Text,out num);
            if (Flag & count == 0) 
            {
                var q = from o in dataSet11.Products.Take(num)
                        select o;
                this.dataGridView1.DataSource = q.ToList();

                var qq = from p in dataSet11.Products
                         select p;
                end = qq.Count();
                count += 1;
                Flag = false;
            }
            else if (!isNum)
            {
                MessageBox.Show("請輸入數字");
            }
            else
            {
                if (count * num > end)
                {
                    MessageBox.Show("沒有更多資料");
                }
                else { 
                    var q = from o in dataSet11.Products.Skip(count * num).Take(num)
                            select o;
                    this.dataGridView1.DataSource = q.ToList();
                    count += 1;
                }
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            bool isNum = int.TryParse(textBox1.Text,out num);
            //if(count == 0 & count == 1)
            //{
            //}
            if (count == 0 || count == 1)
            {
                MessageBox.Show("沒有更多資料");
            }
            else if (!isNum)
            {
                MessageBox.Show("請輸入數字");
            }
            else
            {
                count -= 2;
                var q = from o in dataSet11.Products.Skip(count * num).Take(num)
                        select o;
                this.dataGridView1.DataSource = q.ToList();
                count += 1;
            }
        }

        private void Frm作業_1_Load(object sender, EventArgs e)
        {

        }
    }
}
