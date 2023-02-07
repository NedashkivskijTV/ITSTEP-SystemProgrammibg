using System;
using System.Runtime.InteropServices;
using System.Text;
using static System.Console;

namespace SimpleProject
{
    ////Var2
    //public unsafe struct OsVersionInfo
    //{
    //    public uint osVersionInfoSize;
    //    public uint majorVersion;
    //    public uint minorVersion;
    //    public uint buildNumber;
    //    public uint platformId;
    //    public fixed byte servicePackVersion[128];
    //}
    ////Var2


    //Отдельный класс для работы с небезопасным кодом библиотек
    public class DllImportExample
    {
        ////Var1
        ////Если значение поля ExactSpelling установить в true,
        ////то поиск функции будет осуществляться по точно совпадающему имени
        ////ANSI
        //[DllImport("User32.dll", ExactSpelling = true)]
        //public static extern int MessageBoxA(IntPtr hWnd, string text, string caption, uint type);

        ////Unicode
        //[DllImport("User32.dll", ExactSpelling = true)]
        //public static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);

        ////Auto
        //[DllImport("User32.dll", ExactSpelling = false)]
        //public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
        ////Var1


        ////Var2
        ////Это поле EntryPoint = "GetVersionEx" позволяет явным образом указать имя вызываемой 
        ////неуправляемой функции, вследствие чего вы можете использовать произвольное имя 
        ////для вашего статического метода
        //[DllImport("Kernel32.dll", EntryPoint = "GetVersionEx")]
        //public static extern bool GetVersion(ref OsVersionInfo versionInfo);
        ////Var2


        ////Var3
        ////При помощи этого поля CharSet можно задать кодировку строк при преобразовании 
        ////неуправляемого кода в управляемый
        //[DllImport("User32.dll", CharSet = CharSet.Auto)]
        //public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        ////Unicode
        //[DllImport("user32.dll", CharSet = CharSet.Unicode)]
        //public static extern int MessageBoxW(IntPtr hWnd, string lpText, string lpCption, uint uType);
        ////Var3


        ////Var4
        ////Параметры этого поля CallingConvention позволяют оптимизировать вызов неуправляемой функции, 
        ////регулируя способы ее вызова, передачи параметров и возврата результатов ее работы
        //[DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int printf(string format, int i, double d);

        //[DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        //public static extern int printf(string format, int i, string s);
        ////Var4


        //Var5
        //Этому полю SetLastError задается значение типа bool, которое предоставляет возможность вызова
        //функции SetLastError() WIN API в случае возникновения ошибки при выполнении управляемого метода,
        //в языке C# значение по умолчанию false
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int MessageBox(IntPtr hwnd, string text, string caption, uint type);
        //Var5
    }

    class Program
    {
        static void Main(string[] args)
        {
            ////Var1
            //DllImportExample.MessageBoxA(IntPtr.Zero,
            //"Тестирование прошло успешно",
            //"MessageBoxA", 0);

            //DllImportExample.MessageBoxW(IntPtr.Zero,
            //"Тестирование прошло успешно",
            //"MessageBoxW", 0);

            //DllImportExample.MessageBox(IntPtr.Zero,
            //"Тестирование прошло успешно",
            //"MessageBox", 0);
            ////Var1


            ////Var2
            //OsVersionInfo versionInfo = new OsVersionInfo();
            //versionInfo.osVersionInfoSize = (uint)Marshal.SizeOf(versionInfo);
            //bool result = DllImportExample.GetVersion(ref versionInfo);
            //if (result)
            //{
            //    WriteLine($"Assembly identifer: {versionInfo.buildNumber}");
            //    WriteLine($"Major Version: {versionInfo.majorVersion}");
            //    WriteLine($"Minor Version: {versionInfo.minorVersion}");
            //    WriteLine($"Platform Id: {versionInfo.platformId}");

            //    // Print the 10 elements of the C-style unmanagedArray
            //    byte[] managedArray = new byte[128];
            //    unsafe { Marshal.Copy((IntPtr)versionInfo.servicePackVersion, managedArray, 0, 128); }
            //    string sp = Encoding.Default.GetString(managedArray);
            //    WriteLine($"Service Pack Version: {sp}");
            //}
            ////Var2


            ////Var3
            //DllImportExample.MessageBox(IntPtr.Zero,
            //"Тестирование прошло успешно",
            //"Test DllImportAttribute CharSet.Auto", 0);

            //DllImportExample.MessageBoxW(IntPtr.Zero,
            //"Тестирование прошло успешно",
            //"Test DllImportAttribute CharSet.Unicode", 0);
            ////Var3


            ////Var4
            //DllImportExample.printf("Print params: % i % f\n", 99, 99.99);
            //DllImportExample.printf("Print params: % i % s\n", 99, "abcd");
            ////Var4


            //Var5
            WriteLine("Вызов Win32 MessageBox без ошибок...");
            DllImportExample.MessageBox(IntPtr.Zero, "Нажми OK...", "Win32 MessageBox", 0);

            int error = Marshal.GetLastWin32Error();
            WriteLine($"Последняя Win32 ошибка была: {error}\n");

            //----------- генерування помилки 

            WriteLine("Вызов Win32 MessageBox с ошибкой...");
            DllImportExample.MessageBox(new IntPtr(123132), "Нажми OK...", "Диалог", 0);

            error = Marshal.GetLastWin32Error();
            WriteLine($"Последняя Win32 ошибка была: {error}\n");
            //Var5
        }
    }
}
