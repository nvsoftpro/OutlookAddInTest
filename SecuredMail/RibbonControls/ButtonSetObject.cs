using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;

namespace SecuredMail.RibbonControls
{
    /// <summary>
    /// Button for getting Windows Data over P/Invoke. 
    /// </summary>
    public class ButtonSetObject : RibbonControlObject, IRibbonControlEnabled
    {
        private static bool isEnabled = true;
        public ButtonSetObject(string id, object context, string tag) : base(id, context, tag)
        {
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }
        public override bool GetEnabled()
        {
            return isEnabled;
        }

        public override async Task<bool> OnActionCallback()
        {
            this.IsEnabled = false;
            await base.OnActionCallback();
            var item = this.Context as Inspector;
            var mailItem = item?.CurrentItem as MailItem;
            if (mailItem == null)
            {
                return false;
            }

            using (var mail = new OutlookMailItem(mailItem))
            {
                await mail.ChangeEmailSubjectAndBodyWithWindowsDataAsync();
            }

            this.IsEnabled = true;
            return true;
        }
    }
}
