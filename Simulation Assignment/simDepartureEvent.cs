using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class simDepartureEvent : simEvent
    {
        public simDepartureEvent(int time, int stationID, int tramID )
            :base(EventType.DepartureTram,time)
        {

        }

        public override void executeEvent()
        {
            throw new NotImplementedException();
        }
    }
}
