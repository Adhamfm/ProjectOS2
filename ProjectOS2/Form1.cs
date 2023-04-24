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
            objChart.AxisY.Minimum = 0;
            chart.Series.Clear();

            //random COlor
            //Random random = new Random();

            List<Process> processList = processBindingSource.DataSource as List<Process>;
            //processBindingSource.DataSource = null;

            //testChart(processList);
            List<Process> sortedList = FCFSChart(processList);
            drawChart(sortedList);
        }

        private List<Process> FCFSChart(List<Process> processList)
        {
            int start = 0;
            int end = 0;
            // Sorts array
            List<Process> SortedList = processList.OrderBy(p=>p.arrivalTime).ThenBy(p => p.processTime).ToList();
            // Sorted by 2 values
            // List<Process> SortedList = processList.OrderBy(p=>p.arrivalTime).ThenBy(p=>p.processTime);
            return SortedList;
        }
        private async void drawChart(List<Process> sortedList)
        {
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
                if (start < p.arrivalTime)
                {
                    start = p.arrivalTime;
                }
                end = start + p.processTime;
                int i = chart.Series["s1"].Points.AddXY(xvalue, start, end);
                start = end;
                chart.Series["s1"].Points[i].Label = p.name;
                chart.Series["s1"].Points[i].Font = new Font("Arial", 16, FontStyle.Bold);
                xvalue += -1;
                await Task.Delay(1000);
            }
            xvalue = 0;
        }
        private async void testChart(List<Process> processList)
        {
           // processBindingSource.DataSource = null;
            chart.Series.Add("s1");
            xvalue = 0;
            //Style Bar
            chart.Series["s1"].Color = Color.BlueViolet;
            chart.Series["s1"].Legend = "Legend1";
            chart.Series["s1"].ChartArea = "ChartArea1";
            chart.Series["s1"].BorderColor = Color.Black;
            chart.Series["s1"].BorderWidth = 2;
            chart.Series["s1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
            chart.Series["s1"].YValuesPerPoint = 2;                                                                                   
            foreach (Process p in processList)
            {
                //chart.Series["s1"].Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                
               
                // Add Bar start time  - end time
                int i = chart.Series["s1"].Points.AddXY(xvalue, p.arrivalTime, p.processTime);
                chart.Series["s1"].Points[i].Label = p.name;
                //if (i == 0)
                //chart.Series["s1"].Points[i].Color = Color.Black;
                chart.Series["s1"].Points[i].Font = new Font("Arial", 16, FontStyle.Bold);
                xvalue += -1;
                await Task.Delay(5000);
            }
            xvalue = 0;
            processBindingSource.DataSource = new List<Process>();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            processBindingSource.DataSource = new List<Process>();
        }
    }
}
