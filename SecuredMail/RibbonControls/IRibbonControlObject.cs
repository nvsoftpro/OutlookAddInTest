using System.Threading.Tasks;
using Microsoft.Office.Core;

namespace SecuredMail
{
    /// <summary>
    ///  Declare the Action behavior for RibbonControls
    /// </summary>
    public interface IRibbonControlObject: IRibbonControl
    {
        /// <summary>
        /// Enable/Disable the RibbonControl
        /// </summary>
        /// <returns></returns>
        bool GetEnabled();

        /// <summary>
        /// Click event action
        /// </summary>
        Task<bool> OnActionCallback();
    }
}