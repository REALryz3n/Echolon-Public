namespace External_ESP_Base
{
    using External_ESP_base;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    internal class Program
    {
        public static void ClearCurrentConsoleLine()
        {
            int cursorTop = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("");
            }
            Console.SetCursorPosition(0, cursorTop);
        }

        public static bool GetProcessesByName(string pName, out Process process)
        {
            Process[] processesByName = Process.GetProcessesByName(pName);
            process = (processesByName.Length != 0) ? processesByName[0] : null;
            return (process > null);
        }

        private static void Init()
        {
            Console.Title = "the big radar";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.Write("waiting fоr rust...");
        }

        [STAThread]
        private static void Main(string[] args)
        {
            Init();
            while (true)
            {
                Process process;
                if (GetProcessesByName("rustclient", out process))
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(new string('-', Console.WindowWidth - 1));
                    Console.WriteLine("Status: Loaded{0}", new string(' ', 15));
                    Console.WriteLine("Id: {0}", process.Id);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(true);
                    Rectangle bounds = Screen.AllScreens[0].Bounds;
                    Form form = new External_ESP_base.Menu();
                    form.SetBounds(bounds.X / 2, 50, form.Width, form.Height);
                    form.StartPosition = FormStartPosition.Manual;
                    Form[] forms = new Form[] { form, new Overlay(process) };
                    Application.Run(new MultiFormContext(forms));
                    break;
                }
                Thread.Sleep(100);
            }
            Console.ReadKey();
        }
    }
}

