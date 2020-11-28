using System;
using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace BasicSharp
{
    class BuiltIns
    {
        public static void InstallAll(Interpreter interpreter)
        {
            interpreter.AddFunction("str", Str);
            interpreter.AddFunction("num", Num);
            interpreter.AddFunction("abs", Abs);
            interpreter.AddFunction("min", Min);
            interpreter.AddFunction("max", Max);
            interpreter.AddFunction("not", Not);
        }

        public static Value Str(Interpreter interpreter, ArrayList args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return ((Value)args[0]).Convert(ValueType.String);
        }

        public static Value Num(Interpreter interpreter, ArrayList args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return ((Value)args[0]).Convert(ValueType.Real);
        }

        public static Value Abs(Interpreter interpreter, ArrayList args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(Math.Abs(((Value)args[0]).Real));
        }

        public static Value Min(Interpreter interpreter, ArrayList args)
        {
            if (args.Count < 2)
                throw new ArgumentException();

            return new Value(Math.Min(((Value)args[0]).Real, ((Value)args[1]).Real));
        }

        public static Value Max(Interpreter interpreter, ArrayList args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(Math.Max(((Value)args[0]).Real, ((Value)args[1]).Real));
        }

        public static Value Not(Interpreter interpreter, ArrayList args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(((Value)args[0]).Real == 0 ? 1 : 0);
        }
        /*
          public static Value Str(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return args[0].Convert(ValueType.String);
        }

        public static Value Num(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return args[0].Convert(ValueType.Real);
        }

        public static Value Abs(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(Math.Abs(args[0].Real));
        }

        public static Value Min(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 2)
                throw new ArgumentException();

            return new Value(Math.Min(args[0].Real, args[1].Real));
        }

        public static Value Max(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(Math.Max(args[0].Real, args[1].Real));
        }

        public static Value Not(Interpreter interpreter, List<Value> args)
        {
            if (args.Count < 1)
                throw new ArgumentException();

            return new Value(args[0].Real == 0 ? 1 : 0);
        }
         */

    }
}
