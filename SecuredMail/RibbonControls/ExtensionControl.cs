using System.Diagnostics;
using System.Linq;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;

namespace SecuredMail.RibbonControls
{
    public static class ExtensionControl
    {
        public static void ToggleEnabled(this IRibbonControlEnabled control, bool enabled)
        {
            //ThisRibbonCollection ribbonCollection = Globals.Ribbons[control.Context];
            //IRibbonExtension ribbonExtension = ribbonCollection.GetRibbon(typeof (Ribbon));
            //var rb = Globals.ThisAddIn.ribbon as IRibbonUI;
            //control.IsEnabled = enabled;
            //Debug.WriteLine($"Toggle {control.IsEnabled}");
            //rb?.InvalidateControl(control.Id);
        }
    }
}
