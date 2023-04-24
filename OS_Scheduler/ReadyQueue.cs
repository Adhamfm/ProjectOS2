using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Scheduler
{
    public class ReadyQueue
    {
        private LinkedList<Process> job_queue;

        /*Here below this comment should be a pointer to the currently running process so that it will be 
         * passed on our implemented scheduler function every second, then the scheduler has the opportunity 
         * to change the currently running process to another one or keep it as it is for another second
         * */
        //private Process* currentlyRunningProcess;     
        public static int NumberOfProcesses;
        
        //Running Algorithm on the Queue
        public enum Running_Algorithm
        {
            FCFS                    = 0,
            SJF_PREEMPTIVE          = 1,
            SJF_NON_PREEMPTIVE      = 2,
            PRIORITY_PREEMPTIVE     = 4,
            PRIORITY_NON_PREEMPTIVE = 8,
            ROUNDROBIN              = 16,
            PREEMPTIVE              = SJF_PREEMPTIVE | PRIORITY_PREEMPTIVE     //The Value should be 101 = 5
        }

        public ReadyQueue() 
        {
            job_queue = new LinkedList<Process>();
            NumberOfProcesses = 0;
        }

        public void ReadyQueue_EnQueue(Process process)
        {
            job_queue.AddFirst(process);
            NumberOfProcesses++;
        }

        public void ReadyQueue_Dequeue(Process process) 
        {
            job_queue.Remove(process);
            NumberOfProcesses--;
        }

        public void ReadyQueue_SortQueue_Priority()
        {
            //Insert your code
            //Call your Execution Function
        }

        public void ReadyQueue_SortQueue_SJF() 
        {
            //Insert your code
            //Call your Execution Function
        }
    }
}
