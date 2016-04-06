using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    class station
    {
        private List<int> _arrivalTimes;
        private int _ID;
        private string _name;
        private int _nextStationID;
        private bool _trainInStation;

        //maybe add data for inter arival times
        //maybe add data for travel time to next station


        /// <summary>
        /// checks if a train can arive at the station
        /// </summary>
        /// <returns>returns true if a train can arive, false otherwise</returns>
        public bool canTramArive()
        {
            if (_trainInStation)
                return false;
            return true;
        }

        /// <summary>
        /// handles all internal requirements for the simArrivalEvent and schedules it
        /// </summary>
        /// <param name="state">current state of the simulation</param>
        public void scheduleArival(simulationState state, int tramID)
        {
            _trainInStation = true;
            //schedule arival event
            simArrivalEvent e = new simArrivalEvent(0, _ID, tramID); //TODO get time to arive at the station

            state.simulationManager.addEvent(e);
        }

        public int ID
        {
            get { return _ID; }
        }
    }
}
