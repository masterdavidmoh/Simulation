using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    class simQueueEvent:Event
    {
        public simQueueEvent(int time, int stationID)
            :base(EventType.Queue,time)
        {

        }

        public override void executeEvent()
        {
            throw new NotImplementedException();
        }
    }
}
