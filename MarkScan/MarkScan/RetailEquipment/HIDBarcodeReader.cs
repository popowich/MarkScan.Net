
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkScan.RetailEquipment
{
    /// <summary>
    /// Класс реализует перехват ввода с HID устройств 
    /// </summary>
    public class HIDBarcodeReader : IDisposable
    {
        public string LastReadBarcode { get; set; }

        public bool Enable { get; set; }

        public int LengthBarcode { get; set; }

        public Keys Prefix { get; set; }

        public Keys Suffix { get; set; }

        public event EventHandler<HIDBarcodeReaderEventArgs> ReadDataEvent;

        public HIDBarcodeReader(Keys prefix, Keys suffix, int lengthBarcode)
        {
            LengthBarcode = lengthBarcode;
            Prefix = prefix;
            Suffix = suffix;
        }

        public void Dispose()
        {
            if (ReadDataEvent != null)
                foreach (EventHandler<HIDBarcodeReaderEventArgs> ev in ReadDataEvent.GetInvocationList())
                    ReadDataEvent -= ev;

            StopRead();
        }

        public void OnReadDataEvent(object sender, HIDBarcodeReaderEventArgs e)
        {
            ReadDataEvent?.Invoke(sender, e);
        }

        public void StartRead()
        {
            Enable = true;
            m_callback = lowLevelKeyboardHookProc;
            m_hHook = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback, GetModuleHandle(IntPtr.Zero), 0);
        }

        public void StopRead()
        {
            UnhookWindowsHookEx(m_hHook);
        }

        #region WinAPI

        private char[] bufRead;

        private bool beginReadCode;

        private int currIndexBuf = -1;

        private const int WH_KEYBOARD_LL = 13; //Keyboard low level hook;
        private const int WH_KEYBOARD = 2; //Keyboard hook;

        private IntPtr m_hHook;

        private LowLevelKeyboardProcDelegate m_callback;
        private bool _shiftEnabled;

        //Delegate for using hooks
        private delegate IntPtr LowLevelKeyboardProcDelegate(int nCode, IntPtr wParam, IntPtr lParam);

        //Keys data structure
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys vkCode;
            public uint scanCode;
            public KBDLLHOOKSTRUCTFlags flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [Flags]
        private enum KBDLLHOOKSTRUCTFlags : uint
        {
            LLKHF_EXTENDED = 0x01,
            LLKHF_INJECTED = 0x10,
            LLKHF_ALTDOWN = 0x20,
            LLKHF_UP = 0x80,
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int ToAscii(Keys uVirtKey, uint uScanCode, [In, Optional] byte[] lpKeyState, out char lpChar, uint uFlags);

        [DllImport("user32.dll")]
        public static extern int ToUnicode(Keys virtualKeyCode, uint scanCode, byte[] keyboardState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder receivingBuffer,
            int bufferSize, uint flags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        private IntPtr lowLevelKeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //wParam = 256 - KeyDown
            //wParam = 257 - KeyUp

            // If not alredy captured
            if (!Enable || nCode < 0 || wParam != (IntPtr)256)
                return CallNextHookEx(m_hHook, nCode, wParam, lParam); //Go to next hook

            //Memory allocation and importing code data to KBDLLHOOKSTRUCT
            var objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

            // Старт начала считывания кода
            if (objKeyInfo.vkCode == Prefix)
            {
                beginReadCode = true;
                currIndexBuf = 0;

                bufRead = LengthBarcode > 0 ? new char[LengthBarcode] : new char[1024];

                return (IntPtr)1;
            }

            // Окончание считывания кода 
            if (objKeyInfo.vkCode == Suffix && beginReadCode)
            {
                var code = string.Concat(bufRead).Trim('\0');

                beginReadCode = false;

                bufRead = null;
                currIndexBuf = -1;

                if (code.Length != LengthBarcode && LengthBarcode != 0) return (IntPtr)1;

                LastReadBarcode = code;
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(5);
                    OnReadDataEvent(this, new HIDBarcodeReaderEventArgs(code));
                });

                return (IntPtr)1;
            }

            // Считывание очередного символа кода
            if (beginReadCode)
            {
                if (currIndexBuf < LengthBarcode || LengthBarcode == 0)
                {
                    if (objKeyInfo.vkCode == Keys.ShiftKey
                        || objKeyInfo.vkCode == Keys.LShiftKey
                        || objKeyInfo.vkCode == Keys.RShiftKey)
                    {
                        _shiftEnabled = true;
                        return (IntPtr)1;
                    }

                    var keyState = new byte[256];
                    if (_shiftEnabled)
                    {
                        keyState[(int)Keys.ShiftKey] = 0xff;
                        _shiftEnabled = false;
                    }

                    var buf = new StringBuilder(256);

                    ToUnicode(objKeyInfo.vkCode, objKeyInfo.scanCode, keyState, buf, 256, 0);

                    if (buf.Length > 0)
                    {
                        bufRead[currIndexBuf] = buf[0];
                        currIndexBuf++;
                    }

                    return (IntPtr)1;
                }

                beginReadCode = false;
                bufRead = null;
                currIndexBuf = -1;
            }
            else if (beginReadCode)
            {
                beginReadCode = false;
                bufRead = null;
                currIndexBuf = -1;
            }

            return CallNextHookEx(m_hHook, nCode, wParam, lParam); //Go to next hook
        }

        private static char ConvertKeyToChar(Keys key)
        {
            switch (key)
            {
                case Keys.D0:
                    return '0';
                case Keys.D1:
                    return '1';
                case Keys.D2:
                    return '2';
                case Keys.D3:
                    return '3';
                case Keys.D4:
                    return '4';
                case Keys.D5:
                    return '5';
                case Keys.D6:
                    return '6';
                case Keys.D7:
                    return '7';
                case Keys.D8:
                    return '8';
                case Keys.D9:
                    return '9';
            }
            return ' ';
        }

        #endregion
    }
}
