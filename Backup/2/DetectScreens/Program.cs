using System;
using System.Linq;
using System.Windows.Forms;

using DetectScreens.Controls;
using DetectScreens.Factory;
using DetectScreens.Properties;
using DetectScreens.Store;


namespace DetectScreens
{
	static class Program
	{
		private static Boolean debug = false;
		public static Boolean isDebug
		{
			get
			{
				return debug;
			}
			set
			{
				debug = value;
			}
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(String[] arguments)
		{
			if (arguments.Contains("debug"))
			{
				isDebug = true;
			}

			if (!Win32.DwmIsCompositionEnabled() &&
				MessageBox.Show("This program needs Aero Glass, but it seems to be disabled. Click 'OK' to still open the program." +
								"\n\nWarning: crashes may occur. Try at your own risk.", "Aero Glass disabled", MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			if (Environment.OSVersion.Version.Major < 6 &&
				MessageBox.Show("Your Operating System is not supported by this program. Try it on Windows Vista, Windows 7 and higher. Click 'OK' to still open the program." +
								"\n\nWarning: crashes may occur. Try at your own risk.", "OS not supported", MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(true);
			Application.Run(new frm_screens());
		}
	}
}
