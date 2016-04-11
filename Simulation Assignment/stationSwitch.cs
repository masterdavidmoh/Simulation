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


        public stationSwitch(string name, int nextStation, int travelTimeToNext, string outputPrefix)
            :base(name, nextStation, travelTimeToNext, outputPrefix)
        {
        }


        public override int turnTime(int tram, int time)
        {
            //TODO add check if the train is delayed
            //check if the train is on time
            return 4 * 60;
            //otherwise 
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
            simArrivalEvent e = new simArrivalEvent(40, _ID, tramID);

            state.simulationManager.addEvent(e);

            _switchEmpty = false;

            state.simulationManager.addEvent(new simSwitchEvent(40, _ID));
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
            state.simulationManager.addEvent(new simSwitchEvent(40, _ID));

        }

        public bool switchEmpty
        {
            get { return _switchEmpty; }
            set { _switchEmpty = value; }
        }
    }
}
