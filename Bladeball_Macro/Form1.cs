using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Bladeball_Macro
{
    public partial class Form1 : Form
    {
        [DllImport("winmm.dll")]
        private static extern uint timeBeginPeriod(uint uPeriod);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        private const uint MapVK_TO_VSC = 0;

        private int Start_Stop_Key; // F, 0x46
        private byte BlockKey1;
        private byte BlockKey2;

        private volatile bool running = false;
        private volatile bool AssigningKey = false;

        private int CPS;

        private int KeyIndex = 0;

        public Form1()
        {
            InitializeComponent();

            KeyPreview = true;
            KeyDown += AssignHotkey;
            KeyUp += FinishAssigningHotkey;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSettings();

            Task.Run(Macro);
        }

        private void Macro()
        {
            timeBeginPeriod(1);
            bool wasPressed = false;

            while (true)
            {
                bool pressed = GetAsyncKeyState(Start_Stop_Key) < 0;

                if (pressed && !wasPressed && !AssigningKey)
                {
                    running = !running;
                }

                wasPressed = pressed;

                if (!running)
                {
                    Thread.Sleep(5);
                    continue;
                }

                if (running)
                {
                    SendInputs? sendInp = GetInputs();
                    int delay = CalcDelay(CPS);

                    if (delay <= 0)
                    {
                        running = false;
                        continue;
                    }

                    Stopwatch sw = Stopwatch.StartNew();

                    LClick();
                    sendInp?.SendKeys();

                    sw.Stop();

                    int restDelay = delay - (int)sw.ElapsedMilliseconds;

                    if (restDelay > 0)
                        Thread.Sleep(restDelay);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // Block Key 1
        {
            AssigningKey = true;
            KeyIndex = 1;
            button1.Text = "Enter Hotkey";
        }

        private void button2_Click(object sender, EventArgs e) // Block Key 2
        {
            AssigningKey = true;
            KeyIndex = 2;
            button2.Text = "Enter Hotkey";
        }

        private void button3_Click(object sender, EventArgs e) // Start/Stop Key
        {
            AssigningKey = true;
            KeyIndex = 3;
            button3.Text = "Enter Hotkey";
        }

        private void LoadSettings()
        {
            Start_Stop_Key = Properties.Settings.Default.Start_Stop_Key;
            BlockKey1 = Properties.Settings.Default.BlockKey1;
            BlockKey2 = Properties.Settings.Default.BlockKey2;
            CPS = Properties.Settings.Default.CPS;
            button1.Text = Properties.Settings.Default.BlockKey1Text;
            button2.Text = Properties.Settings.Default.BlockKey2Text;
            button3.Text = Properties.Settings.Default.Start_Stop_KeyText;
            textBox1.Text = CPS.ToString();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Start_Stop_Key = Start_Stop_Key;
            Properties.Settings.Default.BlockKey1 = BlockKey1;
            Properties.Settings.Default.BlockKey2 = BlockKey2;
            Properties.Settings.Default.CPS = CPS;
            Properties.Settings.Default.BlockKey1Text = button1.Text;
            Properties.Settings.Default.BlockKey2Text = button2.Text;
            Properties.Settings.Default.Start_Stop_KeyText = button3.Text;
            Properties.Settings.Default.Save();
        }

        private void AssignHotkey(object sender, KeyEventArgs e)
        {

            if (KeyIndex == 0)
                return;

            byte scancode = (byte)MapVirtualKey((uint)e.KeyCode, MapVK_TO_VSC);

            switch (KeyIndex)
            {
                case 1:
                    {
                        BlockKey1 = scancode;
                        button1.Text = e.KeyCode.ToString();
                        break;
                    }
                case 2:
                    {
                        BlockKey2 = scancode;
                        button2.Text = e.KeyCode.ToString();
                        break;
                    }
                case 3:
                    {
                        Start_Stop_Key = (int)e.KeyCode;
                        button3.Text = e.KeyCode.ToString();
                        break;
                    }
                default:
                    break;
            }

            SaveSettings();

            KeyIndex = 0;

            e.SuppressKeyPress = true;
        }

        private void FinishAssigningHotkey(object sender, KeyEventArgs e)
        {
            if (AssigningKey && KeyIndex == 0)
            {
                AssigningKey = false;
            }
        }

        private SendInputs? GetInputs()
        {
            if (BlockKey1 != 0 && BlockKey2 != 0)
            {
                SendInputs sendInp = new SendInputs(BlockKey1, BlockKey2);
                return sendInp;
            }
            else if (BlockKey1 != 0)
            {
                SendInputs sendInp = new SendInputs(BlockKey1);
                return sendInp;
            }
            else if (BlockKey2 != 0)
            {
                SendInputs sendInp = new SendInputs(BlockKey2);
                return sendInp;
            }
            return null;
        }

        private void CalcCPS()
        {
            if (!int.TryParse(textBox1.Text, out int cps) || cps <= 0)
            {
                CPS = 20;
                textBox1.Text = CPS.ToString();
            }
            else
            {
                CPS = cps;
            }

            SaveSettings();
        }

        private int CalcDelay(int cps)
        {
            if (cps <= 0)
                return 0;

            return 1000 / cps;
        }

        private void LClick()
        {
            LeftClick.LeftDown();
            Thread.Sleep(1);
            LeftClick.LeftUp();
        }

        private void button4_Click(object sender, EventArgs e) // Reset BlockKey1
        {
            AssigningKey = false;
            KeyIndex = 0;
            BlockKey1 = 0;
            button1.Text = "None";
            SaveSettings();
        }

        private void button5_Click(object sender, EventArgs e) // Reset BlockKey2
        {
            AssigningKey = false;
            KeyIndex = 0;
            BlockKey2 = 0;
            button2.Text = "None";
            SaveSettings();
        }

        private void button6_Click(object sender, EventArgs e) // Apply button
        {
            CalcCPS();
        }
    }
}
