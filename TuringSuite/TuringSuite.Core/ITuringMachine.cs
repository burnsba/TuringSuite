using System;
using System.Collections.Generic;

namespace TuringSuite.Core
{
    public interface ITuringMachine<TState, TSymbol>
    {
        List<TState> NonHaltingStates { get; set; }

        List<TSymbol> AlphabetSymbols { get; set; }

        TState InitialState { get; }

        /// <summary>
        /// Gets or sets halting states. Values should be negative.
        /// </summary>
        List<TState> HaltingStates { get; set; }

        int HeadPositionX { get; }

        TState CurrentState { get; }

        TapeType TapeType { get; }

        List<IStateTransitionDescription<TState, TSymbol>> Transitions { get; set; }

        bool Step();
    }
}
