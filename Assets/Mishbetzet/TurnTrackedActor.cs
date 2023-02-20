using Mishbetzet.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishbetzet
{
    /// <summary>
    /// An actor that is tracked by the turn manager
    /// </summary>
    public abstract class TurnTrackedActor : Actor, ITurnTracker
    {

        public TurnTrackedActor()
        {
            Core.Main.TurnManager.AddToTurnOrder(this);
        }

        public abstract void StartTurn();

        void ITurnTracker.OnTurnStart()
        {
            StartTurn();
        }
    }
}
