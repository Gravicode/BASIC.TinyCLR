﻿using GHI.BasicSharp;
using GHI.BasicSharp.Properties;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace GHI.BasicSharp
{
    public class BasicSharpEngine
    {
        public Thread BasicProc { get; set; }
        Interpreter basic = null;
        CommunicationsBus Bus { get; set; }
        public BasicSharpEngine(string uartPort, int baudRate=9600)
        {
       
            Bus = new CommunicationsBus(uartPort,baudRate);
           
            if (basic == null)
            {
                basic = new Interpreter();
                basic.Print += Basic_Print;
                basic.ClearScreen += Basic_ClearScreen;
            }
           
        }
        private void Basic_ClearScreen(Interpreter sender)
        {
            //do nothing
            Debug.WriteLine("CLEAR SCREEN");
        }

        private void Basic_Print(Interpreter sender, string value)
        {
            Debug.WriteLine(value);
            Bus.WriteLine(value);
        }

        public Thread Run()
        {
            if (BasicProc != null) return BasicProc;
            BasicProc = new Thread(new ThreadStart(
                () =>
                {
                    var cmd = string.Empty;
                    PrintStartUpMessage();
                    while (true)
                    {
                        var dataToRead = Bus.ByteToRead;

                        if (dataToRead > 0)
                        {
                            dataToRead = dataToRead < Bus.ReadBufferSize ? dataToRead : Bus.ReadBufferSize;

                            var data = new byte[dataToRead];
                            Bus.Read(data);

                            var dataStr = Encoding.UTF8.GetString(data, 0, dataToRead);

                            //if (uAlfatModule.IsEchoEnabled)
                            //{
                            //    for (var i = 0; i < data.Length; i++)
                            //    {
                            //        // send back whatever the host sent except for terminal line                    
                            //        Bus.Write(data, i, 1);
                            //    }
                            //}

                            cmd += dataStr;

                            if (dataStr.IndexOf(Strings.NewLine) > -1)
                            {
                                var trim = cmd.Trim();

                                this.ProcessCommand(trim);

                                cmd = string.Empty;
                            }
                        }
                        else
                        {
                            Thread.Sleep(1);
                        }
                    }
                }));
            BasicProc.Start();
            return BasicProc;
        }

        private void ProcessCommand(string codes)
        {
            if (string.IsNullOrEmpty(codes))
            {
                return;
            }
            try
            {
                //this is can run if the whole codes send to the parameter, cannot process partially (repl)
                basic.Run(codes);
               
            }
            catch { }
        }
            void PrintStartUpMessage()
        {
            var appVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var bootVer = Resources.GetString(Resources.StringResources.BOOTLOADER_VER);
            Bus.WriteLine($" GHI Electronics, LLC{Strings.NewLine}----------------------{Strings.NewLine}   Boot Loader {bootVer} App Ver.{appVer}{Strings.NewLine}");
            //Debug.WriteLine($" GHI Electronics, LLC{Strings.NewLine}----------------------{Strings.NewLine}   Boot Loader 2.05{Strings.NewLine}   uALFAT(TM) 3.13{Strings.NewLine}{ResponseCode.Success}"); 
        }
    }
}
