using NuGet.Frameworks;
using System;
using TuringSuite.Core;
using TuringSuite.Core.Error;
using Xunit;

namespace TuringSuite.Test
{
    public class ParsingInvalidTest
    {
        [Fact]
        public void Test0010()
        {
            try
            {
                var tms = TuringMachineSimple.FromJson(string.Empty, 100);
            }
            catch (ConfigurationException ce)
            {
                Assert.Equal(ErrorCode.CouldNotParseJson, ce.ErrorCode);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test0020()
        {
            string json = @"
{
	""TapeType"": ""1DimensionalNonFinite"",
	""NonHaltingStates"": [],
	""HaltingStates"": [],
	""InitialState"": """",
	
	""Transitions"": []
}
";
            try
            {
                var tms = TuringMachineSimple.FromJson(json, 100);
            }
            catch (ConfigurationException ce)
            {
                Assert.Equal(ErrorCode.NoHaltingStatesFound, ce.ErrorCode);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test0030()
        {
            string json = @"
{
	""TapeType"": ""1DimensionalNonFinite"",
	""NonHaltingStates"": [],
	""HaltingStates"": [""HALT""],
	""InitialState"": """",
	
	""Transitions"": []
}
";
            try
            {
                var tms = TuringMachineSimple.FromJson(json, 100);
            }
            catch (ConfigurationException ce)
            {
                Assert.Equal(ErrorCode.NoNonHaltingStatesFound, ce.ErrorCode);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test0040()
        {
            string json = @"
{
	""TapeType"": ""1DimensionalNonFinite"",
	""NonHaltingStates"": [""A"", ""B""],
	""HaltingStates"": [""HALT""],
	""InitialState"": """",
	
	""Transitions"": []
}
";
            try
            {
                var tms = TuringMachineSimple.FromJson(json, 100);
            }
            catch (ConfigurationException ce)
            {
                Assert.Equal(ErrorCode.InitialStateNotDescribed, ce.ErrorCode);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test0050()
        {
            string json = @"
{
	""TapeType"": ""1DimensionalNonFinite"",
	""NonHaltingStates"": [""A"", ""B""],
	""HaltingStates"": [""HALT""],
	""InitialState"": ""zzz"",
	
	""Transitions"": []
}
";
            try
            {
                var tms = TuringMachineSimple.FromJson(json, 100);
            }
            catch (ConfigurationException ce)
            {
                Assert.Equal(ErrorCode.InitialStateNotDescribed, ce.ErrorCode);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test0060()
        {
            string json = @"
{
	""TapeType"": ""1DimensionalNonFinite"",
	""NonHaltingStates"": [""A"", ""B""],
	""HaltingStates"": [""HALT""],
	""InitialState"": ""A"",
	
	""Transitions"": []
}
";
            try
            {
                var tms = TuringMachineSimple.FromJson(json, 100);
            }
            catch (ConfigurationException ce)
            {
                Assert.Equal(ErrorCode.NoKnownSymbolsDescribed, ce.ErrorCode);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test0070()
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
            try
            {
                var tms = TuringMachineSimple.FromJson(json, 100);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test0080()
        {
            string json = @"
{
	""TapeType"": ""1DimensionalNonFinite"",
	""NonHaltingStates"": [""A"", ""B""],
	""HaltingStates"": [""HALT""],
	""InitialState"": ""A"",
	
	""Transitions"": [
        {
			""FromState"": ""ZZZ"",
			""FromSymbol"": ""0"",
			""MoveOffsetX"": 1,
			""NextState"": ""B"",
			""WriteSymbol"": ""1"",
		}
    ]
}
";
            try
            {
                var tms = TuringMachineSimple.FromJson(json, 100);
            }
            catch (ConfigurationException ce)
            {
                Assert.Equal(ErrorCode.TransitionFoundButNotListedHaltingNonHalting, ce.ErrorCode);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test0090()
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
			""FromSymbol"": ""zzz"",
			""MoveOffsetX"": 1,
			""NextState"": ""B"",
			""WriteSymbol"": ""1"",
		}
    ]
}
";
            try
            {
                var tms = TuringMachineSimple.FromJson(json, 100);
            }
            catch (ConfigurationException ce)
            {
                Assert.Equal(ErrorCode.TransitionFoundButNotListedHaltingNonHalting, ce.ErrorCode);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test0100()
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
			""NextState"": ""zzz"",
			""WriteSymbol"": ""1"",
		}
    ]
}
";
            try
            {
                var tms = TuringMachineSimple.FromJson(json, 100);
            }
            catch (ConfigurationException ce)
            {
                Assert.Equal(ErrorCode.TransitionFoundButNotListedHaltingNonHalting, ce.ErrorCode);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test0110()
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
			""WriteSymbol"": ""zzz"",
		}
    ]
}
";
            try
            {
                var tms = TuringMachineSimple.FromJson(json, 100);
            }
            catch (ConfigurationException ce)
            {
                Assert.Equal(ErrorCode.TransitionFoundButNotListedHaltingNonHalting, ce.ErrorCode);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}
