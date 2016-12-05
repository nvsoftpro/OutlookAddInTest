using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SecuredMail.Wrappers
{
    /// <summary>
    /// Wrapper for Win API functions
    /// </summary>
    public class Win32
    {
        [DllImport("User32.dll"), SuppressUnmanagedCodeSecurity]
        public static extern Int32 FindWindow(String lpClassName, String lpWindowName);

        [DllImport("User32.dll"), SuppressUnmanagedCodeSecurity]
        public static extern Int32 SetForegroundWindow(int hWnd);

        [DllImport("User32.dll"), SuppressUnmanagedCodeSecurity]
        public static extern Boolean EnumChildWindows(int hWndParent, Delegate lpEnumFunc, int lParam);

        [DllImport("User32.dll"), SuppressUnmanagedCodeSecurity]
        public static extern Int32 GetWindowText(int hWnd, StringBuilder s, int nMaxCount);

        [DllImport("User32.dll"), SuppressUnmanagedCodeSecurity]
        public static extern Int32 GetWindowTextLength(int hwnd);
    }
}
