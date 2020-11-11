"use strict";

const Bb1d2s2s = {
	"TapeType": "1DimensionalNonFinite",
	"NonHaltingStates": ["A", "B"],
	"HaltingStates": ["HALT"],
	"InitialState": "A",
	
	"Transitions": [
		{
			"FromState": "A",
			"FromSymbol": "0",
			"MoveOffsetX": 1,
			"NextState": "B",
			"WriteSymbol": "1",
		},
		{
			"FromState": "A",
			"FromSymbol": "1",
			"MoveOffsetX": -1,
			"NextState": "B",
			"WriteSymbol": "1",
		},
		{
			"FromState": "B",
			"FromSymbol": "0",
			"MoveOffsetX": -1,
			"NextState": "A",
			"WriteSymbol": "1",
		},
		{
			"FromState": "B",
			"FromSymbol": "1",
			"MoveOffsetX": 1,
			"NextState": "HALT",
			"WriteSymbol": "1",
		}
	]
};