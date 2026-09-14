using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Bladeball
{
    internal class Macro
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        public static volatile bool run = false;

        public static void Click()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        }

        public static int GetDelay(int cps)
        {
            if (cps >= 1000)
                return 1;

            if (0 >= cps)
                return 0;

            return 1000 / cps;
        }

        public static SendInput? GetInput(byte key1, byte key2)
        {
            SendInput? sendinp = null;

            if (key1 != 0 && key2 != 0)
                sendinp = new SendInput(key1, key2);

            else if (key1 != 0)
                sendinp = new SendInput(key1);

            else if (key2 != 0)
                sendinp = new SendInput(key2);

            return sendinp;
        }

        public static void Send(SendInput sendinp)
        {
            if (sendinp is not null)
                sendinp.SendKeys();
        }
    }
}
