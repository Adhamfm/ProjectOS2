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
        public int processTime { get; set; }  
        public int priority { get; set; }
       
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set {  this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
    }

    
}
