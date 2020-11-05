using NuGet.Frameworks;
using System;
using TuringSuite.Core;
using TuringSuite.Core.Error;
using Xunit;

namespace TuringSuite.Test
{
    public class ParsingValidTest
    {
        [Fact]
        public void Test0010()
        {
            string json = @"
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
		}
    ]
}
";
			// should succeed without issue.
			var tms = TuringMachineSimple.FromJson(json, 100);
			// States are ordered alphabetically, so initial state should be zero here.
			Assert.Equal(0, tms.InitialState);
		}

		[Fact]
		public void Test0011()
		{
			string json = @"
{
	""TapeType"": ""1DimensionalNonFinite"",
	""NonHaltingStates"": [""A"", ""B""],
	""HaltingStates"": [""HALT""],
	""InitialState"": ""B"",
	
	""Transitions"": [
        {
			""FromState"": ""A"",
			""FromSymbol"": ""0"",
			""MoveOffsetX"": 1,
			""NextState"": ""B"",
			""WriteSymbol"": ""1"",
		}
    ]
}
";
			// should succeed without issue.
			var tms = TuringMachineSimple.FromJson(json, 100);
			// States are ordered alphabetically, so initial state should be 1 here.
			Assert.Equal(1, tms.InitialState);
		}

		/// <summary>
		/// Test guids for states/symbols.
		/// </summary>
		[Fact]
		public void Test0020()
		{
			string json = @"
{
	""TapeType"": ""1DimensionalNonFinite"",
	""NonHaltingStates"": [""89441f23c95646acbbac3b92f00c34e3"", ""7f0be375f51f488c867e15698b860d6c""],
	""HaltingStates"": [""1257d1c4fdb645779dfa4285ce7384d1""],
	""InitialState"": ""89441f23c95646acbbac3b92f00c34e3"",
	
	""Transitions"": [
        {
			""FromState"": ""89441f23c95646acbbac3b92f00c34e3"",
			""FromSymbol"": ""9d8b48e192e84e0babb9588f0890464f"",
			""MoveOffsetX"": 1,
			""NextState"": ""7f0be375f51f488c867e15698b860d6c"",
			""WriteSymbol"": ""918d620a078b4c70ae502b09c53329fc"",
		}
    ]
}
";
			var tms = TuringMachineSimple.FromJson(json, 100);

			Assert.Equal(1, tms.HaltingStates.Count);
			Assert.Equal(2, tms.NonHaltingStates.Count);
			Assert.Equal(2, tms.AlphabetSymbols.Count);
		}
	}
}
