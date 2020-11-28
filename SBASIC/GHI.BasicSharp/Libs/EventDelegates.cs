using System;
using System.Drawing;

namespace GHI.BasicSharp
{

    [Serializable]
    public delegate void OnClearScreen(BasicSharp sender);

    [Serializable]
    public delegate void OnBackColor(BasicSharp sender, Color color);

    [Serializable]
    public delegate void OnForeColor(BasicSharp sender, Color color);

    [Serializable]
    public delegate void OnInKey(BasicSharp sender, ref int keyCode);

    [Serializable]
    public delegate void OnInput(BasicSharp sender, ref string text);

    [Serializable]
    public delegate void OnPrint(BasicSharp sender, string value);


}
