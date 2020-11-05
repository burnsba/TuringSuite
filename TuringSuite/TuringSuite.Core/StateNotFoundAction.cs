using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Core
{
    /// <summary>
    /// Defines what action to perform if a state is encountered without
    /// a well-defined transition defintion.
    /// </summary>
    public enum StateNotFoundAction
    {
        UnknownDefault = 0,

        /// <summary>
        /// Halt execution of machine.
        /// </summary>
        Halt,

        /// <summary>
        /// Throw a program exception.
        /// </summary>
        ThrowException,
    }
}
