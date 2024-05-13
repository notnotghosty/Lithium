# Lithium Launcher

Lithium Launcher is a lightweight C# utility designed to launch applications and inject dynamic link libraries (DLLs) into target processes seamlessly. With Lithium Launcher, you can automate the process of launching applications and injecting DLLs, enhancing their functionality without manual intervention.

## How to Use

1. **Download**: download the Lithium Launcher repository to your local machine.

2. **Configuration**: Edit the source code (`Program.cs`) to specify the applications you want to launch and the DLLs you want to inject into their processes. Modify the `Main` method to define the executable paths, launch arguments, target process names, and DLL names as needed.

3. **Build**: Build the Lithium Launcher project using your preferred C# compiler.

4. **Execution**: Execute the compiled Lithium Launcher executable (`LithiumLauncher.exe`) from the desired location. Ensure that the Lithium Launcher executable, the target applications, and the DLLs to be injected are all placed in the same folder for seamless operation.

## Features

- **Executable Launching**: Launch target applications with custom arguments or parameters directly from the code.
- **DLL Injection**: Inject DLLs into the memory space of running processes for enhanced functionality.
- **Automated Injection**: Automatically inject DLLs into target processes as soon as they start, ensuring seamless integration without user intervention.

### To Do

- **Multiple DLL Injection**: Support Multiple Dlls being injected, with the ability to add a wait time per dll
- **App Tracking**: Tracks the app it starts to make sure the app it's injecting the dll into is the app it started.
