using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecuredMail.DisposeImp;
using SecuredMail.Wrappers;

namespace SecuredMail.DataGen
{
    /// <summary>
    /// Get Windows ContentData from Outlook
    /// </summary>
    public class WindowsContent: DisposableObject
    {
        int hWnd;
        public delegate int Callback(int hWnd, int lParam);

        public WindowsContent()
        {
            OutlookControlContains = new List<string>();
        }
        public List<string> OutlookControlContains { get;  }
        public List<string> GetWindowsText()
        {
            Callback myCallBack = new Callback(EnumChildGetValue);
            hWnd = Win32.FindWindow(null, "Outlook Today - Outlook");
            if (hWnd != 0)
            {
                Win32.EnumChildWindows(hWnd, myCallBack, 0);
            }

            return OutlookControlContains;
        }

        private int EnumChildGetValue(int hWnd, int lParam)
        {
            StringBuilder formDetails = new StringBuilder(256);
            int txtValue;
            string editText = "";
            txtValue = Win32.GetWindowText(hWnd, formDetails, 256);
            editText = formDetails.ToString().Trim();
            OutlookControlContains.Add($"Contains text of control: {editText} value: {txtValue}");
            return 1;
        }
    }
}