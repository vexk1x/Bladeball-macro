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

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;


        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        private const uint MapVK_to_VSC = 0;

        public bool AssigningKey { get; set; } = false;

        private int KeyIndex = 0;

        private const string Version = "1.1.4";

        private Color Theme;
        private Color BackgroundTheme;

        Macro macro;

        public Form1()
        {
            InitializeComponent();

            macro = new Macro(this);

            FormBorderStyle = FormBorderStyle.None;

            Theme = Color.FromArgb(14, 16, 19);
            BackgroundTheme = Color.FromArgb(7, 9, 10);

            Load += Form1_Load;
            FormClosed += Form1_Close;

            main.MouseDown += pMouseDown;
            main.BackColor = BackgroundTheme;

            StyleCorners();
            StyleButton(BlockKey1, BlockKey2, StartKey);

            KeyPreview = true;
            KeyDown += AssignHotkey;
            KeyUp += FinishAssigningHotkey;

            InitTitlebar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSettings();

            InitTrackBar();

            Thread T = new Thread(macro.Start)
            {
                IsBackground = true,
                Priority = ThreadPriority.Highest,
            }; T.Start();

        }

        private void Form1_Close(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void StyleButton(params Button[] button)
        {
            foreach (Button b in button)
            {
                b.BackColor = Theme;
                b.ForeColor = Color.White;

                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
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

            byte scancode = (byte)MapVirtualKey((uint)e.KeyCode, MapVK_to_VSC);

            switch (KeyIndex)
            {
                case 1:
                    {
                        macro.bBlockKey1 = scancode;
                        BlockKey1.Text = e.KeyCode.ToString();

                        if (scancode == 0x01)
                        {
                            macro.bBlockKey1 = 0;
                            BlockKey1.Text = "None";
                        }

                        break;
                    }
                case 2:
                    {
                        macro.bBlockKey2 = scancode;
                        BlockKey2.Text = e.KeyCode.ToString();

                        if (scancode == 0x01)
                        {
                            macro.bBlockKey2 = 0;
                            BlockKey2.Text = "None";
                        }

                        break;
                    }
                case 3:
                    {
                        macro.iStartKey = (int)e.KeyCode;
                        StartKey.Text = e.KeyCode.ToString();

                        if (e.KeyCode == Keys.Escape)
                        {
                            macro.iStartKey = 0;
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
            Settings.LoadSettings();

            macro.iStartKey = Settings.data.StartKey;
            macro.bBlockKey1 = Settings.data.BlockKey1;
            macro.bBlockKey2 = Settings.data.BlockKey2;
            macro.CPS = Settings.data.CPS;
            StartKey.Text = Settings.data.StartKeyText;
            BlockKey1.Text = Settings.data.BlockKey1Text;
            BlockKey2.Text = Settings.data.BlockKey2Text;
        }

        private void SaveSettings()
        {
            Settings.data.StartKey = macro.iStartKey;
            Settings.data.BlockKey1 = macro.bBlockKey1;
            Settings.data.BlockKey2 = macro.bBlockKey2;
            Settings.data.CPS = macro.CPS;
            Settings.data.StartKeyText = StartKey.Text;
            Settings.data.BlockKey1Text = BlockKey1.Text;
            Settings.data.BlockKey2Text = BlockKey2.Text;

            Settings.SaveSettings();
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

        private void InitTitlebar()
        {
            Panel Titlebar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 26,
                BackColor = BackgroundTheme

            }; Titlebar.MouseDown += pMouseDown; 
            
            this.Controls.Add(Titlebar);

            Panel Seperator = new Panel
            {
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(75, 200, 200, 200),
                Height = 1
            };

            Button ExitButton = new Button
            {
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(75, 200, 25, 25), MouseDownBackColor = Color.FromArgb(75, 200, 25, 25) },
                Size = new Size(40, Titlebar.Height),
                Text = "\u2715",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Right

            };  ExitButton.Click += delegate { Close(); };

            ExitButton.Location = new Point(Titlebar.ClientSize.Width - ExitButton.Width, 0);

            Button MinimizeButton = new Button
            {
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = {BorderSize = 0, MouseOverBackColor = Color.FromArgb(75, 200, 200, 200), MouseDownBackColor = Color.FromArgb(75, 200, 200, 200)},
                Size = new Size(40, Titlebar.Height),
                Text = "\u0C7C",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Right

            }; MinimizeButton.Click += delegate { this.WindowState = FormWindowState.Minimized; };

            MinimizeButton.Location = new Point(ExitButton.Left - MinimizeButton.Width, 0);

            Label name = new Label
            {
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                Size = new Size(40, Titlebar.Height),
                AutoSize = true,
                Text = $"Onyx v{Version}",
            }; 

            name.Location = new Point(name.Width / 2 + 6, 5);

            Titlebar.Controls.Add(Seperator);
            Titlebar.Controls.Add(ExitButton);
            Titlebar.Controls.Add(MinimizeButton);
            Titlebar.Controls.Add(name);
  
            Titlebar.BringToFront();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/vexk1x/Bladeball-macro") { UseShellExecute = true });
        }

        private void InitTrackBar()
        {
            CpsTrackBar.Minimum = 1;
            CpsTrackBar.Maximum = 1000;
            CpsTrackBar.Value = macro.CPS;
            CpsTrackBar.TickStyle = TickStyle.None;

            Label Value = new Label
            {
                ForeColor = Color.White,
                Text = CpsTrackBar.Value.ToString(),
                Size = new Size(40, 20),
                AutoSize = true

            }; 
            
            Value.Location = new Point(CpsTrackBar.Location.X + CpsTrackBar.Width / 2 - Value.Width / 2, CpsTrackBar.Location.Y + 20);

            Button IncrementValue = new Button
            {
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 1, BorderColor = Theme},
                Text = "+1",
                Size = new Size(30, 20)
                

            }; IncrementValue.Click += delegate { if (CpsTrackBar.Value == 1000) return;  CpsTrackBar.Value++; };

            IncrementValue.Location = new Point(label2.Location.X, label2.Location.Y + IncrementValue.Height - 5);

            CpsTrackBar.ValueChanged += delegate
            {
                macro.CPS = CpsTrackBar.Value;
                Value.Text = CpsTrackBar.Value.ToString();

                Settings.data.CPS = CpsTrackBar.Value;
                Settings.SaveSettings();
            };

            CpsTrackBar.Parent.Controls.Add(Value);
            
            Value.BringToFront();

            main.Controls.Add(IncrementValue);
        }
    }
}