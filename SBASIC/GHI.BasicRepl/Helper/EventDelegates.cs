using System;
using System.Drawing;

namespace GHI.BasicReplSharp
{

    [Serializable]
    public delegate void OnClearScreen(Interpreter sender);

    [Serializable]
    public delegate void OnBackColor(Interpreter sender, Color color);

    [Serializable]
    public delegate void OnForeColor(Interpreter sender, Color color);

    [Serializable]
    public delegate void OnInKey(Interpreter sender, ref int keyCode);

    [Serializable]
    public delegate void OnInput(Interpreter sender, ref string text);

    [Serializable]
    public delegate void OnPrint(Interpreter sender, string value);


}
