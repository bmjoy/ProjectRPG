using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct DialogueOption
{
    public bool AutoClose { get; private set; }
    public Action OnClick { get; private set; }

    public DialogueOption(bool autoClose, Action onClick)
    {
        AutoClose = autoClose;
        OnClick = onClick;
    }
}
