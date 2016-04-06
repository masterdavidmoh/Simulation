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

        private EventType eventType;
        private int _time;

        public simEvent(EventType eventType, int time)
        {
            this.eventType = eventType;
            this._time = time;
 
        }

        abstract public void executeEvent(simulationState state);
        
        public int CompareTo(simEvent other)
        {
            if (this._time < other._time) return -1;
            else if (this._time > other._time) return 1;
            else return 0;
        }

        public int time
        {
            get{return this._time;}
        }
    }
}
