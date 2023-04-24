using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OS_Scheduler
{
    public static class Timer
    {
        private static int aTimerCount;


        public static void StartTimer()
        {
            // Create a timer with a two second interval.
            System.Timers.Timer aTimer = new System.Timers.Timer(1000);

            //Initialize timer counter to zero
            aTimerCount = 0;

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            /*
            switch(Scheduler)
            {
                case FCFS:
                    FCFC.Scheduler();
                    break;

                case SJF_P:
                    SJF_P.Scheduler(); 
                    break;

                case SJF_NP:
                    SJF_NP.Scheduler();
                    break;

                case Priority_P:
                    Priority_P.Scheduler();
                    break;

                case Priority_NP:
                    Priority_NP.Scheduler();
                    break;

                case RR:
                    RR.Scheduler();
                    break;
            }
            */
            aTimerCount++;
        }

        public static int TimerCount
        {
            get { return aTimerCount; }
            set { }
        }
    }
}
