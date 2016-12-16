using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsers
{
    public class ProcessEx : Process
    {
        /// <summary>
        /// Starts a process resource by specifying the working directory, the name of an application and a
        /// set of command-line arguments, and returns the standard output of the process.
        /// </summary>
        /// <param name="workingDirectory">Working directory of the process.</param>
        /// <param name="filename">The name of an application file to run in the process.</param>
        /// <param name="arguments">Command-line arguments to pass when starting the process.</param>
        /// <exception cref="System.InvalidOperationException">The fileName or arguments parameter is null.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// An error occurred when opening the associated file. -or-The sum of the length
        /// of the arguments and the length of the full path to the process exceeds 2080.
        /// The error message associated with this exception can be one of the following:
        /// "The data area passed to a system call is too small." or "Access is denied."
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">The process object has already been disposed.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
        /// <returns>The standard output string of the process.</returns>
        public static string Execute(string workingDirectory, string filename, string arguments, int timeOut = -1)
        {
            Process process = new Process();
            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            if (timeOut != -1)
            {
                if (!process.WaitForExit(timeOut))
                {
                    process.Kill();
                    return "Time Limit Exceed!";
                }
            }
            else
            {
                process.WaitForExit();
            }

            return process.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// Starts a process resource by specifying the working directory, the name of an application and a
        /// set of command-line arguments.
        /// </summary>
        /// <param name="workingDirectory">Working directory of the process.</param>
        /// <param name="filename">The name of an application file to run in the process.</param>
        /// <param name="arguments">Command-line arguments to pass when starting the process.</param>
        /// <exception cref="System.InvalidOperationException">The fileName or arguments parameter is null.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// An error occurred when opening the associated file. -or-The sum of the length
        /// of the arguments and the length of the full path to the process exceeds 2080.
        /// The error message associated with this exception can be one of the following:
        /// "The data area passed to a system call is too small." or "Access is denied."
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">The process object has already been disposed.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
        /// <returns>The exit code of the process</returns>
        public static int ShellExecute(string workingDirectory, string filename, string arguments)
        {
            Process process = new Process();
            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }

        /// <summary>
        /// Starts a process resource by specifying the working directory, the name of an application and a
        /// set of command-line arguments.
        /// </summary>
        /// <param name="workingDirectory">Working directory of the process.</param>
        /// <param name="filename">The name of an application file to run in the process.</param>
        /// <param name="arguments">Command-line arguments to pass when starting the process.</param>
        /// <exception cref="System.InvalidOperationException">The fileName or arguments parameter is null.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// An error occurred when opening the associated file. -or-The sum of the length
        /// of the arguments and the length of the full path to the process exceeds 2080.
        /// The error message associated with this exception can be one of the following:
        /// "The data area passed to a system call is too small." or "Access is denied."
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">The process object has already been disposed.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
        /// <returns>The exit code of the process</returns>
        public static int ShellExecute(string workingDirectory, string filename, params string[] arguments)
        {
            // TODO: Use CmdLineHelper.ArgvToCommandLine instead of the code below.
            // string quotedArguments = Util.CmdLineHelper.ArgvToCommandLine(arguments);

            string quotedArguments = "";

            for (int i = 0; i < arguments.Length; i++)
            {
                if (!arguments[i].StartsWith("\"") && arguments[i].Contains(" "))
                {
                    quotedArguments += "\"" + arguments[i] + "\" ";
                }
                else
                {
                    quotedArguments += arguments[i] + " ";
                }
            }

            Process process = new Process();
            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = quotedArguments;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }

        /// <summary>
        /// Starts a process resource by specifying the working directory, the name of an application and a
        /// set of command-line arguments, and returns the standard output of the process.
        /// </summary>
        /// <param name="workingDirectory">Working directory of the process.</param>
        /// <param name="filename">The name of an application file to run in the process.</param>
        /// <param name="arguments">Command-line arguments to pass when starting the process.</param>
        /// <exception cref="System.InvalidOperationException">The fileName or arguments parameter is null.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// An error occurred when opening the associated file. -or-The sum of the length
        /// of the arguments and the length of the full path to the process exceeds 2080.
        /// The error message associated with this exception can be one of the following:
        /// "The data area passed to a system call is too small." or "Access is denied."
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">The process object has already been disposed.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
        /// <returns></returns>
        public static void JustStart(string workingDirectory, string filename, string a, params string[] arguments)
        {
            using (Process process = new Process())
            {
                string quotedArguments = "";

                for (int i = 0; i < arguments.Length; i++)
                {
                    if (!arguments[i].StartsWith("\"") && arguments[i].Contains(" "))
                    {
                        quotedArguments += "\"" + arguments[i] + "\" ";
                    }
                    else
                    {
                        quotedArguments += arguments[i] + " ";
                    }
                }
                process.StartInfo.FileName = filename;
                process.StartInfo.Arguments = quotedArguments;
                process.StartInfo.WorkingDirectory = workingDirectory;
                process.StartInfo.UseShellExecute = false;
                process.Start();
            }
        }

        /// <summary>
        /// Starts a process resource by specifying the working directory, the name of an application and a
        /// set of command-line arguments, and returns the standard output of the process.
        /// </summary>
        /// <param name="workingDirectory">Working directory of the process.</param>
        /// <param name="filename">The name of an application file to run in the process.</param>
        /// <param name="arguments">Command-line arguments to pass when starting the process.</param>
        /// <exception cref="System.InvalidOperationException">The fileName or arguments parameter is null.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// An error occurred when opening the associated file. -or-The sum of the length
        /// of the arguments and the length of the full path to the process exceeds 2080.
        /// The error message associated with this exception can be one of the following:
        /// "The data area passed to a system call is too small." or "Access is denied."
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">The process object has already been disposed.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
        /// <returns>The standard output string of the process.</returns>
        public static string Execute(string workingDirectory, string filename, int timeOut = -1, params string[] arguments)
        {
            string quotedArguments = "";

            for (int i = 0; i < arguments.Length; i++)
            {
                if (!arguments[i].StartsWith("\"") && arguments[i].Contains(" "))
                {
                    quotedArguments += "\"" + arguments[i] + "\" ";
                }
                else
                {
                    quotedArguments += arguments[i] + " ";
                }
            }
            Process process = new Process();
            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = quotedArguments;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            if (timeOut != -1)
            {
                if (!process.WaitForExit(timeOut))
                {
                    process.Kill();
                    return "Time Limit Exceed!";
                }
            }
            else
            {
                process.WaitForExit();
            }

            return process.StandardOutput.ReadToEnd();
        }
    }
}
