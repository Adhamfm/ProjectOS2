using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProjectOS2
{
    public partial class Form1 : Form
    {
        int xvalue = 0;
        int counter = 0;
        bool breaker = false;
        Chart chart = new Chart();
        Form2 frm;
        public Form1()
        {
            InitializeComponent();
            

        }


        private void chart1_Click(object sender, EventArgs e)
        {

        }
        private async void btn_add_Click(object sender, EventArgs e) {
            dataGridView1.Rows.Add("P"+counter,0,0);
            counter++;       
        }
        private async void btn_generate_Click(object sender, EventArgs e)
        {
            List<Process> processList = new List<Process>();
            int flag = 0; // Scheduler Type
            int selection = comboBox.SelectedIndex;
            this.Refresh();
            txtConsole.Clear();
            txtConsole.Refresh();
            if (dataGridView1.RowCount <= 1)
            {
                MessageBox.Show("Please enter more data before running", "Fault", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);
                return;
            }
            // flags: 0=fcfs, 1=prem SJF, 2= nonpremSJF, 3=prem priority, 4=nonprem priority, 5=rr
            flag = checkSelectedOptions(selection);
            if (flag == -1) return;

            // Read data from DataGridView

            if(flag == 3 || flag == 4)
            {
                for (int row = 0; row < dataGridView1.RowCount; row++)
                {
                    Process process = new Process();
                    for (int col = 0; col < dataGridView1.ColumnCount; col++)
                    {
                        if (dataGridView1.Rows[row].Cells[col].Value == null)
                        {
                            MessageBox.Show("Please enter all input data!", "Fault", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);
                            return;
                        }

                        if (col == 0)
                        {
                            process.name = dataGridView1.Rows[row].Cells[col].Value.ToString();
                        }
                        else if (col == 1)
                        {
                            process.arrivalTime = int.Parse(dataGridView1.Rows[row].Cells[col].Value.ToString());
                        }
                        else if (col == 2)
                        {
                            process.burstTime = int.Parse(dataGridView1.Rows[row].Cells[col].Value.ToString());
                        }
                        else if (col == 3)
                        {
                            process.priority = int.Parse(dataGridView1.Rows[row].Cells[col].Value.ToString());
                            processList.Add(process);
                        }
                    }
                }
            }
            else
            {
                for (int row = 0; row < dataGridView1.RowCount; row++)
                {
                        Process process = new Process();
                    for (int col = 0; col < dataGridView1.ColumnCount; col++)
                    {
                        if (dataGridView1.Rows[row].Cells[col].Value == null)
                        {
                            MessageBox.Show("Please enter all input data!", "Fault", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);
                            return;
                        }

                        if (col == 0)
                        {
                            process.name = dataGridView1.Rows[row].Cells[col].Value.ToString();
                        }
                        else if (col == 1)
                        {
                            process.arrivalTime = int.Parse(dataGridView1.Rows[row].Cells[col].Value.ToString());
                        }
                        else if (col == 2)
                        {
                            process.burstTime = int.Parse(dataGridView1.Rows[row].Cells[col].Value.ToString());

                            processList.Add(process);
                        }
                    }
                }                
          
            }

            btn_showform2.Enabled = true;
            GanttChart(processList, flag);
        }

        private void GanttChart(List<Process> processList, int choice)
        {
            switch (choice)
            {
                case 0: // FCFS
                    FCFS(processList);
                    break;
                case 1: // Prem SJF
                    PremSJF(processList);
                    break;
                case 2:
                    NonPremSJF(processList);
                    break;
                case 3:
                    PremPriority(processList);
                    break;
                case 4:
                    NonPremPriority(processList);
                    break;
                case 5:
                    RoundRobin(processList);
                    break;
            }
        }
        public List<Process> sortedList;
        public List<Process> SortedList()
        {
           return sortedList;
        }
        private void FCFS(List<Process> processList)
        {
            Process prev = new Process();
            sortedList = processList.OrderBy(p => p.arrivalTime).ThenBy(p => p.burstTime).ToList();
            sortedList[0].serviceTime = sortedList[0].arrivalTime;
            sortedList[0].waitingTime = 0;
            prev = sortedList[0];
            // Calculate waiting time
            foreach(Process p in sortedList.Skip(1))
            {
                p.serviceTime = prev.burstTime + prev.serviceTime;
                p.waitingTime = p.serviceTime - p.arrivalTime;
                if (p.waitingTime < 0) p.waitingTime = 0;
                prev = p;
            }

            foreach(Process p in sortedList)
            {
                p.turnaroundTime = p.burstTime + p.serviceTime;
            }

            //Write in console
            Console.WriteLine("Process ID\tWaiting Time\tTurnaround Time\n");
            foreach (Process p in sortedList)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}", p.name, p.waitingTime, p.turnaroundTime);
            }
            Console.WriteLine();
            Console.WriteLine("\nAverage waiting time: {0}", avgWaitingTime(sortedList));
        }
        private void PremSJF(List<Process> processList)
        {
            //TODO
        }
        private void NonPremSJF(List<Process> processList)
        {
            //TODO
        }
        private void PremPriority(List<Process> processList)
        {
            //TODO
        }
        private void NonPremPriority(List<Process> processList)
        {
            //TODO
        }
        private void RoundRobin(List<Process> processList)
        {
            //TODO
        }
        private int checkSelectedOptions(int selection)
        {
            int flag = 0;
            if (selection == 0) //FCFS
            {
                flag = 0;
                Console.WriteLine("FCFS");
            }
            else if (selection == 1) //SJF
            {
                if (radbtn_prem.Checked == true)
                {
                    flag = 1;
                    Console.WriteLine("Preemptive SJF");
                }
                else if (radbtn_nonprem.Checked == true)
                {
                    flag = 2;
                    Console.WriteLine("Non-Preemptive SJF");
                }
                else
                {
                    MessageBox.Show("Please choose Preemtive or Non-Preemptive", "Fault", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);
                    return -1;
                }
            }
            else if (selection == 2) // Priority
            {
                if (radbtn_prem.Checked == true)
                {
                    flag = 3;
                    Console.WriteLine("Preemptive Priority");
                }
                else if (radbtn_nonprem.Checked == true)
                {
                    flag = 4;
                    Console.WriteLine("Non-Preemptive Priority");
                }
                else
                {
                    MessageBox.Show("Please choose Preemtive or Non-Preemptive", "Fault", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);
                    return -1;
                }
            }
            else if (selection == 3) // Round Robin
            {
                flag = 5;
                if (quantumInput.Value <= 0)
                {
                    MessageBox.Show("Quantum Time must be greater than 0!", "Fault", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);
                    return -1;
                }
                Console.WriteLine("Round Robin");
            }
            return flag;
        }
        public static double avgWaitingTime(List<Process> processList)
        {
            double sum = 0;
            foreach(Process process in processList)
            {
                sum += process.waitingTime;
            }
            return sum / processList.Count();
        }
        public async void drawCharttt(List<Process> sortedList)
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
                if (breaker)
                {

                }
                // Add Bar start time  - end time
                if (start < p.arrivalTime)
                {
                    start = p.arrivalTime;
                }
                end = start + p.burstTime;
                int i = chart.Series["s1"].Points.AddXY(xvalue, start, end);
                start = end;
                chart.Series["s1"].Points[i].Label = p.name;
                chart.Series["s1"].Points[i].Font = new Font("Arial", 16, FontStyle.Bold);
                xvalue += -1;

                await Task.Delay(1000);
            }
            xvalue = 0;
        }
        public async void drawChart(List<ProcessSorted> sortedList)
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

            foreach (ProcessSorted p in sortedList)
            {
                if (breaker)
                {

                }
                // Add Bar start time  - end time
                if (start < p.startTime)
                {
                    start = p.startTime;
                }
                end = start + p.endTime;
                int i = chart.Series["s1"].Points.AddXY(xvalue, start, end);
                start = end;
                chart.Series["s1"].Points[i].Label = p.process.name;
                chart.Series["s1"].Points[i].Font = new Font("Arial", 16, FontStyle.Bold);
                xvalue += -1;
                
                await Task.Delay(1000);
            }
            xvalue = 0;
        }
        /*private async void testChart(List<Process> processList)
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
        }*/
        private void Form1_Load(object sender, EventArgs e)
        {
            TextBoxWriter writer = new TextBoxWriter(txtConsole);
            Console.SetOut(writer);
            frm = new Form2(this);
            txtConsole.Clear();
            txtConsole.Refresh();
            processBindingSource.DataSource = new List<Process>();
            processPriorityBindingSource.DataSource = new List<Process>();
            chart = frm.chart;
            
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox.SelectedIndex == -1) // No choice
            {
                btn_generate.Visible = false;
                
            }
            else if (comboBox.SelectedIndex == 0) // FCFS
            {
                if (dataGridView1.Columns["Priority"] != null)
                {
                    dataGridView1.Columns.Remove("Priority");
                }
                quantumPanel.Visible = false;
                premPanel.Enabled = false;
                dataGridView1.Visible = true;
            }
            else if (comboBox.SelectedIndex == 1) // SJF
            {
                if (dataGridView1.Columns["Priority"] != null)
                {
                    dataGridView1.Columns.Remove("Priority");
                }
                quantumPanel.Visible = false;
                premPanel.Enabled = true;
                dataGridView1.Visible = true;
            }
            else if (comboBox.SelectedIndex == 2) // Priority
            {
                if (dataGridView1.Columns["Priority"] == null)
                {
                    dataGridView1.Columns.Add("Priority", "Priority");
                }
                quantumPanel.Visible = false;
                premPanel.Enabled = true;
                dataGridView1.Visible = true;
            }
            else if (comboBox.SelectedIndex == 3) // Round Robin
            {
                if (dataGridView1.Columns["Priority"] != null)
                {
                    dataGridView1.Columns.Remove("Priority");
                }
                quantumPanel.Show();
                premPanel.Enabled = false;
                dataGridView1.Visible = true;
            }

            Form1_Load(sender, e);
        }

        private void btn_showform2_Click(object sender, EventArgs e)
        {
            frm.Show();
        }

        private void btn_rmv_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                counter--;
            }
            else
            {
                return;
            }
        }
        public int testTimer = 0;
        public int getTimer()
        {
            return testTimer;
        }
        public void setTimer(int timer)
        {
            testTimer = timer;
        }
        bool pause = true;
        public void setPause()
        {
            pause = !pause;
        }
        private async void btn_test_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_test2_Click(object sender, EventArgs e)
        {
            pause = !pause;
        }

        private async void rdn_live_CheckedChanged(object sender, EventArgs e)
        {

            if (testTimer != 0) return;
            testTimer = 0;
            while (true)
            {
                while (pause)
                {
                    testTimer++;
                    comboBox.SuspendLayout();
                    testLabel.Text = testTimer.ToString();
                    await Task.Delay(1000);
                    // comboBox.ResumeLayout();
                }
                await Task.Delay(1000);
            }
        }
    }
}
