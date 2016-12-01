using System.Collections.Generic;
using System.Diagnostics;
using SecuredMail.Logger;
using SecuredMail.RibbonControls;
using Office = Microsoft.Office.Core;

namespace SecuredMail
{
    /// <summary>
    /// Create or provide ribbon controls
    /// </summary>
    class RibbonControlFactory
    {
        private ILogger logger;
        private readonly Dictionary<int, IRibbonControlObject> controls;
        public RibbonControlFactory()
        {
            controls = new Dictionary<int, IRibbonControlObject>();
        }

        public IRibbonControlObject GetControlObject(Office.IRibbonControl control)
        {
            IRibbonControlObject controlObject = null;
            if (control?.Context == null)
            {
                return null;
            }

            var controlObjectHashCode = control.Id.GetHashCode() + control.Context.GetHashCode();
            Debug.WriteLine($"Context hash code:{controlObjectHashCode}");
            logger.Message($"Context hash code:{controlObjectHashCode}");
            controls.TryGetValue(controlObjectHashCode, out controlObject);
            if (controlObject == null)
            {
                switch (control.Tag)
                {
                    case "eratosthenes":
                        controlObject = new ButtonEratosthenes(control.Id, control.Context, control.Tag);
                        break;
                    case "setsubject":
                        controlObject = new ButtonSetObject(control.Id, control.Context, control.Tag);
                        break;
                    case "clearbody":
                        controlObject = new ButtonClearBody(control.Id, control.Context, control.Tag);
                        break;
                    default:
                        Trace.WriteLine("No control object. Make sure the tag of control is correct spelling");
                        logger.Message("No control object. Make sure the tag of control is correct spelling");
                        return null;
                }

                controls.Add(controlObjectHashCode, controlObject);
            }

            return controlObject;
        }
    }
}
