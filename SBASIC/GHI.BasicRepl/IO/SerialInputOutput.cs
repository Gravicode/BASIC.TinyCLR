namespace GHI.BasicRepl.IO
{
    using System;
    using System.Collections;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Implements input and output operations for <see cref="Console">console</see>.
    /// </summary>
    public class SerialInputOutput : IInputOutput
    {
        /// <inheritdoc />
        public event EventHandler OnBreak;
        Queue commands; 
        /// <summary>
        /// Initializes a new instance of the class <see cref="SerialInputOutput"/>.
        /// </summary>
        public SerialInputOutput(string uartPort, int baudRate = 9600)
        {
            Bus = new CommunicationsBus(uartPort, baudRate);
            commands = new Queue();
            

            /*
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
            {
                var onBreak = OnBreak;
                if (onBreak != null)
                    onBreak(this, EventArgs.Empty);

                // Surpise, this means DO NOT CANCEL:
                e.Cancel = true;
            };*/
        }
        public Thread BasicProc { get; set; }
        //GHI.BasicRepl.SBASIC basic = null;
        CommunicationsBus Bus { get; set; }
       
        public Thread Run()
        {
            if (BasicProc != null) return BasicProc;
            BasicProc = new Thread(new ThreadStart(
                () =>
                {
                    var cmd = string.Empty;
                    
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

                                commands.Enqueue(trim);

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


        /// <inheritdoc />
        public virtual string ReadLine()
        {
            var cmd = string.Empty;
            while(true){
                if (commands.Count > 0)
                {
                    cmd = commands.Dequeue().ToString();
                    break;
                }
                Thread.Sleep(10);
            }
            return cmd;
        }

        /// <inheritdoc />
        public virtual void Write(string s)
        {
            Bus.Write(s);
        }

        /// <inheritdoc />
        public virtual void Write(string format, params object[] args)
        {
            var content = string.Format(format, args);
            Bus.Write(content);
        }

        /// <inheritdoc />
        public virtual void WriteLine(string s)
        {
            Bus.WriteLine(s);
        }

        /// <inheritdoc />
        public virtual void WriteLine(string format, params object[] args)
        {
            var content = string.Format(format, args);
            Bus.WriteLine(content);
        }

        /// <inheritdoc />
        public virtual void WriteLine()
        {
            Bus.WriteLine("");
        }
    }
}
