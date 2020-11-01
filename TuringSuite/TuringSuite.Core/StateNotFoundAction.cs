using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Core
{
    public enum StateNotFoundAction
    {
        UnknownDefault = 0,

        Halt,

        ThrowException,
    }
}
