using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class simArrivalEvent : simEvent
    {
        public int _station;
        public int _tram;

        public simArrivalEvent(int time, int stationID, int tramID)
            :base(EventType.ArrivalTram,time)
        {
            _station = stationID;
            _tram = tramID;
        }

        public override void executeEvent(simulationState state)
        {
            throw new NotImplementedException();
        }
    }
}
