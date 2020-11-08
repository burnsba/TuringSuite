using NuGet.Frameworks;
using System;
using System.Reflection;
using TuringSuite.Core;
using TuringSuite.Core.Error;
using Xunit;

namespace TuringSuite.Test
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// Comments give a quick summary of the state of the machine.
	/// ">" means position head is currently pointing at.
	/// "*" means origin head position.
	/// </remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2013:Do not use equality check to check for collection size.", Justification = "<Pending>")]
	public class SmallExponentTest
	{
		// MoveHead on blank tape.
		[Fact]
        public void Test0010()
        {
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
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
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());

			// Move 1 to the left, back to origin
			// before: *0 >0
			// after: >*0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 more to the left
			// before: >*0 0
			// after: >0 *0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 to the right, back to origin
			// before: >0 *0 0
			// after: 0 >*0 0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());

			// Move 3 to the right
			// before: 0 >*0 0
			// after: 0 *0 0 0 >0
			InvokeMoveHead(tms, 3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,5)", currentTape);
			Assert.Equal(3, tms.HeadPositionX);
			Assert.Equal(5, tms.GetCurrentNodeOffset());

			// Move 3 to the left, back to origin
			// before: 0 *0 0 0 >0
			// after: 0 >*0 0 0 0
			InvokeMoveHead(tms, -3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,5)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());

			// Move 3 to the left
			// before: 0 >*0 0 0 0
			// after: >0 0 0 *0 0 0 0
			InvokeMoveHead(tms, -3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(-3, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 3 to the right
			// before: >0 0 0 *0 0 0 0
			// after: 0 0 0 >*0 0 0 0
			InvokeMoveHead(tms, 3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(4, tms.GetCurrentNodeOffset());

			// Move 3 to the right
			// before: 0 0 0 >*0 0 0 0
			// after: 0 0 0 *0 0 0 >0
			InvokeMoveHead(tms, 3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(3, tms.HeadPositionX);
			Assert.Equal(7, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 0 0 *0 0 0 >0
			// after: 0 0 0 *0 0 >0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(6, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 0 0 *0 0 >0 0
			// after: 0 0 0 *0 >0 0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(5, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 0 0 *0 >0 0 0
			// after: 0 0 0 >*0 0 0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(4, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 0 0 >*0 0 0 0
			// after: 0 0 >0 *0 0 0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 0 >0 *0 0 0 0
			// after: 0 >0 0 *0 0 0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: 0 >0 0 *0 0 0 0
			// after: >0 0 0 *0 0 0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,7)", currentTape);
			Assert.Equal(-3, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: >0 0 0 *0 0 0 0
			// after: >0 0 0 0 *0 0 0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,8)", currentTape);
			Assert.Equal(-4, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: >0 0 0 0 *0 0 0 0
			// after: >0 0 0 0 0 *0 0 0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,9)", currentTape);
			Assert.Equal(-5, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 to the left
			// before: >0 0 0 0 0 *0 0 0 0
			// after: >0 0 0 0 0 0 *0 0 0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,10)", currentTape);
			Assert.Equal(-6, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 1 to the right
			// before: >0 0 0 0 0 0 *0 0 0 0
			// after: 0 >0 0 0 0 0 *0 0 0 0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,10)", currentTape);
			Assert.Equal(-5, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());

			// Move 1 to the right
			// before: 0 >0 0 0 0 0 *0 0 0 0
			// after: 0 0 >0 0 0 0 *0 0 0 0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,10)", currentTape);
			Assert.Equal(-4, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());

			// Move 1 to the right
			// before: 0 0 >0 0 0 0 *0 0 0 0
			// after: 0 0 0 >0 0 0 *0 0 0 0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,10)", currentTape);
			Assert.Equal(-3, tms.HeadPositionX);
			Assert.Equal(4, tms.GetCurrentNodeOffset());

			// Move 6 to the right
			// before: 0 0 0 >0 0 0 *0 0 0 0
			// after: 0 0 0 0 0 0 *0 0 0 >0
			InvokeMoveHead(tms, 6);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,10)", currentTape);
			Assert.Equal(3, tms.HeadPositionX);
			Assert.Equal(10, tms.GetCurrentNodeOffset());

			// Move 6 to the right
			// before: 0 0 0 0 0 0 *0 0 0 >0
			// after: 0 0 0 0 0 0 *0 0 0 0 0 0 0 0 0>0
			InvokeMoveHead(tms, 6);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,16)", currentTape);
			Assert.Equal(9, tms.HeadPositionX);
			Assert.Equal(16, tms.GetCurrentNodeOffset());
		}

		// MoveHead on blank tape, from new object.
		[Fact]
		public void Test0012()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			var methodInfo = typeof(TuringMachineSmallExponent).GetMethod(
				"MoveHead",
				BindingFlags.NonPublic | BindingFlags.Instance);

			// Move 100 to the right
			InvokeMoveHead(tms, 100);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,101)", currentTape);
			Assert.Equal(100, tms.HeadPositionX);
			Assert.Equal(101, tms.GetCurrentNodeOffset());
		}

		// MoveHead on blank tape, from new object.
		[Fact]
		public void Test0014()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());

			// Move 100 to the left
			InvokeMoveHead(tms, -100);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,101)", currentTape);
			Assert.Equal(-100, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
		}

		// Write on blank tape, from new object.
		[Fact]
		public void Test0200()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: >*0
			// after: >*1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Change back to blank (0)
			// before: >*1
			// after: >*0
			InvokeWriteSymbol(tms, 0);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change back to 1 again
			// before: >*0
			// after: >*1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Move to the right then change.
		[Fact]
		public void Test0300()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >*0
			// after: *0 >0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: *0 >0
			// after: *0 >1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the left
			// before: *0 >1
			// after: >*0 1
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 1 to the left
			// before: >*0 1
			// after: >0 *0 1
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,1)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: >0 *0 1
			// after: >1 *0 1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,1)(1,1)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Change head, then move to right.
		[Fact]
		public void Test0302()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: *>0
			// after: *>1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the right
			// before: *>1
			// after: *1 >0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,1)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 1 to the left
			// before: *1 >0
			// after: >*1 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the left
			// before: >*1 0
			// after: >0 *1 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)(0,1)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: >0 *1 0
			// after: >1 *1 0
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,2)(0,1)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >1 *1 0
			// after: 1 >*1 0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,2)(0,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_One_Overwrite, no neighbors branch.
		[Fact]
		public void Test0320()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Change symbol under head to 1
			// before: >*0
			// after: >*1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_One_Overwrite, prev node but different branch.
		[Fact]
		public void Test0322()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 1 to the left
			// before: >*0
			// after: >0 *0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >0 *0
			// after: 0 >*0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: 0 >*0
			// after: 0 >*1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_One_Overwrite, next node but different branch.
		[Fact]
		public void Test0324()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 1 to the right
			// before: >*0
			// after: *0 >0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 1 to the left
			// before: *0 >0
			// after: >*0 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: >*0 0
			// after: >*1 0
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_One_Overwrite, next and prev node but different branch.
		[Fact]
		public void Test0326()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 1 to the right
			// before: >*0
			// after: *0 >0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 2 to the left
			// before: *0 >0
			// after: >0 *0 0
			InvokeMoveHead(tms, -2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >0 *0 0
			// after: 0 >*0 0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: 0 >*0 0
			// after: 0 >*1 0
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)(0,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_One_MergePrev, prev node same branch.
		[Fact]
		public void Test0330()
        {
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Change symbol under head to 1
			// before: >*0
			// after: >*1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the left
			// before: >*1
			// after: >0 *1
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >0 *1
			// after: 0 >*1
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Change symbol under head to 0
			// before: 0 >*1
			// after: 0 >*0
			InvokeWriteSymbol(tms, 0);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// Move + write. Write_One_MergePrev, prev and next node but
		// only prev is the same.
		// (0,1)(1,1)(2,1)
		[Fact]
		public void Test0332()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Change symbol under head to 1
			// before: >*0
			// after: >*1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >*1
			// after: *1 >0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,1)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 2
			// before: *1 >0
			// after: *1 >2
			InvokeWriteSymbol(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(2,1)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(2, tms.CurrentSymbol);

			// Move 2 to the left
			// before: *1 >2
			// after: >0 *1 2
			InvokeMoveHead(tms, -2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)(2,1)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >0 *1 2
			// after: 0 >*1 2
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)(2,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Change symbol under head to 0
			// before: 0 >*1 2
			// after: 0 >*0 2
			InvokeWriteSymbol(tms, 0);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(2,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// Move + write. Write_One_MergeNext, next node same branch.
		[Fact]
		public void Test0340()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Change symbol under head to 1
			// before: >*0
			// after: >*1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >*1
			// after: *1 >0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,1)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 1 to the left
			// before: *1 >0
			// after: >*1 0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Change symbol under head to 0
			// before: >*1 0
			// after: >*0 0
			InvokeWriteSymbol(tms, 0);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// Move + write. Write_One_MergeBoth, prev and next node but
		// only next is the same.
		// (0,1)(1,1)(2,1)
		[Fact]
		public void Test0342()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Change symbol under head to 1
			// before: >*0
			// after: >*1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >*1
			// after: *1 >0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,1)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 2
			// before: *1 >0
			// after: *1 >2
			InvokeWriteSymbol(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(2,1)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(2, tms.CurrentSymbol);

			// Move 2 to the left
			// before: *1 >2
			// after: >0 *1 2
			InvokeMoveHead(tms, -2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)(2,1)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >0 *1 2
			// after: 0 >*1 2
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)(2,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Change symbol under head to 2
			// before: 0 >*1 2
			// after: 0 >*2 2
			InvokeWriteSymbol(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(2,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(2, tms.CurrentSymbol);
		}

		// Move + write. Write_One_MergeBoth, prev and next node both the same
		// (0,1)(1,1)(0,1)
		[Fact]
		public void Test0350()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Change symbol under head to 1
			// before: >*0
			// after: >*1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >*1
			// after: *1 >0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,1)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 2 to the left
			// before: *1 >0
			// after: >0 *1 0
			InvokeMoveHead(tms, -2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)(0,1)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >0 *1 0
			// after: 0 >*1 0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,1)(0,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Change symbol under head to 0
			// before: 0 >*1 0
			// after: 0 >*0 0
			InvokeWriteSymbol(tms, 0);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// Move + write. Write_First_Solo, via no neighbors and _headExponent == 1
		// No neighbors and writing a different symbol at the start.
		[Fact]
		public void Test0360()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 2 to the right
			// before: >*0
			// after: *0 0 >0
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 2 to the left
			// before: *0 0 >0
			// after: >*0 0 0
			InvokeMoveHead(tms, -2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: >*0 0 0
			// after: >*1 0 0
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_First_Solo, via ignore next (no prev) and _headExponent == 1
		[Fact]
		public void Test0362()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 3 to the right
			// before: >*0
			// after: *0 0 0 >0
			InvokeMoveHead(tms, 3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,4)", currentTape);
			Assert.Equal(3, tms.HeadPositionX);
			Assert.Equal(4, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 2
			// before: *0 0 0 >0
			// after: *0 0 0 >2
			InvokeWriteSymbol(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)(2,1)", currentTape);
			Assert.Equal(3, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(2, tms.CurrentSymbol);

			// Move 3 to the left
			// before: *0 0 0 >2
			// after: >*0 0 0 2
			InvokeMoveHead(tms, -3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)(2,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: >*0 0 0 2
			// after: >*1 0 0 2
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,2)(2,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_Mid_Solo, via no neighbors and _headExponent == middle
		[Fact]
		public void Test0370()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 2 to the right
			// before: >*0
			// after: *0 0 >0
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 4 to the left
			// before: *0 0 >0
			// after: >0 0 *0 0 0
			InvokeMoveHead(tms, -4);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,5)", currentTape);
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 2 to the right
			// before: >0 0 *0 0 0
			// after: 0 0 >*0 0 0
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,5)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: 0 0 >*0 0 0
			// after: 0 0 >*1 0 0
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,1)(0,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_Mid_Solo, via ignore prev (no next) and _headExponent == middle
		[Fact]
		public void Test0372()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 2 to the right
			// before: >*0
			// after: *0 0 >0
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 4 to the left
			// before: *0 0 >0
			// after: >0 0 *0 0 0
			InvokeMoveHead(tms, -4);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,5)", currentTape);
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 2
			// before: >0 0 *0 0 0
			// after: >2 0 *0 0 0
			InvokeWriteSymbol(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(2,1)(0,4)", currentTape);
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(2, tms.CurrentSymbol);

			// Move 2 to the right
			// before: >2 0 *0 0 0
			// after: 2 0 >*0 0 0
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(2,1)(0,4)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: 2 0 >*0 0 0
			// after: 2 0 >*1 0 0
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(2,1)(0,1)(1,1)(0,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_Mid_Solo, via ignore next (no prev) and _headExponent == middle
		[Fact]
		public void Test0374()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 2 to the right
			// before: >*0
			// after: *0 0 >0
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 2
			// before: *0 0 >0
			// after: *0 0 >2
			InvokeWriteSymbol(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(2,1)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(2, tms.CurrentSymbol);

			// Move 4 to the left
			// before: *0 0 >2
			// after: >0 0 *0 0 2
			InvokeMoveHead(tms, -4);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,4)(2,1)", currentTape);
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 2 to the right
			// before: >0 0 *0 0 2
			// after: 0 0 >*0 0 2
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,4)(2,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: 0 0 >*0 0 2
			// after: 0 0 >*1 0 2
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,1)(0,1)(2,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_Mid_Solo, via ignore both neighbors and _headExponent == middle
		// (2,1)(0,3)(2,1) -> (2,1)(0,1)(1,1)(0,1)(2,1)
		[Fact]
		public void Test0376()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 2 to the right
			// before: >*0
			// after: *0 0 >0
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 2
			// before: *0 0 >0
			// after: *0 0 >2
			InvokeWriteSymbol(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(2,1)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(2, tms.CurrentSymbol);

			// Move 4 to the left
			// before: *0 0 >2
			// after: >0 0 *0 0 2
			InvokeMoveHead(tms, -4);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,4)(2,1)", currentTape);
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 2
			// before: >0 0 *0 0 2
			// after: >2 0 *0 0 2
			InvokeWriteSymbol(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(2,1)(0,3)(2,1)", currentTape);
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(2, tms.CurrentSymbol);

			// Move 2 to the right
			// before: >2 0 *0 0 2
			// after: 2 0 >*0 0 2
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(2,1)(0,3)(2,1)", currentTape);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: 2 0 >*0 0 2
			// after: 2 0 >*1 0 2
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(2,1)(0,1)(1,1)(0,1)(2,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_Last_Solo, via no neighbors and _headExponent == end
		[Fact]
		public void Test0380()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 2 to the right
			// before: >*0
			// after: *0 0 >0
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: *0 0 >0
			// after: *0 0 >1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,1)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_Last_Solo, via ignore prev and _headExponent == end
		// (1,1)(0,3) -> (1,1)(0,2)(1,1)
		[Fact]
		public void Test0382()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 1 to the left
			// before: >*0
			// after: >0 *0
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: >0 *0
			// after: >1 *0
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,1)", currentTape);
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 3 to the right
			// before: >1 *0
			// after: 1 *0 0 >0
			InvokeMoveHead(tms, 3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,3)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: 1 *0 0 >0
			// after: 1 *0 0 >1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,2)(1,1)", currentTape);
			Assert.Equal(2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		// Move + write. Write_IntoFirst, via prev only and _headExponent == 1
		[Fact]
		public void Test0390()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 2 to the left
			// before: >*0
			// after: >0 0 *0
			InvokeMoveHead(tms, -2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 2 to the right
			// before: >0 0 *0
			// after: 0 0 >*0
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: 0 0 >*0
			// after: 0 0 >*1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the right
			// before: 0 0 >*1
			// after: 0 0 *1 >0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,1)(0,1)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: 0 0 *1 >0
			// after: 0 0 *1 >1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,2)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the left
			// before: 0 0 *1 >1
			// after: 0 0 >*1 1
			InvokeMoveHead(tms, -1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Change symbol under head to 0
			// before: 0 0 >*1 1
			// after: 0 0 >*0 1
			InvokeWriteSymbol(tms, 0);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// Move + write. Write_IntoFirst, via prev but ignore next and _headExponent == 1
		// (0,2)(1,2)(0,2) -> (0,3)(1,1)(0,2)
		[Fact]
		public void Test0392()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 2 to the left
			// before: >*0
			// after: >0 0 *0
			InvokeMoveHead(tms, -2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 2 to the right
			// before: >0 0 *0
			// after: 0 0 >*0
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: 0 0 >*0
			// after: 0 0 >*1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the right
			// before: 0 0 >*1
			// after: 0 0 *1 >0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,1)(0,1)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: 0 0 *1 >0
			// after: 0 0 *1 >1
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,2)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 2 to the right
			// before: 0 0 *1 >1
			// after: 0 0 *1 1 0 >0
			InvokeMoveHead(tms, 2);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,2)(0,2)", currentTape);
			Assert.Equal(3, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 3 to the left
			// before: 0 0 *1 1 0 >0
			// after: 0 0 *>1 1 0 0
			InvokeMoveHead(tms, -3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,2)(0,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Change symbol under head to 0
			// before: 0 0 *>1 1 0 0
			// after: 0 0 *>0 1 0 0
			InvokeWriteSymbol(tms, 0);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,3)(1,1)(0,2)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// Move + write. Write_IntoLast, via next only and _headExponent == end
		[Fact]
		public void Test0400()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 3 to the right
			// before: >*0
			// after: *0 0 0 >0
			InvokeMoveHead(tms, 3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,4)", currentTape);
			Assert.Equal(3, tms.HeadPositionX);
			Assert.Equal(4, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 3 to the left
			// before: *0 0 0 >0
			// after: >*0 0 0 0
			InvokeMoveHead(tms, -3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,4)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: >*0 0 0 0
			// after: >*1 0 0 0
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,3)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >*1 0 0 0
			// after: *1 >0 0 0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,3)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: *1 >0 0 0
			// after: *1 >1 0 0
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,2)(0,2)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Change symbol under head to 0
			// before: *1 >1 0 0
			// after: *1 >0 0 0
			InvokeWriteSymbol(tms, 0);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,3)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// Move + write. Write_IntoLast, via next but ignore prev and _headExponent == end
		// (0,2)(1,2)(0,2) -> (0,2)(1,1)(0,3)
		[Fact]
		public void Test0402()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			// Move 3 to the right
			// before: >*0
			// after: *0 0 0 >0
			InvokeMoveHead(tms, 3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,4)", currentTape);
			Assert.Equal(3, tms.HeadPositionX);
			Assert.Equal(4, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 3 to the left
			// before: *0 0 0 >0
			// after: >*0 0 0 0
			InvokeMoveHead(tms, -3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,4)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: >*0 0 0 0
			// after: >*1 0 0 0
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,3)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 1 to the right
			// before: >*1 0 0 0
			// after: *1 >0 0 0
			InvokeMoveHead(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,3)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Change symbol under head to 1
			// before: *1 >0 0 0
			// after: *1 >1 0 0
			InvokeWriteSymbol(tms, 1);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,2)(0,2)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Move 3 to the left
			// before: *1 >1 0 0
			// after: >0 0 *1 1 0 0
			InvokeMoveHead(tms, -3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,2)(0,2)", currentTape);
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// Move 3 to the right
			// before: >0 0 *1 1 0 0
			// after: 0 0 *1 >1 0 0
			InvokeMoveHead(tms, 3);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,2)(0,2)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// Change symbol under head to 0
			// before: 0 0 *1 >1 0 0
			// after: 0 0 *1 >0 0 0
			InvokeWriteSymbol(tms, 0);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,2)(1,1)(0,3)", currentTape);
			Assert.Equal(1, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// MoveHead, (positive) non blank section to the right into blank section.
		[Fact]
		public void Test0500()
        {
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			for (int i=0; i<10; i++)
            {
				InvokeWriteSymbol(tms, 1);
				InvokeMoveHead(tms, 1);
			}

			InvokeWriteSymbol(tms, 1);

			// tape: *1 1 1 1 1 1 1 1 1 1 >1
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,11)", currentTape);
			Assert.Equal(10, tms.HeadPositionX);
			Assert.Equal(11, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// before: *1 1 1 1 1 1 1 1 1 1 >1
			// after: *1 1 1 1 1 >1 1 1 1 1 1
			InvokeMoveHead(tms, -5);

			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,11)", currentTape);
			Assert.Equal(5, tms.HeadPositionX);
			Assert.Equal(6, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// before: *1 1 1 1 1 >1 1 1 1 1 1
			// after: *1 1 1 1 1 1 1 1 1 1 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 >0
			InvokeMoveHead(tms, 20);

			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,11)(0,15)", currentTape);
			Assert.Equal(25, tms.HeadPositionX);
			Assert.Equal(15, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// MoveHead, (negative) non blank section to the left into blank section.
		[Fact]
		public void Test0502()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			for (int i = 0; i < 10; i++)
			{
				InvokeWriteSymbol(tms, 1);
				InvokeMoveHead(tms, -1);
			}

			InvokeWriteSymbol(tms, 1);

			// tape: >1 1 1 1 1 1 1 1 1 1 *1
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,11)", currentTape);
			Assert.Equal(-10, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// before: >1 1 1 1 1 1 1 1 1 1 *1
			// after: 1 1 1 1 1 >1 1 1 1 1 *1
			InvokeMoveHead(tms, 5);

			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,11)", currentTape);
			Assert.Equal(-5, tms.HeadPositionX);
			Assert.Equal(6, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// before: 1 1 1 1 1 >1 1 1 1 1 *1
			// after: >0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 1 1 1 1 1 1 1 1 1 *1
			InvokeMoveHead(tms, -20);

			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,15)(1,11)", currentTape);
			Assert.Equal(-25, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// MoveHead, (positive) non blank section to the left into blank section.
		[Fact]
		public void Test0504()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			for (int i = 0; i < 10; i++)
			{
				InvokeWriteSymbol(tms, 1);
				InvokeMoveHead(tms, 1);
			}

			InvokeWriteSymbol(tms, 1);

			// tape: *1 1 1 1 1 1 1 1 1 1 >1
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,11)", currentTape);
			Assert.Equal(10, tms.HeadPositionX);
			Assert.Equal(11, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// before: *1 1 1 1 1 1 1 1 1 1 >1
			// after: *1 1 1 1 1 >1 1 1 1 1 1
			InvokeMoveHead(tms, -5);

			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,11)", currentTape);
			Assert.Equal(5, tms.HeadPositionX);
			Assert.Equal(6, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// before: *1 1 1 1 1 >1 1 1 1 1 1
			// after: >0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 *1 1 1 1 1 >1 1 1 1 1 1
			InvokeMoveHead(tms, -20);

			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,15)(1,11)", currentTape);
			Assert.Equal(-15, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// MoveHead, (negative) non blank section to the right into blank section.
		[Fact]
		public void Test0506()
		{
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s3s);
			tms.InitRun();

			string currentTape;

			for (int i = 0; i < 10; i++)
			{
				InvokeWriteSymbol(tms, 1);
				InvokeMoveHead(tms, -1);
			}

			InvokeWriteSymbol(tms, 1);

			// tape: >1 1 1 1 1 1 1 1 1 1 *1
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,11)", currentTape);
			Assert.Equal(-10, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// before: >1 1 1 1 1 1 1 1 1 1 *1
			// after: 1 1 1 1 1 >1 1 1 1 1 *1
			InvokeMoveHead(tms, 5);

			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,11)", currentTape);
			Assert.Equal(-5, tms.HeadPositionX);
			Assert.Equal(6, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// before: 1 1 1 1 1 >1 1 1 1 1 *1
			// after: 1 1 1 1 1 >1 1 1 1 1 *1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 >0
			InvokeMoveHead(tms, 20);

			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,11)(0,15)", currentTape);
			Assert.Equal(15, tms.HeadPositionX);
			Assert.Equal(15, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);
		}

		// Bb1d2s2s
		[Fact]
		public void Test0600()
        {
			string currentTape;
			bool step;
			var tms = TuringMachineSmallExponent.FromJson(Constants.Bb1d2s2s);
			tms.InitRun();

			Assert.Equal(1, tms.HaltingStates.Count);
			Assert.Equal(2, tms.NonHaltingStates.Count);
			Assert.Equal(2, tms.AlphabetSymbols.Count);
			Assert.Equal(0, tms.InitialState);
			Assert.True(0 == tms.StepCount);

			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)", currentTape);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// executing step 1 of 6
			// before: 0   0   0[A>0]  0   0   0
			// after:  0   0   0[  1 B>0]  0   0
			step = tms.Step();
			Assert.True(step);
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(1, tms.HeadPositionX);
			Assert.True(1 == tms.StepCount);
			Assert.Equal(1, tms.CurrentState);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,1)(0,1)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// executing step 2 of 6
			// before: 0   0   0[  1 B>0]  0   0
			// after:  0   0   0[A>1   1]  0   0
			step = tms.Step();
			Assert.True(step);
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(0, tms.HeadPositionX);
			Assert.True(2 == tms.StepCount);
			Assert.Equal(0, tms.CurrentState);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,2)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// executing step 3 of 6
			// before: 0   0   0[A>1   1]  0   0
			// after:  0   0[B>0   1   1]  0   0
			step = tms.Step();
			Assert.True(step);
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.True(3 == tms.StepCount);
			Assert.Equal(1, tms.CurrentState);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,2)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// executing step 4 of 6
			// before: 0   0[B>0   1   1]  0   0
			// after:  0[A>0   1   1   1]  0   0
			step = tms.Step();
			Assert.True(step);
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.True(4 == tms.StepCount);
			Assert.Equal(0, tms.CurrentState);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(0,1)(1,3)", currentTape);
			Assert.Equal(1, tms.GetCurrentNodeOffset());
			Assert.Equal(0, tms.CurrentSymbol);

			// executing step 5 of 6
			// before: 0[A>0   1   1   1]  0   0
			// after:  0[  1 B>1   1   1]  0   0
			step = tms.Step();
			Assert.True(step);
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.True(5 == tms.StepCount);
			Assert.Equal(1, tms.CurrentState);
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,4)", currentTape);
			Assert.Equal(2, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// executing step 6 of 6
			// before: 0[  1 B>1   1   1]  0   0
			// after:  0[  1   1 X>1   1]  0   0
			step = tms.Step();
			Assert.False(step); // halted
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(0, tms.HeadPositionX);
			Assert.True(6 == tms.StepCount);
			Assert.Equal(-1, tms.CurrentState); // halted state
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,4)", currentTape);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);

			// step one more time to test that nothing changes
			step = tms.Step();
			Assert.False(step); // halted
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(0, tms.HeadPositionX);
			Assert.True(6 == tms.StepCount);
			Assert.Equal(-1, tms.CurrentState); // halted state
			currentTape = tms.GetExponentTape();
			Assert.Equal("(1,4)", currentTape);
			Assert.Equal(3, tms.GetCurrentNodeOffset());
			Assert.Equal(1, tms.CurrentSymbol);
		}

		private static void InvokeMoveHead(TuringMachineSmallExponent tms, int offset)
        {
			var methodInfo = typeof(TuringMachineSmallExponent).GetMethod(
				"MoveHead",
				BindingFlags.NonPublic | BindingFlags.Instance);

			methodInfo.Invoke(tms, new object[] { offset });
		}

		private static void InvokeWriteSymbol(TuringMachineSmallExponent tms, byte symbol)
		{
			var methodInfo = typeof(TuringMachineSmallExponent).GetMethod(
				"WriteSymbol",
				BindingFlags.NonPublic | BindingFlags.Instance);

			methodInfo.Invoke(tms, new object[] { symbol });
		}
	}
}
