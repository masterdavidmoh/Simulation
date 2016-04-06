using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class tram
    {
        private int _ID;
        private int _passengers;
        private int _station; //id of the station its at or on its way to the queue for

        public int ID
        {
            get { return _ID; }
        }
    }
}
