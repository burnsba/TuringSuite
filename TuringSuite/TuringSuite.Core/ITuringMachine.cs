using System;
using System.Collections.Generic;

namespace TuringSuite.Core
{
    public interface ITuringMachine<TState, TSymbol>
    {
        List<TState> States { get; set; }

        List<TSymbol> AlphabetSymbols { get; set; }

        TSymbol BlankSymbol { get; }

        TState InitialState { get; }

        List<TState> HaltingStates { get; set; }

        int HeadPositionX { get; }

        TState CurrentState { get; }

        List<IStateTransitionDescription<TState, TSymbol>> Transitions { get; set; }

        bool Step();
    }
}
