using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bladeball
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("winmm.dll")]
        private static extern uint timeBeginPeriod(uint uPeriod);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;


        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        private const uint MapVK_TO_VSC = 0;

        private int iStartKey = 0x46; // F, 0x46
        private byte bBlockKey1;
        private byte bBlockKey2;

        private volatile bool running = false;
        private volatile bool AssigningKey = false;

        private int CPS = 25;

        private int KeyIndex = 0;


        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            main.MouseDown += pMouseDown;
            StyleCorners();
            StyleButton(BlockKey1, BlockKey2, StartKey);

            KeyPreview = true;
            KeyDown += AssignHotkey;
            KeyUp += FinishAssigningHotkey;
            Load += Form1_Load;
            FormClosed += Form1_Close;

            InitTitlebar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSettings();

            Task.Run(Start);
        }

        private void Form1_Close(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void Start()
        {
            timeBeginPeriod(1);
            bool waspressed = false;
            SendInput send = null;

            while (true)
            {
                bool pressed = GetAsyncKeyState(iStartKey) < 0;

                if (pressed && !waspressed && !AssigningKey)
                {
                    running = !running;

                    if (running)
                        send = Macro.GetInput(bBlockKey1, bBlockKey2);
                }

                waspressed = pressed;

                if (!running)
                {
                    Thread.Sleep(5);
                    continue;
                }

                int delay = Macro.GetDelay(CPS);

                if (delay <= 0)
                {
                    Thread.Sleep(5);
                    continue;
                }

                Stopwatch sw = Stopwatch.StartNew();

                Macro.Click();

                Macro.Send(send);

                sw.Stop();

                int rest = delay - (int)sw.ElapsedMilliseconds;

                if (rest > 0)
                    Thread.Sleep(rest);
            }
        }

        private void StyleButton(params Button[] button)
        {
            foreach (Button b in button)
            {
                b.BackColor = Color.FromArgb(25, 28, 35);
                b.ForeColor = Color.White;

                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 1;
                b.FlatAppearance.BorderColor = Color.FromArgb(55, 60, 70);
                b.Size = new Size(100, 30);

                b.Text = "None";

                if (b == StartKey)
                    b.Text = "F";

                b.Cursor = Cursors.Hand;
            }
        }

        private void StyleCorners()
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle bounds = new Rectangle(0, 0, Width, Height);
            int d = 15 * 2;

            path.StartFigure();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            Region = new Region(path);
        }

        private void pMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);
            }
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
                        bBlockKey1 = scancode;
                        BlockKey1.Text = e.KeyCode.ToString();

                        if (scancode == 0x01)
                        {
                            bBlockKey1 = 0;
                            BlockKey1.Text = "None";
                        }

                        break;
                    }
                case 2:
                    {
                        bBlockKey2 = scancode;
                        BlockKey2.Text = e.KeyCode.ToString();

                        if (scancode == 0x01)
                        {
                            bBlockKey2 = 0;
                            BlockKey2.Text = "None";
                        }

                        break;
                    }
                case 3:
                    {
                        iStartKey = (int)e.KeyCode;
                        StartKey.Text = e.KeyCode.ToString();

                        if (e.KeyCode == Keys.Escape)
                        {
                            iStartKey = 0;
                            StartKey.Text = "None";
                        }

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

        private void LoadSettings()
        {
            iStartKey = Properties.Settings.Default.StartKey;
            bBlockKey1 = Properties.Settings.Default.BlockKey1;
            bBlockKey2 = Properties.Settings.Default.BlockKey2;
            CPS = Properties.Settings.Default.CPS;
            StartKey.Text = Properties.Settings.Default.StartKeyText;
            BlockKey1.Text = Properties.Settings.Default.BlockKey1Text;
            BlockKey2.Text = Properties.Settings.Default.BlockKey2Text;
            textBox1.Text = CPS.ToString();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.StartKey = iStartKey;
            Properties.Settings.Default.BlockKey1 = bBlockKey1;
            Properties.Settings.Default.BlockKey2 = bBlockKey2;
            Properties.Settings.Default.CPS = CPS;
            Properties.Settings.Default.BlockKey1Text = BlockKey1.Text;
            Properties.Settings.Default.BlockKey2Text = BlockKey2.Text;
            Properties.Settings.Default.StartKeyText = StartKey.Text;
            Properties.Settings.Default.Save();
        }

        private void BlockKey1_Click(object sender, EventArgs e)
        {
            AssigningKey = true;
            KeyIndex = 1;
            BlockKey1.Text = "Enter Hotkey";
        }

        private void BlockKey2_Click(object sender, EventArgs e)
        {
            AssigningKey = true;
            KeyIndex = 2;
            BlockKey2.Text = "Enter Hotkey";
        }

        private void StartKey_Click(object sender, EventArgs e)
        {
            AssigningKey = true;
            KeyIndex = 3;
            StartKey.Text = "Enter Hotkey";
        }

        private void CpsChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int cps) || cps <= 0)
                return;

            CPS = cps;

            Properties.Settings.Default.CPS = CPS;
            Properties.Settings.Default.Save();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void InitTitlebar()
        {
            Panel Titlebar = new Panel();
            Panel Seperator = new Panel();

            Titlebar.Dock = DockStyle.Top;
            Titlebar.Height = 26;
            Titlebar.BackColor = Color.FromArgb(17, 19, 24);

            Seperator.Dock = DockStyle.Bottom;
            Seperator.BackColor = Color.FromArgb(75, 200, 200, 200);
            Seperator.Height = 1;

            this.Controls.Add(Titlebar);
            Titlebar.MouseDown += pMouseDown;

            Button ExitButton = new Button();
            ExitButton.Click += ExitButton_Click;

            Button MinimizeButton = new Button();
            MinimizeButton.Click += MinimizeButton_Click;

            ExitButton.FlatStyle = FlatStyle.Flat;
            ExitButton.FlatAppearance.BorderSize = 0;

            MinimizeButton.FlatStyle = FlatStyle.Flat;
            MinimizeButton.FlatAppearance.BorderSize = 0;

            ExitButton.Size = new Size(40, Titlebar.Height);
            MinimizeButton.Size = new Size(40, Titlebar.Height);

            ExitButton.Location = new Point(Titlebar.ClientSize.Width - ExitButton.Width, 0);
            MinimizeButton.Location = new Point(ExitButton.Left - MinimizeButton.Width, 0);

            ExitButton.Text = "X";
            MinimizeButton.Text = "--";

            ExitButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MinimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            ExitButton.TextAlign = ContentAlignment.MiddleCenter;
            MinimizeButton.TextAlign = ContentAlignment.MiddleCenter;

            ExitButton.ForeColor = Color.White;
            MinimizeButton.ForeColor = Color.White;

            Titlebar.Controls.Add(Seperator);
            Titlebar.Controls.Add(ExitButton);
            Titlebar.Controls.Add(MinimizeButton);
  
            Titlebar.BringToFront();
        }
    }
}