using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Assignment
{
    class Event: IComparable<Event>
    {
        public enum EventType {ArrivalPassenger, Queue, ArrivalTram, DepartureTram };

        EventType eventType;
        int time;

        public Event(EventType eventType, int time)
        {
            this.eventType = eventType;
            this.time = time;
 
        }

        public void executeEvent()
        {


        }

        public int CompareTo(Event other)
        {
            if (this.time < other.time) return -1;
            else if (this.time > other.time) return 1;
            else return 0;
        }
    }
}
