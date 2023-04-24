using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Scheduler
{
    public class RR_Scheduler
    {
        private static int timeSlot;
        private static int runningProcessID;
        private static int runningProcessTime;


        public static void RR_Execution()
        {
            runningProcessTime++;
        }




    }
}
