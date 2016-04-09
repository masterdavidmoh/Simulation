using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Assignment
{
    class EventQueue
    {
        List<simEvent> eventList;

        public EventQueue()
        {
            eventList = new List<simEvent>();
        }

        public void addEvent(simEvent e)
        {
            eventList.Add(e);
            eventList.Sort((x, y) => x.time.CompareTo(y.time));
        }

        /// <summary>
        /// checks if the queue is empty
        /// </summary>
        /// <returns>true if the queue is empty, false otherwise</returns>
        public bool isEmpty()
        {
            return eventList.Count == 0;
        }

        /// <summary>
        /// pops the first element of the queue
        /// </summary>
        /// <returns>the first element of the event queue, null if there is none</returns>
        public simEvent pop()
        {
            simEvent retvalue = eventList[0];
            eventList.RemoveAt(0);

            return retvalue;
        }
    }
}
