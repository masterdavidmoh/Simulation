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
            //int seed = 1337;
            //int seed = 2000;
            //int seed = 4096;
            //int seed = 42;
            int seed = 2048;

            double scale = 1.5;
            int trainsPerHour = 16;
            int doorJamChance = 1;

            /*
             * TODO lijst:
             * 
             */

            // expected arival time at station x
            // arrival time station x-1 + (dwell) 
            //get next by taking the number of trains/hour

            Console.WriteLine("creating stations");

            string[] stationNames = { "P+R Uithof", "WKZ", "UMC", "Heidelberglaan", "Padualaan", "Kromme Rijn", "Galgewaard", "Vaartscherijn", "Centraal Station Centrumzijde", "Vaartscherijn", "Galgewaard", "Kromme Rijn", "Padualaan", "Heidelberglaan", "UMC", "WKZ", "P+R Uithof" };
            Dictionary<int, station> stations = new Dictionary<int, station>();


            string folder = "./output" + seed.ToString();
            string prefix = folder + "/test" + seed.ToString();

            Directory.CreateDirectory(folder);

            simulationManager simManager = new simulationManager(seed);

            int nextStation = 0;
            int[] travelTimes = { 110, 78, 82, 60, 100, 59, 243, 135, 134, 243, 59, 101, 60, 86, 78, 113, 0};
            double[] inAlphas = { 0.0, 0.0, 0.0, 1.9874, 1.644, 0.0, 1.3491, 0.0, 1.7064, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[] outAlphas = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.2593, 1.7085, 0.0, 0.0, 0.0 };
            stationDist[] inDists = { stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.gamma, stationDist.gamma, stationDist.exponential, stationDist.gamma, stationDist.exponential, stationDist.gamma, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential };
            stationDist[] outDist = { stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.exponential, stationDist.gamma, stationDist.gamma, stationDist.exponential, stationDist.exponential, stationDist.exponential };

            int offset = 0;

            int direction = 0;
            bool last = false;
            
            int switchIndex = 0;

            


            valueRef<bool> startEndSwitch = new valueRef<bool>(true);
            //make one of each station we need and add them to the list
            for (int k = 0; k < stationNames.Count(); k++)
            {
                nextStation = k+1;
                if(nextStation >= stationNames.Count())
                {
                    nextStation = -1;
                    last = true;
                    stations.Add(k, new stationEnd(stationNames[k], k, nextStation, travelTimes[k], offset, trainsPerHour, last, prefix, direction, inDists[k], inAlphas[k], outDist[k], outAlphas[k], scale, startEndSwitch, stations[0]));
                    ((stationStart)stations[0]).setTwin(stations[k]);
                    offset += travelTimes[k] + 60;
                    continue;
                }
                else if (k == 0)
                {
                    stations.Add(k, new stationStart(stationNames[k], k, nextStation, travelTimes[k], offset, trainsPerHour, last, prefix, direction, inDists[k], inAlphas[k], outDist[k], outAlphas[k], scale, startEndSwitch));
                    offset += travelTimes[k] + 60;
                    continue;
                }

                if (stationNames[k] == "Centraal Station Centrumzijde")
                {
                    stations.Add(k, new stationSwitch(stationNames[k], k, nextStation, travelTimes[k], offset, trainsPerHour, last, prefix, direction, inDists[k], inAlphas[k], outDist[k], outAlphas[k],scale));
                    direction = 1;
                    switchIndex = k;
                    offset += 4 * 60;
                    continue;
                }

                //string name, int nextStation, int travelTimeToNext, int timeOffset, int trainsPerHour, bool lastStation, string outputPrefix)
                stations.Add(k ,new station(stationNames[k], k, nextStation, travelTimes[k], offset, trainsPerHour, last, prefix, direction, inDists[k], inAlphas[k], outDist[k], outAlphas[k], scale));
                offset += travelTimes[k] + 60;
            }

            parsePassengers(stations, "12b.csv.data");
            parsePassengers(stations, "12a.csv.data");

            Console.WriteLine("creating trams");
            //create queue events for all trains + train objects
            Dictionary<int, tram> trams = new Dictionary<int, tram>();
            //create 4 trams for 6:00 to 7:00
            trams.Add(0,new tram(0, doorJamChance, 0));
            simManager.addEvent(new simQueueEvent(0 - 40, 0, 0));
            trams.Add(1,new tram(1, doorJamChance, 900));
            simManager.addEvent(new simQueueEvent(900 - 40, 0, 1));
            trams.Add(2,new tram(2, doorJamChance, 1800));
            simManager.addEvent(new simQueueEvent(1800 - 40, 0, 2));
            trams.Add(3,new tram(3, doorJamChance, 2700));
            simManager.addEvent(new simQueueEvent(2700 - 40, 0, 3));

            //create trains per hour trains per hour up to 19:00
            int hour = 0;
            int i;
            for (i = 4; i < 12 * trainsPerHour; i++)
            {
                //schedule the first train of the hour on exactly the hour
                if ((i - 4) % trainsPerHour == 0)
                {
                    hour++;
                    simManager.addEvent(new simQueueEvent(hour*3600 - 40, 0, i));
                }
                else
                {
                    simManager.addEvent(new simQueueEvent(hour * 3600 + (((i - 4) % trainsPerHour) * 3600/trainsPerHour) - 40, 0, i));
                }
                trams.Add(i, new tram(i, doorJamChance, hour * 3600 + (((i - 4) % trainsPerHour) * 3600 / trainsPerHour)));
                    
            }
            hour++;
            //create 4 trams per hour up to 21:30
            //and their arrival events
            for (int j = i; j < i + 15; j++)
            {
                simManager.addEvent(new simQueueEvent(hour * 3600 + ((j-(i)) * 900) - 40, 0, j));
                trams.Add(j,new tram(j, doorJamChance,hour * 3600 + ((j-(i)) * 900)));

            }


            //create passenger arrival events for all trains (start the fifteen min cycle)
            simManager.addEvent(new simFiftheenMinEvent(0));

            simManager.state.trams = trams;
            simManager.state.stations = stations;

            Console.WriteLine("done setting up simulating");

            simManager.run();

            Console.ReadLine();
        }

        static void parsePassengers(Dictionary<int,station> stations, string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            string data = sr.ReadLine();
            string[] splitData;
            station s = null;
            Tuple<int, int> interval;
            double passengersIn;
            double passengersOut;

            while (data != null)
            {
                splitData = data.Split(new char[] {';'});
                for (int i = 0; i < stations.Count; i++)
                {
                    if (stations[i].name == splitData[0] && (stations[i].direction == Convert.ToInt32(splitData[1])||stations[i].direction ==2))
                    {
                        s = stations[i];
                        break;
                    }
                }
                if (s != null)
                {
                    //calculate the interval in seconds
                    interval = new Tuple<int, int>(Convert.ToInt32((Convert.ToDouble(splitData[2]) - 6.0) * 60 * 60), Convert.ToInt32((Convert.ToDouble(splitData[3]) - 6.0) * 60 * 60));
                    passengersIn = Convert.ToDouble(splitData[4]);
                    passengersOut = Convert.ToDouble(splitData[5]);

                    s.addInterval(interval, passengersIn, passengersOut, Convert.ToInt32(splitData[1]));
                }
                s = null;
                data = sr.ReadLine();
            }
            sr.Close();
        }
    }
}
