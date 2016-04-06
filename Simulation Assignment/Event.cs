using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Assignment
{
    public abstract class simEvent: IComparable<simEvent>
    {
        public enum EventType {ArrivalPassenger, Queue, ArrivalTram, DepartureTram, Switch };

        EventType eventType;
        int time;

        public simEvent(EventType eventType, int time)
        {
            this.eventType = eventType;
            this.time = time;
 
        }

        abstract public void executeEvent();
        
        public int CompareTo(simEvent other)
        {
            if (this.time < other.time) return -1;
            else if (this.time > other.time) return 1;
            else return 0;
        }
    }
}
