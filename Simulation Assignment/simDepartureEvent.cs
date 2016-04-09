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

            //TODO add list of trains that want to depart but cant yet
            if (s.canTramDepart())
            {
                s.departTrain(state);

                //schedule queue event
                state.simulationManager.addEvent(new simQueueEvent(0, s.nextStation, _tram)); //TODO get travel time (probably from station)

                //check if this stations queue is empty
                if (sq.Count > 0)
                {
                    //if not schedule an arrival with the first tram
                    if (s.canTramArive())
                        s.scheduleArival(state, sq.Dequeue());
                }
            }
        }
    }
}
