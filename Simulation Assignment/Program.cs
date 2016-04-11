using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Simulation_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
             * TODO lijst:
             * add random variables and distributions
             * add output depays
             * add tram schedule
             * 
             * 
             */

            // expected arival time at station x
            // arrival time station x-1 + (dwell) 
            //get next by taking the number of trains/hour

            string[] stationNames = { };

            //make one of each station we need and add them to the list

        }

        static List<station> parsePassengers(List<station> stations, string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            string data = sr.ReadLine();
            string[] splitData;
            station s = null;
            Tuple<int, int> interval;
            int passengersIn;
            int passengersOut;

            while (data != null)
            {
                splitData = data.Split(new char[] {';'});
                for (int i = 0; i < stations.Count; i++)
                {
                    if (stations[i].name == splitData[0] && stations[i].direction == Convert.ToInt32(splitData[1]))
                    {
                        s = stations[i];
                        break;
                    }
                }
                if (s != null)
                {
                    //calculate the interval in seconds
                    interval = new Tuple<int, int>((Convert.ToInt32(splitData[2]) - 6) * 60 * 60, (Convert.ToInt32(splitData[3]) - 6) * 60 * 60);
                    passengersIn = Convert.ToInt32(splitData[4]);
                    passengersOut = Convert.ToInt32(splitData[5]);

                    s.addInterval(interval, passengersIn, passengersOut);
                }

                data = sr.ReadLine();
            }

            return stations;
        }
    }
}
