"use strict";

const Hmabu90_no5 = {
	"Author": "Heiner Marxen",
	"Date": "1990",
	"Version": "",
	"Comment": "simple counter",
	"Description": "TM #5 from MaBu90-Paper",
	"Reference": "<a href=\"http://turbotm.de/~heiner/BB/simmbP_5.html\">http://turbotm.de/~heiner/BB/simmbP_5.html</a>",
	
	"TapeType": "1DimensionalNonFinite",
	"NonHaltingStates": ["A", "B", "C", "D", "E"],
	"HaltingStates": ["HALT"],
	"InitialState": "A",
	
	"Transitions": [
		{
			"FromState": "A",
			"FromSymbol": "0",
			"MoveOffsetX": -1,
			"NextState": "B",
			"WriteSymbol": "1"
		},
		{
			"FromState": "A",
			"FromSymbol": "1",
			"MoveOffsetX": 1,
			"NextState": "A",
			"WriteSymbol": "1"
		},
		{
			"FromState": "B",
			"FromSymbol": "0",
			"MoveOffsetX": 1,
			"NextState": "A",
			"WriteSymbol": "0"
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
			"NextState": "A",
			"WriteSymbol": "0"
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
			"MoveOffsetX": -1,
			"NextState": "E",
			"WriteSymbol": "0"
		},
		{
			"FromState": "D",
			"FromSymbol": "1",
			"MoveOffsetX": 1,
			"NextState": "B",
			"WriteSymbol": "1"
		},
		{
			"FromState": "E",
			"FromSymbol": "0",
			"MoveOffsetX": 1,
			"NextState": "B",
			"WriteSymbol": "0"
		},
		{
			"FromState": "E",
			"FromSymbol": "1",
			"MoveOffsetX": -1,
			"NextState": "HALT",
			"WriteSymbol": "1"
		}
	]
};