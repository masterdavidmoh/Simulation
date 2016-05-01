using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    class stationSwitch : station
    {
        private bool _switchEmpty;
        private bool _trainInStation2;

        //public station(string name, int nextStation, int travelTimeToNext, int timeOffset, int trainsPerHour, bool lastStation, string outputPrefix)

        public stationSwitch(string name, int id, int nextStation, int travelTimeToNext, int timeOffset, int trainsPerHour, bool lastStation, string outputPrefix, int direction, stationDist inDist, double inAlpha, stationDist outDist, double outalpha, double scale)
            :base(name, id, nextStation, travelTimeToNext, timeOffset, trainsPerHour, lastStation, outputPrefix, 2, inDist, inAlpha, outDist, outalpha, scale)
        {
            _switchEmpty = true;
        }


        public override int turnTime(int tram, int time)
        {
            //check if the train is on time
            if(time <= _nextTrain)
                return 4 * 60;
            //otherwise 
            else
                return 3 * 60;
        }

        /// <summary>
        /// checks if a train can arive at the station
        /// </summary>
        /// <returns>returns true if a train can arive, false otherwise</returns>
        public override bool canTramArive()
        {
            if (_trainInStation && _trainInStation2)
                return false;
            else
            {
                if (!_switchEmpty)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// handles all internal requirements for the simArrivalEvent and schedules it
        /// </summary>
        /// <param name="state">current state of the simulation</param>
        public override void scheduleArival(simulationState state, int tramID)
        {
            if (_trainInStation)
                _trainInStation2 = true;
            else
                _trainInStation = true;

            //schedule arival event
            simArrivalEvent e = new simArrivalEvent(state.simulationManager.simulationTime + 40, _ID, tramID);

            state.simulationManager.addEvent(e);

            _switchEmpty = false;

            state.simulationManager.addEvent(new simSwitchEvent(state.simulationManager.simulationTime + 40, _ID));
        }

        /// <summary>
        /// checks if the tram currently at the station can depart
        /// </summary>
        /// <returns>true if the train can depart, false otherwise</returns>
        public override bool canTramDepart()
        {
            if(!_switchEmpty)
                return false;
            else
                return true;
        }

        /// <summary>
        /// handles the internal values for a departure of trains
        /// </summary>
        public override void departTrain(simulationState state)
        {
            if (_trainInStation)
                _trainInStation = false;
            else
                _trainInStation2 = false;

            _switchEmpty = false;
            state.simulationManager.addEvent(new simSwitchEvent(state.simulationManager.simulationTime + 40, _ID));

            //update the time for the next train
            int time = state.simulationManager.simulationTime;
            //if we are before 7am or after 7 pm set rate to 4 trams
            if (time - _offset < 3600 || time - _offset > 3600 * 13)
                _nextTrain = _nextTrain + Convert.ToInt32(3600.0 / 4.0);
            else
                _nextTrain = _nextTrain + Convert.ToInt32(3600.0 / _trainsPerHour);

        }

        public override void addInterval(Tuple<int, int> interval, double pasin, double pasout, int direction)
        {
            intervals.Add(interval);
            if(direction == 0)
                outPassengers.Add(interval, pasout);
            else
                inPassengers.Add(interval, pasin);
            
        }

        public override int getExiting(int max, int time, simulationState state)
        {
            return max;
        }

        public override void setSwitch(bool value)
        {
            _switchEmpty = value;
        }
    }
}
