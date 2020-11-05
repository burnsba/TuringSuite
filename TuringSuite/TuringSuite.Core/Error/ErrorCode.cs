using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Core.Error
{
    public static class ErrorCode
    {
        public const int CouldNotParseJson = 100;
        public const int NoHaltingStatesFound = 110;
        public const int NoNonHaltingStatesFound = 120;
        public const int InitialStateNotDescribed = 130;
        public const int NoKnownSymbolsDescribed = 140;
        public const int NoTransitionsFound = 150;
        public const int TransitionFoundButNotListedHaltingNonHalting = 160;
    }
}
