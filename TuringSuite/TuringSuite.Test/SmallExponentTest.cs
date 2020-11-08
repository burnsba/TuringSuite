using NuGet.Frameworks;
using System;
using System.Reflection;
using TuringSuite.Core;
using TuringSuite.Core.Error;
using Xunit;

namespace TuringSuite.Test
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2013:Do not use equality check to check for collection size.", Justification = "<Pending>")]
	public class SmallExponentTest
	{
		private const string bb1d2s2s = @"
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
		// MoveHead on blank tape.
		[Fact]
        public void Test0010()
        {
			var tms = TuringMachineSmallExponent.FromJson(bb1d2s2s);
			tms.InitRun();

			string currentTape;

			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			var methodInfo = typeof(TuringMachineSmallExponent).GetMethod(
				"MoveHead",
				BindingFlags.NonPublic | BindingFlags.Instance);

			// Move 1 to the right
			// before: >*0
			// after: *0 >0
			methodInfo.Invoke(tms, new object[] { 1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(2, tms.GetCurrentNodeOffset());

			// Move 1 to the left, back to origin
			// before: *0 >0
			// after: >*0 0
			methodInfo.Invoke(tms, new object[] { -1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 more to the left
			// before: >*0 0
			// after: >0 *0 0
			methodInfo.Invoke(tms, new object[] { -1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 to the right, back to origin
			// before: >0 *0 0
			// after: 0 >*0 0
			methodInfo.Invoke(tms, new object[] { 1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(2, tms.GetCurrentNodeOffset());

			// Move 3 to the right
			// before: 0 >*0 0
			// after: 0 *0 0 0 >0
			methodInfo.Invoke(tms, new object[] { 3 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,5)", currentTape);
			Assert.Equal(5, tms.GetCurrentNodeOffset());

			// Move 3 to the left, back to origin
			// before: 0 *0 0 0 >0
			// after: 0 >*0 0 0 0
			methodInfo.Invoke(tms, new object[] { -3 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,5)", currentTape);
			Assert.Equal(2, tms.GetCurrentNodeOffset());

			// Move 3 to the left
			// before: 0 >*0 0 0 0
			// after: >0 0 0 *0 0 0 0
			methodInfo.Invoke(tms, new object[] { -3 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 3 to the right
			// before: >0 0 0 *0 0 0 0
			// after: 0 0 0 >*0 0 0 0
			methodInfo.Invoke(tms, new object[] { 3 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(4, tms.GetCurrentNodeOffset());

			// Move 3 to the right
			// before: 0 0 0 >*0 0 0 0
			// after: 0 0 0 *0 0 0 >0
			methodInfo.Invoke(tms, new object[] { 3 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(7, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 0 0 *0 0 0 >0
			// after: 0 0 0 *0 0 >0 0
			methodInfo.Invoke(tms, new object[] { -1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(6, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 0 0 *0 0 >0 0
			// after: 0 0 0 *0 >0 0 0
			methodInfo.Invoke(tms, new object[] { -1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(5, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 0 0 *0 >0 0 0
			// after: 0 0 0 >*0 0 0 0
			methodInfo.Invoke(tms, new object[] { -1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(4, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 0 0 >*0 0 0 0
			// after: 0 0 >0 *0 0 0 0
			methodInfo.Invoke(tms, new object[] { -1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(3, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 0 >0 *0 0 0 0
			// after: 0 >0 0 *0 0 0 0
			methodInfo.Invoke(tms, new object[] { -1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(2, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 >0 0 *0 0 0 0
			// after: >0 0 0 *0 0 0 0
			methodInfo.Invoke(tms, new object[] { -1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: >0 0 0 *0 0 0 0
			// after: >0 0 0 0 *0 0 0 0
			methodInfo.Invoke(tms, new object[] { -1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,8)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: >0 0 0 0 *0 0 0 0
			// after: >0 0 0 0 0 *0 0 0 0
			methodInfo.Invoke(tms, new object[] { -1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,9)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: >0 0 0 0 0 *0 0 0 0
			// after: >0 0 0 0 0 0 *0 0 0 0
			methodInfo.Invoke(tms, new object[] { -1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,10)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 to the right
			// before: >0 0 0 0 0 0 *0 0 0 0
			// after: 0 >0 0 0 0 0 *0 0 0 0
			methodInfo.Invoke(tms, new object[] { 1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,10)", currentTape);
			Assert.Equal(2, tms.GetCurrentNodeOffset());

			// Move 1 to the right
			// before: 0 >0 0 0 0 0 *0 0 0 0
			// after: 0 0 >0 0 0 0 *0 0 0 0
			methodInfo.Invoke(tms, new object[] { 1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,10)", currentTape);
			Assert.Equal(3, tms.GetCurrentNodeOffset());

			// Move 1 to the right
			// before: 0 0 >0 0 0 0 *0 0 0 0
			// after: 0 0 0 >0 0 0 *0 0 0 0
			methodInfo.Invoke(tms, new object[] { 1 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,10)", currentTape);
			Assert.Equal(4, tms.GetCurrentNodeOffset());

			// Move 6 to the right
			// before: 0 0 0 >0 0 0 *0 0 0 0
			// after: 0 0 0 0 0 0 *0 0 0 >0
			methodInfo.Invoke(tms, new object[] { 6 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,10)", currentTape);
			Assert.Equal(10, tms.GetCurrentNodeOffset());

			// Move 6 to the right
			// before: 0 0 0 0 0 0 *0 0 0 >0
			// after: 0 0 0 0 0 0 *0 0 0 0 0 0 0 0 0>0
			methodInfo.Invoke(tms, new object[] { 6 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,16)", currentTape);
			Assert.Equal(16, tms.GetCurrentNodeOffset());
		}

		// MoveHead on blank tape, from new object.
		[Fact]
		public void Test0012()
		{
			var tms = TuringMachineSmallExponent.FromJson(bb1d2s2s);
			tms.InitRun();

			string currentTape;

			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			var methodInfo = typeof(TuringMachineSmallExponent).GetMethod(
				"MoveHead",
				BindingFlags.NonPublic | BindingFlags.Instance);

			// Move 100 to the right
			methodInfo.Invoke(tms, new object[] { 100 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,101)", currentTape);
			Assert.Equal(101, tms.GetCurrentNodeOffset());
		}

		// MoveHead on blank tape, from new object.
		[Fact]
		public void Test0014()
		{
			var tms = TuringMachineSmallExponent.FromJson(bb1d2s2s);
			tms.InitRun();

			string currentTape;

			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			var methodInfo = typeof(TuringMachineSmallExponent).GetMethod(
				"MoveHead",
				BindingFlags.NonPublic | BindingFlags.Instance);

			// Move 100 to the left
			methodInfo.Invoke(tms, new object[] { -100 });
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,101)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
		}
	}
}
