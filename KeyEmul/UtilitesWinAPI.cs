using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseIO
{
    /// <summary>
    /// Утилиты обращения к winAPI
    /// </summary>
    public class UtilitesWinAPI
    {
        /// <summary>
        /// Установить блокировку клавиш
        /// </summary>
        public static void SetLockKeyDown(Keys _key)
        {
            keyLock = _key;
            //Hooks callbacks by delegate
            m_callback = LowLevelKeyboardHookProc_alt_tab;

            //Hooks setting
            m_hHook = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback, GetModuleHandle(IntPtr.Zero), 0);
        }
        /// <summary>
        /// Отключить блокировку клавиш
        /// </summary>
        public static void SetUnKeyDown()
        {
            UnhookWindowsHookEx(m_hHook);
        }
        /// <summary>
        /// Эмулировать нажатие клавишь
        /// </summary>
        /// <param name="_key"></param>
        public static void EmulatePressKey(System.Windows.Forms.Keys _key)
        {
            keybd_event((byte)_key, 0x45, KEYEVENTF_EXTENDEDKEY | 0, (IntPtr)0);
            System.Threading.Thread.Sleep(10);
            keybd_event((byte)_key, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (IntPtr)0);
        }

        /// <summary>
        /// Конвертировать в символ
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public static char ConvertKeyToChar(System.Windows.Forms.Keys _key)
        {
            if (_key == System.Windows.Forms.Keys.D0)
                return '0';
            else if (_key == System.Windows.Forms.Keys.D1)
                return '1';
            else if (_key == System.Windows.Forms.Keys.D2)
                return '2';
            else if (_key == System.Windows.Forms.Keys.D3)
                return '3';
            else if (_key == System.Windows.Forms.Keys.D4)
                return '4';
            else if (_key == System.Windows.Forms.Keys.D5)
                return '5';
            else if (_key == System.Windows.Forms.Keys.D6)
                return '6';
            else if (_key == System.Windows.Forms.Keys.D7)
                return '7';
            else if (_key == System.Windows.Forms.Keys.D8)
                return '8';
            else if (_key == System.Windows.Forms.Keys.D9)
                return '9';
            else
                return ' ';
        }
        /// <summary>
        /// Конвертировать char в Keys
        /// </summary>
        /// <param name="_char"></param>
        /// <returns></returns>
        public static System.Windows.Forms.Keys ConvertCharToKey(char _char)
        {
            if (_char == '0')
                return System.Windows.Forms.Keys.D0;
            else if (_char == '1')
                return System.Windows.Forms.Keys.D1;
            else if (_char == '2')
                return System.Windows.Forms.Keys.D2;
            else if (_char == '3')
                return System.Windows.Forms.Keys.D3;
            else if (_char == '4')
                return System.Windows.Forms.Keys.D4;
            else if (_char == '5')
                return System.Windows.Forms.Keys.D5;
            else if (_char == '6')
                return System.Windows.Forms.Keys.D6;
            else if (_char == '7')
                return System.Windows.Forms.Keys.D7;
            else if (_char == '8')
                return System.Windows.Forms.Keys.D8;
            else if (_char == '9')
                return System.Windows.Forms.Keys.D9;
            else if (_char >= 'A' && _char <= 'Z')
            {
                var values = Enum.GetValues(typeof(System.Windows.Forms.Keys));

                return (System.Windows.Forms.Keys)values.GetValue(((int)_char) - 4);
            }
            else
                return System.Windows.Forms.Keys.None;
        }

        #region Private methods

        /// <summary>
        /// Блокируемая кнопка
        /// </summary>
        private static Keys keyLock = Keys.None;
        //Delegate for using hooks
        private delegate IntPtr LowLevelKeyboardProcDelegate(int nCode, IntPtr wParam, IntPtr lParam);
        //<Alt>+<Tab> blocking
        private static IntPtr LowLevelKeyboardHookProc_alt_tab(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)//If not alredy captured
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));//Memory allocation and importing code data to KBDLLHOOKSTRUCT
                if (objKeyInfo.key == keyLock)
                {
                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(m_hHook, nCode, wParam, lParam);//Go to next hook
        }

        #endregion

        #region WinAPI

        //Using hooks
        private static IntPtr m_hHook;
        //Using callbacks
        private static LowLevelKeyboardProcDelegate m_callback;
        private const int WH_KEYBOARD_LL = 13;//Keyboard hook;
        private const UInt32 KEYEVENTF_EXTENDEDKEY = 1;
        private const UInt32 KEYEVENTF_KEYUP = 2;

        //Keys data structure
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
        }
  

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, IntPtr hMod, int dwThreadId);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);
        [DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern void keybd_event(byte bVk, byte bScan, UInt32 dwFlags, IntPtr dwExtraInfo);

        #endregion
    }
}
