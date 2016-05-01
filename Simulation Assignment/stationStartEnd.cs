using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class valueRef<T>
    {
        T _value;

        public valueRef(T value) 
        {
            _value = value;
        }
        
        public T value
        {
            get{ return _value; }
            set{ _value = value; }
        }
    }


    public class stationStart: station
    {
        valueRef<bool> _switchEmpty;
        station _twin;

        public stationStart(string name, int id, int nextStation, int travelTimeToNext, int timeOffset, int trainsPerHour, bool lastStation,
            string outputPrefix, int direction, stationDist inDist, double inAlpha, stationDist outDist, double outalpha, double scale,
            valueRef<bool> switchEmpty)
            :base(name, id, nextStation, travelTimeToNext, timeOffset, trainsPerHour, lastStation, outputPrefix, direction, inDist, inAlpha, outDist, outalpha, scale)
        {
            _switchEmpty = switchEmpty;
        }

        /// <summary>
        /// checks if a train can arive at the station
        /// </summary>
        /// <returns>returns true if a train can arive, false otherwise</returns>
        public override bool canTramArive()
        {
            if (_trainInStation)
                return false;
            else
            {
                if (!_switchEmpty.value)
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
            _trainInStation = true;

            //schedule arival event
            simArrivalEvent e = new simArrivalEvent(state.simulationManager.simulationTime + 40, _ID, tramID);

            state.simulationManager.addEvent(e);

            _switchEmpty.value = false;

            state.simulationManager.addEvent(new simSwitchEvent(state.simulationManager.simulationTime + 40, _ID));
        }

        /// <summary>
        /// checks if the tram currently at the station can depart
        /// </summary>
        /// <returns>true if the train can depart, false otherwise</returns>
        public override bool canTramDepart()
        {
            if (!_switchEmpty.value)
                return false;
            else
                return true;
        }

        /// <summary>
        /// handles the internal values for a departure of trains
        /// </summary>
        public override void departTrain(simulationState state)
        {
            _trainInStation = false;

            _switchEmpty.value = false;
            state.simulationManager.addEvent(new simSwitchEvent(state.simulationManager.simulationTime + 40, _ID));

            //update the time for the next train
            int time = state.simulationManager.simulationTime;
            //if we are before 7am or after 7 pm set rate to 4 trams
            if (time - _offset < 3600 || time - _offset > 3600 * 13)
                _nextTrain = _nextTrain + Convert.ToInt32(3600.0 / 4.0);
            else
                _nextTrain = _nextTrain + Convert.ToInt32(3600.0 / _trainsPerHour);

        }

        public override int getExiting(int max, int time, simulationState state)
        {
            return 0;
        }

        public bool switchEmpty
        {
            get { return _switchEmpty.value; }
            set { _switchEmpty.value = value; }
        }

        public override bool isStartEnd()
        {
            return true;
        }

        public override station getTwin()
        {
            return _twin;
        }

        public void setTwin(station twin)
        {
            _twin = twin;
        }

         public override void setSwitch(bool value)
        {
            _switchEmpty.value = value;
        } 

    }

    
    public class stationEnd: station
    {
        valueRef<bool> _switchEmpty;
        station _twin;

        public stationEnd(string name, int id, int nextStation, int travelTimeToNext, int timeOffset, int trainsPerHour, bool lastStation, string outputPrefix, int direction, stationDist inDist, double inAlpha, stationDist outDist, double outalpha, double scale, valueRef<bool> switchEmpty, station twin)
            :base(name, id, nextStation, travelTimeToNext, timeOffset, trainsPerHour, lastStation, outputPrefix, direction, inDist, inAlpha, outDist, outalpha, scale)
        {
            _switchEmpty = switchEmpty;
            _twin = twin;
        }

        /// <summary>
        /// checks if a train can arive at the station
        /// </summary>
        /// <returns>returns true if a train can arive, false otherwise</returns>
        public override bool canTramArive()
        {
            if (_trainInStation)
                return false;
            else
            {
                if (!_switchEmpty.value)
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
            _trainInStation = true;

            //schedule arival event
            simArrivalEvent e = new simArrivalEvent(state.simulationManager.simulationTime + 40, _ID, tramID);

            state.simulationManager.addEvent(e);

            _switchEmpty.value = false;

            state.simulationManager.addEvent(new simSwitchEvent(state.simulationManager.simulationTime + 40, _ID));
        }

        /// <summary>
        /// checks if the tram currently at the station can depart
        /// </summary>
        /// <returns>true if the train can depart, false otherwise</returns>
        public override bool canTramDepart()
        {
            if (!_switchEmpty.value)
                return false;
            else
                return true;
        }

        /// <summary>
        /// handles the internal values for a departure of trains
        /// </summary>
        public override void departTrain(simulationState state)
        {
            _trainInStation = false;

            _switchEmpty.value = false;
            state.simulationManager.addEvent(new simSwitchEvent(state.simulationManager.simulationTime + 40, _ID));

            //update the time for the next train
            int time = state.simulationManager.simulationTime;
            //if we are before 7am or after 7 pm set rate to 4 trams
            if (time - _offset < 3600 || time - _offset > 3600 * 13)
                _nextTrain = _nextTrain + Convert.ToInt32(3600.0 / 4.0);
            else
                _nextTrain = _nextTrain + Convert.ToInt32(3600.0 / _trainsPerHour);

        }

        public override int getExiting(int max, int time, simulationState state)
        {
            return max;
        }

        public bool switchEmpty
        {
            get { return _switchEmpty.value; }
            set { _switchEmpty.value = value; }
        }

        public override bool isStartEnd()
        {
            return true;
        }

        public override station getTwin()
        {
            return _twin;
        }
          
        public override void setSwitch(bool value)
        {
            _switchEmpty.value = value;
        }

    }


}
