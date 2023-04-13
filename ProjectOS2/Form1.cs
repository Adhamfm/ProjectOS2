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
    public partial class Form1 : Form
    {
        int xvalue = 0;
        public Form1()
        {
            InitializeComponent();

        }


        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            
            var objChart = chart.ChartAreas[0];
            objChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            objChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            objChart.AxisY.Minimum = 0;
            chart.Series.Clear();
            //random COlor
            //Random random = new Random();

            
            testChart();
        }
        private async void testChart()
        {
            chart.Series.Add("s1");
            xvalue = 0;
            foreach (Process p in processBindingSource.DataSource as List<Process>)
            {
                //chart.Series["s1"].Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                //Style Bar
                chart.Series["s1"].Color = Color.BlueViolet;
                chart.Series["s1"].Legend = "Legend1";
                chart.Series["s1"].ChartArea = "ChartArea1";
                chart.Series["s1"].BorderColor = Color.Black;
                chart.Series["s1"].BorderWidth = 2;
                chart.Series["s1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
                chart.Series["s1"].YValuesPerPoint = 2;
               
                // Add Bar start time  - end time
                int i = chart.Series["s1"].Points.AddXY(xvalue, p.arrivalTime, p.processTime);
                chart.Series["s1"].Points[i].Label = p.name;
                if (i == 0)
                chart.Series["s1"].Points[i].Color = Color.Black;
                chart.Series["s1"].Points[i].Font = new Font("Arial", 16, FontStyle.Bold);
                xvalue += -1;
                await Task.Delay(1000);
            }
            xvalue = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            processBindingSource.DataSource = new List<Process>();
        }
    }
}
