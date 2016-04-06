using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    public class simulationManager
    {
        private EventQueue queue;
        int simulationTime;

        public simulationManager()
        {
            simulationTime = 0;
        }

        public void run()
        {
            simEvent currentEvent;

            // while there are events continue with the simulation
            while(!queue.isEmpty())
            {
                currentEvent = queue.pop();
                simulationTime = currentEvent.time;

                currentEvent.executeEvent();
            }
        }

        public void addEvent(simEvent e)
        {
            queue.addEvent(e);
        }

    }
}
