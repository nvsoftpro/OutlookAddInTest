using System.Diagnostics;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;

namespace SecuredMail.RibbonControls
{
    public static class ExtensionControl
    {
        public static void ToggleEnabled(this IRibbonControlEnabled control, IRibbonUI ribbon, bool enabled)
        {
            var inspector = control.Context as Inspector;

            control.IsEnabled = enabled;
            Debug.WriteLine($"Toggle {control.IsEnabled}");
            ribbon?.InvalidateControl(control.Id);
        }
    }
}
