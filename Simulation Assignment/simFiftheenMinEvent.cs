using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment.Simulation_Assignment
{
    class simFiftheenMinEvent:simEvent
    {
        public simFiftheenMinEvent(int time)
            :base(EventType.Fifteen, time)
        {

        }

        public override void executeEvent(simulationState state)
        {
            //go over all stations and schedule a new passenger arival event
            int i = 0;
            station s = state.getStation(i);

            while (s != null)
            {
                //schedule new pasenger arival 
                state.simulationManager.addEvent(new simPasengerArivalEvent(0, i, time)); //TODO get time from somewhere (probably station)

                i++;
                s = state.getStation(i);
            }
        }
    }
}
