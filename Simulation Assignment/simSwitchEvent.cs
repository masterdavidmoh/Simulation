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
            station s = state.getStation(_station);
            Queue<int> sq = state.getStationQueue(_station);
            Queue<int> sqTwin = null;
            if(s.isStartEnd())
                sqTwin = state.getStationQueue(s.getTwin().ID);
            //release the switch
            s.setSwitch(true);
            //check if there is a train that can depart
            if (!s.isDepartureQueueEmpty())
            {
                //if so schedule a departure event for that train
                int t = s.popDepartureQueue();
                state.simulationManager.addEvent(new simDepartureEvent(state.simulationManager.simulationTime, _station, t));
            }
            else if (s.isStartEnd() && !s.getTwin().isDepartureQueueEmpty())
            {
                //if so schedule a departure event for that train
                int t = s.getTwin().popDepartureQueue();
                state.simulationManager.addEvent(new simDepartureEvent(state.simulationManager.simulationTime, s.getTwin().ID, t));
            }
            //else check if there is a train in the queue that can arive
            else if (sq.Count > 0)
            {
                //if so schedule arival event
                if (s.canTramArive())
                    s.scheduleArival(state, sq.Dequeue());
            }
            else if(sqTwin!=null && sqTwin.Count > 0)
            {
                if (s.getTwin().canTramArive())
                    s.getTwin().scheduleArival(state, sqTwin.Dequeue());
            }
        }
    }
}
