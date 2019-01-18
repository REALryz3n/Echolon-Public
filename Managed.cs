namespace External_ESP_Base
{
    using System;
    using System.Runtime.InteropServices;

    internal class Managed
    {
        public static int GWL_STYLE = -16;
        public const int KEY_PRESSED = 0x8000;
        public static uint PAGE_READWRITE = 4;
        public static uint PROCESS_VM_OPERATION = 8;
        public static uint PROCESS_VM_READ = 0x10;
        public static uint PROCESS_VM_WRITE = 0x20;
        public const int VK_DOWN = 40;
        public const int VK_INSERT = 0x2d;
        public const int VK_LBUTTON = 1;
        public const int VK_LEFT = 0x25;
        public const int VK_RBUTTON = 2;
        public const int VK_RIGHT = 0x27;
        public const int VK_UP = 0x26;
        public static uint WS_BORDER = 0x800000;
        public static uint WS_CAPTION = 0xc00000;
        public static uint WS_MAXIMIZE = 0x1000000;
        public static uint WS_VISIBLE = 0x10000000;

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);
        [DllImport("dwmapi.dll")]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);
        [DllImport("user32.dll", SetLastError=true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int KeyStates);
        [DllImport("user32.dll", SetLastError=true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("User32.dll")]
        public static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwAccess, bool inherit, int pid);
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, long lpBaseAddress, [In, Out] byte[] lpBuffer, ulong dwSize, out IntPtr lpNumberOfBytesRead);
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, long lpBaseAddress, [In, Out] byte[] lpBuffer, ulong dwSize, out IntPtr lpNumberOfBytesWritten);
    }
}

