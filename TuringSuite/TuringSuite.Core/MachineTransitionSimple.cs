using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Core
{
    public class MachineTransitionSimple : IStateTransitionDescription<int, byte>
    {
        public byte FromSymbol { get; set; }

        public int FromState { get; set; }

        public byte WriteSymbol { get; set; }

        /// <summary>
        /// Gets or sets the next state. If the next state is to halt,
        /// this value should be negative.
        /// </summary>
        public int NextState { get; set; }

        public bool NextStateHalts { get; set; }

        public int MoveOffsetX { get; set; }

        public override string ToString()
        {
            return $"({FromState},{FromSymbol}) -> ({WriteSymbol},{MoveOffsetX},{NextState})";
        }
    }
}
