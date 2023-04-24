using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Scheduler
{
    public class Process
    {
        private string process_name;
        private int process_remainingtime;
        private int process_arrivaltime;
        private int process_priority;       //Priority of process (1 = MAX) and (5 = MIN)
        private int process_ID;
        private static int processes_count = 0;


        //Constructor for process with parameters (Process Name, Process Remaining Time, Process Arrival Time, Process ID)
        public Process(string aProcessName, int aProcessRemainingTime, int aProcessArrivingTime)
        {
            ProcessName = aProcessName;
            ProcessRemainingTime = aProcessRemainingTime;
            ProcessArrivalTime = aProcessArrivingTime;
            process_ID = processes_count;
            processes_count++;
        }

        //Constructor of process with parameters (Process Name, Process Remaining Time, Process Priority, Process Arival Time, Process ID)
        //Process Priority is used for Priority Scheduling
        public Process(string aProcessName, int aProcessRemainingTime, int aProcessArrivingTime, int aProcessPriority)
        {
            ProcessName = aProcessName;
            ProcessRemainingTime = aProcessRemainingTime;
            ProcessArrivalTime = aProcessArrivingTime;
            ProcessPriority = aProcessPriority;
            process_ID = processes_count;
            processes_count++;
        }

        //Setter and Getter for Process Name
        public string ProcessName
        {
            get { return process_name; }
            set { process_name = value; }
        }

        //Setter and Getter for Process Remaining Time
        public int ProcessRemainingTime
        {
            get { return process_remainingtime; }
            set 
            {
                if(value > 0)
                    process_remainingtime = value;

                else
                    process_remainingtime = 0;
            }
        }

        //Setter and Getter for Process Priority
        public int ProcessPriority
        {
            get { return process_priority; }
            set
            {
                if (value > 0 && value <= 5)
                    process_priority = value;

                else
                    process_priority = 5;
            }
        }

        public int ProcessArrivalTime
        {
            get { return process_arrivaltime;} 
            set 
            {
                if(value >= 0)
                    process_arrivaltime = value;

                else
                    process_arrivaltime = 0;
            }
        }


        public int ProcessID
        {
            get { return process_ID; }
            set { }
        }
    }
}
