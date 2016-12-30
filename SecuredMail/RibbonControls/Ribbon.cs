using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;
using SecuredMail.RibbonControls;
using Office = Microsoft.Office.Core;

namespace SecuredMail
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;
        private readonly RibbonControlFactory _controlFactory;

        public Ribbon()
        {
            _controlFactory = new RibbonControlFactory();
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return RibbonSelector.Get(ribbonID);
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit http://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        public Bitmap GetImage(Office.IRibbonControl control)
        {
            var controlObj = _controlFactory.GetControlObject(control) as IImageable;
            return controlObj?.GetImage();
        }

        public bool GetEnabled(Office.IRibbonControl control)
        {
            var controlObject = _controlFactory.GetControlObject(control);
            if (controlObject == null)
            {
                return true;
            }

            return controlObject.GetEnabled();
        }

        public async void OnActionCallback(Office.IRibbonControl control)
        {
            IRibbonControlObject controlObject = _controlFactory.GetControlObject(control);
            if (controlObject == null)
            {
                return;
            }

            IRibbonControlEnabled controlObjectEnabled = controlObject as IRibbonControlEnabled;

            //ribbon.ToggleEnabled(false);
            var rb1 = Globals.Ribbons[control.Context as Inspector];
            await controlObject.OnActionCallback();
            //controlObjectEnabled?.ToggleEnabled(true);
        }
        #endregion
    }
}
