using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace HelperFramework.Extension
{
	public static class GraphicExtension
	{
		private static GraphicsPath DrawText(String text, Point position, Font font)
		{
			GraphicsPath textPath = new GraphicsPath(FillMode.Winding);
			textPath.AddString(text, font.FontFamily, (int)font.Style, font.Size, new Point(0, 0), StringFormat.GenericDefault);

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
}