using System;
using System.Collections.Generic;
using TuringSuite.Core;

namespace Run
{
    class Program
    {
        static void Main(string[] args)
        {
            var tms = new TuringMachineSimple(100000, 0, 0);

            // two symbol, two state
            //tms.Transitions.Add(new MachineTransitionSimple()
            //{
            //    FromState = 0,
            //    FromSymbol = 0,
            //    MoveOffsetX = 1,
            //    NextState = 1,
            //    NextStateHalts = false,
            //    WriteSymbol = 1,
            //});

            //tms.Transitions.Add(new MachineTransitionSimple()
            //{
            //    FromState = 0,
            //    FromSymbol = 1,
            //    MoveOffsetX = -1,
            //    NextState = 1,
            //    NextStateHalts = false,
            //    WriteSymbol = 1,
            //});

            //tms.Transitions.Add(new MachineTransitionSimple()
            //{
            //    FromState = 1,
            //    FromSymbol = 0,
            //    MoveOffsetX = -1,
            //    NextState = 0,
            //    NextStateHalts = false,
            //    WriteSymbol = 1,
            //});

            //tms.Transitions.Add(new MachineTransitionSimple()
            //{
            //    FromState = 1,
            //    FromSymbol = 1,
            //    MoveOffsetX = 1,
            //    NextState = -1,
            //    NextStateHalts = true,
            //    WriteSymbol = 1,
            //});

            //tms.States = new List<int>() { 0, 1 };
            //tms.AlphabetSymbols = new List<byte>() { 0, 1 };

            // 4 state, 2 symbol
            
            tms.Transitions.Add(new MachineTransitionSimple()
            {
                FromState = 0,
                FromSymbol = 0,
                MoveOffsetX = 1,
                NextState = 1,
                NextStateHalts = false,
                WriteSymbol = 1,
            });

            tms.Transitions.Add(new MachineTransitionSimple()
            {
                FromState = 0,
                FromSymbol = 1,
                MoveOffsetX = -1,
                NextState = 1,
                NextStateHalts = false,
                WriteSymbol = 1,
            });

            tms.Transitions.Add(new MachineTransitionSimple()
            {
                FromState = 1,
                FromSymbol = 0,
                MoveOffsetX = -1,
                NextState = 0,
                NextStateHalts = false,
                WriteSymbol = 1,
            });

            tms.Transitions.Add(new MachineTransitionSimple()
            {
                FromState = 1,
                FromSymbol = 1,
                MoveOffsetX = -1,
                NextState = 2,
                NextStateHalts = false,
                WriteSymbol = 0,
            });

            tms.Transitions.Add(new MachineTransitionSimple()
            {
                FromState = 2,
                FromSymbol = 0,
                MoveOffsetX = 1,
                NextState = -1,
                NextStateHalts = true,
                WriteSymbol = 1,
            });

            tms.Transitions.Add(new MachineTransitionSimple()
            {
                FromState = 2,
                FromSymbol = 1,
                MoveOffsetX = -1,
                NextState = 3,
                NextStateHalts = false,
                WriteSymbol = 1,
            });

            tms.Transitions.Add(new MachineTransitionSimple()
            {
                FromState = 3,
                FromSymbol = 0,
                MoveOffsetX = 1,
                NextState = 3,
                NextStateHalts = false,
                WriteSymbol = 1,
            });

            tms.Transitions.Add(new MachineTransitionSimple()
            {
                FromState = 3,
                FromSymbol = 1,
                MoveOffsetX = 1,
                NextState = 0,
                NextStateHalts = false,
                WriteSymbol = 0,
            });

            tms.States = new List<int>() { 0, 1, 2, 3 };
            tms.AlphabetSymbols = new List<byte>() { 0, 1 };

            // -----

            tms.InitTransitions();
            
            while (tms.Step())
            {
                ;
            }

            var visited = tms.GetVisitedTape();

            Console.Write(String.Join(",", visited));

            int a = 9;
        }
    }
}
