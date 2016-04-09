using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class simArrivalEvent : simEvent
    {
        public int _station;
        public int _tram;

        public simArrivalEvent(int time, int stationID, int tramID)
            :base(EventType.ArrivalTram,time)
        {
            _station = stationID;
            _tram = tramID;
        }

        public override void executeEvent(simulationState state)
        {
            int delay = 0;
            if (true != false)//if the door jams
                delay += 60; //add 60 second delay to the dwell time

            //12.5 + 0.22 pin+0.13 pout
            //get passengers entering
            tram t = state.getTram(_tram);
            station s = state.getStation(_station);

            int pout = s.getExiting(t.passengers);
            t.exitPassngers(pout);
            int pin = s.getPassengersIn(t.spacesInTram);

            state.simulationManager.addEvent(new simDepartureEvent(Convert.ToInt32(Math.Ceiling(12.5 + 0.22 * pin + 0.12 * pout)) + delay, _station, _tram));//TODO get dwell time time (probably from station)
        }
    }
}
