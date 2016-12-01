using Microsoft.Office.Core;

namespace SecuredMail
{
    /// <summary>
    /// Enable/disable the ribbon control
    /// </summary>
    public interface IRibbonControlEnabled : IRibbonControl
    {
        bool IsEnabled { get; set; }
    }
}