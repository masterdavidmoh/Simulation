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


        public void exitPassengers(int n)
        {
           _passengers -= n;
        }

        public void addPassengers(int n)
        {
            _passengers += n;
        }

        public int ID
        {
            get { return _ID; }
        }

        public int spacesInTram
        {
            get { return 420 - _passengers; }
        }

        public int passengers
        {
            get { return _passengers; }
        }
    }
}
