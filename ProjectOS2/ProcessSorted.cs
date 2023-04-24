using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS2
{
    public class ProcessSorted
    {
        public Process process { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public ProcessSorted(Process process, int startTime, int endTime)
        {
            this.process = process;
            this.startTime = startTime;
            this.endTime = endTime;
        }
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
    }
}
