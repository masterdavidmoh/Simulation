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
            _scheduleTime = scheduleTime;
        }

        public override void executeEvent(simulationState state)
        {
            //find if time is not in the same 15 min timeframe as schedule time, return as we have a new event
            if (Math.Floor((time / (15.0*60))) != Math.Floor((_scheduleTime / (15.0*60.0))))
                return;

            station s = state.getStation(_station);
            //add current time to the station arival list
            s.addPassenger(_time);
            //get inter arival time to the next arival
            double arivalTime = s.getInterArrivalTime(state, time);
            //schedule new pasenger arival 
            if (arivalTime != -1.0)
                state.simulationManager.addEvent(new simPasengerArivalEvent(time + Convert.ToInt32(arivalTime), _station, time)); 
        }
    }
}
