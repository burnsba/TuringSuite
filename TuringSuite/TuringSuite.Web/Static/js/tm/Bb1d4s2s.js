"use strict";

const Bb1d4s2s = {
	"Author": "",
	"Date": "",
	"Version": "",
	"Comment": "",
	"Description": "4-state, 2-symbol busy beaver",
	"Reference": "<a href=\"https://en.wikipedia.org/wiki/Busy_beaver\">https://en.wikipedia.org/wiki/Busy_beaver</a>",
	
	"TapeType": "1DimensionalNonFinite",
	"NonHaltingStates": ["A", "B", "C", "D"],
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
			"MoveOffsetX": -1,
			"NextState": "C",
			"WriteSymbol": "0"
		},
		{
			"FromState": "C",
			"FromSymbol": "0",
			"MoveOffsetX": 1,
			"NextState": "HALT",
			"WriteSymbol": "1"
		},
		{
			"FromState": "C",
			"FromSymbol": "1",
			"MoveOffsetX": -1,
			"NextState": "D",
			"WriteSymbol": "1"
		},
		{
			"FromState": "D",
			"FromSymbol": "0",
			"MoveOffsetX": 1,
			"NextState": "D",
			"WriteSymbol": "1"
		},
		{
			"FromState": "D",
			"FromSymbol": "1",
			"MoveOffsetX": 1,
			"NextState": "A",
			"WriteSymbol": "0"
		},
	]
};