using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS2
{
    public class Process
    {
        
        public string name { get; set; }

       // public static string schedulerType { get; set; }
        public int arrivalTime { get; set; }
        public int burstTime { get; set; }
        public int priority { get; set; }

        public int waitingTime { get; set; }
        public int serviceTime { get; set; } //todo change to startTime
        public int turnaroundTime { get; set; }
        /*
        public string pid { get; set; }
        public int ArrivalTime { get; set; }
        public int BurstTime { get; set; }
        public int Priority { get; set; }
        public int WaitingTime { get; set; }
        public int TurnaroundTime { get; set; }
        public int RemainingBurstTime { get; set; }
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set {  this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }*/
    }

    
}
