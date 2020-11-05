using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Core
{
    public class JsonTuringMachineDescription
    {
        public string Author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/ISO_8601
        /// </remarks>
        public string Date { get; set; }
        public string Version { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }

        public string TapeType { get; set; }

        public List<string> NonHaltingStates { get; set; }

        public List<string> HaltingStates { get; set; }

        public string InitialState { get; set; }

        public List<ParsedStateTransitionDescription> Transitions { get; set; }
    }
}
