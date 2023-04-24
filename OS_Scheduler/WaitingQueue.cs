using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Scheduler
{
    public class WaitingQueue
    {
        private LinkedList<Process> waiting_queue;

        public static int NumberOfProcesses;

        //Running Algorithm on the Ready Queue.
        private ReadyQueue.Running_Algorithm Running_Algorithm;

        //Constructor
        public WaitingQueue()
        {
            //Binding Method
            //Assign the Private Running Algorithm attribute
        }

        public void WaitingQueue_AddToReadyQueue(ReadyQueue jobQueue)
        {
            //Looping on Process to determine if their arrival time has arrived ot not
            foreach (Process p in waiting_queue)
            {
                if(p.ProcessArrivalTime == Timer.TimerCount)
                {
                    jobQueue.ReadyQueue_EnQueue(p);
                    waiting_queue.Remove(p);
                }
            }

            //The Running Algorithm is Preemptive Algorithm.
            if(Running_Algorithm == ReadyQueue.Running_Algorithm.PREEMPTIVE)
            {
                if (Running_Algorithm == ReadyQueue.Running_Algorithm.SJF_PREEMPTIVE)
                {
                    jobQueue.ReadyQueue_SortQueue_SJF();
                }

                else if (Running_Algorithm == ReadyQueue.Running_Algorithm.PRIORITY_PREEMPTIVE)
                {
                    jobQueue.ReadyQueue_SortQueue_Priority();
                }
            }

            //The Running Algorithm is not Preemptive Algorithm.
            else
            {
                switch (Running_Algorithm)
                {
                    case ReadyQueue.Running_Algorithm.FCFS:
                        //FCFS Execution Function
                        break;

                    case ReadyQueue.Running_Algorithm.SJF_NON_PREEMPTIVE:
                        //SJF_NP Execution Function
                        break;

                    case ReadyQueue.Running_Algorithm.PRIORITY_NON_PREEMPTIVE:
                        //PRIORITY_NP Execution Function
                        break;

                    case ReadyQueue.Running_Algorithm.ROUNDROBIN:
                        RR_Scheduler.RR_Execution();
                        break;
                }
            }
        }


    }
}
