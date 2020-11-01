using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Core
{
    public interface IStateTransitionDescription<TState, TSymbol>
    {
        TSymbol FromSymbol { get; set; }

        TState FromState { get; set; }

        TSymbol WriteSymbol { get; set; }

        TState NextState { get; set; }

        bool NextStateHalts { get; set; }

        int MoveOffsetX { get; set; }
    }
}
