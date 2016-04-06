using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class simPasengerArivalEvent : simEvent
    {
        int _station;

        public simPasengerArivalEvent(int time, int stationID)
            :base(EventType.ArrivalPassenger,time)
        {
            _station = stationID;
        }

        public override void executeEvent(simulationState state)
        {
            //add current time to the station arival list
            state.getStation(_station).addPassenger(_time);
            //schedule new pasenger arival 
            state.simulationManager.addEvent(new simPasengerArivalEvent(0, _station)); //TODO get time from somewhere (probably station)
        }
    }
}
