using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    /// <summary>
    /// datastructure containing the state of the current simulation
    /// </summary>
    public class simulationState
    {
        private Dictionary<int,station> _stations;
        private Dictionary<int,tram> _trams;
        private Dictionary<int, Queue<int>> _stationQueues;
        private simulationManager _manager;
        private simRandom _r;

        public simulationState(simulationManager manager, int seed)
        {
            _manager = manager;
            _stations = new Dictionary<int,station>();
            _trams = new Dictionary<int,tram>();
            _r = new simRandom(seed);
        }

        public simulationManager simulationManager
        {
            get { return _manager; }
        }

        /// <summary>
        /// adds the station to the simulation state
        /// </summary>
        /// <param name="s">station to be added</param>
        /// <returns>true if he station was added succesfully false otherwise</returns>
        public bool addStation(station s)
        {
            try
            {
                _stations.Add(s.ID, s);
                _stationQueues.Add(s.ID, new Queue<int>());
                return true;
            }
                //should never happen, as this would mean we have multiple stations with the same id, CHECK WHEN ADDING!!!!!
            catch (ArgumentException)
            {
                return false;
            }
        }

        /// <summary>
        /// adds a tram to the simulation state
        /// </summary>
        /// <param name="t">tram to be added</param>
        /// <returns>true if he tram was added succesfully false otherwise</returns>
        public bool addTram(tram t)
        {
            try
            {
                _trams.Add(t.ID, t);
                return true;
            }
            //should never happen, as this would mean we have multiple stations with the same id, CHECK WHEN ADDING!!!!!
            catch (ArgumentException)
            {
                return false;
            }
        }

        /// <summary>
        /// gets the station with ID
        /// </summary>
        /// <param name="ID">ID of the station to get</param>
        /// <returns>the station with id ID if it exists, null otherwise</returns>
        public station getStation(int ID)
        {
            station s;
            _stations.TryGetValue(ID, out s);
            return s;
                
        }

        /// <summary>
        /// gets queue before the station with ID
        /// </summary>
        /// <param name="ID">ID of the station to get the queue for</param>
        /// <returns>the the queue for station with id ID if it exists, null otherwise</returns>
        public Queue<int> getStationQueue(int ID)
        {
            Queue<int> q;
            _stationQueues.TryGetValue(ID, out q);
            return q;
        }

        /// <summary>
        /// gets the tram with ID
        /// </summary>
        /// <param name="ID">ID of the tram to get</param>
        /// <returns>the tram with id ID if it exist, null otherwise</returns>
        public tram getTram(int ID)
        {
            tram t;
            _trams.TryGetValue(ID, out t);
            return t;
        }

        public simRandom getRandom
        {
            get { return _r; }
        }
    }
}
