﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using SecuredMail.Properties;
using SecuredMail.WindowControls;

namespace SecuredMail.RibbonControls
{
    /// <summary>
    /// Button for getting EratosthenesSieveNumbers and put them into an Email body 
    /// </summary>
    class ButtonEratosthenes : RibbonControlObject, IImageable, IRibbonControlEnabled
    {
        private static bool isEnabled = true;

        public ButtonEratosthenes(string id, object context, string tag) : base(id, context, tag)
        {
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }

        public Bitmap GetImage()
        {
            return Resources.counting;
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

            var dialogWindow = new DialogForm();
            DialogResult result = dialogWindow.ShowDialog();
            if (result == DialogResult.OK)
            {
                IProgress<int> progress = new Progress<int>(ProgressUpdate);
                int maxNumber = 100;
                Int32.TryParse(dialogWindow.MaxNumber, out maxNumber);
                using (var mail = new OutlookMailItem(mailItem))
                {
                    await mail.ChangeEmailBodyWithEratosthenesSieveNumbers(maxNumber, progress);
                }
            }
            dialogWindow.Dispose();

            this.IsEnabled = true;
            return true;
        }

        private void ProgressUpdate(int value)
        {
            Debug.WriteLine(value);
        }
    }
}
