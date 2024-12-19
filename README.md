# Lithium Launcher

Lithium Launcher is a lightweight C# utility designed to launch applications and inject dynamic link libraries (DLLs) into target processes seamlessly. With Lithium Launcher, you can automate the process of launching applications and injecting DLLs, enhancing their functionality without manual intervention.

## How to Use

1. **Download**: download Lithium Launcher project files to your local machine.

2. **Configuration**: Edit the source code (`Program.cs`) to specify the applications you want to launch and the DLLs you want to inject into their processes. Modify the `Main` method to define the executable paths, launch arguments, target process names, and DLL names as needed. (if you're using Fortnite, cobalt redirect, and you have already installed Lawin's Fortnite.exe from his discord then no modifcations are needed.)

3. **Build**: Build Lithium Launcher project using your preferred C# compiler (like VS Studio).

4. **Execution**: Execute compiled Launcher executable (`LithiumLauncher.exe`) from the desired location (ie: Fortnite). Ensure that Lithium's executable, the target applications, and the DLLs to be injected are all placed in the same folder for the program to function properly.

## Features

- **Executable Launching**: Launch target applications with custom arguments or parameters directly from the code.
- **DLL Injection**: Inject DLLs into the memory space of running processes for enhanced functionality.
- **Automated Injection**: Automatically inject DLLs into target processes as soon as they start, ensuring seamless integration without user intervention.

### To Do

- **Multiple DLL Injection**: Support Multiple DLL injection, with the ability to add wait time per dll. (I did actually have this working for a minute, but I broke it when I tried to add App Tracking and lost the working version)
- **App Tracking**: Tracks the app started by Lithium to make sure the app it's injecting the dlls into is the app it started.

### Lithium is designed with the intention to be used with OG Fortnite.
