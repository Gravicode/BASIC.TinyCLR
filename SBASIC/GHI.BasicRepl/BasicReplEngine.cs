using GHI.BasicRepl;
using GHI.BasicRepl.IO;
using GHI.BasicRepl.Parsing;
using GHI.BasicRepl.Properties;
using GHI.BasicRepl.Runtime;
using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;

namespace GHI.BasicRepl
{
    public class BasicReplEngine
    {
       
       

        BasicReplEngine(string Uart,int BaudRate=9600)
        {
            var inputOutput = new SerialInputOutput(Uart,BaudRate);
            var parser = new Parser();
            var programRepository = new FileProgramRepository(parser);

            using (var rte = new RunTimeEnvironment(inputOutput, programRepository))
            {
                var readEvaluatePrintLoop = new ReadEvaluatePrintLoop(rte, parser);

                PrintSalute(inputOutput);
                Run(readEvaluatePrintLoop);
            }
        }

        private void PrintSalute(IInputOutput inputOutput)
        {
            //var assembly = Assembly.GetExecutingAssembly();
            //var assemblyName = assembly.GetName();
            //inputOutput.WriteLine("{0} {1}", assemblyName.Name, assemblyName.Version);
            var appVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var bootVer = Resources.GetString(Resources.StringResources.BOOTLOADER_VER);
            inputOutput.WriteLine($" GHI Electronics, LLC{Strings.NewLine}----------------------{Strings.NewLine}   Boot Loader {bootVer} App Ver.{appVer}{Strings.NewLine}");

        }

        private void Run(ReadEvaluatePrintLoop readEvaluatePrintLoop)
        {
            do
            {
                readEvaluatePrintLoop.TakeStep();
            }
            while (!readEvaluatePrintLoop.IsOver);
        }
    }
}
