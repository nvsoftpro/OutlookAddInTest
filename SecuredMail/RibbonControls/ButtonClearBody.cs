using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;

namespace SecuredMail.RibbonControls
{
    /// <summary>
    /// Button to Clear of Email Body 
    /// </summary>
    public class ButtonClearBody : RibbonControlObject
    {
        public ButtonClearBody(string id, object context, string tag) : base(id, context, tag)
        {
        }

        public override Task<bool> OnActionCallback()
        {
            var item = this.Context as Inspector;
            var mailItem = item?.CurrentItem as MailItem;

            using (var mail = new OutlookMailItem(mailItem))
            {
                DialogResult result = MessageBox.Show("Are you sure to clear the email body?", "Warning",
                    MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    mail.ClearEmailBody();
                }
            }

            return Task.Run(() => true);
        }
    }
}
