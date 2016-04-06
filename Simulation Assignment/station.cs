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

        //maybe add data for inter arival times
        //maybe add data for travel time to next station


        public int ID
        {
            get { return _ID; }
        }
    }
}
