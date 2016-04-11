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
        public int _doorFailPercentage;

        public simArrivalEvent(int time, int stationID, int tramID)
            :base(EventType.ArrivalTram,time)
        {
            _station = stationID;
            _tram = tramID;
        }

        public override void executeEvent(simulationState state)
        {
            int delay = 0;
            tram t = state.getTram(_tram);
            //check if the door fails
            if (state.getRandom.getUniform(0,100) > t.doorJamChance)
                delay += 60; //add 60 second delay to the dwell time

            //12.5 + 0.22 pin+0.13 pout
            //get passengers entering
            
            station s = state.getStation(_station);

            s.ariveTrain(state, _tram);
            int pout = s.getExiting(t.passengers);
            t.exitPassengers(pout);
            int pin = s.getPassengersIn(t.spacesInTram, time);
            t.addPassengers(pin);

            state.simulationManager.addEvent(new simDepartureEvent(Math.Max(Convert.ToInt32(Math.Ceiling(12.5 + 0.22 * pin + 0.12 * pout)), s.turnTime(_tram,time)) + delay , _station, _tram));
        }
    }
}
