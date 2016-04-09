using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class simPasengerArivalEvent : simEvent
    {
        int _station;
        int _scheduleTime;

        public simPasengerArivalEvent(int time, int stationID, int scheduleTime)
            :base(EventType.ArrivalPassenger,time)
        {
            _station = stationID;
        }

        public override void executeEvent(simulationState state)
        {
            //find if time is not in the same 15 min timeframe as schedule time, return as we have a new event
            if (Math.Floor((time / 15.0)) != Math.Floor((_scheduleTime / 15.0)))
                return;

            //add current time to the station arival list
            state.getStation(_station).addPassenger(_time);
            //schedule new pasenger arival 
            state.simulationManager.addEvent(new simPasengerArivalEvent(0, _station, time)); //TODO get time from somewhere (probably station)
        }
    }
}
