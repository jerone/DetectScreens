using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DetectScreens.Factory
{
	internal static class GraphicsExtension
	{
		private static GraphicsPath DrawText(String text, Point position, Font font)
		{
			GraphicsPath textPath = new GraphicsPath(FillMode.Winding);
			textPath.AddString(text, font.FontFamily, (int)font.Style, (float)font.Size, new Point(0, 0), StringFormat.GenericDefault);

			Rectangle textBounds = Rectangle.Round(textPath.GetBounds());

			Matrix translateMatrix = new Matrix();
			translateMatrix.Translate(  // move text to center (there's a weird offset from point 0,0);
				(-textBounds.X - (textBounds.Width / 2)) + position.X,
				(-textBounds.Y - (textBounds.Height / 2)) + position.Y);

			textPath.Transform(translateMatrix);

			return textPath;
		}
		public static void DrawText(this Graphics g, String text, Point position, Font font, Color color)
		{
			GraphicsPath textPath = DrawText(text, position, font);

			g.FillPath(new SolidBrush(color), textPath);  // fill text;
		}
		public static void DrawText(this Graphics g, String text, Point position, Font font, Color color, Pen border)
		{
			GraphicsPath textPath = DrawText(text, position, font);

			g.FillPath(new SolidBrush(color), textPath);  // fill text;

			g.DrawPath(border, textPath);  // draw text border;
		}
	}

	/// <summary>
	/// Class to extend .NET functionality;
	/// </summary>
	public static class Utilities
	{
		/// <summary>
		/// Divides a rectangle with an value;
		/// </summary>
		/// <param name="source">The rectangle;</param>
		/// <param name="divider">The divider;</param>
		/// <returns>A new rectangle divided by the <paramref name="divider"/>;</returns>
		public static Rectangle Divide(Rectangle source, Int32 divider)
		{
			return new Rectangle(source.Left / divider, source.Top / divider, source.Width / divider, source.Height / divider);
		}

		/// <summary>
		/// Checks of current window is already running;
		/// </summary>
		/// <param name="windowTitle">Window title</param>
		/// <returns>true if single instance, false if already running</returns>
		public static Boolean IsSingleInstance(String windowTitle)
		{
			foreach (Process process in Process.GetProcesses())
			{
				if (process.MainWindowTitle == windowTitle)
				{
					return false;
				}
			}
			return true;
		}

	}
}
