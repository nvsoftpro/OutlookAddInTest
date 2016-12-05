using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private readonly Dictionary<int, List<IRibbonControlObject>> controls;
        public RibbonControlFactory()
        {
            controls = new Dictionary<int, List<IRibbonControlObject>>();
        }

        public IRibbonControlObject GetControlObject(Office.IRibbonControl control)
        {
            List<IRibbonControlObject> controlObjects = null;
            if (control?.Context == null)
            {
                return null;
            }

            var contextHashCode = control.Context.GetHashCode();
            Debug.WriteLine($"Context hash code:{contextHashCode}");
            logger.Message($"Context hash code:{contextHashCode}");

            IRibbonControlObject controlObject = null;
            controls.TryGetValue(contextHashCode, out controlObjects);

            if (controlObjects != null)
            {
                controlObject = controlObjects.FirstOrDefault(c => c.Tag == control.Tag);
            }
           

            if (controlObject == null)
            {
                controlObject = ControlFactoryMethod(control);
                if (controlObject != null)
                {
                    
                    if (controlObjects != null)
                    {
                        controlObjects.Add(controlObject);
                        controls[contextHashCode] = controlObjects;
                    }
                    else
                    {
                        controlObjects = new List<IRibbonControlObject>(new[] {controlObject});
                        controls.Add(contextHashCode, controlObjects);
                    }
                }
            }

            return controlObject;
        }

        private IRibbonControlObject ControlFactoryMethod(Office.IRibbonControl control)
        {
            IRibbonControlObject controlObject;
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

            return controlObject;
        }
    }
}
