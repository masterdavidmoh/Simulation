﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation_Assignment
{
    class simSwitchEvent:Event
    {
        public simSwitchEvent(int time, int stationID)
            :base(EventType.Switch,time)
        {

        }

        public override void executeEvent()
        {
            throw new NotImplementedException();
        }
    }
}