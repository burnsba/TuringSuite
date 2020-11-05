using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Core
{
    /// <summary>
    /// Intermediate class to transfer JSON description into strongly typed class.
    /// </summary>
    public class ParsedStateTransitionDescription : IStateTransitionDescription<string, string>
    {
        public string FromSymbol { get; set; }

        public string FromState { get; set; }

        public string WriteSymbol { get; set; }

        public string NextState { get; set; }

        /// <summary>
        /// Not used by JSON. Determined by other properties.
        /// </summary>
        public bool NextStateHalts { get; set; }

        public int MoveOffsetX { get; set; }

        public override string ToString()
        {
            return $"({FromState},{FromSymbol}) -> ({WriteSymbol},{MoveOffsetX},{NextState})";
        }
    }
}
