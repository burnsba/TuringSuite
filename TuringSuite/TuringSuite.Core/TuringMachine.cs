using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Core
{
    public class TuringMachine : ITuringMachine
    {
        private int _headPosition = 0;
        private int _currentState = -1;

        public List<int> States { get; set; }

        public List<int> AlphabetSymbols { get; set; }

        public int BlankSymbol { get; set; }

        public int InitialState { get; set; }

        public List<int> HaltingStates { get; set; }

        public int HeadPosition
        {
            get
            {
                return _headPosition;
            }
        }

        public int CurrentState
        {
            get
            {
                return _currentState;
            }
        }

        public TuringMachine()
        {
            States = new List<int>();
            AlphabetSymbols = new List<int>();
            HaltingStates = new List<int>();
        }

        public void TransitionFunction()
        {
            throw new NotImplementedException();
        }
    }
}
