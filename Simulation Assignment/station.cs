﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simulation_Assignment
{
    public class station
    {
        public enum stationDist { gamma, exponential };

        protected List<int> _arrivalTimes;
        protected int _ID;
        protected string _name;
        protected int _nextStationID;
        protected int _traveToNext;
        protected bool _trainInStation;
        protected List<int> _departureQue;
        protected int _traveltTimeToNextStation;
        protected int _offset;
        protected StreamWriter _swWaiting;
        protected StreamWriter _swPunctual;
        protected int _direction;
        protected List<Tuple<int, int>> intervals;
        protected Dictionary<Tuple<int, int>, int> inPassengers;
        protected Dictionary<Tuple<int, int>, int> outPassengers;
        protected stationDist inDist;
        protected stationDist outDist;
        protected bool _lastStation;
        protected int _trainsPerHour;
        protected int _nextTrain;
        protected double _arrivalRate;


        //maybe add data for inter arival times
        //maybe add data for travel time to next station

        public station(string name, int nextStation, int travelTimeToNext, int timeOffset, int trainsPerHour, bool lastStation, string outputPrefix)
        {
            _name = name;
            _nextStationID = nextStation;
            _traveToNext = travelTimeToNext;
            _swWaiting = new StreamWriter(outputPrefix + "_waiting_times_" + name + ".data");
            _swPunctual = new StreamWriter(outputPrefix + "_punctuality_" + name + ".data");
            _offset = timeOffset;
            _lastStation = lastStation;
            _trainsPerHour = trainsPerHour;
            _nextTrain = timeOffset;

            intervals = new List<Tuple<int, int>>();
            inPassengers = new Dictionary<Tuple<int, int>, int>();
            outPassengers = new Dictionary<Tuple<int, int>, int>();
        }

        ~station()
        {
            _swPunctual.Close();
            _swWaiting.Close();
        }

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
            simArrivalEvent e = new simArrivalEvent(40, _ID, tramID); 

            state.simulationManager.addEvent(e);
        }

        public virtual void ariveTrain(simulationState state, int tramID)
        {
            //check arival time expected vs actual arival time

            //write # seconds difference from expected time
            _swPunctual.WriteLine(_nextTrain - state.simulationManager.simulationTime);
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
            //update the time for the next train
            int time = state.simulationManager.simulationTime;
            //if we are before 7am or after 7 pm set rate to 4 trams
            if (time - _offset < 3600 || time - _offset > 3600 * 13)
                _nextTrain = Convert.ToInt32(_nextTrain + 3600 / 4.0);
            else
                _nextTrain = Convert.ToInt32(_nextTrain + 3600 / _trainsPerHour);
        }

        /// <summary>
        /// adds a passenger to the station
        /// </summary>
        /// <param name="time">time the passenger arived at the station</param>
        public void addPassenger(int time)
        {
            _arrivalTimes.Add(time);
        }

        /// <summary>
        /// get the number of passengers entering the tram
        /// </summary>
        /// <param name="space">available space in the tram</param>
        /// <returns>the number of people entering the tram</returns>
        public int getPassengersIn(int space, int time)
        {
            int entering;
            if (_arrivalTimes.Count() > space)
                entering = space;
            else
                entering = _arrivalTimes.Count;

            for (int i = 0; i < entering; i++)
            {
                _swWaiting.WriteLine(time.ToString() + " \t" + (time - _arrivalTimes[i]).ToString());
            }

            _arrivalTimes.RemoveRange(0, entering);

                return entering;
        }

        /// <summary>
        /// gets the number of people exiting the tram at this point
        /// </summary>
        /// <param name="max">maximum number of people that can exit</param>
        /// <returns>the number of people leaving the tram</returns>
        public int getExiting(int max, int time, simulationState state)
        {
            if(_lastStation)
                return max;
            
            double passengers;
            int exiting = outPassengers[findInterval(time)];

            if (outDist == stationDist.exponential)
            {
                passengers = state.getRandom.getExponential(exiting);
            }
            else
                passengers = state.getRandom.getGamma(exiting, 1.0);//TODO add alpha
            
            return Math.Min(Convert.ToInt32(passengers) ,max); 
        }

        /// <summary>
        /// get the time required to turn the tram around
        /// </summary>
        /// <param name="tram">id of the tram</param>
        /// <param name="time">the current time</param>
        /// <returns>time required at this station to turn</returns>
        public virtual int turnTime(int tram, int time)
        {
            return 0;
        }

        /// <summary>
        /// adds a tram to the departure queue
        /// </summary>
        /// <param name="tram">tramID that wants to depart</param>
        public void addDepartureQueue(int tram)
        {
            _departureQue.Add(tram);
        }

        /// <summary>
        /// pops the first tram from the departure queue
        /// </summary>
        /// <returns>first tram in the queue</returns>
        public int popDepartureQueue()
        {
            int d = _departureQue[0];
            _departureQue.RemoveAt(0);
            return d;
        }

        /// <summary>
        /// check if the departure queue is empty;
        /// </summary>
        /// <returns>true if the queue is emtpy</returns>
        public bool isDepartureQueueEmpty()
        {
            return _departureQue.Count == 0;
        }

        /// <summary>
        /// get the travel time to the next station
        /// </summary>
        /// <returns>travel time to the next station</returns>
        public int getTravelTime(simulationState state )
        {
            //alpha 34.784 en beta 0.0287
            double multiplier = state.getRandom.getGamma(34.784, 0.0287);

            return Convert.ToInt32(_traveltTimeToNextStation * multiplier);
        }

        public double getInterArrivalTime(simulationState state, int time)
        {
            //have # per hour
            //arivals / hours = _arivalRate
            // time between arivals in hours is 1/ arivals / hour == hour/arival
            // hour/arival * 3600 = seconds/arival

            return state.getRandom.getExponential( 3600 / _arrivalRate);

        }

            

        public void updateArivalRate(int time, simulationState state)
        {
            int passengers = inPassengers[findInterval(time)];

            if (inDist == stationDist.exponential)
                _arrivalRate = state.getRandom.getExponential(passengers);
            else
                _arrivalRate = state.getRandom.getGamma(passengers, 1.0); //TODO add gamma alpha 
        
        }

        /// <summary>
        /// adds a interval of ariving and exiting passengers
        /// </summary>
        /// <param name="interval">range of time</param>
        /// <param name="pasin">entering passengers/hour</param>
        /// <param name="pasout">exiting passngers/hour</param>
        public void addInterval(Tuple<int,int> interval, int pasin, int pasout)
        {
            intervals.Add(interval);
            inPassengers.Add(interval, pasin);
            outPassengers.Add(interval, pasout);
        }

        /// <summary>
        /// finds the interval a certain time lies in
        /// </summary>
        /// <param name="time">time to locate an interval for</param>
        /// <returns>the found interval</returns>
        private Tuple<int,int> findInterval(int time)
        {
            foreach(Tuple<int,int> i in intervals)
            {
                if (i.Item1 < time-_offset && i.Item2 > time-_offset)
                    return i;
            }
            return intervals[0];
        }

        /// <summary>
        /// gets the ID of this station
        /// </summary>
        public int ID
        {
            get { return _ID; }
        }

        /// <summary>
        /// gets the next station on the line
        /// </summary>
        public int nextStation
        {
            get { return _nextStationID; }
        }

        /// <summary>
        /// gets the name of the station
        /// </summary>
        public string name
        {
            get { return _name; }
        }

        /// <summary>
        /// gets the direction of the station (0 == from uithof, 1 == to uithof)
        /// </summary>
        public int direction
        {
            get { return _direction; }
        }
    }
}
