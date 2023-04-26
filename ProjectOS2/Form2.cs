using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectOS2
{
    public partial class Form2 : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        public Form2()
        {
            InitializeComponent();
        }
        private Form1 mainForm = null;
        List<Process> mylist;
        public Form2(Form callingForm)
        {
            mainForm = callingForm as Form1;
           // mylist = mainForm.SortedList();
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            mylist = mainForm.SortedList();
            drawChart(mylist);
        }
        private void test()
        {
            for (int i = 0; i < 100; i++)
            {
                int width = rd.Next(0, this.Width);
                int height = rd.Next(50, this.Height);
                this.CreateGraphics().DrawEllipse(new Pen(Brushes.Red, 1), new Rectangle(width, height, 10, 10));
            }
        }
        Random rd;
        private void Form2_Load(object sender, EventArgs e)
        {
            rd = new Random();
        }
        int xvalue = 0;
        public async void drawChart(List<Process> sortedList)
        {
            // Chart chart = new Chart();
            // chart.Anchor = AnchorStyles.Left & AnchorStyles.Right & AnchorStyles.Bottom & AnchorStyles.Top;
            // chart.Dock = DockStyle.Top;
            //// chart.Width = 1283;
            // //chart.Height = 541;

            // ChartArea chartArea = new ChartArea();
            //chart.ChartAreas.Add(chartArea);
            var objChart = chart.ChartAreas[0];
            // this.Controls.Add(chart);
            objChart.AxisY.Minimum = 0;
            chart.Series.Clear();
            int start = 0;
            int end = 0;
            chart.Series.Add("s1");
            xvalue = 0;
            //Style Bar
            chart.Series["s1"].Color = Color.BlueViolet;
            //chart.Series["s1"].Legend = "Legend1";
            //chart.Series["s1"].ChartArea = "ChartArea1";
            chart.Series["s1"].BorderColor = Color.Black;
            chart.Series["s1"].BorderWidth = 2;
            chart.Series["s1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
            chart.Series["s1"].YValuesPerPoint = 2;

            foreach (Process p in sortedList)
            { 
                // Add Bar start time  - end time
                //if (start < p.arrivalTime)
                //{
                //    start = p.arrivalTime;
                //}
                //end = start + p.burstTime;
                int i = chart.Series["s1"].Points.AddXY(xvalue, p.serviceTime, p.turnaroundTime);
                //start = end;
                chart.Series["s1"].Points[i].Label = p.name;
                chart.Series["s1"].Points[i].Font = new Font("Arial", 16, FontStyle.Bold);
                xvalue += -1;

               // await Task.Delay(1000);
            }
            xvalue = 0;
        }
    }
}
