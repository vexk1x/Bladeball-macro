using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Bladeball_Macro
{
    internal class SendInputs
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint KEYEVENTF_SCANCODE = 0x0008;

        private byte[] keys;

        public SendInputs(params byte[] keys)
        {
            this.keys = keys;
        }

        public void SendKeys()
        {
            if (keys.Length <= 0)
                return;

            for (int i = 0; i < keys.Length; i++)
            {
                keybd_event(0, keys[i], KEYEVENTF_SCANCODE, UIntPtr.Zero);
                Thread.Sleep(1);
                keybd_event(0, keys[i], KEYEVENTF_SCANCODE | KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
        }
    }
}
