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
        private int _headPositionX = 0;
        private int _currentState = -1;
        private uint _tapeSize = 0;
        private int _positionOffset = 0;

        private int _minHeadIndex = int.MaxValue;
        private int _maxHeadIndex = -1;
        private bool _reachedHalt = false;

        private ulong _stepCount = 0;

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

        private byte[] _tape;

        public List<int> NonHaltingStates { get; set; } = new List<int>();

        public List<byte> AlphabetSymbols { get; set; } = new List<byte>();

        public int InitialState { get; private set; }

        public List<int> HaltingStates { get; set; } = new List<int>();

        public int HeadPositionX
        {
            get
            {
                return _headPositionX - _positionOffset;
            }
        }

        public int CurrentState
        {
            get
            {
                return _currentState;
            }
        }

        public ulong StepCount => _stepCount;

        public List<IStateTransitionDescription<int, byte>> Transitions { get; set; } = new List<IStateTransitionDescription<int, byte>>();

        /// <summary>
        /// Gets the simulation tape size.
        /// </summary>
        public uint TapeSize
        {
            get
            {
                return _tapeSize;
            }
        }

        public StateNotFoundAction StateNotFoundAction { get; set; }

        public TapeType TapeType => TapeType.OneDimensionalNonFinite;

        public TuringMachineSimple(uint tapeSize, int initialState)
        {
            InitialState = initialState;

            StateNotFoundAction = StateNotFoundAction.ThrowException;

            _tapeSize = tapeSize;
        }

        /// <summary>
        /// Use this to manually configure the Turing machine (not parsed from JSON).
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
                .Where(x => x.NextStateHalts == true)
                .Select(x => x.NextState)
                .Where(x => x < 0)
                .Distinct()
                .OrderByDescending(x => x)
                .ToList();

            BuildTransitionLookup();
        }

        public void InitRun()
        {
            _stepCount = 0;

            _positionOffset = (int)(_tapeSize >> 1);
            _headPositionX = _positionOffset;

            // Array is inititalized to all zeroes
            _tape = new byte[_tapeSize];

            _currentState = InitialState;

            _reachedHalt = false;
        }

        public bool Step()
        {
            if (_reachedHalt)
            {
                return false;
            }

            var symbolTransitionLookup = _transitionLookup[_currentState];

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
            _currentState = transition.NextState;

            _stepCount++;

            if (transition.NextStateHalts)
            {
                Halt();
                return false;
            }

            return true;
        }

        public byte[] GetVisitedTape()
        {
            var visitLength = _maxHeadIndex - _minHeadIndex + 1;
            var t = new byte[visitLength];
            Array.Copy(_tape, _minHeadIndex, t, 0, visitLength);

            return t;
        }

        public static TuringMachineSimple FromJsonFile(string path, uint tapeSize)
        {
            var fileContents = System.IO.File.ReadAllText(path);

            return FromJson(fileContents, tapeSize);
        }

        public static TuringMachineSimple FromJson(string json, uint tapeSize)
        {
            var jtmd = JsonConvert.DeserializeObject<JsonTuringMachineDescription>(json);

            var friendlyStateMap = new Dictionary<int, string>();
            var unfriendlyStateMap = new Dictionary<string, int>();
            var friendlySymbolMap = new Dictionary<byte, string>();
            var unfriendlySymbolMap = new Dictionary<string, byte>();

            var haltingStates = new List<int>();
            var nonHaltingStates = new List<int>();
            var alphabetSymbols = new List<byte>();

            if (jtmd.HaltingStates.Count < 1)
            {
                throw new ConfigurationException("No halting states found.");
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
                throw new ConfigurationException("No non-halting states found.");
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
                throw new ConfigurationException($"{nameof(JsonTuringMachineDescription.InitialState)}='{initialState}', but not included in {nameof(JsonTuringMachineDescription.NonHaltingStates)} or {nameof(JsonTuringMachineDescription.HaltingStates)}");
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
                throw new ConfigurationException($"No symbols found in transition defintions (property {nameof(ParsedStateTransitionDescription.FromSymbol)} and {nameof(ParsedStateTransitionDescription.WriteSymbol)}).");
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
                throw new ConfigurationException("No transition definitions were found.");
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
                    throw new ConfigurationException($"{nameof(ParsedStateTransitionDescription.FromState)}='{t.FromState}' was listed in transition definitions, but not included in {nameof(JsonTuringMachineDescription.NonHaltingStates)} or {nameof(JsonTuringMachineDescription.HaltingStates)}");
                }

                if (!unfriendlyStateMap.TryGetValue(t.NextState, out nextState))
                {
                    throw new ConfigurationException($"{nameof(ParsedStateTransitionDescription.NextState)}='{t.NextState}' was listed in transition definitions, but not included in {nameof(JsonTuringMachineDescription.NonHaltingStates)} or {nameof(JsonTuringMachineDescription.HaltingStates)}");
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

        private void Halt()
        {
            _reachedHalt = true;
        }

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
