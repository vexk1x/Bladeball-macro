using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Bladeball
{
    internal class Macro
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [DllImport("winmm.dll")]
        private static extern uint timeBeginPeriod(uint uPeriod);

        [DllImport("winmm.dll")]
        private static extern uint timeEndPeriod(uint uPeriod);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;


        public int iStartKey { get; set; } = 0x46; // F
        public byte bBlockKey1 { get; set; } = 0;
        public byte bBlockKey2 { get; set; } = 0;

        public volatile bool running = false;

        public int CPS { get; set; } = 25;

        private void Click()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            Sleep(0.5);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        }

        private double GetDelay(int cps)
        {
            if (cps >= 1000)
                return 1;

            if (0 >= cps)
                return 0;

            return 1000.0 / cps;
        }

        private SendInput GetInput(byte key1, byte key2)
        {
            SendInput sendinp = null;

            if (key1 != 0 && key2 != 0)
                sendinp = new SendInput(key1, key2);

            else if (key1 != 0)
                sendinp = new SendInput(key1);

            else if (key2 != 0)
                sendinp = new SendInput(key2);

            return sendinp;
        }

        public void Start(CancellationToken t)
        {
            SendInput send = GetInput(bBlockKey1, bBlockKey2);
            double delay = GetDelay(CPS);

            Stopwatch sw = new Stopwatch();

            if (iStartKey == bBlockKey1 || iStartKey == bBlockKey2)
            {
                MessageBox.Show("Macro will not Start if your start hotkey is a block hotkey.", "Onyx | Bladeball macro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            while (!t.IsCancellationRequested)
            {
                timeBeginPeriod(1);

                try
                {
                    sw.Restart();

                    Click();

                    if (send != null)
                        send.SendKeys();

                    double remaining = delay - sw.Elapsed.TotalMilliseconds;

                    if (remaining > 0)
                        Sleep(remaining);
                }
                catch { }
                finally
                {
                    timeEndPeriod(1);
                }
            }
        }
        
        public void Sleep(double ms)
        {
            long ticks = (long)(ms * Stopwatch.Frequency / 1000);

            long timestamp = Stopwatch.GetTimestamp() + ticks;

            long e = Stopwatch.Frequency / 500;


            while (true)
            {
                long remaining = timestamp - Stopwatch.GetTimestamp();

                if (remaining <= 0)
                {
                    return;
                }

                if (remaining > e)
                    Thread.Sleep(1);
                else
                    Thread.SpinWait(10);
            }
        }
    }
}