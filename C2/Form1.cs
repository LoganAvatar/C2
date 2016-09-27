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

namespace C2
{
    
    public partial class Form1 : Form
    {
        private static Win32Trash.INPUT[] _junkForMouseInput = CreateMouseInput();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 6000;
            timer1.Enabled = true;
        }

        private static Win32Trash.INPUT[] CreateMouseInput()
        {
            var i = new Win32Trash.INPUT()
            {
                dwType = Win32Trash.InputType.INPUT_MOUSE,
                mkhi = new Win32Trash.MOUSEKEYBDHARDWAREINPUT()
                {
                    mi = new Win32Trash.MOUSEINPUT()
                    {
                        dx = 0,
                        dy = 0,
                        mouseData = 0,
                        dwFlags = Win32Trash.MouseEventFlags.MOVE,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };
            return new Win32Trash.INPUT[] { i };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            Win32Trash.SendInput(1, _junkForMouseInput, Marshal.SizeOf(_junkForMouseInput[0]));


            //Win32.POINT p = new Win32.POINT();
            //p.x = Cursor.Position.X + 10;
            //p.y = Cursor.Position.Y + 10;
            ////p.x = 0;
            ////p.y = 0;
            ////p.x = Convert.ToInt16(txtMouseX.Text);
            ////p.y = Convert.ToInt16(txtMouseY.Text);

            ////Win32.ClientToScreen(this.Handle, ref p);
            //Win32.SetCursorPos(p.x, p.y);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }

    public class Win32
    {
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }
    }

    class Win32Trash
    {
        [DllImport("User32")]
        public static extern int SetForegroundWindow(IntPtr hwnd);

        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        [DllImport("User32.dll")]
        public static extern uint SendInput(uint numberOfInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] input, int structSize);

        public enum InputType
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1,
            INPUT_HARDWARE = 2
        };

        public enum MouseEventFlags
        {
            MOVE = 0x00000001,
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            RIGHTDOWN = 0x00000008,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            ABSOLUTE = 0x00008000,
            RIGHTUP = 0x00000010
        };

        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public MouseEventFlags dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        };

        public struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        };

        public struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        };

        public struct INPUT
        {
            public InputType dwType;
            public MOUSEKEYBDHARDWAREINPUT mkhi;
        };

    }

}
