using System;
using System.Collections.Generic;

namespace TuringSuite.Core
{
    /// <summary>
    /// Interface to define a Turing machine.
    /// </summary>
    /// <typeparam name="TState">Runtime type for machine state.</typeparam>
    /// <typeparam name="TSymbol">Runtime type for machine symbol.</typeparam>
    public interface ITuringMachine<TState, TSymbol>
    {
        /// <summary>
        /// Gets or sets the collection of states used by the machine that do not halt.
        /// </summary>
        List<TState> NonHaltingStates { get; set; }

        /// <summary>
        /// Gets or sets the collection of symbols used by the machine.
        /// </summary>
        List<TSymbol> AlphabetSymbols { get; set; }

        /// <summary>
        /// Gets the state the machine states in.
        /// </summary>
        TState InitialState { get; }

        /// <summary>
        /// Gets or sets halting states. Values should be negative.
        /// </summary>
        List<TState> HaltingStates { get; set; }

        /// <summary>
        /// Gets the current position of the tape head.
        /// </summary>
        int HeadPositionX { get; }

        /// <summary>
        /// Gets the current state of the machine.
        /// </summary>
        TState CurrentState { get; }

        /// <summary>
        /// Gets the description of the machine tape (1-dimensional or more, finite or not, etc).
        /// </summary>
        TapeType TapeType { get; }

        /// <summary>
        /// Gets or sets the transitions used by this machine.
        /// </summary>
        List<IStateTransitionDescription<TState, TSymbol>> Transitions { get; set; }

        /// <summary>
        /// Performs one iteration of execution.
        /// </summary>
        /// <returns>True, if the machine is able to continue execution after the step, false if it reached a halting state.</returns>
        bool Step();
    }
}
