"use strict";

class TuringMachineSimple {
	 constructor(tapeSize, initialState) {
		 this._headPositionX = 0;
		 this._positionOffset = 0;
		 this._tape = [];
		 
		 this._minHeadIndex = 0;
		 this._maxHeadIndex = 0;
		 this._reachedHalt = false;
		 
		 this._friendlyStateMap = {};
		 this._unfriendlyStateMap = {};
		 this._friendlySymbolMap = {};
		 this._unfriendlySymbolMap = {};
		 
		 this._transitionLookup = {};
		 
		 this.NonHaltingStates = [];
		 this.AlphabetSymbols = [];
		 this.InitialState = [];
		 this.BlankSymbol = [];
		 this.HaltingStates = [];
		 
		 Object.defineProperty(this, 'HeadPositionX', {
			get() { return this._headPositionX - this._positionOffset; } 
		 });
		 
		 this.CurrentState = -1;
		 
		 Object.defineProperty(this, 'CurrentSymbol', {
			get() {
				if (this._tape === null || this._tape === undefined) {
                    return 0;
                }

                if (this._tape.length < 1) {
                    return 0;
                }

                if (this._headPositionX < 0 || (this._headPositionX + 1) > this._tape.length) {
                    return 0;
                }

                return this._tape[this._headPositionX];
			} 
		 });
		 
		 this.StepCount = 0;
		 this.Transitions = [];
		 this.TapeSize = 0;
		 this.StateNotFoundAction = 'ThrowException';
		 this.TapeType = 'OneDimensionalNonFinite';
		 
		 // ////// Actual constructor code now
		 this.InitialState = initialState;
		 this.TapeSize = tapeSize;
		 
		 this._tape = Array.from({length: this.TapeSize}, x => 0);
	 }
	 
	 InitFromTransitions() {
		for (const x of this.Transitions) {
			if (x.FromState >= 0 
					&& this.NonHaltingStates.indexOf(x.FromState) < 0) {
				this.NonHaltingStates.push(x.FromState);
			}
			
			if (x.NextState >= 0
					&& this.NonHaltingStates.indexOf(x.NextState) < 0) {
				this.NonHaltingStates.push(x.NextState);
			}
		}
		
		this.NonHaltingStates.sort(compareNumbers);
		
		for (const x of this.Transitions) {
			if (x.FromSymbol >= 0 
					&& this.AlphabetSymbols.indexOf(x.FromSymbol) < 0) {
				this.AlphabetSymbols.push(x.FromSymbol);
			}
			
			if (x.WriteSymbol >= 0
					&& this.AlphabetSymbols.indexOf(x.WriteSymbol) < 0) {
				this.AlphabetSymbols.push(x.WriteSymbol);
			}
		}
		
		this.AlphabetSymbols.sort(compareNumbers);
		
		for (const x of this.Transitions) {
			if (x.NextStateHalts == true
					&& x.NextState < 0
					&& this.HaltingStates.indexOf(x.NextState) < 0) {
				this.HaltingStates.push(x.NextState);
			}
		}
		
		this.HaltingStates.sort(compareNumbers);
		
		this.BuildTransitionLookup();
	}
	
	InitRun() {
		this.StepCount = 0;

		this._positionOffset = (this.TapeSize >> 1);
		this._headPositionX = this._positionOffset;

		this._minHeadIndex = this._positionOffset;
		this._maxHeadIndex = this._positionOffset;

		this._tape = Array.from({length: this.TapeSize}, x => 0);

		this.CurrentState = this.InitialState;

		this._reachedHalt = false;
	}
	
	Step() {
		if (this._reachedHalt) {
			return false;
		}

		let symbolTransitionLookup = this._transitionLookup[this.CurrentState];

		let transition = null;
		try {
			transition = symbolTransitionLookup[this._tape[this._headPositionX]];
		} catch {
			if (this.StateNotFoundAction === "Halt") {
				this.Halt();
				return false;
			} else if (this.StateNotFoundAction === "ThrowException") {
				throw "InvalidOperationException";
			} else {
				throw "NotSupportedException";
			}			
		}

		// Update tape value.
		this._tape[this._headPositionX] = transition.WriteSymbol;

		// This will move _headPositionX.
		this.MoveHead(transition.MoveOffsetX);

		// Ran out of ways to fail, now update the current state
		this.CurrentState = transition.NextState;

		this.StepCount++;

		if (transition.NextStateHalts) {
			this.Halt();
			return false;
		}

		return true;
	}
	
	GetVisitedTape() {
		let visitLength = this._maxHeadIndex - this._minHeadIndex + 1;

		if (visitLength <= 0) {
			return [0];
		}
		
		let t = Array.from({length: visitLength}, x => 0);
		
		for (let i=0; i<visitLength; i++) {
			t[i] = this._tape[this._minHeadIndex + i];
		}
		
		return t;
	}
	
	GetVisitedTapeSections() {
		let visitLength = this._maxHeadIndex - this._minHeadIndex + 1;
		let originOffset = this._positionOffset - this._minHeadIndex;
		
		if (visitLength <= 0) {
			return [0];
		}
		
		let t = {"left": [], "origin": [], "right": []};
		
		for (let i=0; i<visitLength; i++) {
			if (i < originOffset) {
				t["left"].push(this._tape[this._minHeadIndex + i]);
			} else if (i === originOffset) {
				t["origin"].push(this._tape[this._minHeadIndex + i]);
			} else {
				t["right"].push(this._tape[this._minHeadIndex + i]);
			}
		}

		return t;
	}
	
	static FromJson(json, tapeSize) {
		let jtmd = JSON.parse(json);
		
		return TuringMachineSimple.FromJsonObject(jtmd, tapeSize);
	}
	
	static FromJsonObject(jtmd, tapeSize) {
		if (jtmd === null || jtmd === undefined) {
			throw "Could not parse definition.";
		}
		
		let friendlyStateMap = {};
		let unfriendlyStateMap = {};
		let friendlySymbolMap = {};
		let unfriendlySymbolMap = {};
		
		let haltingStates = [];
		let nonHaltingStates = [];
		let alphabetSymbols = [];
		
		if (jtmd.HaltingStates.length < 1)
		{
			throw "No halting states found.";
		}
		
		jtmd.HaltingStates.sort();
		let haltingState = -1;
		for (const s of jtmd.HaltingStates)
		{
			friendlyStateMap[haltingState] = s;
			unfriendlyStateMap[s] = haltingState;
			haltingStates.push(haltingState);
			haltingState--;
		}
		
		if (jtmd.NonHaltingStates.length < 1)
		{
			throw "No non-halting states found.";
		}
		
		jtmd.NonHaltingStates.sort();
		let state = 0;
		for (const s of jtmd.NonHaltingStates)
		{
			friendlyStateMap[state] = s;
			unfriendlyStateMap[s] = state;
			nonHaltingStates.push(state);
			state++;
		}
		
		let initialState = unfriendlyStateMap[jtmd.InitialState];
		
		if (initialState === null || initialState === undefined)
		{
			throw `InitialState='${initialState}', but not included in NonHaltingStates or HaltingStates`;
		}
		
		let tms = new TuringMachineSimple(tapeSize, initialState);
		
		tms._friendlyStateMap = friendlyStateMap;
		tms._unfriendlyStateMap = unfriendlyStateMap;
		
		let knownSymbols = [];
		
		for (const x of jtmd.Transitions) {
			if (x.FromSymbol >= 0 
					&& knownSymbols.indexOf(x.FromSymbol) < 0) {
				knownSymbols.push(x.FromSymbol);
			}
			
			if (x.WriteSymbol >= 0
					&& knownSymbols.indexOf(x.WriteSymbol) < 0) {
				knownSymbols.push(x.WriteSymbol);
			}
		}
		
		knownSymbols.sort(compareNumbers);
		
		if (knownSymbols.length < 1)
		{
			throw "No symbols found in transition defintions (property FromSymbol and WriteSymbol).";
		}
		
		let symbol = 0;
		for (const s of knownSymbols)
		{
			friendlySymbolMap[symbol] = s;
			unfriendlySymbolMap[s] = symbol;
			alphabetSymbols.push(symbol);
			symbol++;
		}
		
		tms._friendlySymbolMap = friendlySymbolMap;
		tms._unfriendlySymbolMap = unfriendlySymbolMap;
		tms.HaltingStates = haltingStates;
		tms.NonHaltingStates = nonHaltingStates;
		tms.AlphabetSymbols = alphabetSymbols;
		
		if (jtmd.Transitions.length < 1)
		{
			throw "No transition definitions were found.";
		}
		
		for (const t of jtmd.Transitions)
		{
			// States need to match the HaltingStates+NonHaltingStates properties, but symbols are
			// extracted from the transition definitions.
			let fromState;
			let nextState;
		
			let fromSymbol = unfriendlySymbolMap[t.FromSymbol];
			let writeSymbol = unfriendlySymbolMap[t.WriteSymbol];
			
			fromState = unfriendlyStateMap[t.FromState];
			if (fromState === null || fromState === undefined) 
			{
				throw `FromState='${t.FromState}' was listed in transition definitions, but not included in NonHaltingStates or HaltingStates`;
			}
		
			nextState = unfriendlyStateMap[t.NextState];
			if (nextState === null || nextState === undefined)
			{
				throw `NextState='${t.NextState}' was listed in transition definitions, but not included in NonHaltingStates or HaltingStates`;
			}
		
			let mts = new MachineTransitionSimple({
				FromState: fromState,
				FromSymbol: fromSymbol,
				MoveOffsetX: t.MoveOffsetX,
				NextState: nextState,
				WriteSymbol: writeSymbol
			});
		
			if (haltingStates.indexOf(nextState) > -1)
			{
				mts.NextStateHalts = true;
			}
			else
			{
				mts.NextStateHalts = false;
			}
		
			tms.Transitions.push(mts);
		}
		
		tms.BuildTransitionLookup();
		
		return tms;
	}
	
	MoveHead(offset) {
		if (this._headPositionX + offset < 0)
		{
			throw "IndexOutOfRangeException";
		}

		this._headPositionX += offset;

		if (this._headPositionX < this._minHeadIndex)
		{
			this._minHeadIndex = this._headPositionX;
		}

		// Very first step will set min and max, and it may be the min/max
		// for the entire run. Therefore, this can't be an else if.
		if (this._headPositionX > this._maxHeadIndex)
		{
			this._maxHeadIndex = this._headPositionX;
		}
	}
	
	Halt() {
		this._reachedHalt = true;
	}
	
	BuildTransitionLookup() {
		this._transitionLookup = {};

		for (const state of this.NonHaltingStates)
		{
			let stateLookup = {};
			let stateTransitions = this.Transitions.filter(x => x.FromState === state);

			for (const symbol of this.AlphabetSymbols)
			{
				let symbolTransition = stateTransitions.find(x => x.FromSymbol === symbol);

				if (this.symbolTransition === null || this.symbolTransition === undefined)
				{
					stateLookup[symbol] = symbolTransition;
				}
			}

			this._transitionLookup[state] = stateLookup;
		}
	}
}