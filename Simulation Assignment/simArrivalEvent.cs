using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class simArrivalEvent : simEvent
    {
        public simArrivalEvent(int time, int stationID)
            :base(EventType.ArrivalTram,time)
        {

        }

        public override void executeEvent()
        {
            throw new NotImplementedException();
        }
    }
}
