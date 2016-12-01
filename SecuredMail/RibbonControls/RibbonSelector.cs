using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using SecuredMail.Logger;

namespace SecuredMail.RibbonControls
{
    /// <summary>
    /// Select the RibbonResource according the RibbonID string
    /// </summary>
    public class RibbonSelector
    {
        private static ILogger logger;
        public static string Get(string ribbonId)
        {
            Debug.WriteLine($"RibbonID:{ribbonId}");
            logger.Message("RibbonID: ", ribbonId);

            switch (ribbonId)
            {
                case "Microsoft.Outlook.Explorer":
                case "Microsoft.Outlook.Appointment":
                case "Microsoft.Outlook.Contact":
                case "Microsoft.Outlook.Task":
                    return null;

                case "Microsoft.Outlook.Mail.Compose":
                        return GetResourceText("SecuredMail.RibbonControls.Ribbon.xml");
                
                default:
                    return null;
            }
        }

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
