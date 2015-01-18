using System;
using System.Linq;
using System.Windows.Forms;
using DetectScreens.Properties;
using DetectScreens.Store;
using HelperFramework.PInvoke;

namespace DetectScreens
{

	static class Program
	{
		static Program()
		{
			IsDebug = false;
		}

		public static Boolean IsDebug { get; set; }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(String[] arguments)
		{
			if (arguments.Contains("debug"))
			{
				IsDebug = true;
			}
			HelperFramework.Debug.Console.Log(IsDebug);

			if (!DwmGlass.DwmIsCompositionEnabled() &&
				MessageBox.Show(Resources.Program_Main_CompositionNotEnabled, Resources.Program_Main_CompositionNotEnabled_Title, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			if (Environment.OSVersion.Version.Major < 6 &&
				MessageBox.Show(Resources.Program_Main_OSVistaOrHigher, Resources.Program_Main_OSVistaOrHigher_Title, MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(true);
			Application.Run(new FrmDetectScreens());
		}
	}
}
