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
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1 = 100;
            int n2 = 200;

            MessageBox.Show(n1 + " , " + n2);
            Swap(ref n1, ref n2);
            MessageBox.Show(n1 + " , " + n2);
            //===========================
            string s1 = "yyy";
            string s2 = "xxx";
            MessageBox.Show(s1 + " , " + s2);
            Swap(ref s1, ref s2);
            MessageBox.Show(s1 + " , " + s2);

        }
        void SwapObject(ref object n1, ref object n2)
        {
            object temp = n2;
            n2 = n1;
            n1 = temp;
        }
        void Swap(ref int n1, ref int n2)
        {
            int temp = n2;
            n2 = n1;
            n1 = temp;
        }
        void Swap(ref string n1, ref string n2)
        {
            string temp = n2;
            n2 = n1;
            n1 = temp;
        }
        void Swap(ref Point e, ref Point f)
        {
            Point temp = e;
            e = f;
            f = temp;
        }
        void Swap(int a, int b, out int y, out int x)
        {
            y = b;
            x = a;
        }
        static void SwapAnyType<T>(ref T n1, ref T n2)
        {
            T temp = n2;
            n2 = n1;
            n1 = temp;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;
            MessageBox.Show(n1 + " , " + n2);
            SwapAnyType<int>(ref n1, ref n2);
            MessageBox.Show(n1 + " , " + n2);
            //===================

            string s1, s2;
            s1 = "aaaaa";
            s2 = "bbbbb";
            MessageBox.Show(s1 + " , " + s2);
            SwapAnyType(ref s1, ref s2);
            MessageBox.Show(s1 + " , " + s2);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void buttonX_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.buttonX.Click += ButtonX_Click;
            this.buttonX.Click += new EventHandler(aaa);
            this.buttonX.Click += bbb;
            this.buttonX.Click += delegate (object sender1, EventArgs e1)
            {
                MessageBox.Show("匿名方法");
            };
            this.buttonX.Click += (object sender1, EventArgs e1) =>
           {
               MessageBox.Show("lamba匿名方法");
           };
        }

        private void ButtonX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("buttonX click");
        }
        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }
        private void bbb(object sender, EventArgs e)
        {
            MessageBox.Show("bbb");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool result = Test(4);
            MessageBox.Show("bool result = " + result);
            //==========================
            MyDelegate delegateObj = new MyDelegate(Test);
            result = delegateObj.Invoke(7);
            MessageBox.Show("delegate ojb = " + result);
            //==========================
            delegateObj = Test1;
            result = delegateObj(2);
            MessageBox.Show("result = " + result);
            //==========================
            delegateObj = delegate (int n)
            {
                return n > 5;
            };
            result = delegateObj(6);
            MessageBox.Show("result = " + result);
            //===========================
            delegateObj = n => n > 5;
            result = delegateObj(1);
            MessageBox.Show("result = " + result);
        }
        delegate bool MyDelegate(int n);

        private bool Test(int v)
        {
            //if (v > 5)
            //    return true;
            //else
            //    return false;
            return v > 5;
        }
        bool Test1(int n)
        {
            return n % 2 == 0;
        }
        List<int> MyWhere(int[] nums, MyDelegate delegateObj)
        {
            List<int> list = new List<int>();
            foreach (int n in nums)
            {
                if (delegateObj(n))
                {
                    list.Add(n);
                }
            }
            return list;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //List<int> Large_list = MyWhere(nums, Test1);

            //foreach(int n in Large_list)
            //{
            //    this.listBox1.Items.Add(n);
            //}
            List<int> list1 = MyWhere(nums, n => n > 5);
            List<int> oddlist = MyWhere(nums, n => n % 2 == 1);
            List<int> evenList = MyWhere(nums, n => n % 2 == 0);
            foreach (int n in list1)
            {
                this.listBox1.Items.Add(n);
            }
            foreach (int n in oddlist)
            {
                this.listBox1.Items.Add(n);
            }
            foreach (int n in evenList)
            {
                this.listBox2.Items.Add(n);
            }
        }
        IEnumerable<int> MyIterator(int[] nums, MyDelegate delegateObj)
        {
            foreach (int n in nums)
            {
                yield return n;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = MyIterator(nums, n => n > 5);

            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 7, 8, 9, 10 };
            IEnumerable<int> q = nums.Where(n => n > 5);
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
            string[] words = { "aaa", "bbbbbbb", "ccccccccc" };
            IEnumerable<string> q1 = words.Where<string>(w => w.Length > 3);
            foreach (string w in q1)
            {
                this.listBox2.Items.Add(w);
            }
            this.productsTableAdapter1.Fill(this.dataSet11.Products);
            var q2 = this.dataSet11.Products.Where(p => p.UnitPrice > 30);
            this.dataGridView1.DataSource = q2.ToList();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            Mypoint pt1 = new Mypoint();
            pt1.P1 = 100;
            int w = pt1.P1;
            pt1.P2 = 200;
            //MessageBox.Show(pt1.P1.ToString());

            List<Mypoint> list = new List<Mypoint>();
            list.Add(new Mypoint());
            list.Add(new Mypoint(100));
            list.Add(new Mypoint(100, 300));
            list.Add(new Mypoint("xxxx"));
            //========================================
            list.Add(new Mypoint { P1 = 1, P2 = 1, Field1 = "aaa", Field2 = "BBB" });
            list.Add(new Mypoint { P1 = 333 });
            list.Add(new Mypoint { P1 = 3, P2 = 3, Field1 = "aaa", Field2 = "BBB" });
            this.dataGridView1.DataSource = list;
            //========================================
            List<Mypoint> list2 = new List<Mypoint>
            {
            new Mypoint{P1=1, P2=2,Field1="xxx"},
            new Mypoint{P1=11, P2=2,Field1="xxx"},
            new Mypoint { P1 = 111, P2 = 2, Field1 = "xxx" },
            new Mypoint { P1 = 1111, P2 = 2, Field1 = "xxx" },
            };
            this.dataGridView2.DataSource = list2;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            var x = new { P1 = 99, P2 = 88, P3 = 33 };
            var y = new { P1 = 77, P2 = 66, P3 = 22 };
            var z = new { UserName = "aaa", Password = "BBB" };

            this.listBox1.Items.Add(x.GetType());
            this.listBox1.Items.Add(y.GetType());
            this.listBox1.Items.Add(z.GetType());
            //=====================================
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var q = from n in nums
                    where n > 5
                    select new { N = n, S = n * n, C = n * n * n };
            this.dataGridView1.DataSource = q.ToList();

            var qq = nums.Where(n => n > 5).Select(n => new { N = n, S = n * n, C = n * n * n });
            this.dataGridView1.DataSource = qq.ToList();

            this.productsTableAdapter1.Fill(this.dataSet11.Products);

            var qqq = from p in this.dataSet11.Products
                      where p.UnitPrice > 30
                      select new 
                      { 
                          ID = p.ProductID, 
                          ProductName = p.ProductName, 
                          p.UnitPrice, p.UnitsInStock, 
                          TotalPrice = p.UnitPrice * p.UnitsInStock 
                      };
            this.dataGridView2.DataSource = qqq.ToList();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            string s1 = "abcd";
            int n = s1.WordCount();
            MessageBox.Show("WordCount = " + n);

            string s2 = "28934729307402983";
            n = s2.WordCount();
            MessageBox.Show("WordCount = " + n);
            n = MyStringExtend.WordCount(s2);

            char ch = s2.Chars(5);
            MessageBox.Show("Char = " + ch);
        }
    }
}
public static class MyStringExtend
{
    public static char Chars(this string s, int index)
    {
        return s[index];
    }
    public static int WordCount(this string s)
    {
        return s.Length;
    }
}
public class Mypoint
{
    public Mypoint()
    {

    }
    public Mypoint(int p1)
    {
        this.P1 = p1;
    }
    public Mypoint(int p1, int p2)
    {
        this.P1 = p1;
        this.P2 = p2;
    }
    public Mypoint(string field1)
    {

    }
    public string Field1 = "XXX", Field2 = "YYY";
    private int mp_1;
    public int P1
    {
        get
        {
            return mp_1;
        }
        set
        {
            mp_1 = value;
        }
    }
    public int P2 { get; set; }
}
