using System;
using System.Collections.Generic;

namespace TuringSuite.Core
{
    public interface ITuringMachine
    {
        List<int> States { get; set; }

        List<int> AlphabetSymbols { get; set; }

        int BlankSymbol { get; set; }

        int InitialState { get; set; }

        List<int> HaltingStates { get; set; }

        int HeadPosition { get; }

        int CurrentState { get; }

        void TransitionFunction();
    }
}
