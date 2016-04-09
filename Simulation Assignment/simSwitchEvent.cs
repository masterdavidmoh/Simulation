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
            stationSwitch s = (stationSwitch)state.getStation(_station);
            Queue<int> sq = state.getStationQueue(_station);
            //release the switch
            s.switchEmpty = true;
            //check if there is a train that can depart
            if (!s.isDepartureQueueEmpty())
            {
                //if so schedule a departure event for that train
                int t = s.popDepartureQueue();
                s.departTrain(state);
                state.simulationManager.addEvent(new simDepartureEvent(0, _station, t));
            }
            //else check if there is a train in the queue that can arive
            else if(sq.Count > 0)
            {
                //if so schedule arival event
                if (s.canTramArive())
                        s.scheduleArival(state, sq.Dequeue());
            }
        }
    }
}
