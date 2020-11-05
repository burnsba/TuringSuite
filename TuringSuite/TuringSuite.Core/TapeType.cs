using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Core
{
    /// <summary>
    /// Type of tapes supported by the application.
    /// </summary>
    public enum TapeType
    {
        DefaultUnknown = 0,

        /// <summary>
        /// One dimensional tape.
        /// Tape is non-finite.
        /// </summary>
        OneDimensionalNonFinite = 1,
    }
}
