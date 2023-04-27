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
        int rowsSaved = 0;
        private async void btn_add_Click(object sender, EventArgs e) {
            if (getGraphRunning())
            {
                setPause();
                setGraphRunning(false);
                rowsSaved = dataGridView1.RowCount;
                btn_prc_add.Enabled = true;
                label3.Text = "PAUSED";
                for (int row = 0; row < dataGridView1.RowCount; row++)
                {
                    dataGridView1.Rows[row].ReadOnly = true;
                }
            }
            dataGridView1.Rows.Add("P" + counter, 0, 0);
            counter++;
        }
        int flag = 0;
        private async void btn_generate_Click(object sender, EventArgs e)
        {
            List<Process> processList = new List<Process>();
            btn_generate.Enabled = false;
            flag = 0; // Scheduler Type
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

            if (flag == 3 || flag == 4)
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
            foreach (Process p in sortedList.Skip(1))
            {
                p.serviceTime = prev.burstTime + prev.serviceTime;
                p.waitingTime = p.serviceTime - p.arrivalTime;
                if (p.waitingTime < 0) p.waitingTime = 0;
                prev = p;
            }

            foreach (Process p in sortedList)
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
            int currentTime = 0;
            int completedProcessCount = 0;
            bool anyProcessArrived = true;
            Process currentProcess = null;
            sortedList = new List<Process>();
            foreach (Process process in processList)
            {
                process.RemainingTime = process.burstTime;
            }
            while (completedProcessCount < processList.Count)
            {
                anyProcessArrived = false;
                foreach (Process process in processList)
                {
                    if (process.arrivalTime <= currentTime)
                    {
                        anyProcessArrived = true;
                        if ((currentProcess == null || process.RemainingTime < currentProcess.RemainingTime) && process.RemainingTime > 0)
                        {
                            if (currentProcess != null)
                            {
                                Process temp = new Process();
                                currentProcess.turnaroundTime = currentTime;
                                temp.name = currentProcess.name;
                                temp.serviceTime = currentProcess.serviceTime;
                                temp.turnaroundTime = currentProcess.turnaroundTime;
                                sortedList.Add(temp);
                            }
                            currentProcess = process;
                            currentProcess.serviceTime = currentTime;
                        }
                    }
                }
                if (!anyProcessArrived)
                {
                    currentTime++;
                    continue;
                }
                currentProcess.RemainingTime--;
                currentTime++;
                if (currentProcess.RemainingTime == 0)
                {
                    Process temp = new Process();
                    currentProcess.turnaroundTime = currentTime;
                    temp.name = currentProcess.name;
                    temp.serviceTime = currentProcess.serviceTime;
                    temp.turnaroundTime = currentProcess.turnaroundTime;
                    sortedList.Add(temp);
                    completedProcessCount++;
                    currentProcess.turnaroundTime = currentTime - currentProcess.arrivalTime;
                    currentProcess.waitingTime = currentProcess.turnaroundTime - currentProcess.burstTime;
                    currentProcess = null;
                }
            }
            Console.WriteLine("Process ID\tWaiting Time\tTurnaround Time\n");
            foreach (Process p in processList)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}", p.name, p.waitingTime, p.turnaroundTime);
            }
            Console.WriteLine();
            Console.WriteLine("\nAverage waiting time: {0}", avgWaitingTime(processList));
        }
        private void NonPremSJF(List<Process> processList)
        {
            //TODO
        }
        // this section is for preemp priority
        private void PremPriority(List<Process> processList)
        {
            int currentTime = 0;
            int completedProcessCount = 0;
            bool anyProcessArrived = true;
            Process currentProcess = null;
            sortedList = new List<Process>();
            foreach (Process process in processList)
            {
                process.RemainingTime = process.burstTime;
            }
            while (completedProcessCount < processList.Count)
            {
                anyProcessArrived = false;
                foreach (Process process in processList)
                {
                    if (process.arrivalTime <= currentTime)
                    {
                        anyProcessArrived = true;
                        if ((currentProcess == null || process.priority < currentProcess.priority) && process.RemainingTime > 0)
                        {
                            if (currentProcess != null)
                            {
                                Process temp = new Process();
                                currentProcess.turnaroundTime = currentTime;
                                temp.name = currentProcess.name;
                                temp.serviceTime = currentProcess.serviceTime;
                                temp.turnaroundTime = currentProcess.turnaroundTime;
                                sortedList.Add(temp);
                            }
                            currentProcess = process;
                            currentProcess.serviceTime = currentTime;
                        }
                    }
                }
                if (!anyProcessArrived)
                {
                    currentTime++;
                    continue;
                }
                currentProcess.RemainingTime--;
                currentTime++;
                if (currentProcess.RemainingTime == 0)
                {
                    Process temp = new Process();
                    currentProcess.turnaroundTime = currentTime;
                    temp.name = currentProcess.name;
                    temp.serviceTime = currentProcess.serviceTime;
                    temp.turnaroundTime = currentProcess.turnaroundTime;
                    sortedList.Add(temp);
                    completedProcessCount++;
                    currentProcess.turnaroundTime = currentTime - currentProcess.arrivalTime;
                    currentProcess.waitingTime = currentProcess.turnaroundTime - currentProcess.burstTime;
                    currentProcess = null;
                }
            }
            Console.WriteLine("Process ID\tWaiting Time\tTurnaround Time\n");
            foreach (Process p in processList)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}", p.name, p.waitingTime, p.turnaroundTime);
            }
            Console.WriteLine();
            Console.WriteLine("\nAverage waiting time: {0}", avgWaitingTime(processList));
        
    }
       
        // end of the section

        //non preemp prio
        public static List<Process> sorted = new List<Process>();
        public static List<Process> preready = new List<Process>();
        public static int currentTime;
        public static int endOfBurst;
        public static List<Process> SortByArrival(List<Process> listOFProcesses)
        {
            List<Process> sorted = new List<Process>(listOFProcesses);
            sorted.Sort((p1, p2) =>
            {
                if (p1.priority != p2.priority && p1.arrivalTime == p2.arrivalTime)
                {//lw m3ndhom4 nfs el priority bs nfs el arrival time then sort wrt to their priorities
                    return p1.priority.CompareTo(p2.priority);
                }
                else if (p1.priority == p2.priority && p1.arrivalTime == p2.arrivalTime) //lw 3ndhom nfs el arrival wel priority time w nfs el priority
                {                                                                   //then compare their pid            
                    return int.Parse(p1.name.Substring(1)).CompareTo(int.Parse(p2.name.Substring(1)));
                }
                else if (p1.priority != p2.priority && p1.arrivalTime != p2.arrivalTime)
                {//lw m3ndhom4 nfs el priority bs nfs el arrival time then sort wrt to their priorities
                    return p1.arrivalTime.CompareTo(p2.arrivalTime);
                }
                else if (p1.priority == p2.priority && p1.arrivalTime != p2.arrivalTime)
                {
                    return p1.arrivalTime.CompareTo(p2.arrivalTime);
                }
                else
                {  
                    return p1.arrivalTime.CompareTo(p2.arrivalTime);
                }
            });
            return sorted;
        }
        private void NonPremPriority(List<Process> processList)
        {
            foreach (Process process in processList)
            {
                process.RemainingTime = process.burstTime;
            }
            sorted = SortByArrival(processList);
            //now the processes are sorted, and i assigned the current time to the arrival time of the process which arrived first (whatever b2a its priority) 
            sortedList = new List<Process>();

            //awl arrived process will always be executed first b3d kda b2a nshoof n3eed el trteeb w kda
            sortedList.Add(sorted[0]);       //7ttha fel readylist
            Process old = new Process();
            old = sortedList[0];

            sorted[0].serviceTime = sorted[0].arrivalTime;    //1
            endOfBurst = old.serviceTime + old.burstTime;      //hena =7 msln 
            sorted[0].waitingTime = 0;
            //sorted[0].turnaroundTime = endOfBurst - old.serviceTime;
            sorted.RemoveAt(0);                   //4eltaha mn el sorted list  

            while (sorted.Count > 0)
            {
                for (int i = 0; i < sorted.Count; i++)
                {
                    if (sorted.Count > 0 && sorted[i].arrivalTime <= endOfBurst)
                    {
                        //hena h3ml sort by priority el priority el as8r howa el hy execute el awl 
                        preready.Add(sorted[i]);
                        sorted.Remove(sorted[i]);
                        i--;
                    }
                    else
                        continue;
                }
                //ascneding order of processes in preready based on their priorities
                preready.Sort((p1, p2) => p1.priority.CompareTo(p2.priority));

                //changes done here
                for (int j = 0; j < preready.Count; j++)
                {
                    //Console.WriteLine("inside pre ready b3d el sorting " + preready[j].pid);
                    sortedList.Add(preready[j]);
                    preready[j].serviceTime = old.burstTime + old.serviceTime;
                    preready[j].waitingTime = preready[j].serviceTime - preready[j].arrivalTime;
                    endOfBurst += sortedList.Last().burstTime;
                    old = preready[j];
                    //Console.WriteLine("accumu: " + endOfBurst);
                    preready.Remove(preready[j]);
                    j--;
                    for (int i = 0; i < sorted.Count; i++)
                    {
                        if (sorted.Count > 0 && sorted[i].arrivalTime <= endOfBurst)
                        {
                            //hena h3ml sort by priority el priority el as8r howa el hy execute el awl 
                            preready.Add(sorted[i]);
                            sorted.Remove(sorted[i]);
                            i--;
                        }
                    }
                    preready.Sort((p1, p2) => p1.priority.CompareTo(p2.priority));
                }
                foreach (Process p in sortedList)
                {
                    p.turnaroundTime = p.burstTime + p.serviceTime;
                }

                // Execute processes in the sortedList
                Console.WriteLine("Process ID\tWaiting Time\tTurnaround Time\n");
                Process runningProcess = new Process();
                foreach (Process p in sortedList)
                {
                    runningProcess = p;
                    Console.WriteLine("{0}\t\t{1}\t\t{2}", p.name, p.waitingTime, p.turnaroundTime - p.arrivalTime);
                    while (p.RemainingTime != 0)
                    {
                        runningProcess.RemainingTime--;
                    }
                    runningProcess = null;
                }

                Console.WriteLine("\nAverage waiting time: {0}", avgWaitingTime(processList));
            }
        }
        private void RoundRobin(List<Process> processList)
        {
            //TODO
            int quantum = (int)quantumInput.Value;
            int current_time = 0;
            int current_executed_time = 0;
            int index = 0;
            int processList_count = processList.Count;

            sortedList = new List<Process>();
            List<Process> waiting_list = new List<Process>();
            List<Process> calculate_list = new List<Process>();


            //Assign remaining time to burst time
            foreach (Process p in processList)
            {
                p.RemainingTime = p.burstTime;
            }


            //Add processes to waiting list on each second
            do
            {
                bool added_process = false;
                int i = 0;
                int count = 0;

                while (count < processList_count)
                {
                    if (processList[i].arrivalTime == current_time)
                    {
                        waiting_list.Add(processList[i]);
                        processList.RemoveAt(i);
                        added_process = true;
                        i--;
                    }
                    i++;
                    count++;
                }

                if (!added_process)
                {
                    current_time++;
                }


            }
            while (waiting_list.Count == 0);

            Process current_process = waiting_list[index];
            current_process.serviceTime = current_time;


            //Execute processes in waiting list on each second
            //Add them to sorted list
            //Non current increase waiting time

            while (waiting_list.Count > 0)
            {
                int i = 0;
                int count = 0;
                processList_count = processList.Count;

                while (count < processList_count)
                {
                    if (processList[i].arrivalTime == current_time)
                    {
                        waiting_list.Add(processList[i]);
                        processList.RemoveAt(i);
                        i--;
                    }
                    i++;
                    count++;
                }

                //Normal Execution (Process Execution time < Quantum time)
                if (current_executed_time < quantum)
                {
                    current_process.RemainingTime--;
                    current_executed_time++;

                    if (current_process.RemainingTime == 0)
                    {
                        //current_process.turnaroundTime = current_time - current_process.arrivalTime;
                        //sortedList.Add(current_process);

                        if (waiting_list.Count > 1)
                        {
                            //Temp Process for calculation
                            Process temp_calc = new Process();
                            temp_calc.name = current_process.name;
                            temp_calc.burstTime = current_process.burstTime;
                            temp_calc.turnaroundTime = current_time + 1;
                            temp_calc.waitingTime = temp_calc.turnaroundTime - temp_calc.burstTime;
                            calculate_list.Add(temp_calc);

                            waiting_list.Remove(current_process);
                            index--;
                        }

                        else
                        {
                            //Temp Process for calculation
                            Process temp_calc = new Process();
                            temp_calc.name = current_process.name;
                            temp_calc.burstTime = current_process.burstTime;
                            temp_calc.turnaroundTime = current_time + 1;
                            temp_calc.waitingTime = temp_calc.turnaroundTime - temp_calc.burstTime;
                            calculate_list.Add(temp_calc);

                            current_process.turnaroundTime = current_time + 1;
                            sortedList.Add(current_process);
                            waiting_list.Remove(current_process);
                        }

                    }

                }

                //Switch process
                //Try and Catch
                else
                {
                    //Add already executed process to sorted list
                    //Clone Current Process
                    Process temp = new Process();
                    temp.name = current_process.name;
                    temp.turnaroundTime = current_time;
                    temp.serviceTime = current_process.serviceTime;
                    temp.waitingTime = current_process.waitingTime;

                    sortedList.Add(temp);

                    //Switch to next process
                    try
                    {
                        current_process = waiting_list[++index];
                    }

                    catch
                    {
                        index = 0;
                        current_process = waiting_list[index];
                    }

                    //Assign service time and waiting time
                    current_process.serviceTime = current_time;
                    current_process.waitingTime = current_process.serviceTime - current_process.arrivalTime;


                    current_executed_time = 0;
                    current_time--;
                }




                current_time++;
            }


            Console.WriteLine("Process ID\tWaiting Time\tTurnaround Time\n");
            foreach (Process p in calculate_list)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}", p.name, p.waitingTime, p.turnaroundTime);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\nAverage waiting time: {0}", avgWaitingTime(calculate_list));
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
            if (dataGridView1.Rows.Count != rowsSaved)
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
        bool graphRunning = false;
        public void setGraphRunning(bool running)
        {
            graphRunning = running;
        }
        public bool getGraphRunning()
        {
            return graphRunning;
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
                    if (getGraphRunning())
                    {
                       // btn_prc_add.Enabled = true;
                    }
                    else
                    {
                        btn_prc_add.Enabled = false;
                    }
                    testTimer++;
                    comboBox.SuspendLayout();
                    testLabel.Text = testTimer.ToString();
                    await Task.Delay(1000);
                    // comboBox.ResumeLayout();
                }
                await Task.Delay(1000);
            }
        }
        List<Process> newAddedProcesses;
        private void btn_prc_add_Click(object sender, EventArgs e)
        {
            newAddedProcesses = new List<Process>();
            setPause();
            setGraphRunning(true);
            btn_prc_add.Enabled=false;
            label3.Text = " ";
            // Get new data
            if (flag == 3 || flag == 4)
            {
                for (int row = rowsSaved; row < dataGridView1.RowCount; row++)
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
                            newAddedProcesses.Add(process);
                        }
                    }
                }
            }
            else
            {
                for (int row = rowsSaved; row < dataGridView1.RowCount; row++)
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

                            newAddedProcesses.Add(process);
                        }
                    }
                }
            }
            for (int row = rowsSaved; row < dataGridView1.RowCount; row++)
            {
                dataGridView1.Rows[row].ReadOnly = true;
            }
            rowsSaved = dataGridView1.RowCount;
            // call scheduler function
           // TODO call the function... 
           // New Processes is in **newAddedProcesses**;
           foreach (Process p in newAddedProcesses)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}", p.name, p.arrivalTime, p.burstTime);
            }
        }
    }
}
