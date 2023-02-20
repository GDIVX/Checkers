using System.Collections;
using System.Collections.Generic;

namespace Mishbetzet.Turns
{
    /// <summary>
    /// Manages the turn order for ITurnTrackers in the game
    /// </summary>
    public class TurnManager : IEnumerable
    {
        List<ITurnTracker> _turnOrder = new();
        int index = 0;

        /// <summary>
        /// Add an ITurnTracker to the turn order
        /// </summary>
        /// <param name="turnTracker"></param>
        public void AddToTurnOrder(ITurnTracker turnTracker)
        {
            _turnOrder.Add(turnTracker);
        }

        public void AddToTurnOrder(ITurnTracker turnTracker, int index)
        {
            _turnOrder[index] = turnTracker;
        }

        /// <summary>
        /// Remove ITurnTracker from the turn order
        /// </summary>
        /// <param name="turnTracker"></param>
        public void RemoveFromTurnOrder(ITurnTracker turnTracker)
        {
            _turnOrder.Remove(turnTracker);
        }

        /// <summary>
        /// Start the current turn
        /// </summary>
        public void StartTurn()
        {
            //null check
            if (_turnOrder.Count == 0) return;

            _turnOrder[index].OnTurnStart();
        }

        /// <summary>
        /// Skip the current turn
        /// </summary>
        public void Skip()
        {
            index++;
            StartTurn();
        }

        /// <summary>
        /// End the current turn
        /// </summary>
        public void EndTurn()
        {
            index++;
            if (index >= _turnOrder.Count)
            {
                index = 0;
            }
        }

        /// <summary>
        /// Stop tracking all turns
        /// </summary>
        public void ClearTurnOrder()
        {
            _turnOrder.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            return _turnOrder.GetEnumerator();
        }
    }

}