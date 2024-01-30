using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        int score = 0;
        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set form properties
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;

            // Make the form click-through
            SetWindowExClickThrough();

        }

        private void SetWindowExClickThrough()
        {
            // Set the window style to WS_EX_LAYERED and WS_EX_TRANSPARENT
            int extendedStyle = (int)GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, extendedStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT);

            // Set the form as layered and make it click-through
            SetLayeredWindowAttributes(this.Handle, 0, 255, LWA_ALPHA);
        }

        // Constants for window styles and attributes
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int LWA_ALPHA = 0x2;

        // Import necessary methods from User32.dll
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out Point lpPoint);

        private void Form1_Load(object sender, EventArgs e)
        {
            BackColor = Color.Blue;
            TransparencyKey = Color.Blue;
            // Get the primary screen
            Screen screen = Screen.PrimaryScreen;

            // Set the form size to the screen size
            Width = screen.WorkingArea.Width;
            Height = screen.WorkingArea.Height;
            this.Location = new Point(0, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }



        private void timer1_Tick(object sender, EventArgs e)
        {

            Point cursorPosition;

            if (GetCursorPos(out cursorPosition))
            {

                Point finalPosition = cursorPosition;
                finalPosition.X -= panel1.Width / 2;
                finalPosition.Y -= panel1.Height / 2;
                this.panel1.Location = finalPosition;
            }
            else
            {
                Console.WriteLine("Failed to get mouse position");
            }

            if (Math.Abs(panel1.Location.X - panel2.Location.X) < 60 && Math.Abs(panel1.Location.Y - panel2.Location.Y) < 60)
            {
                Point newLocation = new Point(random.Next(Width), random.Next(Height));
                panel2.Location = newLocation;
                score++;
                label1.Text = score.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
