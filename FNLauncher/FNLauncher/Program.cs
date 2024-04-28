using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

class Class1
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
        uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    static extern IntPtr CreateRemoteThread(IntPtr hProcess,
        IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    // privileges
    const int PROCESS_CREATE_THREAD = 0x0002;
    const int PROCESS_QUERY_INFORMATION = 0x0400;
    const int PROCESS_VM_OPERATION = 0x0008;
    const int PROCESS_VM_WRITE = 0x0020;
    const int PROCESS_VM_READ = 0x0010;

    // used for memory allocation
    const uint MEM_COMMIT = 0x00001000;
    const uint MEM_RESERVE = 0x00002000;
    const uint PAGE_READWRITE = 4;

    static void Main(string[] args)
    {
        // Display watermark
        DisplayWatermark();

        // Get the directory of the current executable
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Launch FortniteLauncher.exe with specified arguments
        string executablePath = Path.Combine(currentDirectory, "FortniteLauncher.exe");
        string arguments = "-NOSSLPINNING -skippatchcheck -epicportal -HTTP=WinINet";
        Process.Start(executablePath, arguments);

        // Wait for the target process to start and inject the DLL immediately
        string targetProcessName = "FortniteClient-Win64-Shipping";
        InjectDllWhenProcessStarts(targetProcessName, currentDirectory, "Cobalt.dll");
    }

    static void DisplayWatermark()
    {
        Console.WriteLine("Lithium Launcher By NotNotGhosty");
        Console.WriteLine("Join our discord: https://discord.gg/4R8qCwEVhk");
    }

    static void InjectDllWhenProcessStarts(string processName, string currentDirectory, string dllName)
    {
        while (true)
        {
            // Check if the target process is running
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                Process targetProcess = processes[0];

                // Get the handle of the target process - with required privileges
                IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);

                // searching for the address of LoadLibraryA and storing it in a pointer
                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

                // alocating some memory on the target process - enough to store the name of the dll
                // and storing its address in a pointer
                IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);

                // writing the name of the dll there
                UIntPtr bytesWritten;
                WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(dllName), (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);

                // creating a thread that will call LoadLibraryA with allocMemAddress as argument
                CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                break; // Exit the loop after injection
            }

            // Wait for a short duration before checking again
            Thread.Sleep(1000); // Wait for 1 second
        }
    }
}
