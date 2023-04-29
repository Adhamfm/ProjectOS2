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
                setPause(false);
                setGraphRunning(false);
                rowsSaved = dataGridView1.RowCount;
                btn_prc_add.Enabled = true;
                label3.Text = "PAUSED";
                for (int row = 0; row < dataGridView1.RowCount; row++)
                {
                    dataGridView1.Rows[row].ReadOnly = true;
                }
                btn_add.Enabled = false;
            }
            dataGridView1.Rows.Add("P" + counter, 0, 0);
            counter++;
        }
        int flag = 0;
        List<Process> processList;
        private async void btn_generate_Click(object sender, EventArgs e)
        {
            processList = new List<Process>();
            
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
            try
            {

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
            } catch(Exception ex)
            {
                MessageBox.Show("Enter NUMBERS only!", "Fault", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);
                processList.Clear();
                return;
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
        int fcfs_counter = 0;
        int preemppriority_counter = 0;
        int nonPreemSJF_counter = 0;
        private void FCFS(List<Process> processList)
        {
            fcfs_counter++;
            if (!getGraphRunning())
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
                Console.WriteLine("\nAverage Waiting time: {0}", avgWaitingTime(sortedList));
                Console.WriteLine("\nAverage Turnaround time: {0}", avgTurnAroundTime(sortedList));
            }
            else // Function called more than once (FOR LIVE ADDING)
            {
                Process prev = new Process();
                int criticalIndex=0;
                int listCount = sortedList.Count;
                //Add new processes to sorted list
                foreach(Process p in newAddedProcesses)
                {
                    List<Process> test = sortedList;
                    for (int i=0; i<sortedList.Count; i++)
                    {
                        if(sortedList[i].arrivalTime == p.arrivalTime)
                        {
                            if(sortedList[i].burstTime > p.burstTime)
                            {
                                sortedList.Insert(i , p);
                                criticalIndex = i;
                            }
                            else
                            {
                                sortedList.Insert(i, p);
                                criticalIndex = i;
                            }
                            break;
                        } 
                        else if (sortedList[i].arrivalTime > p.arrivalTime)
                        {
                            sortedList.Insert(i, p);
                            criticalIndex = i;
                            break;
                        }
                    }
                    if (listCount == sortedList.Count)
                    {
                        sortedList.Add(p);
                        criticalIndex = listCount;
                    }
                    p.serviceTime = sortedList[criticalIndex-1].turnaroundTime;
                    p.waitingTime = p.serviceTime - p.arrivalTime;
                }
                prev = sortedList[criticalIndex];
                foreach (Process p in sortedList.Skip(criticalIndex+1))
                {
                    p.serviceTime = prev.burstTime + prev.serviceTime;
                    p.waitingTime = p.serviceTime - p.arrivalTime;
                    if (p.waitingTime < 0) p.waitingTime = 0;
                    prev = p;
                }
                foreach (Process p in sortedList.Skip(criticalIndex))
                {
                    p.turnaroundTime = p.burstTime + p.serviceTime;
                }
                //txtConsole.Clear();
                Console.WriteLine("AFTER ADDING PROCESS");
                Console.WriteLine("Process ID\tWaiting Time\tTurnaround Time\tService Time\n");
                foreach (Process p in sortedList)
                {
                    Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", p.name, p.waitingTime, p.turnaroundTime,p.serviceTime);
                }
                Console.WriteLine();
                Console.WriteLine("\nAverage waiting time: {0}", avgWaitingTime(sortedList));
                Console.WriteLine("\nAverage Turnaround time: {0}", avgTurnAroundTime(sortedList));
            }

        }
        int callCount = 0;
        private void PremSJF(List<Process> processList)
        {
            callCount++;
            if (callCount == 1) { sortedList = new List<Process>(); }
            else {sortedList.Clear(); }
            
            int currentTime = 0;
            int completedProcessCount = 0;
            bool anyProcessArrived = true;
            Process currentProcess = null;
           
                foreach (Process p in processList)
                {
                    Process process = new Process();
                    process.name = p.name;
                    process.burstTime = p.burstTime;
                    process.arrivalTime = p.arrivalTime;
                    save_list.Add(process);
                }
                foreach (Process process in save_list)
            {
                process.RemainingTime = process.burstTime;
            }

            while (completedProcessCount < save_list.Count)
            {
                anyProcessArrived = false;
                foreach (Process process in save_list)
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
            if(callCount > 1) Console.WriteLine("AFTER ADDING PROCESS");
            Console.WriteLine("Process ID\tWaiting Time\tTurnaround Time\n");
            foreach (Process p in save_list)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}", p.name, p.waitingTime, p.turnaroundTime);
            }
            Console.WriteLine();
            Console.WriteLine("\nAverage waiting time: {0}", avgWaitingTime(save_list));
            processList.Clear();
            
        }
        private void NonPremSJF(List<Process> processList)
        {
            //TODO
            nonPreemSJF_counter++;                 //to determine the number of times we call the function

            //when the function run for the first time
            if (nonPreemSJF_counter == 1)
            {
                            
                int min_duration = int.MaxValue;    //to include the minimum burst time in the list 
                Process shortest_process = null;    //to include the shortest process in the list
                int current_time = 0;
                sortedList = new List<Process>();

                //assign the processlist to a new list to save it for the second call
                foreach (Process p in processList)
                {
                    Process process = new Process();
                    process.name = p.name;
                    process.burstTime = p.burstTime;
                    process.arrivalTime = p.arrivalTime;
                    save_list.Add(process);
                }

                //loop over the list to find the process with the shortest job 
                //make sure that the arrival time is smaller than or equal to the current time 
                //the list must not be empty
                while (processList.Count > 0)
                {

                    shortest_process = null;
                    min_duration = int.MaxValue;

                    //find the process with the shortest job time
                    foreach (Process p in processList)
                    {
                        if ((p.burstTime < min_duration) && (p.arrivalTime <= current_time))
                        {
                            shortest_process = p;
                            min_duration = p.burstTime;
                        }
                    }

                    //calculate waiting time, service time and turnaround time
                    shortest_process.serviceTime = current_time;
                    shortest_process.waitingTime = shortest_process.serviceTime - shortest_process.arrivalTime;
                    shortest_process.turnaroundTime = shortest_process.serviceTime + shortest_process.burstTime;

                    //add the shortest process to the sorted list and remove it from the unsorted list
                    sortedList.Add(shortest_process);
                    processList.Remove(shortest_process);




                    //update the current time
                    current_time = current_time + shortest_process.burstTime;

                }

                Console.WriteLine("Process ID\tWaiting Time\tTurnaround Time\tService Time\n");
                foreach (Process p in sortedList)
                {
                    Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", p.name, p.waitingTime, p.turnaroundTime, p.serviceTime);
                }
                Console.WriteLine();
                Console.WriteLine("\nAverage waiting time: {0}", avgWaitingTime(sortedList));

            }
            else // if we add a new process to the live graph
            {
                nonPreemSJF_counter++;              //to determine the number of times we call the function
                int min_duration = int.MaxValue;    //to include the minimum burst time in the list 
                Process shortest_process = null;    //to include the shortest process in the list
                int current_time = 0;
                sortedList.Clear();                 //clear the previous sorted list

                //add the new process to the saved list
                foreach (Process p in processList)
                {
                    Process process = new Process();
                    process.name = p.name;
                    process.burstTime = p.burstTime;
                    process.arrivalTime = p.arrivalTime;
                    save_list.Add(process);
                }

                

                //apply the algorithm on the saved list
                //loop over the list to find the process with the shortest job 
                //make sure that the arrival time is smaller than or equal to the current time 
                //the list must not be empty
                while (save_list.Count > 0)
                {

                    shortest_process = null;
                    min_duration = int.MaxValue;

                    //find the process with the shortest job time
                    foreach (Process p in save_list)
                    {
                        if ((p.burstTime < min_duration) && (p.arrivalTime <= current_time))
                        {
                            shortest_process = p;
                            min_duration = p.burstTime;
                        }
                    }

                    //calculate waiting time, service time and turnaround time
                    shortest_process.serviceTime = current_time;
                    shortest_process.waitingTime = shortest_process.serviceTime - shortest_process.arrivalTime;
                    shortest_process.turnaroundTime = shortest_process.serviceTime + shortest_process.burstTime;

                    //add the shortest process to the sorted list and remove it from the unsorted list
                    sortedList.Add(shortest_process);
                    save_list.Remove(shortest_process);




                    //update the current time
                    current_time = current_time + shortest_process.burstTime;

                }

                Console.WriteLine("Process ID\tWaiting Time\tTurnaround Time\tService Time\n");
                foreach (Process p in sortedList)
                {
                    Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", p.name, p.waitingTime, p.turnaroundTime, p.serviceTime);
                }
                Console.WriteLine();
                Console.WriteLine("\nAverage waiting time: {0}", avgWaitingTime(sortedList));
            }
        }
        // this section is for preemp priority
        private void PremPriority(List<Process> processList)
        {
            preemppriority_counter++;
            if (preemppriority_counter == 1)
            {
                int currentTime = 0;
                int completedProcessCount = 0;
                bool anyProcessArrived = true;
                Process currentProcess = null;
                sortedList = new List<Process>();
                foreach(Process p in processList)
            {
                p.RemainingTime = p.burstTime;
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
                    if (currentProcess != null)
                    { currentProcess.RemainingTime--; }
                    currentTime++;
                    if (currentProcess != null && currentProcess.RemainingTime == 0)
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
                Console.WriteLine("\nAverage Turnaround time: {0}", avgTurnAroundTime(processList));
            }
            else
            {
                List <Process> processDrawn = new List <Process>();
                currentTime = getTimer();
                int completedProcessCount = 0;
                bool anyProcessArrived = true;
                Process currentProcess = null;
                foreach(Process p in processList)
                {
                    p.RemainingTime = p.burstTime;
                }
                foreach(Process p in sortedList)
                {
                    if (p.turnaroundTime <= currentTime)
                    {
                        foreach (Process proc in processList)
                        {
                            if (proc.name == p.name)
                            {
                                proc.RemainingTime = proc.RemainingTime - p.turnaroundTime + p.serviceTime;
                                processDrawn.Add(p);
                            }
                        }
                    }
                    else if(p.serviceTime < currentTime)
                    {
                    
                    foreach (Process proc in processList)
                            {
                                if (proc.name == p.name)
                                {
                                    proc.RemainingTime = proc.RemainingTime - currentTime + p.serviceTime;
                                processDrawn.Add(p);
                                }
                            }
                        
                    }
                    else { break; }
                }
                foreach(Process p in processList)
                {
                    if (p.RemainingTime == 0)
                    {
                        completedProcessCount++;
                    }
                }
                sortedList.Clear();
                foreach(Process p in processDrawn)
                {
                    sortedList.Add(p);
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
                                    if (temp.serviceTime < temp.turnaroundTime)
                                    {
                                        sortedList.Add(temp);
                                    }
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
                    if (currentProcess != null)
                    { currentProcess.RemainingTime--; }
                    currentTime++;
                    if (currentProcess != null && currentProcess.RemainingTime == 0)
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
                Console.WriteLine("\nAFTER ADDING NEW PROCESS\n");
                Console.WriteLine("Process ID\tWaiting Time\tTurnaround Time\n");
                foreach (Process p in processList)
                {
                    Console.WriteLine("{0}\t\t{1}\t\t{2}", p.name, p.waitingTime, p.turnaroundTime);
                }
                Console.WriteLine();
                Console.WriteLine("\nAverage waiting time: {0}", avgWaitingTime(processList));
                Console.WriteLine("\nAverage Turnaround time: {0}", avgTurnAroundTime(processList));
                
            }

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

        public int CompareProcesses(Process p1, Process p2)
        {
            if (p1.priority == p2.priority)
            {
                return int.Parse(p1.name.Substring(1)).CompareTo(int.Parse(p2.name.Substring(1)));
            }
            else
            {
                return p1.priority.CompareTo(p2.priority);
            }
        }
        
        int nonpreempprio_counter=0;
        private void NonPremPriority(List<Process> processList)
        {
            if (nonpreempprio_counter==0)
            {
                foreach (Process process in processList)
                {
                    process.RemainingTime = process.burstTime;
                }
                nonpreempprio_counter++;
                sorted = SortByArrival(processList);
                sortedList = new List<Process>();

                //the first arrived will always be executed first, so it is added to our sortedList
                sortedList.Add(sorted[0]);       
                Process old = new Process();
                old = sortedList[0];

                sorted[0].serviceTime = sorted[0].arrivalTime;    
                endOfBurst = old.serviceTime + old.burstTime;      
                sorted[0].waitingTime = 0;
                sorted.RemoveAt(0);                   //no longer needed in the sorted list, so i remove it

                while (sorted.Count > 0)
                {
                    for (int i = 0; i < sorted.Count; i++)
                    {
                        if (sorted.Count > 0 && sorted[i].arrivalTime <= endOfBurst)
                        {
                                //if a process arrives at a time that is <= endofburst of the previous process
                                //then it is added to the preready list
                            preready.Add(sorted[i]);
                            sorted.Remove(sorted[i]);
                            i--;
                        }
                        else
                            continue;
                    }
                    preready.Sort(CompareProcesses);
                    
                    for (int j = 0; j < preready.Count; j++)
                    {
                        sortedList.Add(preready[j]);
                        preready[j].serviceTime = old.burstTime + old.serviceTime;
                        preready[j].waitingTime = preready[j].serviceTime - preready[j].arrivalTime;
                        endOfBurst += sortedList.Last().burstTime;
                        old = preready[j];
                        preready.Remove(preready[j]);
                        j--;
                        for (int i = 0; i < sorted.Count; i++)
                        {
                            if (sorted.Count > 0 && sorted[i].arrivalTime <= endOfBurst)
                            {
                                preready.Add(sorted[i]);
                                sorted.Remove(sorted[i]);
                                i--;
                            }
                            else
                                continue;
                        }
                        preready.Sort(CompareProcesses);
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
            else    //in case process is added live
            {
                sortedList.Clear();              
                sorted = SortByArrival(processList);
                sortedList.Add(sorted[0]);       
                Process old = new Process();
                old = sortedList[0];

                sorted[0].serviceTime = sorted[0].arrivalTime;    
                endOfBurst = old.serviceTime + old.burstTime;      
                sorted[0].waitingTime = 0;
                sorted.RemoveAt(0);                    

                while (sorted.Count > 0)
                {
                    for (int i = 0; i < sorted.Count; i++)
                    {
                        if (sorted.Count > 0 && sorted[i].arrivalTime <= endOfBurst)
                        {
                            preready.Add(sorted[i]);
                            sorted.Remove(sorted[i]);
                            i--;
                        }
                        else
                            continue;
                    }
                    preready.Sort(CompareProcesses);
                    for (int j = 0; j < preready.Count; j++)
                    {
                        sortedList.Add(preready[j]);
                        preready[j].serviceTime = old.burstTime + old.serviceTime;
                        preready[j].waitingTime = preready[j].serviceTime - preready[j].arrivalTime;
                        endOfBurst += sortedList.Last().burstTime;
                        old = preready[j];
                        preready.Remove(preready[j]);
                        j--;
                        for (int i = 0; i < sorted.Count; i++)
                        {
                            if (sorted.Count > 0 && sorted[i].arrivalTime <= endOfBurst)
                            {
                                preready.Add(sorted[i]);
                                sorted.Remove(sorted[i]);
                                i--;
                            }
                            else
                                continue;
                        }
                        preready.Sort(CompareProcesses);
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
        }
        
        
        bool RoundRobin_First = true;
        List<Process> save_list = new List<Process>();
        List<Process> temp_save = new List<Process>();
        private void RoundRobin(List<Process> processList)
        {

            

            if (RoundRobin_First)
            {
                foreach(Process p in processList)
                {
                    Process process = new Process();
                    process.name = p.name;
                    process.burstTime = p.burstTime;
                    process.arrivalTime = p.arrivalTime;
                    save_list.Add(process);
                }

                RoundRobin_First = false;

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
                            //processList.RemoveAt(i);
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
                Console.WriteLine("\nAverage Turnaround time: {0}", avgTurnAroundTime(calculate_list));

            }

            else
            {
                foreach (Process p in processList)
                {
                    Process process = new Process();
                    process.name = p.name;
                    process.burstTime = p.burstTime;
                    process.arrivalTime = p.arrivalTime;
                    save_list.Add(process);
                }

                temp_save.Clear();

                foreach (Process p in save_list)
                {
                    Process process = new Process();
                    process.name = p.name;
                    process.burstTime = p.burstTime;
                    process.arrivalTime = p.arrivalTime;
                    temp_save.Add(process);
                }

                int quantum = (int)quantumInput.Value;
                int current_time = 0;
                int current_executed_time = 0;
                int index = 0;
                int processList_count = temp_save.Count;

                sortedList.Clear();
                List<Process> waiting_list = new List<Process>();
                List<Process> calculate_list = new List<Process>();


                //Assign remaining time to burst time
                foreach (Process p in temp_save)
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
                        if (temp_save[i].arrivalTime == current_time)
                        {
                            waiting_list.Add(temp_save[i]);
                            temp_save.RemoveAt(i);
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
                    processList_count = temp_save.Count;

                    while (count < processList_count)
                    {
                        if (temp_save[i].arrivalTime == current_time)
                        {
                            waiting_list.Add(temp_save[i]);
                            temp_save.RemoveAt(i);
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

                Console.WriteLine();
                Console.WriteLine("\nAFTER ADDING NEW PROCESS\n");
                Console.WriteLine("Process ID\tWaiting Time\tTurnaround Time\n");
                foreach (Process p in calculate_list)
                {
                    Console.WriteLine("{0}\t\t{1}\t\t{2}", p.name, p.waitingTime, p.turnaroundTime);
                }
                Console.WriteLine();
                Console.WriteLine("\nAverage waiting time: {0}", avgWaitingTime(calculate_list));
                Console.WriteLine("\nAverage Turnaround time: {0}", avgTurnAroundTime(calculate_list));

            }
 
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
        public static double avgTurnAroundTime(List<Process> processList)
        {
            double sum = 0;
            foreach (Process process in processList)
            {
                sum += process.turnaroundTime;
            }
            return sum / processList.Count();
        }



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
        public void setPause(bool pa)
        {
            pause = pa;
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
                        btn_generate.Enabled = false;
                    }
                    else
                    {
                        btn_generate.Enabled = true;
                        btn_prc_add.Enabled = false;
                    }
                    testTimer++;
                    comboBox.SuspendLayout();
                    //testLabel.Text = testTimer.ToString();
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

            label3.Text = " ";
            bool added = false;
            // Get new data
            try
            {
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
                                if (process.arrivalTime < getTimer())
                                {
                                    throw new ArgumentException();
                                }
                            }
                            else if (col == 2)
                            {
                                process.burstTime = int.Parse(dataGridView1.Rows[row].Cells[col].Value.ToString());
                            }
                            else if (col == 3)
                            {
                                process.priority = int.Parse(dataGridView1.Rows[row].Cells[col].Value.ToString());
                                newAddedProcesses.Add(process);
                                processList.Add(process);
                                added = true;
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
                                if (process.arrivalTime < getTimer())
                                {
                                    throw new ArgumentException();
                                }
                            }
                            else if (col == 2)
                            {
                                process.burstTime = int.Parse(dataGridView1.Rows[row].Cells[col].Value.ToString());
                                newAddedProcesses.Add(process);
                                processList.Add(process);
                                added = true;
                            }
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Make sure Arrival time > Timer", "Fault", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter NUMBERS only!", "Fault", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);
                if (added)
                {
                    processList.RemoveAt(processList.Count - 1);
                    newAddedProcesses.RemoveAt(newAddedProcesses.Count - 1);
                }
                return;
            }
            setPause(true);
            setGraphRunning(true);
            btn_prc_add.Enabled = false;
            for (int row = rowsSaved; row < dataGridView1.RowCount; row++)
            {
                dataGridView1.Rows[row].ReadOnly = true;
            }
            rowsSaved = dataGridView1.RowCount;
            btn_add.Enabled = true;
            // call scheduler function
            // TODO call the function... 
            // New Processes is in **newAddedProcesses**;
            GanttChart(processList, flag);
            //foreach (Process p in newAddedProcesses)
            //{
            //    Console.WriteLine("{0}\t\t{1}\t\t{2}", p.name, p.arrivalTime, p.burstTime);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void rdn_instant_CheckedChanged(object sender, EventArgs e)
        {
            btn_generate.Enabled = true;
        }
    }
}
