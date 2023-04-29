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
        int timer = 0;
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
            if (mainForm.rdn_instant.Checked == true)
            {
                drawChartInstant(mylist);
            }
            else if (mainForm.rdn_live.Checked == true)
            {
                drawChartLive(mylist);
            }
            else
            {
                MessageBox.Show("Please choose Live or Insant", "Fault", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);
                return;
            }
        }
        private async void button2_Click2(object sender, EventArgs e)
        {
            mainForm.setTimer(0);
            button2.Enabled = false;
            while(true)
            {
                timer = mainForm.getTimer();
                label1.Text = timer.ToString();
                await Task.Delay(100);
            }
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
            this.button2.Click += new System.EventHandler(this.button2_Click);
        }
        int xvalue = 0;
        public void drawChartInstant(List<Process> sortedList)
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
            Random random = new Random();
            

            foreach (Process p in sortedList)
            {
                Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                // Add Bar start time  - end time
                //if (start < p.arrivalTime)
                //{
                //    start = p.arrivalTime;
                //}
                //end = start + p.burstTime;
                int i = chart.Series["s1"].Points.AddXY(1, p.serviceTime, p.turnaroundTime);
                //start = end;
                //chart.Series["s1"].Points[i].LabelToolTip = String.Format("{0} - {1} - {2}", p.serviceTime, p.name, p.turnaroundTime);
                chart.Series["s1"].Points[i].Label = p.name;
                chart.Series["s1"].Points[i].ToolTip = String.Format("{0} - {1} - {2}", p.serviceTime, p.name, p.turnaroundTime);
                chart.Series["s1"].Points[i].Font = new Font("Arial", 12, FontStyle.Bold);
                chart.Series["s1"].Points[i].Color = color;
                xvalue += -1;

                //await Task.Delay(1000);
            }
            //chart.Series["s1"].Points.RemoveAt(3);
            xvalue = 0;
        }

        public async void drawChartLive(List<Process> sortedList)
        {
            button3.Visible = true;
            // Chart chart = new Chart();
            // chart.Anchor = AnchorStyles.Left & AnchorStyles.Right & AnchorStyles.Bottom & AnchorStyles.Top;
            // chart.Dock = DockStyle.Top;
            //// chart.Width = 1283;
            // //chart.Height = 541;

            // ChartArea chartArea = new ChartArea();
            //chart.ChartAreas.Add(chartArea);
            mainForm.setGraphRunning(true);
            var objChart = chart.ChartAreas[0];
            // this.Controls.Add(chart);
            objChart.AxisY.Maximum = sortedList.Last().turnaroundTime+10;
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
            Random random = new Random();

            int item = 0;
            while (item < sortedList.Count) 
            {
                Process p = sortedList[item];
                int x = p.serviceTime;

                int prevCounter = -1;
                Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                // Add Bar start time  - end time
                //if (start < p.arrivalTime)
                //{
                //    start = p.arrivalTime;
                //}
                //end = start + p.burstTime;
                 int i = chart.Series["s1"].Points.AddXY(1, p.serviceTime, p.serviceTime);
                 //chart.Series["s1"].Points.RemoveAt(i);
                while (mainForm.getTimer() < p.turnaroundTime)
                {
                    if (prevCounter == mainForm.getTimer())
                    {
                        await Task.Delay(100);
                        continue;
                    }
                    prevCounter = mainForm.getTimer();
                    chart.Series["s1"].Points.RemoveAt(i);
                    i = chart.Series["s1"].Points.AddXY(1, p.serviceTime, x++);
                    await Task.Delay(100);
                }
                chart.Series["s1"].Points.RemoveAt(i);
                i = chart.Series["s1"].Points.AddXY(1, p.serviceTime, p.turnaroundTime);
                //start = end;
                //chart.Series["s1"].Points[i].LabelToolTip = String.Format("{0} - {1} - {2}", p.serviceTime, p.name, p.turnaroundTime);
                chart.Series["s1"].Points[i].Label = p.name;
                chart.Series["s1"].Points[i].ToolTip = String.Format("{0} - {1} - {2}", p.serviceTime, p.name, p.turnaroundTime);
                chart.Series["s1"].Points[i].Font = new Font("Arial", 12, FontStyle.Bold);
                chart.Series["s1"].Points[i].Color = color;
                xvalue += -1;

                //await Task.Delay(1000);
                item++;
            }

            xvalue = 0;
            button2.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.setPause(false);
            if (button3.Text != "Resume Timer")
                button3.Text = "Resume Timer";
            else
                button3.Text = "Pause Timer";
        }
    }
}
