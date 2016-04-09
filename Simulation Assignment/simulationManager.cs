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
        simulationState _state;

        public simulationManager()
        {
            simulationTime = 0;
        }

        public void run()
        {
            simEvent currentEvent;

            // while there are events continue with the simulation
            while(!queue.isEmpty()) // probably change to EOS because of leftover events
            {
                currentEvent = queue.pop();
                simulationTime = currentEvent.time;

                currentEvent.executeEvent(_state);
            }
        }

        public void addEvent(simEvent e)
        {
            queue.addEvent(e);
        }

    }
}
