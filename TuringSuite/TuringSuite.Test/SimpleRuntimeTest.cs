using NuGet.Frameworks;
using System;
using TuringSuite.Core;
using TuringSuite.Core.Error;
using Xunit;

namespace TuringSuite.Test
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2013:Do not use equality check to check for collection size.", Justification = "<Pending>")]
	public class SimpleRuntimeTest
    {
        [Fact]
        public void Test0010()
        {
			uint tapeSize = 100;
			bool step;
			var tms = TuringMachineSimple.FromJson(Constants.Bb1d2s2s, tapeSize);

			Assert.Equal(1, tms.HaltingStates.Count);
			Assert.Equal(2, tms.NonHaltingStates.Count);
			Assert.Equal(2, tms.AlphabetSymbols.Count);
			Assert.Equal(0, tms.InitialState);
			Assert.Equal(tapeSize, tms.TapeSize);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.True(0 == tms.StepCount);

			// hmm, should this be the default behavior before calling InitRun?
			Assert.Equal(-1, tms.CurrentState);

			byte[] visited = tms.GetVisitedTape();
			Assert.Equal(1, visited.Length);
			Assert.Equal(0, visited[0]);

			tms.InitRun();

			Assert.Equal(1, tms.HaltingStates.Count);
			Assert.Equal(2, tms.NonHaltingStates.Count);
			Assert.Equal(2, tms.AlphabetSymbols.Count);
			Assert.Equal(0, tms.InitialState);
			Assert.Equal(tapeSize, tms.TapeSize);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.True(0 == tms.StepCount);
			Assert.Equal(tms.InitialState, tms.CurrentState);
			visited = tms.GetVisitedTape();
			Assert.Equal(1, visited.Length);
			Assert.Equal(0, visited[0]);

			// executing step 1 of 6
			// before: 0   0   0[A>0]  0   0   0
			// after:  0   0   0[  1 B>0]  0   0
			step = tms.Step();
			Assert.True(step);
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(tapeSize, tms.TapeSize); // no change
			Assert.Equal(1, tms.HeadPositionX);
			Assert.True(1 == tms.StepCount);
			Assert.Equal(1, tms.CurrentState);
			visited = tms.GetVisitedTape();
			Assert.Equal(2, visited.Length);
			Assert.Equal(1, visited[0]);
			Assert.Equal(0, visited[1]);

			// executing step 2 of 6
			// before: 0   0   0[  1 B>0]  0   0
			// after:  0   0   0[A>1   1]  0   0
			step = tms.Step();
			Assert.True(step);
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(tapeSize, tms.TapeSize); // no change
			Assert.Equal(0, tms.HeadPositionX);
			Assert.True(2 == tms.StepCount);
			Assert.Equal(0, tms.CurrentState);
			visited = tms.GetVisitedTape();
			Assert.Equal(2, visited.Length);
			Assert.Equal(1, visited[0]);
			Assert.Equal(1, visited[1]);

			// executing step 3 of 6
			// before: 0   0   0[A>1   1]  0   0
			// after:  0   0[B>0   1   1]  0   0
			step = tms.Step();
			Assert.True(step);
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(tapeSize, tms.TapeSize); // no change
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.True(3 == tms.StepCount);
			Assert.Equal(1, tms.CurrentState);
			visited = tms.GetVisitedTape();
			Assert.Equal(3, visited.Length);
			Assert.Equal(0, visited[0]);
			Assert.Equal(1, visited[1]);
			Assert.Equal(1, visited[2]);

			// executing step 4 of 6
			// before: 0   0[B>0   1   1]  0   0
			// after:  0[A>0   1   1   1]  0   0
			step = tms.Step();
			Assert.True(step);
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(tapeSize, tms.TapeSize); // no change
			Assert.Equal(-2, tms.HeadPositionX);
			Assert.True(4 == tms.StepCount);
			Assert.Equal(0, tms.CurrentState);
			visited = tms.GetVisitedTape();
			Assert.Equal(4, visited.Length);
			Assert.Equal(0, visited[0]);
			Assert.Equal(1, visited[1]);
			Assert.Equal(1, visited[2]);
			Assert.Equal(1, visited[3]);

			// executing step 5 of 6
			// before: 0[A>0   1   1   1]  0   0
			// after:  0[  1 B>1   1   1]  0   0
			step = tms.Step();
			Assert.True(step);
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(tapeSize, tms.TapeSize); // no change
			Assert.Equal(-1, tms.HeadPositionX);
			Assert.True(5 == tms.StepCount);
			Assert.Equal(1, tms.CurrentState);
			visited = tms.GetVisitedTape();
			Assert.Equal(4, visited.Length);
			Assert.Equal(1, visited[0]);
			Assert.Equal(1, visited[1]);
			Assert.Equal(1, visited[2]);
			Assert.Equal(1, visited[3]);

			// executing step 6 of 6
			// before: 0[  1 B>1   1   1]  0   0
			// after:  0[  1   1 X>1   1]  0   0
			step = tms.Step();
			Assert.False(step); // halted
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(tapeSize, tms.TapeSize); // no change
			Assert.Equal(0, tms.HeadPositionX);
			Assert.True(6 == tms.StepCount);
			Assert.Equal(-1, tms.CurrentState); // halted state
			visited = tms.GetVisitedTape();
			Assert.Equal(4, visited.Length);
			Assert.Equal(1, visited[0]);
			Assert.Equal(1, visited[1]);
			Assert.Equal(1, visited[2]);
			Assert.Equal(1, visited[3]);

			// step one more time to test that nothing changes
			step = tms.Step();
			Assert.False(step); // halted
			Assert.Equal(1, tms.HaltingStates.Count); // no change
			Assert.Equal(2, tms.NonHaltingStates.Count); // no change
			Assert.Equal(2, tms.AlphabetSymbols.Count); // no change
			Assert.Equal(0, tms.InitialState); // no change
			Assert.Equal(tapeSize, tms.TapeSize); // no change
			Assert.Equal(0, tms.HeadPositionX);
			Assert.True(6 == tms.StepCount);
			Assert.Equal(-1, tms.CurrentState); // halted state
			visited = tms.GetVisitedTape();
			Assert.Equal(4, visited.Length);
			Assert.Equal(1, visited[0]);
			Assert.Equal(1, visited[1]);
			Assert.Equal(1, visited[2]);
			Assert.Equal(1, visited[3]);
		}

		[Fact]
		public void Test0020()
        {
			uint tapeSize = 100;
			var tms = TuringMachineSimple.FromJson(Constants.Bb1d4s2s, tapeSize);

			Assert.Equal(1, tms.HaltingStates.Count);
			Assert.Equal(4, tms.NonHaltingStates.Count);
			Assert.Equal(2, tms.AlphabetSymbols.Count);
			Assert.Equal(0, tms.InitialState);
			Assert.Equal(tapeSize, tms.TapeSize);
			Assert.Equal(0, tms.HeadPositionX);
			Assert.True(0 == tms.StepCount);

			// hmm, should this be the default behavior before calling InitRun?
			Assert.Equal(-1, tms.CurrentState);

			byte[] visited = tms.GetVisitedTape();
			Assert.Equal(1, visited.Length);
			Assert.Equal(0, visited[0]);

			tms.InitRun();

			while (tms.Step())
			{
				;
			}

			visited = tms.GetVisitedTape();

			Assert.Equal(-9, tms.HeadPositionX);
			Assert.True(107 == tms.StepCount);
			Assert.Equal(-1, tms.CurrentState); // halted state
			visited = tms.GetVisitedTape();
			Assert.Equal(14, visited.Length);
			Assert.Equal(1, visited[0]);
			Assert.Equal(0, visited[1]);
			Assert.Equal(1, visited[2]);
			Assert.Equal(1, visited[3]);
			Assert.Equal(1, visited[4]);
			Assert.Equal(1, visited[5]);
			Assert.Equal(1, visited[6]);
			Assert.Equal(1, visited[7]);
			Assert.Equal(1, visited[8]);
			Assert.Equal(1, visited[9]);
			Assert.Equal(1, visited[10]);
			Assert.Equal(1, visited[11]);
			Assert.Equal(1, visited[12]);
			Assert.Equal(1, visited[13]);
		}
	}
}
