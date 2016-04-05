using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    class simPasengerArivalEvent:Event
    {
        public simPasengerArivalEvent(int time, int stationID)
            :base(EventType.ArrivalPassenger,time)
        {

        }

        public override void executeEvent()
        {
            throw new NotImplementedException();
        }
    }
}
