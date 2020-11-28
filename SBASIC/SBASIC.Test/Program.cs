
using GHIElectronics.TinyCLR.Pins;
using SBASIC.Library;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;
namespace Basic.Test
{
    class Program
    {
       
        static void Main()
        {
            //use UART5 on SITCore SC20260

            //basic testing
            var module = new SBasicEngine(SC20260.UartPort.Uart5);
            module.Run();
            Thread.Sleep(Timeout.Infinite);
        }


    }
}
