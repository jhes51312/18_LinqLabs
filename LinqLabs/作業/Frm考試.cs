using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class Frm考試 : Form
    {
        public Frm考試()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };
        }

        List<Student> students_scores;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }
        void ViewsClear()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            this.dataGridView3.DataSource = null;
            this.treeView1.Nodes.Clear();
            this.chart1.DataSource = null;
            this.chart2.DataSource = null;
        }
        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?						
            ViewsClear();
            var q = from p in students_scores
                    group p by Name into g
                    select new { Count = g.Count() };
            //select new { Name = p.Name.Count() };

            this.dataGridView1.DataSource = q.ToList();

            // 找出 前面三個 的學員所有科目成績		
            var qq = from p in students_scores
                     select new { Name = p.Name, Chinese = p.Chi, English = p.Eng, Math = p.Math };
            this.dataGridView2.DataSource = qq.Take(3).ToList();

            // 找出 後面兩個 的學員所有科目成績					
            var qqq = (from p in students_scores
                       select new { Name = p.Name, Chinese = p.Chi, English = p.Eng, Math = p.Math }).Skip(students_scores.Count() - 2);
            this.dataGridView3.DataSource = qqq.ToList();


            #endregion

        }

        private void button37_Click(object sender, EventArgs e)
        {
            ViewsClear();
            //個人 sum, min, max, avg
            var q = from p in students_scores
                    select new
                    {
                        Name = p.Name,
                        TotalScore = (p.Chi + p.Math + p.Eng),
                        LowestScore = new int[] { p.Chi, p.Math, p.Eng }.Min(),
                        HighesestScore = new int[] { p.Chi, p.Math, p.Eng }.Max(),
                        AvgScore = new int[] { p.Chi, p.Math, p.Eng }.Average()
                    };

            dataGridView1.DataSource = q.ToList();

            //各科 sum, min, max, avg
            //var qq = from p in students_scores.Select(s => new { 最小分數=MIN(s.Math,s.Chi,s.Eng) })
            int chiSum = students_scores.Sum(n => n.Chi);
            int chiMax = students_scores.Max(n => n.Chi);
            int chiMin = students_scores.Min(n => n.Chi);
            double chiAvg = students_scores.Average(n => n.Chi);

            int mathSum = students_scores.Sum(n => n.Math);
            int mathMax = students_scores.Max(n => n.Math);
            int mathMin = students_scores.Min(n => n.Math);
            double mathAvg = students_scores.Average(n => n.Math);

            int engSum = students_scores.Sum(n => n.Eng);
            int engMax = students_scores.Max(n => n.Eng);
            int engMin = students_scores.Min(n => n.Eng);
            double engAvg = students_scores.Average(n => n.Eng);
            studentCompute = new List<StudentAllScore>()            {
            new StudentAllScore  {Name = "Chinese", Sum = chiSum, Max = chiMax, Min = chiMin, Avg = Math.Round(chiAvg,2)},
            new StudentAllScore  {Name = "Math", Sum = mathSum, Max = mathMax, Min = mathMin, Avg = Math.Round(mathAvg,2)},
            new StudentAllScore  {Name = "English", Sum = engSum, Max = engMax, Min = engMin, Avg = Math.Round(engAvg,2)}};
            this.dataGridView2.DataSource = studentCompute.ToList();


        }
        List<StudentAllScore> studentCompute;

        public class StudentAllScore
        {
            public string Name { get; set; }
            public int Sum { get; set; }
            public int Max { get; set; }
            public int Min { get; set; }
            public double Avg { get; set; }
        }

        //private object MIN(int math, int chi, int eng)
        //{
        //    if(students_scores[Math])
        //}
        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
            ViewsClear();
            int count;
            Random ran = new Random();
            for (int i = 1; i <= 100; i++)
            {
                count = ran.Next(0, 101);
                StudentRandom100.Add(count);
            }
            var q1 = from p in StudentRandom100
                     select new { Score = p };
            this.dataGridView1.DataSource = q1.ToList();
            var q = from p in StudentRandom100
                    group p by MyKey(p) into g
                    select new { Group = g.Key, count = g.Count(), gorup = g };
            this.dataGridView2.DataSource = q.ToList();

            foreach (var group in q)
            {
                string s = $"{group.Group}({group.count})";
                TreeNode node = this.treeView1.Nodes.Add(group.Group.ToString(), s);
                foreach (var item in group.Group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            button35.Enabled = true;
        }
        List<int> StudentRandom100 = new List<int>();
        private string MyKey(int n)
        {
            if (n < 70)
                return "待加強";
            else if (n < 90)
                return "佳";
            else
                return "優良";
        }
        private void button35_Click(object sender, EventArgs e)
        {
            ViewsClear();
            var q = from p in StudentRandom100
                    group p by MyKey(p) into g
                    select new { Test = g.Key, Count = g.Count(), Percent = (((float)g.Count() / 100) * 100 + "%") };
            this.dataGridView2.DataSource = q.ToList();            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%
        }
        NorthwindEntities dbContext = new NorthwindEntities();
        private void button34_Click(object sender, EventArgs e)
        {
            ViewsClear();
            // 年度最高銷售金額 年度最低銷售金額
            var qqq = from p in dbContext.Products
                      from od in p.Order_Details
                      join o in dbContext.Orders on od.OrderID equals o.OrderID
                      join c in dbContext.Categories on p.CategoryID equals c.CategoryID
                      select new { Year = o.OrderDate.Value.Year, CategoryName = c.CategoryName, TotalPrice = Math.Round(od.Quantity * (1 - od.Discount) * (int)od.UnitPrice, 2) };
            var q3 = (from q4 in /*qqq.OrderByDescending(n => n.TotalPrice).*/qqq.GroupBy(n => new { n.Year, n.CategoryName })
                      select new { Year = q4.Key.Year, CategoryName = q4.Key.CategoryName, TotalPrice = Math.Round(q4.Sum(n => n.TotalPrice), 2) }).OrderByDescending(n => n.TotalPrice);
            this.dataGridView3.DataSource = q3.ToList();

            // 那一年總銷售最好 ? 那一年總銷售最不好 ?  
            var q = from o in dbContext.Orders.AsEnumerable()
                    from od in o.Order_Details
                    select new { Year = o.OrderDate.Value.Year, TotalPrice = Math.Round(od.Quantity * (1 - od.Discount) * (int)od.UnitPrice, 2) };
            var qq = from q2 in q.OrderByDescending(n => n.TotalPrice).GroupBy(n => n.Year)
                     select new { Year = q2.Key, TotalPrice = Math.Round(q2.Sum(n => n.TotalPrice), 2) };
            this.dataGridView1.DataSource = qq.ToList();
            this.chart1.DataSource = qq.ToList();
            this.chart1.Series[0].XValueMember = "Year";
            this.chart1.Series[0].YValueMembers = "TotalPrice";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            ////// 那一個月總銷售最好 ? 那一個月總銷售最不好 ?
            var q1 = from o in dbContext.Orders.AsEnumerable()
                     from od in o.Order_Details
                     select new { Month = o.OrderDate.Value.Month, TotalPrice = Math.Round(od.Quantity * (1 - od.Discount) * (int)od.UnitPrice, 2) };
            var qq2 = (from q2 in q1.OrderByDescending(n => n.TotalPrice).GroupBy(n => n.Month)
                       select new { Month = q2.Key, TotalPrice = Math.Round(q2.Sum(n => n.TotalPrice), 2) }).OrderBy(n => n.Month);
            this.dataGridView1.DataSource = qq2.ToList();
            this.chart2.DataSource = qq2.ToList();
            this.chart2.Series[0].XValueMember = "Month";
            this.chart2.Series[0].YValueMembers = "TotalPrice";
            this.chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            // 每年 總銷售分析 圖   
            // 每月 總銷售分析 圖
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ViewsClear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewsClear();
            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						
            var q = from p in students_scores
                    where p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc"
                    select new { Name = p.Name, Chinese = p.Chi, English = p.Eng };
            dataGridView1.DataSource = q.ToList();
            // 找出學員 'bbb' 的成績	                          
            var qq = from p in students_scores
                     where p.Name == "bbb"
                     select new { Name = p.Name, Chinese = p.Chi, English = p.Eng, Math = p.Math };
            dataGridView2.DataSource = qq.ToList();
            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
            var qqq = from p in students_scores
                      where p.Name != "bbb"
                      select new { Name = p.Name, Chinese = p.Chi, English = p.Eng, Math = p.Math };
            dataGridView3.DataSource = qqq.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |	
            ViewsClear();
            var q = from p in students_scores
                    where p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc"
                    select new { Name = p.Name, Chinese = p.Chi, Math = p.Math };
            dataGridView1.DataSource = q.ToList();
            // 數學不及格 ... 是誰 
            var qq = from p in students_scores
                     where p.Math < 60
                     select new { Name = p.Name, Math = p.Math };
            dataGridView2.DataSource = qq.ToList();
        }
    }
}
