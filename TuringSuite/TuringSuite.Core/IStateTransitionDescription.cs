using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Core
{
    /// <summary>
    /// Interface to define a state transition, used by the transition function.
    /// </summary>
    /// <typeparam name="TState">Runtime type for machine state.</typeparam>
    /// <typeparam name="TSymbol">Runtime type for machine symbol.</typeparam>
    public interface IStateTransitionDescription<TState, TSymbol>
    {
        /// <summary>
        /// Gets or sets the symbol this transition applies to.
        /// </summary>
        TSymbol FromSymbol { get; set; }

        /// <summary>
        /// Gets or sets the state this transition applies to.
        /// </summary>
        TState FromState { get; set; }

        /// <summary>
        /// Gets or sets the symbol that is written to the tape for this transition.
        /// </summary>
        TSymbol WriteSymbol { get; set; }

        /// <summary>
        /// Gets or sets the state to be transitioned to. If the next state should halt,
        /// this value should be negative.
        /// </summary>
        TState NextState { get; set; }

        /// <summary>
        /// Gets or sets the number of cells to move the tape head for this transition.
        /// Positive value is to the right, negative value is to the left.
        /// </summary>
        int MoveOffsetX { get; set; }
    }
}
