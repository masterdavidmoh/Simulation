using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
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
            double arivalTime = 0.0;

            while (s != null)
            {
                s.updateArivalRate(time,state);
                //get time to next arival
                arivalTime = s.getInterArrivalTime(state, time);

                //schedule new pasenger arival 
                if (arivalTime >= 0.0)
                    state.simulationManager.addEvent(new simPasengerArivalEvent(time + Convert.ToInt32(arivalTime), i, time)); 

                i++;
                s = state.getStation(i);
            }
            if (time <= 77400)
                state.simulationManager.addEvent(new simFiftheenMinEvent(time + (15 * 60)));
        }
    }
}
