using System;
using System.Diagnostics;
using System.Linq;

namespace HelperFramework
{
	/// <summary>
	/// Class to extend .NET functionality;
	/// </summary>
	public static class Utilities
	{
		/// <summary>
		/// Checks of current window is already running;
		/// </summary>
		/// <param name="windowTitle">Window title</param>
		/// <returns>true if single instance, false if already running</returns>
		public static Boolean IsSingleInstance(String windowTitle)
		{
			return Process.GetProcesses().All(process => process.MainWindowTitle != windowTitle);
		}
	}
}
