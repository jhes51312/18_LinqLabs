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
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            productPhotoTableAdapter1.Fill(dataSet21.ProductPhoto);
            ComboboxYearCreation();
        }
        private void ComboboxYearCreation()
        {
            var q = from p in this.dataSet21.ProductPhoto
                    group p by p.ModifiedDate.Year into Year /*&& !p.IsOrderDateNull()*/
                    orderby Year.Key
                    select Year;
            //var q = from p in this.dataSet21.ProductPhoto
            //        select p.ModifiedDate.Year;

            foreach (var Year in q)
            {
                comboBox3.Items.Add($"{Year.Key}");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from p in dataSet21.ProductPhoto
                    orderby p.ModifiedDate
                    select p;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var q = from n in dataSet21.ProductPhoto
                    where n.ModifiedDate.Year.ToString() == comboBox3.Text
                    select n;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime A, B;
            DateTime.TryParse(dateTimePicker1.Text, out A);
            DateTime.TryParse(dateTimePicker2.Text, out B);

            var q = from n in dataSet21.ProductPhoto
                    where n.ModifiedDate >= A && n.ModifiedDate <= B
                    orderby n.ModifiedDate
                    select n;
            dataGridView1.DataSource = q.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var q = from n in dataSet21.ProductPhoto
                    where n.ProductPhotoID == (int)(dataGridView1.CurrentRow.Cells[0].Value)
                    select n.LargePhoto;
            var k = q.FirstOrDefault();

            System.IO.MemoryStream ms = new System.IO.MemoryStream(k);
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 0)
            {
                var q = from n in dataSet21.ProductPhoto
                        where n.ModifiedDate.Month >= 1 && n.ModifiedDate.Month <= 3
                        select n;
                dataGridView1.DataSource = q.ToList();
            }
            if(comboBox2.SelectedIndex == 1)
            {
                var q = from n in dataSet21.ProductPhoto
                        where n.ModifiedDate.Month >= 4 && n.ModifiedDate.Month <= 6
                        select n;
                dataGridView1.DataSource = q.ToList();
            }
            if(comboBox2.SelectedIndex == 2)
            {
                var q = from n in dataSet21.ProductPhoto
                        where n.ModifiedDate.Month >= 7 && n.ModifiedDate.Month <= 9
                        select n;
                dataGridView1.DataSource = q.ToList();
            }
            if(comboBox2.SelectedIndex == 3)
            {
                var q = from n in dataSet21.ProductPhoto
                        where n.ModifiedDate.Month >= 10 && n.ModifiedDate.Month <= 12
                        select n;
                dataGridView1.DataSource = q.ToList();
            }
            //lblMaster.Text = ($"共有(dataGridView1.Rows.Count)筆");
        }
    }
}
