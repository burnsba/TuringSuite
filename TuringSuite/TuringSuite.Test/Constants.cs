using System;
using System.Collections.Generic;
using System.Text;

namespace TuringSuite.Test
{
    public static class Constants
    {
		public const string Bb1d2s2s = @"
{
	""TapeType"": ""1DimensionalNonFinite"",
	""NonHaltingStates"": [""A"", ""B""],
	""HaltingStates"": [""HALT""],
	""InitialState"": ""A"",
	
	""Transitions"": [
		{
			""FromState"": ""A"",
			""FromSymbol"": ""0"",
			""MoveOffsetX"": 1,
			""NextState"": ""B"",
			""WriteSymbol"": ""1"",
		},
		{
			""FromState"": ""A"",
			""FromSymbol"": ""1"",
			""MoveOffsetX"": -1,
			""NextState"": ""B"",
			""WriteSymbol"": ""1"",
		},
		{
			""FromState"": ""B"",
			""FromSymbol"": ""0"",
			""MoveOffsetX"": -1,
			""NextState"": ""A"",
			""WriteSymbol"": ""1"",
		},
		{
			""FromState"": ""B"",
			""FromSymbol"": ""1"",
			""MoveOffsetX"": 1,
			""NextState"": ""HALT"",
			""WriteSymbol"": ""1"",
		}
	]
}
";
		public const string Bb1d4s2s = @"
{
	""Author"": """",
	""Date"": """",
	""Version"": """",
	""Comment"": """",
	""Description"": """",
	""Reference"": """",

	""TapeType"": ""1DimensionalNonFinite"",
	""NonHaltingStates"": [""A"", ""B"", ""C"", ""D""],
	""HaltingStates"": [""HALT""],
	""InitialState"": ""A"",
	
	""Transitions"": [
		{
			""FromState"": ""A"",
			""FromSymbol"": ""0"",
			""MoveOffsetX"": 1,
			""NextState"": ""B"",
			""WriteSymbol"": ""1"",
		},
		{
			""FromState"": ""A"",
			""FromSymbol"": ""1"",
			""MoveOffsetX"": -1,
			""NextState"": ""B"",
			""WriteSymbol"": ""1"",
		},
		{
			""FromState"": ""B"",
			""FromSymbol"": ""0"",
			""MoveOffsetX"": -1,
			""NextState"": ""A"",
			""WriteSymbol"": ""1"",
		},
		{
			""FromState"": ""B"",
			""FromSymbol"": ""1"",
			""MoveOffsetX"": -1,
			""NextState"": ""C"",
			""WriteSymbol"": ""0"",
		},
		{
			""FromState"": ""C"",
			""FromSymbol"": ""0"",
			""MoveOffsetX"": 1,
			""NextState"": ""HALT"",
			""WriteSymbol"": ""1"",
		},
		{
			""FromState"": ""C"",
			""FromSymbol"": ""1"",
			""MoveOffsetX"": -1,
			""NextState"": ""D"",
			""WriteSymbol"": ""1"",
		},
		{
			""FromState"": ""D"",
			""FromSymbol"": ""0"",
			""MoveOffsetX"": 1,
			""NextState"": ""D"",
			""WriteSymbol"": ""1"",
		},
		{
			""FromState"": ""D"",
			""FromSymbol"": ""1"",
			""MoveOffsetX"": 1,
			""NextState"": ""A"",
			""WriteSymbol"": ""0"",
		},
	]
}
";

		// This has extra symbols for testing branches below.
		public const string Bb1d2s3s = @"
{
	""TapeType"": ""1DimensionalNonFinite"",
	""NonHaltingStates"": [""A"", ""B""],
	""HaltingStates"": [""HALT""],
	""InitialState"": ""A"",
	
	""Transitions"": [
		{
			""FromState"": ""A"",
			""FromSymbol"": ""0"",
			""MoveOffsetX"": 1,
			""NextState"": ""B"",
			""WriteSymbol"": ""2"",
		},
		{
			""FromState"": ""A"",
			""FromSymbol"": ""1"",
			""MoveOffsetX"": -1,
			""NextState"": ""B"",
			""WriteSymbol"": ""1"",
		},
		{
			""FromState"": ""B"",
			""FromSymbol"": ""0"",
			""MoveOffsetX"": -1,
			""NextState"": ""A"",
			""WriteSymbol"": ""1"",
		},
		{
			""FromState"": ""B"",
			""FromSymbol"": ""1"",
			""MoveOffsetX"": 1,
			""NextState"": ""HALT"",
			""WriteSymbol"": ""1"",
		}
	]
}
";
	}
}
