using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    class simDepartureEvent:Event
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
