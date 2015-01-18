using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DisplayWindowManager.Properties;
using HelperFramework.Drawing;
using HelperFramework.PInvoke;
using Math = System.Math;

namespace DisplayWindowManager.Factory
{

	/// <summary>
	/// Class for a computer display;
	/// </summary>
	public class Display : IDisposable
	{

		#region Fields & Properties;

		private Boolean _disposed;

		/// <summary>
		/// Display left;
		/// </summary>
		public Int32 Left { get; set; }

		/// <summary>
		/// Display right;
		/// </summary>
		public Int32 Right { get; set; }

		/// <summary>
		/// Display top;
		/// </summary>
		public Int32 Top { get; set; }

		/// <summary>
		/// Display bottom;
		/// </summary>
		public Int32 Bottom { get; set; }

		private Int32 _width;
		/// <summary>
		/// Display width;
		/// </summary>
		public Int32 Width
		{
			get
			{
				return _width = Right - Left;
			}
			set
			{
				_width = value;
				Right = Left + _width;
			}
		}

		private Int32 _height;
		/// <summary>
		/// Display height;
		/// </summary>
		public Int32 Height
		{
			get
			{
				return _height = Bottom - Top;
			}
			set
			{
				_height = value;
				Bottom = Top + _height;
			}
		}

		private Rectangle _bounds;
		/// <summary>
		/// Display bounds;
		/// </summary>
		public Rectangle Bounds
		{
			get
			{
				return _bounds = new Rectangle(Left, Top, Width, Height);
			}
			set
			{
				_bounds = value;
				Left = _bounds.Left;
				Top = _bounds.Top;
				Width = _bounds.Width;
				Height = _bounds.Height;
			}
		}

		public ScaledBound ScaledBounds { get; set; }

		/// <summary>
		/// Display number;
		/// </summary>
		public Int32 Identifier { get; set; }

		/// <summary>
		/// Main Display;
		/// </summary>
		public Boolean Primairy { get; set; }

		/// <summary>
		/// List of all windows;
		/// </summary>
		public Windows Windows { get; set; }

		#endregion Fields & Properties;


		#region Constructor;

		/// <summary>
		/// Make a computer display;
		/// </summary>
		public Display()
		{
			Top = 0;
			Left = 0;
			Identifier = 1;
			Bottom = 0;
			Right = 0;
			Primairy = false;
			ScaledBounds = new ScaledBound();
		}

		#endregion Constructor;


		#region Methods;

		/// <summary>
		/// Determains if the display contains a window;
		/// </summary>
		/// <param name="window">The window to check for;</param>
		/// <returns>true when the display contains that window. Otherwise false;</returns>
		public Boolean HasWindow(Window window)
		{
			return Bounds.IntersectsWith(window.Bounds);
		}

		#endregion Methods;


		#region Override;

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override String ToString()
		{
			return String.Format(
@"Display {0}:
	Top: {1}
	Right: {2}
	Bottom: {3}
	Left: {4}
	Width: {5}
	Height: {6}
	Primairy: {7}",
				Identifier, Top, Right, Bottom, Left, Width, Height, Primairy);
		}

		#endregion Override;


		#region Interface;

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(Boolean disposing)
		{
			if (!_disposed)
			{
				if (disposing) { }

				Bounds = Rectangle.Empty;
				Left = Right = Top = Bottom = Width = Height = 0;
				Identifier = 0;
				Primairy = false;
				if (Windows != null && Windows.Count > 0)
				{
					Windows.Dispose();
				}

				_disposed = true;
			}
		}

		/// <summary>
		/// Allows an <see cref="T:System.Object"/> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object"/> is reclaimed by garbage collection.
		/// </summary>
		~Display()
		{
			Dispose(false);
		}

		#endregion Interface;


		#region SubClass;

		public class ScaledBound
		{
			public Rectangle Inside;
			public Rectangle Outside;
		}

		#endregion SubClass;

	}

	/// <summary>
	/// List of all display's;
	/// </summary>
	public class Displays : List<Display>, IDisposable, ICloneable
	{

		#region Fields & Properties;

		private Boolean _disposed;
		private readonly SolidBrush _whiteCircleBorder = new SolidBrush(Color.FromArgb(195, 248, 248, 255));
		private readonly SolidBrush _blueCircleMiddle = new SolidBrush(Color.FromArgb(137, 0, 0, 122));

		private readonly DisplayWindows _windows = new DisplayWindows();

		/// <summary>
		/// Scale of the displays;
		/// </summary>
		public Double Scale { get; private set; }

		/// <summary>
		/// The area where we can paint all displays;
		/// </summary>
		public Size PaintArea { get; set; }

		/// <summary>
		/// Display opacity;
		/// </summary>
		public Byte Opacity { get; set; }

		/// <summary>
		/// Display offset;
		/// </summary>
		public Point Border { get; set; }

		/// <summary>
		/// Handler to form;
		/// </summary>
		/// <remarks>Used in Dwm Thunmbnails</remarks>
		public IntPtr Handler { get; set; }

		public Color Transparent { get; set; }

		private Graphics _graphics;
		/// <summary>
		/// Paint graphics;
		/// </summary>
		public Graphics Graphics
		{
			get { return _graphics; }
			set
			{
				_graphics = value;
				_graphics.Clear(Color.Transparent);  // Make transparent;
				_graphics.SmoothingMode = SmoothingMode.AntiAlias;  // smoother borders;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public DisplayWindows Windows
		{
			get
			{
				_windows.WindowsList = this;
				return _windows;
			}
		}

		#endregion Fields & Properties;


		#region Constructor;

		/// <summary>
		/// List of Displays;
		/// </summary>
		public Displays()
			: this(false, true, true) { }
		/// <summary>
		/// List of all Displays;
		/// </summary>
		/// <param name="noWindows">Displays with or without Windows;</param>
		public Displays(Boolean noWindows)
			: this(noWindows, true, true) { }
		/// <summary>
		/// List of all Displays;
		/// </summary>
		/// <param name="addDesktop">Add desktop too</param>
		/// <param name="addTaskbar">Add taskbar too</param>
		public Displays(Boolean addDesktop, Boolean addTaskbar)
			: this(false, addDesktop, addTaskbar) { }
		private Displays(Boolean noWindows, Boolean addDesktop, Boolean addTaskbar)
		{
			Border = new Point(0, 0);
			Opacity = Byte.MaxValue;
			Scale = 1;

			foreach (Screen screen in Screen.AllScreens)
			{
				Display display = new Display
				{
					Top = screen.Bounds.Top,
					Right = screen.Bounds.Right,
					Bottom = screen.Bounds.Bottom,
					Left = screen.Bounds.Left,
					Identifier = Count + 1,
					Primairy = screen.Primary
				};

				if (!noWindows)
				{
					display.Windows = new Windows(addDesktop, addTaskbar);
				}

				Add(display);
			}
		}

		#endregion Constructor;


		#region Methods;

		/// <summary>
		/// Paint all;
		/// </summary>
		public void Paint()
		{
			Paint(true, true);
		}
		public void Paint(Boolean paintBorders, Boolean paintWindows)
		{
#if NOSHELL
			paintWindows = false;
#endif

			// Create a copy to iterate over;
			Displays displays = Clone() as Displays;
			if (displays == null) return;

			Rectangle workingArea = WorkingArea();

			Int32 borderCountX = displays.OrderBy(__display => __display.Left).Select(__display => __display.Left).Distinct().Count() * 2;
			Int32 borderCountY = displays.OrderBy(__display => __display.Top).Select(__display => __display.Top).Distinct().Count() * 2;
			Double ratioX = ((Double)PaintArea.Width - (Border.X * borderCountX)) / workingArea.Width;
			Double ratioY = ((Double)PaintArea.Height - (Border.Y * borderCountY)) / workingArea.Height;
			Scale = Math.Max(Math.Min(ratioX, ratioY), 0.01);  // The 0.1 is for weird closing & designer problems;

			Int32 displayIndex = 0;
			foreach (Display display in displays.OrderBy(__display => __display.Left).OrderBy(__display => __display.Top))
			{
				Point bordersTotal = new Point(
					displays.OrderBy(__display => __display.Left).Take(displayIndex + 1).Select(__display => __display.Left).Distinct().Count() * 2,
					displays.OrderBy(__display => __display.Top).Take(displayIndex + 1).Select(__display => __display.Top).Distinct().Count() * 2);

				if (paintBorders)
				{
					#region Draw borders;

					Point pntDisplayOutside = new Point(
						(Int32)((Math.Abs(workingArea.Left) + display.Left) * Scale) + (Border.X * (bordersTotal.X - 2)),
						(Int32)((Math.Abs(workingArea.Top) + display.Top) * Scale) + (Border.Y * (bordersTotal.Y - 2)));
					Point pntDisplayInside = new Point(
						pntDisplayOutside.X + Border.X,
						pntDisplayOutside.Y + Border.Y);
					Size sizeDisplayInside = new Size(
						(Int32)(display.Width * Scale),
						(Int32)(display.Height * Scale));
					Size sizeDisplayOutside = new Size(
						sizeDisplayInside.Width + Border.X * 2,
						sizeDisplayInside.Height + Border.Y * 2);

					display.ScaledBounds.Outside = new Rectangle(pntDisplayOutside, sizeDisplayOutside);
					//Graphics.DrawRectangle(new Pen(Color.Red), display.ScaledBounds.Outside);
					display.ScaledBounds.Inside = new Rectangle(pntDisplayInside, sizeDisplayInside);
					//Graphics.DrawRectangle(new Pen(Color.Green), display.ScaledBounds.Inside);

					Bitmap border1 = Resources.border_1;	// border top left;
					Bitmap border2 = Resources.border_2;	// border top middle;
					Bitmap border3 = Resources.border_3;	// border top right;
					Bitmap border4 = Resources.border_4;	// border right middle;
					Bitmap border5 = Resources.border_5;	// border bottom right;
					Bitmap border6 = Resources.border_6;	// border bottom middle;
					Bitmap border7 = Resources.border_7;	// border bottom left;
					Bitmap border8 = Resources.border_8;	// border left middle;

					border1.MakeTransparent(Transparent);	// border top left;
					border2.MakeTransparent(Transparent);	// border top middle;
					border3.MakeTransparent(Transparent);	// border top right;
					border4.MakeTransparent(Transparent);	// border right middle;
					border5.MakeTransparent(Transparent);	// border bottom right;
					border6.MakeTransparent(Transparent);	// border bottom middle;
					border7.MakeTransparent(Transparent);	// border bottom left;
					border8.MakeTransparent(Transparent);	// border left middle;

					float scaleTop = (float)Border.Y / border2.Height;
					float scaleRight = (float)Border.X / border4.Width;
					float scaleBottom = (float)Border.Y / border6.Height;
					float scaleLeft = (float)Border.X / border8.Width;

					RectangleF rectangle1 = new RectangleF(  // border top left;
						pntDisplayOutside.X,
						pntDisplayOutside.Y,
						border1.Width * scaleLeft,
						border1.Height * scaleTop);
					RectangleF rectangle3 = new RectangleF(  // border top right;
						pntDisplayOutside.X + sizeDisplayOutside.Width - border3.Width * scaleRight,
						pntDisplayOutside.Y,
						border3.Width * scaleRight,
						border3.Height * scaleTop);
					RectangleF rectangle5 = new RectangleF(  // border bottom right;
						pntDisplayOutside.X + sizeDisplayOutside.Width - border5.Width * scaleRight,
						pntDisplayOutside.Y + sizeDisplayOutside.Height - border5.Height * scaleBottom,
						border5.Width * scaleRight,
						border5.Height * scaleBottom);
					RectangleF rectangle7 = new RectangleF(  // border bottom left;
						pntDisplayOutside.X,
						pntDisplayOutside.Y + sizeDisplayOutside.Height - border7.Height * scaleBottom,
						border7.Width * scaleLeft,
						border7.Height * scaleBottom);
					RectangleF rectangle2 = new RectangleF(  // border top middle;
						pntDisplayOutside.X + rectangle1.Width,
						pntDisplayOutside.Y,
						sizeDisplayOutside.Width - rectangle1.Width - rectangle3.Width,
						border2.Height * scaleTop);
					RectangleF rectangle4 = new RectangleF(  // border right middle;
						pntDisplayOutside.X + sizeDisplayOutside.Width - border8.Width * scaleRight,
						pntDisplayOutside.Y + rectangle3.Height,
						border4.Width * scaleRight,
						sizeDisplayOutside.Height - rectangle3.Height - rectangle5.Height);
					RectangleF rectangle6 = new RectangleF(  // border bottom middle;
						pntDisplayOutside.X + rectangle7.Width,
						pntDisplayOutside.Y + sizeDisplayOutside.Height - border2.Height * scaleBottom,
						sizeDisplayOutside.Width - rectangle1.Width - rectangle3.Width,
						border6.Height * scaleBottom);
					RectangleF rectangle8 = new RectangleF(  // border left middle;
						pntDisplayOutside.X,
						pntDisplayOutside.Y + rectangle1.Height,
						border8.Width * scaleLeft,
						sizeDisplayOutside.Height - rectangle3.Height - rectangle5.Height);

					_graphics.SmoothingMode = SmoothingMode.None;  // Need staight borders;
					_graphics.InterpolationMode = InterpolationMode.NearestNeighbor;  // When not used, we get transparent stretch;
					_graphics.PixelOffsetMode = PixelOffsetMode.Half;  // If not used, width is off by 1px;

					//_graphics.FillRectangle(Brushes.Green, rectangle_1);	// border top left;
					//_graphics.FillRectangle(Brushes.Red, rectangle_2);	// border top middle;
					//_graphics.FillRectangle(Brushes.Green, rectangle_3);	// border top right;
					//_graphics.FillRectangle(Brushes.Purple, rectangle_4);	// border right middle;
					//_graphics.FillRectangle(Brushes.Green, rectangle_5);	// border bottom right;
					//_graphics.FillRectangle(Brushes.Red, rectangle_6);	// border bottom middle;
					//_graphics.FillRectangle(Brushes.Green, rectangle_7);	// border bottom left;
					//_graphics.FillRectangle(Brushes.Red, rectangle_8);	// border left middle;

					_graphics.DrawImage(border2, rectangle2);	// border top middle;
					_graphics.DrawImage(border4, rectangle4);	// border right middle;
					_graphics.DrawImage(border6, rectangle6);	// border bottom middle;
					_graphics.DrawImage(border8, rectangle8);	// border left middle;
					// Draw corners after sides;
					_graphics.DrawImage(border1, rectangle1);	// border top left;
					_graphics.DrawImage(border3, rectangle3);	// border top right;
					_graphics.DrawImage(border5, rectangle5);	// border bottom right;
					_graphics.DrawImage(border7, rectangle7);	// border bottom left;

					#endregion Draw borders;

					#region Draw numbered circle;

					Point numberOffset = new Point();
					if (display.Identifier == 1)  // Number 1 looks weird when centering, a little to the left fixes this;
					{
						numberOffset.X = (Int32)(-15 * Scale);
					}

					Point centerCircle = new Point(pntDisplayOutside.X + sizeDisplayOutside.Width / 2,
												   pntDisplayOutside.Y + sizeDisplayOutside.Height / 2);

					Int32 outerCircleRadius = (Int32)(Scale * 420);
					Rectangle rectOuterCircle = new Rectangle(
						centerCircle.X - outerCircleRadius / 2,
						centerCircle.Y - outerCircleRadius / 2,
						outerCircleRadius,
						outerCircleRadius);
					Int32 innerCircleRadius = (Int32)(Scale * 350);
					Rectangle rectInnerCircle = new Rectangle(
						centerCircle.X - innerCircleRadius / 2,
						centerCircle.Y - innerCircleRadius / 2,
						innerCircleRadius,
						innerCircleRadius);

					_graphics.SmoothingMode = SmoothingMode.HighQuality;  // Need high quality rounded border;
					GraphicsPath ellipseLine = new GraphicsPath { FillMode = FillMode.Alternate };
					ellipseLine.AddEllipse(rectOuterCircle);  // White circle border;
					ellipseLine.AddEllipse(rectInnerCircle);  // Transparent circle middle;
					_graphics.FillPath(_whiteCircleBorder, ellipseLine);  // White circle border;
					_graphics.FillEllipse(_blueCircleMiddle, rectInnerCircle);  // Blue circle middle;

					_graphics.DrawText(
						display.Identifier.ToString(CultureInfo.InvariantCulture),
						centerCircle.Add(numberOffset),
						new Font(new FontFamily("Arial"), (float)(300.0 * Scale), FontStyle.Bold),
						Color.White);

					#endregion Draw numbered circle;
				}

				if (paintWindows)
				{
					#region Draw windows;

					try  // Removing this try-catch crashes Visual Studio :(
					{
						foreach (Window window in display.Windows)
						{
							if (window == null)
							{
								continue;
							}

							IntPtr handle = window.Handle;

							if (handle == Handler)  // Don't make a thumbnail for this program;
							{
								continue;
							}

							if (handle == IntPtr.Zero || !HelperFramework.PInvoke.Window.IsWindow(handle))  // Check if we should remove the thumbnail;
							{
								window.Dispose();
								continue;
							}

							// Get window bounds;
							Win32.RECT rect;
							HelperFramework.PInvoke.Window.GetWindowRect(handle, out rect);
							window.Bounds = rect.ToRectangle();
							if (window.Bounds.Y <= -30000)  // Winamp partial fix;
							{
								window.Bounds.Y = 0;
							}

							// Check if window is maximized;
							Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
							placement.length = Marshal.SizeOf(placement);
							HelperFramework.PInvoke.Window.GetWindowPlacement(handle, ref placement);
							window.Maximized = placement.showCmd == Win32.SW.SHOWMAXIMIZED;
							if (window.Maximized && !(window.GetType() == typeof(Taskbar) || window.GetType() == typeof(Desktop)))  // When window is maximized, the borders are still included;
							{
								//window.Bounds.X += SystemInformation.FrameBorderSize.Width;
								//window.Bounds.Y += SystemInformation.FrameBorderSize.Height;
								//window.Bounds.Width -= SystemInformation.FrameBorderSize.Width * 2;
								//window.Bounds.Height -= SystemInformation.FrameBorderSize.Height * 2;
								window.Bounds.Width -= SystemInformation.FrameBorderSize.Width;
								window.Bounds.Height -= SystemInformation.FrameBorderSize.Height;
							}

							if (!display.HasWindow(window))  // Check if display still has window, otherwise remove thumbnail, but keep window reference;
							{
								window.Unregister();
								continue;
							}

							Rectangle windowPosition = window.Bounds;
							Rectangle windowIntersection = Rectangle.Intersect(windowPosition, display.Bounds);
							Rectangle offsetter = new Rectangle(0, 0, 0, windowIntersection.Height);

							if (/*display.Identifier == 2 &&*/ window.Title.Contains("Notepad"))
							{
								HelperFramework.Debug.Console.Identifier = display.Identifier.ToString(CultureInfo.InvariantCulture);
								HelperFramework.Debug.Console.Log(null, "window.Bounds      : ", window.Bounds);
								HelperFramework.Debug.Console.Log(null, "windowIntersection : ", windowIntersection);
								HelperFramework.Debug.Console.Log(null, "display.Bounds     : ", display.Bounds);
							}
							if (window.Bounds.Right > display.Bounds.Right)  // Right side intersection;
							{
								offsetter.X = windowIntersection.Width - window.Bounds.Width;
								windowPosition.X += offsetter.X;
							}
							if (window.Bounds.Left < display.Bounds.Left)  // Left side intersection;
							{
								offsetter.X = window.Bounds.Width - windowIntersection.Width;
								windowPosition.X += offsetter.X;
							}
							if (window.Bounds.Right > display.Bounds.Right && window.Bounds.Left < display.Bounds.Left)
							{
								//windowPosition.Width = windowIntersection.Width;
							}
							if (window.Bounds.Bottom > display.Bounds.Bottom)  // Bottom side intersection;
							{
								windowPosition.Height = offsetter.Height;
							}
							if (window.Bounds.Top < display.Bounds.Top)  // Top side intersection;
							{
								offsetter.Y = window.Bounds.Height - windowIntersection.Height;
								windowPosition.Y += offsetter.Y;
								windowPosition.Height = windowIntersection.Height;
							}

							if (window.Thumb == IntPtr.Zero || window.ForceRefresh)  // If zero, we need to make a new thumbnail;
							{
								if (window.ForceRefresh)
								{
									window.Unregister();
									window.ForceRefresh = false;
								}
								IntPtr thumbTemp;
								if (DwmThumbnail.DwmRegisterThumbnail(Handler, handle, out thumbTemp) != 0)  // Draw thumbnail;
								{
									Debug.WriteLine(String.Format("ERROR: problems creating thumbnail for window {0} ({1}).", window.Title, window.Handle));
									window.Dispose();
									continue;
								}
								window.Thumb = thumbTemp;
							}

							Rectangle thumbnailSource = new Rectangle(
								offsetter.Left, offsetter.Top,
								offsetter.Right, offsetter.Bottom);

							if (/*display.Identifier == 2 &&*/ window.Title.Contains("Notepad"))
							{
								HelperFramework.Debug.Console.Log(null, "offsetter          : ", offsetter);
								HelperFramework.Debug.Console.Log(null, "windowPosition     : ", windowPosition);
								HelperFramework.Debug.Console.Log(null, "thumbnailSource    : ", thumbnailSource);
								HelperFramework.Debug.Console.Log("-----");
								if (display.Identifier == 3)
								{
									int i = 0;
								}
							}

							Rectangle thumbnailDestination = new Rectangle(  // Thumbnail destination needs to have the same width & height as the none-scaled window;
								(Int32)((Math.Abs(workingArea.Left) + windowPosition.Left) * Scale) + (Border.X * (bordersTotal.X - 1)),
								(Int32)((Math.Abs(workingArea.Top) + windowPosition.Top) * Scale) + (Border.Y * (bordersTotal.Y - 1)),
								(Int32)((Math.Abs(workingArea.Left) + windowPosition.Right) * Scale) + (Border.X * (bordersTotal.X - 1)),
								(Int32)((Math.Abs(workingArea.Top) + windowPosition.Bottom) * Scale) + (Border.Y * (bordersTotal.Y - 1)));

							//if (display.Identifier == 2)
							//{
							Win32.DWM_THUMBNAIL_PROPERTIES props = new Win32.DWM_THUMBNAIL_PROPERTIES
																	{
																		dwFlags = /*0x1f*/
																			Win32.DWM.TNP_OPACITY | Win32.DWM.TNP_VISIBLE |
																			Win32.DWM.TNP_SOURCECLIENTAREAONLY | Win32.DWM.TNP_RECTDESTINATION |
																			Win32.DWM.TNP_RECTSOURCE,
																		opacity = Opacity,
																		fVisible = true,
																		fSourceClientAreaOnly = false,
																		rcSource = thumbnailSource,
																		rcDestination = thumbnailDestination
																	};
							DwmThumbnail.DwmUpdateThumbnailProperties(window.Thumb, ref props);
							//}
						}
					}
					catch (Exception e)
					{
						Debug.WriteLine(e);
					}

					#endregion Draw windows;
				}

				displayIndex++;
			}
		}

		#endregion Methods;


		#region Override;

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override String ToString()
		{
			String[] rtrn = new String[Count];
			Int32 i = 0;
			ForEach(delegate(Display d)
			{
				rtrn[i] = String.Format(
@"Display {0}:
	Top: {1}
	Right: {2}
	Bottom: {3}
	Left: {4}
	Width: {5}
	Height: {6}
	Primairy: {7}",
					++i, d.Top, d.Right, d.Bottom, d.Left, d.Width, d.Height, d.Primairy);
			});
			return String.Join(Environment.NewLine, rtrn);
		}

		#endregion Override;


		#region Static Methods;

		/// <summary>
		/// Calculate compleet working area;
		/// </summary>
		/// <returns>Working area</returns>
		/// <remarks>Gaps between displays are also calculated</remarks>
		public static Rectangle WorkingArea()
		{
			// SystemInformation.WorkingArea <-- doesn't seems to return all monitors;

			Rectangle workingArea = new Rectangle(Int32.MaxValue, Int32.MaxValue, 0, 0);

			Displays displays = new Displays(true);
			foreach (Display display in displays)
			{
				if (display.Left < workingArea.Left)
				{
					workingArea.X = display.Left;
					workingArea.Width += Math.Abs(display.Left);
				}
				if (display.Top < workingArea.Top)
				{
					workingArea.Y = display.Top;
					workingArea.Height += Math.Abs(display.Top);
				}
				if (display.Right > workingArea.Right)
				{
					workingArea.Width += display.Right - workingArea.Right;
				}
				if (display.Bottom > workingArea.Bottom)
				{
					workingArea.Height += display.Bottom - workingArea.Bottom;
				}
			}

			return workingArea;
		}

		#endregion Static Methods;


		#region Interface;

		#region ICloneable Members

		public Object Clone()
		{
			return MemberwiseClone();
		}

		#endregion ICloneable Members

		#region IDisposable Members;

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(Boolean disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					foreach (Display display in this)
					{
						display.Dispose();
					}
				}
				Scale = Int32.MinValue;
				Opacity = Byte.MinValue;
				Border = Point.Empty;
				Handler = IntPtr.Zero;
				if (Graphics != null)
				{
					Graphics.Dispose();
				}

				_disposed = true;
			}
		}

		/// <summary>
		/// Allows an <see cref="T:System.Object"/> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object"/> is reclaimed by garbage collection.
		/// </summary>
		~Displays()
		{
			Dispose(false);
		}

		#endregion IDisposable Members;

		#endregion Interface;


		#region SubClass;

		public class DisplayWindows
		{
			public Displays WindowsList { set; private get; }

			/// <summary>
			/// Add window to all displays;
			/// </summary>
			/// <param name="hwnd">Window handle</param>
			public void AddAll(IntPtr hwnd)
			{
				WindowsList.ForEach(__display => __display.Windows.Add(hwnd));
			}

			/// <summary>
			/// Move window on all displays;
			/// </summary>
			/// <param name="hwnd">Window handle</param>
			public void MoveAll(IntPtr hwnd)
			{
				WindowsList.ForEach(__display => __display.Windows.Move(hwnd));
			}

			/// <summary>
			/// Delete window on all displays;
			/// </summary>
			/// <param name="hwnd">Window handle</param>
			public void RemoveAll(IntPtr hwnd)
			{
				WindowsList.ForEach(__display => __display.Windows.Remove(hwnd));
			}
		}

		#endregion SubClass;

	}
}