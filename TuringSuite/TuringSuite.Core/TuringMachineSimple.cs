using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringSuite.Core.Error;

namespace TuringSuite.Core
{
    public class TuringMachineSimple : ITuringMachine<int, byte>
    {
        // There is the head position (theoretical Turing machine head position), which is being tracked, but
        // implemented in code on a limited machine. The tape is being modeled as an array, and the
        // head position starts out in the middle of the array. Therefore, the theoretical head position
        // needs to be tracked, as well as the actual array offset.
        private int _headPositionX = 0;
        private int _positionOffset = 0;
        private byte[] _tape;

        // Track the farthest left on the tape the machine head has visited.
        private int _minHeadIndex = 0;

        // Track the farthest right on the tape the machine head has visited.
        private int _maxHeadIndex = 0;

        // Track whether execution has reached a halting state.
        private bool _reachedHalt = false;

        private Dictionary<int, string> _friendlyStateMap = new Dictionary<int, string>();
        private Dictionary<string, int> _unfriendlyStateMap = new Dictionary<string, int>();
        private Dictionary<byte, string> _friendlySymbolMap = new Dictionary<byte, string>();
        private Dictionary<string, byte> _unfriendlySymbolMap = new Dictionary<string, byte>();

        /// <summary>
        /// Don't want to search through the List of transitions step, so build a lookup table.
        /// First key is the current state. This returns another lookup.
        /// Second key is the current symbol. This returns the transition information.
        /// </summary>
        private Dictionary<int, Dictionary<byte, IStateTransitionDescription<int, byte>>> _transitionLookup = new Dictionary<int, Dictionary<byte, IStateTransitionDescription<int, byte>>>();

        /// <inheritdoc />
        public List<int> NonHaltingStates { get; set; } = new List<int>();

        /// <inheritdoc />
        public List<byte> AlphabetSymbols { get; set; } = new List<byte>();

        /// <inheritdoc />
        public int InitialState { get; private set; }

        /// <inheritdoc />
        public List<int> HaltingStates { get; set; } = new List<int>();

        /// <inheritdoc />
        public int HeadPositionX
        {
            get
            {
                return _headPositionX - _positionOffset;
            }
        }

        /// <inheritdoc />
        public int CurrentState { get; private set; } = -1;

        /// <inheritdoc />
        public ulong StepCount { get; private set; } = 0;

        /// <inheritdoc />
        public List<IStateTransitionDescription<int, byte>> Transitions { get; set; } = new List<IStateTransitionDescription<int, byte>>();

        /// <summary>
        /// Gets the simulation tape size.
        /// </summary>
        public uint TapeSize { get; } = 0;

        /// <summary>
        /// Defines what action to perform if a state is encountered without
        /// a well-defined transition defintion.
        /// </summary>
        public StateNotFoundAction StateNotFoundAction { get; set; }

        /// <summary>
        /// Describes the underlying tape of the Turing machine.
        /// </summary>
        public TapeType TapeType => TapeType.OneDimensionalNonFinite;

        public TuringMachineSimple(uint tapeSize, int initialState)
        {
            InitialState = initialState;

            StateNotFoundAction = StateNotFoundAction.ThrowException;

            TapeSize = tapeSize;

            _tape = new byte[TapeSize];
        }

        /// <summary>
        /// Use this to manually configure the Turing machine (not parsed from JSON).
        /// Should be called before <see cref="InitRun"/>.
        /// </summary>
        public void InitFromTransitions()
        {
            NonHaltingStates = Transitions.Select(x => x.FromState)
                .Union(Transitions.Select(x => x.NextState))
                .Where(x => x >= 0)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            AlphabetSymbols = Transitions.Select(x => x.FromSymbol)
                .Union(Transitions.Select(x => x.WriteSymbol))
                .Where(x => x >= 0)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            HaltingStates = Transitions
                .Cast<MachineTransitionSimple>()
                .Where(x => x.NextStateHalts == true)
                .Select(x => x.NextState)
                .Where(x => x < 0)
                .Distinct()
                .OrderByDescending(x => x)
                .ToList();

            BuildTransitionLookup();
        }

        /// <summary>
        /// Use this to initialize the software implementation of the Turing machine
        /// before running it.
        /// </summary>
        public void InitRun()
        {
            StepCount = 0;

            _positionOffset = (int)(TapeSize >> 1);
            _headPositionX = _positionOffset;

            _minHeadIndex = _positionOffset;
            _maxHeadIndex = _positionOffset;

            // Array is inititalized to all zeroes. Otherwise, memset or something.
            _tape = new byte[TapeSize];

            CurrentState = InitialState;

            _reachedHalt = false;
        }

        /// <inheritdoc />
        public bool Step()
        {
            if (_reachedHalt)
            {
                return false;
            }

            var symbolTransitionLookup = _transitionLookup[CurrentState];

            IStateTransitionDescription<int, byte> transition = null;
            
            if (!symbolTransitionLookup.TryGetValue(_tape[_headPositionX], out transition))
            {
                switch (StateNotFoundAction)
                {
                    case StateNotFoundAction.Halt:
                        Halt();
                        return false;

                    case StateNotFoundAction.ThrowException:
                        throw new InvalidOperationException();

                    default:
                        throw new NotSupportedException();
                }
            }

            // Update tape value.
            _tape[_headPositionX] = transition.WriteSymbol;

            // This will move _headPositionX.
            MoveHead(transition.MoveOffsetX);

            // Ran out of ways to fail, now update the current state
            CurrentState = transition.NextState;

            StepCount++;

            if (((MachineTransitionSimple)transition).NextStateHalts)
            {
                Halt();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Copies the section of the tape that has been visited so far.
        /// </summary>
        /// <returns>Copy of visited tape.</returns>
        public byte[] GetVisitedTape()
        {
            var visitLength = _maxHeadIndex - _minHeadIndex + 1;

            if (visitLength <= 0)
            {
                return new byte[0];
            }

            var t = new byte[visitLength];
            Array.Copy(_tape, _minHeadIndex, t, 0, visitLength);

            return t;
        }

        /// <summary>
        /// Reads the content of a file as JSON and parses it into a Turning machine.
        /// </summary>
        /// <param name="path">Path of file to read.</param>
        /// <param name="tapeSize">Number of array elements to allocate for machine execution.</param>
        /// <returns>Turing machine.</returns>
        public static TuringMachineSimple FromJsonFile(string path, uint tapeSize)
        {
            var fileContents = System.IO.File.ReadAllText(path);

            return FromJson(fileContents, tapeSize);
        }

        /// <summary>
        /// Reads JSON and parses it into a Turning machine.
        /// </summary>
        /// <param name="json">JSON to read.</param>
        /// <param name="tapeSize">Number of array elements to allocate for machine execution.</param>
        /// <returns>Turing machine.</returns>
        public static TuringMachineSimple FromJson(string json, uint tapeSize)
        {
            var jtmd = JsonConvert.DeserializeObject<JsonTuringMachineDescription>(json);

            if (object.ReferenceEquals(null, jtmd))
            {
                throw new ConfigurationException("Could not parse definition.")
                {
                    ErrorCode = TuringSuite.Core.Error.ErrorCode.CouldNotParseJson,
                };
            }

            var friendlyStateMap = new Dictionary<int, string>();
            var unfriendlyStateMap = new Dictionary<string, int>();
            var friendlySymbolMap = new Dictionary<byte, string>();
            var unfriendlySymbolMap = new Dictionary<string, byte>();

            var haltingStates = new List<int>();
            var nonHaltingStates = new List<int>();
            var alphabetSymbols = new List<byte>();

            if (jtmd.HaltingStates.Count < 1)
            {
                throw new ConfigurationException("No halting states found.")
                {
                    ErrorCode = TuringSuite.Core.Error.ErrorCode.NoHaltingStatesFound,
                };
            }

            var haltingState = -1;
            foreach (var s in jtmd.HaltingStates.OrderBy(x => x))
            {
                friendlyStateMap.Add(haltingState, s);
                unfriendlyStateMap.Add(s, haltingState);
                haltingStates.Add(haltingState);
                haltingState--;
            }

            if (jtmd.NonHaltingStates.Count < 1)
            {
                throw new ConfigurationException("No non-halting states found.")
                {
                    ErrorCode = TuringSuite.Core.Error.ErrorCode.NoNonHaltingStatesFound,
                };
            }

            var state = 0;
            foreach (var s in jtmd.NonHaltingStates.OrderBy(x => x))
            {
                friendlyStateMap.Add(state, s);
                unfriendlyStateMap.Add(s, state);
                nonHaltingStates.Add(state);
                state++;
            }

            int initialState;

            if (!unfriendlyStateMap.TryGetValue(jtmd.InitialState, out initialState))
            {
                throw new ConfigurationException($"{nameof(JsonTuringMachineDescription.InitialState)}='{initialState}', but not included in {nameof(JsonTuringMachineDescription.NonHaltingStates)} or {nameof(JsonTuringMachineDescription.HaltingStates)}")
                {
                    ErrorCode = TuringSuite.Core.Error.ErrorCode.InitialStateNotDescribed,
                };
            }

            var tms = new TuringMachineSimple(tapeSize, initialState);

            tms._friendlyStateMap = friendlyStateMap;
            tms._unfriendlyStateMap = unfriendlyStateMap;

            var knownSymbols = jtmd.Transitions.Select(x => x.FromSymbol)
                .Union(jtmd.Transitions.Select(y => y.WriteSymbol))
                .Distinct()
                .ToList();

            if (knownSymbols.Count < 1)
            {
                throw new ConfigurationException($"No symbols found in transition defintions (property {nameof(ParsedStateTransitionDescription.FromSymbol)} and {nameof(ParsedStateTransitionDescription.WriteSymbol)}).")
                {
                    ErrorCode = TuringSuite.Core.Error.ErrorCode.NoKnownSymbolsDescribed,
                };
            }

            byte symbol = 0;
            foreach (var s in knownSymbols)
            {
                friendlySymbolMap.Add(symbol, s);
                unfriendlySymbolMap.Add(s, symbol);
                alphabetSymbols.Add(symbol);
                symbol++;
            }

            tms._friendlySymbolMap = friendlySymbolMap;
            tms._unfriendlySymbolMap = unfriendlySymbolMap;
            tms.HaltingStates = haltingStates;
            tms.NonHaltingStates = nonHaltingStates;
            tms.AlphabetSymbols = alphabetSymbols;

            if (jtmd.Transitions.Count < 1)
            {
                throw new ConfigurationException("No transition definitions were found.")
                {
                    ErrorCode = TuringSuite.Core.Error.ErrorCode.NoTransitionsFound,
                };
            }

            foreach (var t in jtmd.Transitions)
            {
                // States need to match the HaltingStates+NonHaltingStates properties, but symbols are
                // extracted from the transition definitions.
                int fromState;
                int nextState;

                byte fromSymbol = unfriendlySymbolMap[t.FromSymbol];
                byte writeSymbol = unfriendlySymbolMap[t.WriteSymbol];

                if (!unfriendlyStateMap.TryGetValue(t.FromState, out fromState)) 
                {
                    throw new ConfigurationException($"{nameof(ParsedStateTransitionDescription.FromState)}='{t.FromState}' was listed in transition definitions, but not included in {nameof(JsonTuringMachineDescription.NonHaltingStates)} or {nameof(JsonTuringMachineDescription.HaltingStates)}")
                    {
                        ErrorCode = TuringSuite.Core.Error.ErrorCode.TransitionFoundButNotListedHaltingNonHalting,
                    };
                }

                if (!unfriendlyStateMap.TryGetValue(t.NextState, out nextState))
                {
                    throw new ConfigurationException($"{nameof(ParsedStateTransitionDescription.NextState)}='{t.NextState}' was listed in transition definitions, but not included in {nameof(JsonTuringMachineDescription.NonHaltingStates)} or {nameof(JsonTuringMachineDescription.HaltingStates)}")
                    {
                        ErrorCode = TuringSuite.Core.Error.ErrorCode.TransitionFoundButNotListedHaltingNonHalting,
                    };
                }

                var mts = new MachineTransitionSimple()
                {
                    FromState = fromState,
                    FromSymbol = fromSymbol,
                    MoveOffsetX = t.MoveOffsetX,
                    NextState = nextState,
                    WriteSymbol = writeSymbol,
                };

                if (haltingStates.Contains(nextState))
                {
                    mts.NextStateHalts = true;
                }
                else
                {
                    mts.NextStateHalts = false;
                }

                tms.Transitions.Add(mts);
            }

            tms.BuildTransitionLookup();

            return tms;
        }

        /// <summary>
        /// Moves the machine head by the given offset. Updates min/max position visited
        /// appropriately.
        /// </summary>
        /// <param name="offset"></param>
        private void MoveHead(int offset)
        {
            if ((long)_headPositionX + (long)offset >=  (long)int.MaxValue)
            {
                throw new IndexOutOfRangeException();
            }
            else if (_headPositionX + offset < 0)
            {
                throw new IndexOutOfRangeException();
            }

            _headPositionX += offset;

            if (_headPositionX < _minHeadIndex)
            {
                _minHeadIndex = _headPositionX;
            }

            // Very first step will set min and max, and it may be the min/max
            // for the entire run. Therefore, this can't be an else if.
            if (_headPositionX > _maxHeadIndex)
            {
                _maxHeadIndex = _headPositionX;
            }
        }

        /// <summary>
        /// Changes application state of machine to be halted, updating internal variables.
        /// </summary>
        private void Halt()
        {
            _reachedHalt = true;
        }

        /// <summary>
        /// Creates lookup table of transitions for program execution.
        /// </summary>
        private void BuildTransitionLookup()
        {
            _transitionLookup = new Dictionary<int, Dictionary<byte, IStateTransitionDescription<int, byte>>>();

            foreach (var state in NonHaltingStates)
            {
                var stateLookup = new Dictionary<byte, IStateTransitionDescription<int, byte>>();
                var stateTransitions = Transitions.Where(x => x.FromState == state);

                foreach (var symbol in AlphabetSymbols)
                {
                    var symbolTransition = stateTransitions.FirstOrDefault(x => x.FromSymbol == symbol);

                    if (!object.ReferenceEquals(null, symbolTransition))
                    {
                        stateLookup.Add(symbol, symbolTransition);
                    }
                }

                _transitionLookup.Add(state, stateLookup);
            }
        }
    }
}
