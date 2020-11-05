using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Core
{
    /// <summary>
    /// Intermediate class to transfer JSON description into strongly typed class.
    /// </summary>
    internal class ParsedStateTransitionDescription : IStateTransitionDescription<string, string>
    {
        public string FromSymbol { get; set; }

        public string FromState { get; set; }

        public string WriteSymbol { get; set; }

        public string NextState { get; set; }

        public int MoveOffsetX { get; set; }

        public override string ToString()
        {
            return $"({FromState},{FromSymbol}) -> ({WriteSymbol},{MoveOffsetX},{NextState})";
        }
    }
}
