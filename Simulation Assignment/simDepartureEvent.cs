using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class simDepartureEvent : simEvent
    {
        int _tram;
        int _station;

        public simDepartureEvent(int time, int stationID, int tramID )
            :base(EventType.DepartureTram,time)
        {
            _tram = tramID;
            _station = stationID;
        }

        public override void executeEvent(simulationState state)
        {
            station s = state.getStation(_station);
            Queue<int> sq = state.getStationQueue(_station);

            if (s.canTramDepart())
            {
                s.departTrain(state);

                //schedule queue event
                if(s.nextStation != -1)
                    state.simulationManager.addEvent(new simQueueEvent(state.simulationManager.simulationTime + s.getTravelTime(state), s.nextStation, _tram));

                //check if this stations queue is empty
                if (sq.Count > 0)
                {
                    //if not schedule an arrival with the first tram
                    if (s.canTramArive())
                        s.scheduleArival(state, sq.Dequeue());
                }
            }
            else
            {
                s.addDepartureQueue(_tram);
            }
        }
    }
}
