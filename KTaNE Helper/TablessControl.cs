using System;
using System.ComponentModel;
using System.Windows.Forms;

// ReSharper disable once CheckNamespace
public class TablessControl : TabControl
{
    protected override void WndProc(ref Message m)
    {
        // Hide tabs by trapping the TCM_ADJUSTRECT message
        if (m.Msg == 0x1328 && DesignMode && !Multiline && _hideTabsinDesignMode) m.Result = (IntPtr)1;
        else if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr) 1;
        else base.WndProc(ref m);
    }

    private bool _hideTabsinDesignMode;

    [DefaultValue(true)]
    public bool HideTabsInDesignMode
    {
        get { return _hideTabsinDesignMode; }
        set
        {
            if (_hideTabsinDesignMode == value)
                return;

            _hideTabsinDesignMode = value;

            if(!Multiline && DesignMode)
                RecreateHandle();
        }
    }
}