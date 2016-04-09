using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class simSwitchEvent : simEvent
    {
        int _station;
        public simSwitchEvent(int time, int stationID)
            :base(EventType.Switch,time)
        {
            _station = stationID;

        }

        public override void executeEvent(simulationState state)
        {
            //release the switch
            //check if there is a train that can depart
            //if so schedule a departure event for that train
            //else check if there is a train in the queue that can arive
            //if so schedule arival event
            throw new NotImplementedException();
        }
    }
}
