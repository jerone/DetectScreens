﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace DetectScreens.Factory
{
	/// <summary>
	/// Class for a computer display;
	/// </summary>
	internal class Display : IDisposable
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
			if (!this._disposed)
			{
				if (disposing) { }

				this.Bounds = Rectangle.Empty;
				this.Left = this.Right = this.Top = this.Bottom = this.Width = this.Height = 0;
				this.Identifier = 0;
				this.Primairy = false;
				if (this.Windows != null && this.Windows.Count > 0)
				{
					this.Windows.Dispose();
				}

				this._disposed = true;
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
	internal class Displays : List<Display>, IDisposable
	{

		#region Fields & Properties;

		private Boolean _disposed = false;

		/// <summary>
		/// Scale of the displays;
		/// </summary>
		public Int32 Scale { get; set; }

		/// <summary>
		/// Display opacity;
		/// </summary>
		public Byte Opacity { get; set; }

		/// <summary>
		/// Display offset;
		/// </summary>
		public Point Offset { get; set; }

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
		/// <param name="addDesktop">Add desktop too</param>
		/// <param name="addTaskbar">Add taskbar too</param>
		public Displays(Boolean addDesktop, Boolean addTaskbar)
			: this(false, addDesktop, addTaskbar) { }
		/// <summary>
		/// List of all Displays;
		/// </summary>
		/// <param name="noWindows">Displays with or without Windows;</param>
		public Displays(Boolean noWindows)
			: this(noWindows, true, true) { }

		private Displays(Boolean noWindows, Boolean addDesktop, Boolean addTaskbar)
		{
			Offset = new Point(0, 0);
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
					Identifier = this.Count + 1,
					Primairy = screen.Primary

				};
				if (!noWindows)
				{
					display.Windows = new Windows(addDesktop, addTaskbar);
				}

				this.Add(display);
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
		public void Paint(Boolean paintDisplays, Boolean paintWindows)
		{
			Rectangle workingArea = WorkingArea();

			foreach (Display display in this)
			{
				if (paintDisplays)
				{
					Rectangle rectBorder = new Rectangle(
						((Math.Abs(workingArea.Left) + display.Left) / Scale) + Offset.X,
						((Math.Abs(workingArea.Top) + display.Top) / Scale) + Offset.Y,
						display.Width / Scale,
						display.Height / Scale);
					Rectangle rectInnerCircle = new Rectangle(
						rectBorder.X + ((rectBorder.Width / 2) - (350 / 2) / Scale),
						rectBorder.Y + ((rectBorder.Height / 2) - (350 / 2) / Scale),
						350 / Scale,
						350 / Scale);
					Rectangle rectOuterCircle = new Rectangle(
						rectBorder.X + ((rectBorder.Width / 2) - (420 / 2) / Scale),
						rectBorder.Y + ((rectBorder.Height / 2) - (420 / 2) / Scale),
						420 / Scale,
						420 / Scale);

					_graphics.DrawRectangle(new Pen(Color.Red), rectBorder);  // display border;

					GraphicsPath ellipseLine = new GraphicsPath { FillMode = FillMode.Alternate };
					ellipseLine.AddEllipse(rectOuterCircle);  // white circle border;
					ellipseLine.AddEllipse(rectInnerCircle);  // transparent middle circle;
					_graphics.FillPath(new SolidBrush(Color.FromArgb(195, 248, 248, 255)), ellipseLine);

					_graphics.FillEllipse(new SolidBrush(Color.FromArgb(137, 0, 0, 122)), rectInnerCircle);  // blue circle;

					Point numberOffset = new Point();
					// Number 1 looks weird when centering, a little to the left fixes this;
					if (display.Identifier == 1)
					{
						numberOffset.X = -10 / Scale;
					}

					_graphics.DrawText(  // number;
						display.Identifier.ToString(),
						new Point(rectBorder.X + (rectBorder.Width / 2) + numberOffset.X, rectBorder.Y + (rectBorder.Height / 2) + numberOffset.Y),
						new Font(new FontFamily("Arial"), 300 / Scale, FontStyle.Bold),
						Color.White);
				}

				if (paintWindows)
				{
					try  // removing this try-catch crashes Visual Studio :(
					{
						foreach (Window window in display.Windows)
						{
							if (window != null)
							{
								IntPtr handle = window.Handle;

								if (Handler != null && handle != Handler)  // don't make a thumbnail for this program;
								{
									if (!window.Unregister())  // unregister all thumbnails;
									{
										Debug.WriteLine(String.Format("ERROR: detroying thumbnail of window {0} ({1})!", window.Title, window.Handle));
									}

									if (handle == IntPtr.Zero || !Win32.IsWindow(handle))  // check if we should remove the thumbnail;
									{
										window.Dispose();
										continue;
									}

									Win32.RECT rect;
									Win32.GetWindowRect(handle, out rect);
									window.Bounds = rect.ToRectangle();
									if (window.Bounds.Y <= -30000)  // winamp partial fix;
									{
										window.Bounds.Y = 0;
									}

									Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
									placement.length = Marshal.SizeOf(placement);
									Win32.GetWindowPlacement(handle, ref placement);
									window.Maximized = placement.showCmd == Win32.SW.SHOWMAXIMIZED;
									if (window.Maximized)  // when window is maximized, the borders are still included;
									{
										//window.Bounds.X += SystemInformation.FrameBorderSize.Width;
										//window.Bounds.Y += SystemInformation.FrameBorderSize.Height;
										//window.Bounds.Width -= SystemInformation.FrameBorderSize.Width * 2;
										//window.Bounds.Height -= SystemInformation.FrameBorderSize.Height * 2;
										window.Bounds.Width -= SystemInformation.FrameBorderSize.Width;
										window.Bounds.Height -= SystemInformation.FrameBorderSize.Height;
									}

									if (display.HasWindow(window))  // If the display doesn't have the window, we don't need to create a thumbnail;
									{
										Rectangle windowPosition = window.Bounds;
										Rectangle windowIntersection = Rectangle.Intersect(windowPosition, display.Bounds);
										Rectangle offsetter = new Rectangle(0, 0, 0, windowIntersection.Height);

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

										if (Win32.IsWindow(handle))  // extra check if window isn't destroyed already;
										{
											IntPtr thumbNew;
											if (Win32.DwmRegisterThumbnail(Handler, handle, out thumbNew) == 0)  // draw thumbnail with succes;
											{
												if (thumbNew != IntPtr.Zero)  // needs to be filled;
												{
													Rectangle thumbnailSource = new Rectangle(
														offsetter.Left, offsetter.Top,
														offsetter.Right, offsetter.Bottom);
													Rectangle thumbnailDestination = new Rectangle(  // thumbnail destination needs to have the same width & height as the none-scaled window;
														((Math.Abs(workingArea.Left) + windowPosition.Left) / Scale) + Offset.X,
														((Math.Abs(workingArea.Top) + windowPosition.Top) / Scale) + Offset.Y,
														((Math.Abs(workingArea.Left) + windowPosition.Right) / Scale) + Offset.X,
														((Math.Abs(workingArea.Top) + windowPosition.Bottom) / Scale) + Offset.Y);
													Rectangle thumbnailSize = new Rectangle(
														thumbnailDestination.X,
														thumbnailDestination.Y,
														(windowPosition.Width / Scale) + Offset.X,
														(windowPosition.Height / Scale) + Offset.Y
													);

													Win32.DWM_THUMBNAIL_PROPERTIES props = new Win32.DWM_THUMBNAIL_PROPERTIES
													{
														dwFlags =
															Win32.DWM.TNP_OPACITY | Win32.DWM.TNP_VISIBLE |
															Win32.DWM.TNP_SOURCECLIENTAREAONLY |
															Win32.DWM.TNP_RECTDESTINATION | Win32.DWM.TNP_RECTSOURCE,
														opacity = Opacity,
														fVisible = true,
														fSourceClientAreaOnly = false,
														rcSource = thumbnailSource,
														rcDestination = thumbnailDestination
													};
													Win32.DwmUpdateThumbnailProperties(thumbNew, ref props);

													window.Thumb = thumbNew;

													if (!String.IsNullOrEmpty(window.Title) && window.Title.Contains("Calculator"))
													{
														_graphics.DrawRectangle(new Pen(Brushes.Green), thumbnailSize);
													}
												}
											}
										}
										else
										{
											Debug.WriteLine(String.Format("ERROR: window {0} ({1}) doesn't exist!", window.Title, window.Handle));
											window.Dispose();
										}
									}
								}
							}
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
			return this.FindAll(display => new Rectangle(display.Left, display.Top, display.Width, display.Height).IntersectsWith(window.Bounds));
			/*
			List<Display> list = new List<Display>();
			foreach (Display display in this)
			{
				Rectangle r = new Rectangle(display.Left, display.Top, display.Width, display.Height);
				if (r.IntersectsWith(window.Bounds))
				{
					list.Add(display);
				}
			}
			return list;*/
		}

		/// <summary>
		/// Add window to all dispays;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void WindowAdd(IntPtr hwnd)
		{
			this.ForEach(display => display.Windows.Add(hwnd));
		}

		/// <summary>
		/// Move window on all displays;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void WindowMove(IntPtr hwnd)
		{
			this.ForEach(display => display.Windows.Move(hwnd));
		}

		/// <summary>
		/// Remove window on all displays;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void WindowRemove(IntPtr hwnd)
		{
			this.ForEach(display => display.Windows.Remove(hwnd));
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
			String[] rtrn = new String[this.Count];
			Int32 i = 0;
			this.ForEach(delegate(Display d)
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

		/// <summary>
		/// Calculate compleet working area;
		/// </summary>
		/// <returns>Working area</returns>
		/// <remarks>Gaps between displays are also calculated</remarks>
		public static Rectangle WorkingArea()
		{
			Displays displays = new Displays(true);
			Rectangle area = new Rectangle(Int32.MaxValue, Int32.MaxValue, 0, 0);

			foreach (Display display in displays)
			{
				if (display.Left < area.Left)
				{
					area.X = display.Left;
					area.Width += Math.Abs(display.Left);
				}
				if (display.Top < area.Top)
				{
					area.Y = display.Top;
					area.Height += Math.Abs(display.Top);
				}
				if (display.Right > area.Right)
				{
					area.Width += display.Width;
				}
				if (display.Bottom > area.Bottom)
				{
					area.Height += display.Height;
				}
			}
			return area;
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
			if (!this._disposed)
			{
				if (disposing)
				{
					foreach (Display display in this)
					{
						display.Dispose();
					}
				}
				this.Scale = Int32.MinValue;
				this.Opacity = Byte.MinValue;
				this.Offset = Point.Empty;
				this.Handler = IntPtr.Zero;
				if (this.Graphics != null)
				{
					this.Graphics.Dispose();
				}

				this._disposed = true;
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
}