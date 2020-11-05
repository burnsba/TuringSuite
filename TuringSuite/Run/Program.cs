using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TuringSuite.Core;

namespace Run
{
    class Program
    {
        static void Main(string[] args)
        {
            var tms = TuringMachineSimple.FromJsonFile("../../../../MachineDefinitions/bb1d4s2s.json", 100000);

            tms.InitRun();

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
