using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Bladeball
{
    internal class Macro
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [DllImport("winmm.dll")]
        private static extern uint timeBeginPeriod(uint uPeriod);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;


        public int iStartKey { get; set; } = 0x46; // F
        public byte bBlockKey1 { get; set; } = 0;
        public byte bBlockKey2 { get; set; } = 0;

        public volatile bool running = false;

        public int CPS { get; set; } = 25;

        private Form1 form;

        public Macro(Form1 form)
        {
            this.form = form;
        }

        private void Click()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            Sleep(1);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        }

        private int GetDelay(int cps)
        {
            if (cps >= 1000)
                return 1;

            if (0 >= cps)
                return 0;

            return 1000 / cps;
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

        public void Start()
        {
            timeBeginPeriod(1);
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            bool waspressed = false;
            SendInput send = null;

            while (true)
            {
                bool pressed = GetAsyncKeyState(iStartKey) < 0;

                if (pressed && !waspressed && !form.AssigningKey)
                {
                    running = !running;

                    if (running)
                        send = GetInput(bBlockKey1, bBlockKey2);
                }

                waspressed = pressed;

                if (!running)
                {
                    Thread.Sleep(5);
                    continue;
                }

                int delay = GetDelay(CPS);

                if (delay <= 0)
                {
                    Thread.Sleep(5);
                    continue;
                }

                Stopwatch sw = Stopwatch.StartNew();

                Click();

                if (send != null)
                    send.SendKeys();

                Sleep(delay);
            }
        }
        
        public void Sleep(int ms)
        {
            long Tsmp = Stopwatch.GetTimestamp();

            long Ticks = (long)(ms * Stopwatch.Frequency / 1000);

            while (Stopwatch.GetTimestamp() - Tsmp < Ticks)
                Thread.SpinWait(100);
        }
    }
}