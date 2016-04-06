using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class simQueueEvent : simEvent
    {
        private int _station;
        private int _tram;
        public simQueueEvent(int time, int stationID, int tramID)
            :base(EventType.Queue,time)
        {
            _station = stationID;
            _tram = tramID;
        }

        public override void executeEvent(simulationState state)
        {
            //check if the queue is empty
            if (state.getStationQueue(_station).Count != 0)
            {
                //if not add the tram to the queue
                state.getStationQueue(_station).Enqueue(_tram);
            }

            station s = state.getStation(_station);
            //check if the tram can arrive
            if (s.canTramArive())
            {
                //if so schedule the arival
                s.scheduleArival(state, _tram);
            }
            else
            {
                //else add train to the queue
                state.getStationQueue(_station).Enqueue(_tram);
            }
        }
    }
}
