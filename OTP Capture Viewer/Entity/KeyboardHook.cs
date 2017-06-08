using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    public delegate int HookProc(int iCode, int wParam, IntPtr lParam);

    public class KeyboardHook
    {
        private bool keyboardHooked = false;
        public int lastErrorCode = 0;
        private int keyboardHook = 0;

        public event KeyEventHandler OnKeyPressActivity;

        private static HookProc keyBoardHookProcedure;
        private Keys[] hookedKeys;

        public const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;

        #region COM Methods for Keyboard Hook

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int iThreadID);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        #endregion


        public KeyboardHook(Keys[] hookedKeys)
        {
            HookKeyboard(hookedKeys);
        }

        ~KeyboardHook()
        {
            this.UnhookKeyboard(true);
        }

        private bool UnhookKeyboard(bool bLogError)
        {
            if (this.keyboardHook != 0)
            {
                if (UnhookWindowsHookEx(this.keyboardHook))
                {
                    this.keyboardHook = 0;
                    this.keyboardHooked = false;
                }
                else if (bLogError)
                {
                    this.lastErrorCode = Marshal.GetLastWin32Error();
                }
            }
            return this.keyboardHooked;
        }

        private bool HookKeyboard(Keys[] hookedKeys)
        {
            //if (this.keyboardHook == 0)
            //{
            keyBoardHookProcedure = new HookProc(this.KeyBoardHookProc);
            this.hookedKeys = hookedKeys;

            //keyboardHook = SetWindowsHookEx(2, keyBoardHookProcedure,GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), GetCurrentThreadId());
            keyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, keyBoardHookProcedure, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);

            if (this.keyboardHook == 0)
            {
                this.keyboardHooked = false;
                this.lastErrorCode = Marshal.GetLastWin32Error();
                this.UnhookKeyboard(false);
            }
            else
            {
                this.keyboardHooked = true;
            }
            //}
            return this.keyboardHooked;
        }

        private int KeyBoardHookProc(int iCode, int wParam, IntPtr lParam)
        {
            if (iCode >= 0)
            {
                KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
                Keys key = (Keys)kbh.vkCode;

                foreach (Keys k in hookedKeys)
                {
                    if (key == k)
                    {
                        KeyEventArgs arg = new KeyEventArgs(key);

                        if (key == Keys.Oemcomma || key == Keys.OemPeriod)
                        {
                            if (kbh.flags == 32 && OnKeyPressActivity != null)
                            {
                                OnKeyPressActivity(this, arg);
                                return 1;
                            }
                            else
                            {
                                return CallNextHookEx(this.keyboardHook, iCode, wParam, lParam);
                            }
                        }
                        else if (OnKeyPressActivity != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                        {
                            OnKeyPressActivity(this, arg);
                            return 1;
                        }
                        else
                        {
                            return CallNextHookEx(this.keyboardHook, iCode, wParam, lParam);
                        }
                    }
                }

            }

            return CallNextHookEx(this.keyboardHook, iCode, wParam, lParam);

        }

        [StructLayout(LayoutKind.Sequential)]
        public class KeyBoardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

    }
}
