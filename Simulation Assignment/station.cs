using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class station
    {
        protected List<int> _arrivalTimes;
        protected int _ID;
        protected string _name;
        protected int _nextStationID;
        protected bool _trainInStation;

        //maybe add data for inter arival times
        //maybe add data for travel time to next station


        /// <summary>
        /// checks if a train can arive at the station
        /// </summary>
        /// <returns>returns true if a train can arive, false otherwise</returns>
        public virtual bool canTramArive()
        {
            if (_trainInStation)
                return false;
            return true;
        }

        /// <summary>
        /// handles all internal requirements for the simArrivalEvent and schedules it
        /// </summary>
        /// <param name="state">current state of the simulation</param>
        public virtual void scheduleArival(simulationState state, int tramID)
        {
            _trainInStation = true;
            //schedule arival event
            simArrivalEvent e = new simArrivalEvent(0, _ID, tramID); //TODO get time to arive at the station

            state.simulationManager.addEvent(e);
        }

        /// <summary>
        /// checks if the tram currently at the station can depart
        /// </summary>
        /// <returns>true if the train can depart, false otherwise</returns>
        public virtual bool canTramDepart()
        {
            return true;
        }

        /// <summary>
        /// handles the internal values for a departure of trains
        /// </summary>
        public virtual void departTrain(simulationState state)
        {
            _trainInStation = false;
        }

        public void addPassenger(int time)
        {
            _arrivalTimes.Add(time);
        }

        public int ID
        {
            get { return _ID; }
        }

        public int nextStation
        {
            get { return _nextStationID; }
        }
    }
}
