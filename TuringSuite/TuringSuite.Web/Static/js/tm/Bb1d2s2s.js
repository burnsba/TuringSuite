"use strict";

const Bb1d2s2s = {
	"Author": "",
	"Date": "",
	"Version": "",
	"Comment": "",
	"Description": "2-state, 2-symbol busy beaver",
	"Reference": "<a href=\"https://en.wikipedia.org/wiki/Busy_beaver\">https://en.wikipedia.org/wiki/Busy_beaver</a>",
	
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
			"WriteSymbol": "1"
		},
		{
			"FromState": "A",
			"FromSymbol": "1",
			"MoveOffsetX": -1,
			"NextState": "B",
			"WriteSymbol": "1"
		},
		{
			"FromState": "B",
			"FromSymbol": "0",
			"MoveOffsetX": -1,
			"NextState": "A",
			"WriteSymbol": "1"
		},
		{
			"FromState": "B",
			"FromSymbol": "1",
			"MoveOffsetX": 1,
			"NextState": "HALT",
			"WriteSymbol": "1"
		}
	]
};