using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HelperFramework;
using System.Linq;
using HelperFramework.Extension;
using Math = System.Math;


namespace DisplayWindowManager.Factory
{
	/// <summary>
	/// Class for a computer display;
	/// </summary>
	public class Display : IDisposable
	{

		#region Fields & Properties;

		private Boolean _disposed = false;

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

		private Int32 _width = 0;
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

		private Int32 _height = 0;
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

		private Rectangle _bounds = new Rectangle();
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

	}

	/// <summary>
	/// List of all display's;
	/// </summary>
	public class Displays : List<Display>, IDisposable
	{

		#region Fields & Properties;

		private Boolean _disposed = false;

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
				_graphics.Clear(Color.Transparent);  // empty;
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
		/// <summary>
		/// Paint Displays and/or Windows;
		/// </summary>
		/// <param name="paintDisplays">Paint Displays</param>
		/// <param name="paintWindows">Paint Windows</param>
		public void Paint2(Boolean paintDisplays, Boolean paintWindows)
		{
#if NOSHELL
            paintWindows = false;
#else
			paintWindows = paintWindows;
#endif

			Rectangle workingArea = WorkingArea();

			int index = 0;
			foreach (Display display in this)
			{
				index++;

				//Double ratio = (Double)display.Height / display.Width;
				Double ratioX = ((Double)PaintArea.Width - (Border.X * 2)) / display.Width;
				Double ratioY = ((Double)PaintArea.Height - (Border.Y * 2)) / display.Height;

				Double ratio = Math.Min(ratioX, ratioY);
				ratioX = ratio;
				ratioY = ratio;

				Point pntDisplayOutside = new Point(
					(Int32)((Math.Abs(workingArea.Left) + display.Left) * ratioX),
					(Int32)((Math.Abs(workingArea.Top) + display.Top) * ratioY));
				Size sizeDisplayOutside = new Size(
					(Int32)(display.Width * ratioX),
					(Int32)(display.Height * ratioY));
				Size sizeDisplayInside = new Size(
					(Int32)(display.Width * ratioX) - (Border.X * 2),
					(Int32)(display.Height * ratioY) - (Border.Y * 2));

				if (paintDisplays)
				{
					Int32 outerCircleRadius = (Int32)(Math.Min(ratioX, ratioY) * 420);
					Rectangle rectOuterCircle = new Rectangle(
						pntDisplayOutside.X + (sizeDisplayOutside.Width - outerCircleRadius) / 2,
						pntDisplayOutside.Y + (sizeDisplayOutside.Height - outerCircleRadius) / 2,
						outerCircleRadius,
						outerCircleRadius);
					Int32 innerCircleRadius = (Int32)(Math.Min(ratioX, ratioY) * 350);
					Rectangle rectInnerCircle = new Rectangle(
						pntDisplayOutside.X + (sizeDisplayOutside.Width - innerCircleRadius) / 2,
						pntDisplayOutside.Y + (sizeDisplayOutside.Height - innerCircleRadius) / 2,
						innerCircleRadius,
						innerCircleRadius);

					// Draw borders;
					_graphics.SmoothingMode = SmoothingMode.None;  // need staight borders;
					_graphics.FillRectangle(Brushes.Green, new Rectangle(pntDisplayOutside.X, pntDisplayOutside.Y, Border.X, Border.Y));																// border top left;
					_graphics.FillRectangle(Brushes.Red, new Rectangle(pntDisplayOutside.X + Border.X, pntDisplayOutside.Y, sizeDisplayInside.Width, Border.Y));												// border top middle;
					_graphics.FillRectangle(Brushes.Green, new Rectangle(pntDisplayOutside.X + sizeDisplayInside.Width + Border.X, pntDisplayOutside.Y, Border.X, Border.Y));								// border top right;
					_graphics.FillRectangle(Brushes.Purple, new Rectangle(pntDisplayOutside.X + sizeDisplayInside.Width + Border.X, pntDisplayOutside.Y + Border.Y, Border.X, sizeDisplayInside.Height));			// border right middle;
					_graphics.FillRectangle(Brushes.Green, new Rectangle(pntDisplayOutside.X + sizeDisplayInside.Width + Border.X, pntDisplayOutside.Y + sizeDisplayInside.Height + Border.Y, Border.X, Border.Y));	// border bottom right;
					_graphics.FillRectangle(Brushes.Red, new Rectangle(pntDisplayOutside.X + Border.X, pntDisplayOutside.Y + sizeDisplayInside.Height + Border.Y, sizeDisplayInside.Width, Border.Y));				// border bottom middle;
					_graphics.FillRectangle(Brushes.Green, new Rectangle(pntDisplayOutside.X, pntDisplayOutside.Y + sizeDisplayInside.Height + Border.Y, Border.X, Border.Y));								// border bottom left;
					_graphics.FillRectangle(Brushes.Red, new Rectangle(pntDisplayOutside.X, pntDisplayOutside.Y + Border.Y, Border.X, sizeDisplayInside.Height));											// border left middle;

					// Draw numbered circle;
					_graphics.SmoothingMode = SmoothingMode.HighQuality;  // need high quality rounded border;
					GraphicsPath ellipseLine = new GraphicsPath { FillMode = FillMode.Alternate };
					ellipseLine.AddEllipse(rectOuterCircle);  // white circle border;
					ellipseLine.AddEllipse(rectInnerCircle);  // transparent middle circle;
					_graphics.FillPath(new SolidBrush(Color.FromArgb(195, 248, 248, 255)), ellipseLine);
					_graphics.FillEllipse(new SolidBrush(Color.FromArgb(137, 0, 0, 122)), rectInnerCircle);  // blue circle;
					Point numberOffset = new Point();
					// Number 1 looks weird when centering, a little to the left fixes this;
					if (display.Identifier == 1)
					{
						numberOffset.X = (Int32)(-15 * ratioX);
					}
					_graphics.DrawText(  // number;
						display.Identifier.ToString(),
						new Point(pntDisplayOutside.X + (sizeDisplayOutside.Width / 2) + numberOffset.X, pntDisplayOutside.Y + (sizeDisplayOutside.Height / 2) + numberOffset.Y),
						new Font(new FontFamily("Arial"), (float)(300.0 * Math.Min(ratioX, ratioY)), FontStyle.Bold),
						Color.White);
				}

				if (paintWindows)
				{
					try  // removing this try-catch crashes Visual Studio :(
					{
						foreach (Window window in display.Windows)
						{
							if (window == null)
							{
								continue;
							}

							IntPtr handle = window.Handle;

							if (handle == Handler)  // don't make a thumbnail for this program;
							{
								continue;
							}

							if (handle == IntPtr.Zero || !Win32.IsWindow(handle))  // check if we should remove the thumbnail;
							{
								window.Dispose();
								continue;
							}

							// Get window bounds;
							Win32.RECT rect;
							Win32.GetWindowRect(handle, out rect);
							window.Bounds = rect.ToRectangle();
							if (window.Bounds.Y <= -30000)  // winamp partial fix;
							{
								window.Bounds.Y = 0;
							}

							// Check if window is maximized;
							Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
							placement.length = Marshal.SizeOf(placement);
							Win32.GetWindowPlacement(handle, ref placement);
							window.Maximized = placement.showCmd == Win32.SW.SHOWMAXIMIZED;
							if (window.Maximized && !(window.GetType() == typeof(Taskbar) || window.GetType() == typeof(Desktop)))  // when window is maximized, the borders are still included;
							{
								//window.Bounds.X += SystemInformation.FrameBorderSize.Width;
								//window.Bounds.Y += SystemInformation.FrameBorderSize.Height;
								//window.Bounds.Width -= SystemInformation.FrameBorderSize.Width * 2;
								//window.Bounds.Height -= SystemInformation.FrameBorderSize.Height * 2;
								window.Bounds.Width -= SystemInformation.FrameBorderSize.Width * 2;
								window.Bounds.Height -= SystemInformation.FrameBorderSize.Height * 2;
							}

							if (!display.HasWindow(window))  // Check if display still has window, otherwise remove thumbnail, but keep window reference;
							{
								window.Unregister();
								continue;
							}
							if (!String.IsNullOrEmpty(window.Title) && window.Title.Contains("JR Screen Ruler"))
							{
								int i = 0;
							}

							Rectangle windowPosition = window.Bounds;
							Rectangle windowIntersection = Rectangle.Intersect(windowPosition, display.Bounds);
							Rectangle offsetter = new Rectangle(0, 0, 0, windowIntersection.Height);

							// fix some weird scaling and sizing adjustments for the thumbnails;
							if (window.Bounds.Right > display.Bounds.Right)  // right side intersection;
							{
								windowPosition.X += offsetter.X = windowIntersection.Width - window.Bounds.Width;
							}
							if (window.Bounds.Left < display.Bounds.Left)  // left side intersection;
							{
								windowPosition.X += offsetter.X = window.Bounds.Width - windowIntersection.Width;
							}
							if (window.Bounds.Bottom > display.Bounds.Bottom)  // bottom side intersection;
							{
								windowPosition.Height = offsetter.Height;
							}
							if (window.Bounds.Top < display.Bounds.Top)  // top side intersection;
							{
								windowPosition.Y += offsetter.Y = window.Bounds.Height - windowIntersection.Height;
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
								if (Win32.DwmRegisterThumbnail(Handler, handle, out thumbTemp) != 0) // draw thumbnail;
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
							//Rectangle thumbnailDestination2 = new Rectangle(  // thumbnail destination needs to have the same width & height as the none-scaled window;
							//    (Int32)((Math.Abs(workingArea.Left) + windowPosition.Left) * Scale) + Border.X,
							//    (Int32)((Math.Abs(workingArea.Top) + windowPosition.Top) * Scale) + Border.Y,
							//    (Int32)((Math.Abs(workingArea.Left) + windowPosition.Right) * Scale) - Border.X,
							//    (Int32)((Math.Abs(workingArea.Top) + windowPosition.Bottom) * Scale) - Border.Y);
							//Rectangle thumbnailDestination = new Rectangle(  // thumbnail destination needs to have the same width & height as the none-scaled window;
							//    (Int32)((Math.Abs(workingArea.Left) + windowPosition.Left) * Scale) + (Border.X * (((index - 1) * 2) + 1)),
							//    (Int32)((Math.Abs(workingArea.Top) + windowPosition.Top) * Scale) + Border.Y,
							//    (Int32)((Math.Abs(workingArea.Left) + windowPosition.Right) * Scale) + (Border.X * (((index - 1) * 2) + 1)),
							//    (Int32)((Math.Abs(workingArea.Top) + windowPosition.Bottom) * Scale) + 25);
							//                        Rectangle test = new Rectangle(
							//                            (Int32)((Math.Abs(workingArea.Left) + windowPosition.Left) * Scale) + (Border.X * 1),
							//                            (Int32)((Math.Abs(workingArea.Top) + windowPosition.Top) * Scale) + Border.Y,
							//                            (Int32)((windowPosition.Width * Scale) - Border.X),
							//                            (Int32)((windowPosition.Height * Scale) - Border.Y)
							//                            );

							//Win32.PSIZE size;
							//Win32.DwmQueryThumbnailSourceSize(window.Thumb, out size);

							//if (window.Bounds.Width <= 0)
							//{
							//    window.Bounds.Width = size.x;
							//}
							//if (window.Bounds.Height <= 0)
							//{
							//    window.Bounds.Height = size.y;
							//}

							//                        Rectangle test2 = new Rectangle(
							//                            (Int32)((Math.Abs(workingArea.Left) + windowPosition.Left) * Scale) + Border.X,
							//                            (Int32)((Math.Abs(workingArea.Top) + windowPosition.Top) * Scale) + Border.Y,
							//                            (Int32)(windowPosition.Width * Scale),
							//                            (Int32)(windowPosition.Width * Scale * ratio)
							//                            );
							//                        if (test2.Height < 0)
							//                        {
							//                            //test2.Height += Border.Y;
							//                        }



							//                        Rectangle thumbnailDestination = new Rectangle(  // thumbnail destination needs to have the same width & height as the none-scaled window;
							//(Int32)(((Math.Abs(workingArea.Left) + windowPosition.Left) / Scale) + Border.X),
							//(Int32)(((Math.Abs(workingArea.Top) + windowPosition.Top) / Scale) + Border.Y),
							//(Int32)(((Math.Abs(workingArea.Left) + windowPosition.Right) / Scale) + Border.X),
							//(Int32)(((Math.Abs(workingArea.Top) + windowPosition.Bottom) / Scale) + Border.Y));


							Win32.RECT rcDestination = new Win32.RECT(
								workingArea.Left + 5 + (Int32)(windowPosition.Left * ratioX),
								workingArea.Top + 15 + (Int32)(windowPosition.Top * ratioY),
								workingArea.Left + 5 + (Int32)((Double)(windowPosition.Left + window.Bounds.Width) * ratioX),
								workingArea.Top + 15 + (Int32)((Double)(windowPosition.Top + window.Bounds.Height) * ratioY));
							if (rcDestination.Width > 10)
							{
								rcDestination.Width -= 10;
							}
							else
							{
								rcDestination.Left -= 10;
								rcDestination.Right -= 10;
							}
							if (rcDestination.Height > 30)
							{
								rcDestination.Height -= 30;
							}
							else
							{
								rcDestination.Top -= 30;
								rcDestination.Bottom -= 30;
							}
							/*
							Win32.DWM_THUMBNAIL_PROPERTIES props = new Win32.DWM_THUMBNAIL_PROPERTIES
							{
								dwFlags = 0x1f,// Win32.DWM.TNP_OPACITY | Win32.DWM.TNP_VISIBLE | Win32.DWM.TNP_SOURCECLIENTAREAONLY | Win32.DWM.TNP_RECTDESTINATION | Win32.DWM.TNP_RECTSOURCE,
								opacity = Opacity,
								fVisible = true,
								fSourceClientAreaOnly = false,
								rcSource = thumbnailSource,
								rcDestination = rcDestination
							};
							Win32.DwmUpdateThumbnailProperties(window.Thumb, ref props);
							*/
							/*if(_graphics!=null)
							{
								if (!String.IsNullOrEmpty(window.Title) && window.Title.Contains("Calculator"))
								{
									_graphics.DrawRectangle(new Pen(Brushes.Green), thumbnailSize);
								}
							}*/
						}
					}
					catch (Exception e)
					{
						Debug.WriteLine(e);
					}
				}
			}
		}

		public void Paint(Boolean paintDisplays, Boolean paintWindows)
		{
#if NOSHELL
			paintWindows = false;
#else
			paintWindows = paintWindows;
#endif

			Rectangle workingArea = WorkingArea();

			//Double ratio = (Double)display.Height / display.Width;
			Double ratioX = ((Double)PaintArea.Width - (Border.X * 4)) / workingArea.Width;
			Double ratioY = ((Double)PaintArea.Height - (Border.Y * 4)) / workingArea.Height;

			Double ratio = Math.Min(ratioX, ratioY);
			ratioX = ratio;
			ratioY = ratio;
			Scale = ratio;


			//#if DEBUG
			//            if (_graphics != null)
			//            {
			//                try
			//                {
			//                    _graphics.DrawRectangle(new Pen(Color.Yellow), 0, 0,
			//                        (Int32)(workingArea.Width * Scale) - 1 + Border.X,
			//                        (Int32)(workingArea.Height * Scale) - 1 + Border.Y);
			//                }
			//                catch { }
			//            }
			//#endif

			int index = 0;
			foreach (Display display in this)
			{
				index++;

				Point pntDisplayOutside = new Point(
					(Int32)((Math.Abs(workingArea.Left) + display.Left) * ratioX),
					(Int32)((Math.Abs(workingArea.Top) + display.Top) * ratioY));
				Size sizeDisplayOutside = new Size(
					(Int32)(display.Width * ratioX),
					(Int32)(display.Height * ratioY));
				Size sizeDisplayInside = new Size(
					(Int32)(display.Width * ratioX) /*- (Border.X * 2)*/,
					(Int32)(display.Height * ratioY)/* - (Border.Y * 2)*/);

				if (paintDisplays)
				{
					Int32 outerCircleRadius = (Int32)(Math.Min(ratioX, ratioY) * 420);
					Rectangle rectOuterCircle = new Rectangle(
						pntDisplayOutside.X + (sizeDisplayOutside.Width - outerCircleRadius) / 2,
						pntDisplayOutside.Y + (sizeDisplayOutside.Height - outerCircleRadius) / 2,
						outerCircleRadius,
						outerCircleRadius);
					Int32 innerCircleRadius = (Int32)(Math.Min(ratioX, ratioY) * 350);
					Rectangle rectInnerCircle = new Rectangle(
						pntDisplayOutside.X + (sizeDisplayOutside.Width - innerCircleRadius) / 2,
						pntDisplayOutside.Y + (sizeDisplayOutside.Height - innerCircleRadius) / 2,
						innerCircleRadius,
						innerCircleRadius);

					// Draw borders;
					_graphics.SmoothingMode = SmoothingMode.None;  // need staight borders;
					_graphics.FillRectangle(Brushes.Green, new Rectangle(pntDisplayOutside.X, pntDisplayOutside.Y, Border.X, Border.Y));																// border top left;
					_graphics.FillRectangle(Brushes.Red, new Rectangle(pntDisplayOutside.X + Border.X, pntDisplayOutside.Y, sizeDisplayInside.Width, Border.Y));												// border top middle;
					_graphics.FillRectangle(Brushes.Green, new Rectangle(pntDisplayOutside.X + sizeDisplayInside.Width + Border.X, pntDisplayOutside.Y, Border.X, Border.Y));								// border top right;
					_graphics.FillRectangle(Brushes.Purple, new Rectangle(pntDisplayOutside.X + sizeDisplayInside.Width + Border.X, pntDisplayOutside.Y + Border.Y, Border.X, sizeDisplayInside.Height));			// border right middle;
					_graphics.FillRectangle(Brushes.Green, new Rectangle(pntDisplayOutside.X + sizeDisplayInside.Width + Border.X, pntDisplayOutside.Y + sizeDisplayInside.Height + Border.Y, Border.X, Border.Y));	// border bottom right;
					_graphics.FillRectangle(Brushes.Red, new Rectangle(pntDisplayOutside.X + Border.X, pntDisplayOutside.Y + sizeDisplayInside.Height + Border.Y, sizeDisplayInside.Width, Border.Y));				// border bottom middle;
					_graphics.FillRectangle(Brushes.Green, new Rectangle(pntDisplayOutside.X, pntDisplayOutside.Y + sizeDisplayInside.Height + Border.Y, Border.X, Border.Y));								// border bottom left;
					_graphics.FillRectangle(Brushes.Red, new Rectangle(pntDisplayOutside.X, pntDisplayOutside.Y + Border.Y, Border.X, sizeDisplayInside.Height));											// border left middle;

					// Draw numbered circle;
					_graphics.SmoothingMode = SmoothingMode.HighQuality;  // need high quality rounded border;
					GraphicsPath ellipseLine = new GraphicsPath { FillMode = FillMode.Alternate };
					ellipseLine.AddEllipse(rectOuterCircle);  // white circle border;
					ellipseLine.AddEllipse(rectInnerCircle);  // transparent middle circle;
					_graphics.FillPath(new SolidBrush(Color.FromArgb(195, 248, 248, 255)), ellipseLine);
					_graphics.FillEllipse(new SolidBrush(Color.FromArgb(137, 0, 0, 122)), rectInnerCircle);  // blue circle;
					Point numberOffset = new Point();
					// Number 1 looks weird when centering, a little to the left fixes this;
					if (display.Identifier == 1)
					{
						numberOffset.X = (Int32)(-15 * ratioX);
					}
					_graphics.DrawText(  // number;
						display.Identifier.ToString(),
						new Point(pntDisplayOutside.X + (sizeDisplayOutside.Width / 2) + numberOffset.X, pntDisplayOutside.Y + (sizeDisplayOutside.Height / 2) + numberOffset.Y),
						new Font(new FontFamily("Arial"), (float)(300.0 * Math.Min(ratioX, ratioY)), FontStyle.Bold),
						Color.White);
				}

				if (paintWindows)
				{
					try  // removing this try-catch crashes Visual Studio :(
					{
						foreach (Window window in display.Windows)
						{
							if (window == null)
							{
								continue;
							}

							IntPtr handle = window.Handle;

							if (handle == Handler)  // don't make a thumbnail for this program;
							{
								continue;
							}

							if (handle == IntPtr.Zero || !Win32.IsWindow(handle))  // check if we should remove the thumbnail;
							{
								window.Dispose();
								continue;
							}

							// Get window bounds;
							Win32.RECT rect;
							Win32.GetWindowRect(handle, out rect);
							window.Bounds = rect.ToRectangle();
							if (window.Bounds.Y <= -30000)  // winamp partial fix;
							{
								window.Bounds.Y = 0;
							}

							// Check if window is maximized;
							Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
							placement.length = Marshal.SizeOf(placement);
							Win32.GetWindowPlacement(handle, ref placement);
							window.Maximized = placement.showCmd == Win32.SW.SHOWMAXIMIZED;
							if (window.Maximized && !(window.GetType() == typeof(Taskbar) || window.GetType() == typeof(Desktop)))  // when window is maximized, the borders are still included;
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

							// fix some weird scaling and sizing adjustments for the thumbnails;
							if (window.Bounds.Right > display.Bounds.Right)  // right side intersection;
							{
								windowPosition.X += offsetter.X = windowIntersection.Width - window.Bounds.Width;
							}
							if (window.Bounds.Left < display.Bounds.Left)  // left side intersection;
							{
								windowPosition.X += offsetter.X = window.Bounds.Width - windowIntersection.Width;
							}
							if (window.Bounds.Bottom > display.Bounds.Bottom)  // bottom side intersection;
							{
								windowPosition.Height = offsetter.Height;
							}
							if (window.Bounds.Top < display.Bounds.Top)  // top side intersection;
							{
								windowPosition.Y += offsetter.Y = window.Bounds.Height - windowIntersection.Height;
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
								if (Win32.DwmRegisterThumbnail(Handler, handle, out thumbTemp) != 0) // draw thumbnail;
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
							Rectangle thumbnailDestination = new Rectangle(  // thumbnail destination needs to have the same width & height as the none-scaled window;
								(Int32)((Math.Abs(workingArea.Left) + windowPosition.Left) * Scale) + (Border.X * (((index - 1) * 2) + 1)),
								(Int32)((Math.Abs(workingArea.Top) + windowPosition.Top) * Scale) + Border.Y,
								(Int32)((Math.Abs(workingArea.Left) + windowPosition.Right) * Scale) + (Border.X * (((index - 1) * 2) + 1)),
								(Int32)((Math.Abs(workingArea.Top) + windowPosition.Bottom) * Scale) + Border.Y);
							Rectangle thumbnailSize = new Rectangle(
								thumbnailDestination.X,
								thumbnailDestination.Y,
								(Int32)(windowPosition.Width * Scale) + Border.X,
								(Int32)(windowPosition.Height * Scale) + Border.Y);

							Win32.DWM_THUMBNAIL_PROPERTIES props = new Win32.DWM_THUMBNAIL_PROPERTIES
							{
								dwFlags = 0x1f,// Win32.DWM.TNP_OPACITY | Win32.DWM.TNP_VISIBLE | Win32.DWM.TNP_SOURCECLIENTAREAONLY | Win32.DWM.TNP_RECTDESTINATION | Win32.DWM.TNP_RECTSOURCE,
								opacity = Opacity,
								fVisible = true,
								fSourceClientAreaOnly = false,
								rcSource = thumbnailSource,
								rcDestination = thumbnailDestination
							};
							Win32.DwmUpdateThumbnailProperties(window.Thumb, ref props);

							/*if(_graphics!=null)
							{
								if (!String.IsNullOrEmpty(window.Title) && window.Title.Contains("Calculator"))
								{
									_graphics.DrawRectangle(new Pen(Brushes.Green), thumbnailSize);
								}
							}*/
						}
					}
					catch (Exception e)
					{
						Debug.WriteLine(e);
					}
				}
			}
		}

		/// <summary>
		/// Returns a list of Displays where a window intersects with;
		/// </summary>
		/// <param name="window">Window to check for</param>
		/// <returns>A list of Displays</returns>
		public List<Display> IntersectDisplay(Window window)
		{
			return FindAll(display => new Rectangle(display.Left, display.Top, display.Width, display.Height).IntersectsWith(window.Bounds));
		}

		/// <summary>
		/// Add window to all dispays;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void WindowAdd(IntPtr hwnd)
		{
			ForEach(display => display.Windows.Add(hwnd));
		}

		/// <summary>
		/// Move window on all displays;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void WindowMove(IntPtr hwnd)
		{
			ForEach(display => display.Windows.Move(hwnd));
		}

		/// <summary>
		/// Remove window on all displays;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void WindowRemove(IntPtr hwnd)
		{
			ForEach(display => display.Windows.Remove(hwnd));
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
			return String.Join("\r\n", rtrn);
		}

		#endregion Override;


		#region Static Methods;

		public static Display TopMost()
		{
			Displays displays = new Displays(true);
			Display[] topMost = { displays.First() };
			foreach (Display display in displays.Where(display => display.Top <= topMost[0].Top))
			{
				topMost[0] = display;
			}
			return topMost[0];
		}

		public static Display LeftMost()
		{
			Displays displays = new Displays(true);
			Display[] leftMost = { displays.First() };
			foreach (Display display in displays.Where(display => display.Left <= leftMost[0].Left))
			{
				leftMost[0] = display;
			}
			return leftMost[0];
		}

		public static Point CountAll()
		{
			Point count = new Point(2, 2);

			Display leftMost = LeftMost();
			Displays displays = new Displays(true);
			displays.Remove(leftMost);
			displays.OrderBy(display => display.Left);
			foreach (Display display in displays)
			{
				if (display.Right > leftMost.Right)
				{
					count.X++;
					if (display.Left >= leftMost.Right)
					{
						count.X++;
					}
					leftMost = display;
				}
			}

			Display topMost = TopMost();
			displays = new Displays(true);
			displays.Remove(topMost);
			displays.OrderBy(display => display.Top);
			foreach (Display display in displays)
			{
				if (display.Bottom > topMost.Bottom)
				{
					count.Y++;
					if (display.Top >= topMost.Bottom)
					{
						count.Y++;
						topMost = display;
					}
				}
			}

			return count;
		}

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

		#endregion Interface;

	}

	public class DisplayWindows
	{
		public Displays WindowsList { set; private get; }

		/// <summary>
		/// Add window to all displays;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void AddAll(IntPtr hwnd)
		{
			WindowsList.ForEach(display => display.Windows.Add(hwnd));
		}

		/// <summary>
		/// Move window on all displays;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void MoveAll(IntPtr hwnd)
		{
			WindowsList.ForEach(display => display.Windows.Move(hwnd));
		}

		/// <summary>
		/// Delete window on all displays;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void RemoveAll(IntPtr hwnd)
		{
			WindowsList.ForEach(display => display.Windows.Remove(hwnd));
		}
	}
}