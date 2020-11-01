using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        /// <summary>
        /// Don't want to search through the List of transitions step, so build a lookup table.
        /// First key is the current state. This returns another lookup.
        /// Second key is the current symbol. This returns the transition information.
        /// </summary>
        private Dictionary<int, Dictionary<byte, IStateTransitionDescription<int, byte>>> _transitionLookup;

        private byte[] _tape;

        public List<int> States { get; set; }

        public List<byte> AlphabetSymbols { get; set; }

        public byte BlankSymbol { get; private set; }

        public int InitialState { get; private set; }

        public List<int> HaltingStates { get; set; }

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

        public List<IStateTransitionDescription<int, byte>> Transitions { get; set; }

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

        public TuringMachineSimple(uint tapeSize, byte blankSymbol, int initialState)
        {
            States = new List<int>();
            AlphabetSymbols = new List<byte>();
            HaltingStates = new List<int>();

            Transitions = new List<IStateTransitionDescription<int, byte>>();

            _transitionLookup = new Dictionary<int, Dictionary<byte, IStateTransitionDescription<int, byte>>>();

            BlankSymbol = blankSymbol;
            InitialState = initialState;

            StateNotFoundAction = StateNotFoundAction.ThrowException;

            _tapeSize = tapeSize;
            _positionOffset = (int)(_tapeSize >> 1);
            _headPositionX = _positionOffset;

            // Array is inititalized to all zeroes
            _tape = new byte[_tapeSize];

            _currentState = initialState;
        }

        public void InitTransitions()
        {
            foreach (var state in States)
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
    }
}
