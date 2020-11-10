"use strict";

class MachineTransitionSimple {
	constructor(values) {
		this.FromSymbol = 0;
		this.FromState = 0;
		this.WriteSymbol = 0;
		this.NextState = 0;
		this.NextStateHalts = false;
		this.MoveOffsetX = 0;
		
		if (values) {
			if (values.hasOwnProperty("FromSymbol")) { this.FromSymbol = values.FromSymbol; }
			if (values.hasOwnProperty("FromState")) { this.FromState = values.FromState; }
			if (values.hasOwnProperty("WriteSymbol")) { this.WriteSymbol = values.WriteSymbol; }
			if (values.hasOwnProperty("NextState")) { this.NextState = values.NextState; }
			if (values.hasOwnProperty("NextStateHalts")) { this.NextStateHalts = values.NextStateHalts; }
			if (values.hasOwnProperty("MoveOffsetX")) { this.MoveOffsetX = values.MoveOffsetX; }
		}
	}
	
	toString() {
		return `(${this.FromState},${this.FromSymbol}) -> (${this.WriteSymbol},${this.MoveOffsetX},${this.NextState})`;
	}
}