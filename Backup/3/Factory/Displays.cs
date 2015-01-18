using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Diagnostics;

using DetectScreens.Controls;
using DetectScreens.Factory;
using DetectScreens.Properties;
using DetectScreens.Store;


namespace DetectScreens.Factory
{
	/// <summary>
	/// Class for a computer display;
	/// </summary>
	internal class Display
	{
		private Boolean disposed = false;

		private Int32 left = 0;
		public Int32 Left
		{
			get
			{
				return left;
			}
			set
			{
				left = value;
				Preview.Left = left;
			}
		}
		private Int32 right = 0;
		public Int32 Right
		{
			get
			{
				return right;
			}
			set
			{
				right = value;
				//Preview.Right = right;
			}
		}
		private Int32 top = 0;
		public Int32 Top
		{
			get
			{
				return top;
			}
			set
			{
				top = value;
				Preview.Top = top;
			}
		}
		private Int32 bottom = 0;
		public Int32 Bottom
		{
			get
			{
				return bottom;
			}
			set
			{
				bottom = value;
				//Preview.Bottom = bottom;
			}
		}
		private Int32 width = 0;
		public Int32 Width
		{
			get
			{
				return width = Right - Left;
			}
			set
			{
				width = value;
				Right = Left + width;
				Preview.Width = width;
			}
		}
		private Int32 height = 0;
		public Int32 Height
		{
			get
			{
				return height = Bottom - Top;
			}
			set
			{
				height = value;
				Bottom = top + height;
				Preview.Height = height;
			}
		}
		private Rectangle bounds = new Rectangle();
		public Rectangle Bounds
		{
			get
			{
				return bounds = new Rectangle(Left, Top, Width, Height);
			}
			set
			{
				bounds = value;
				Left = bounds.Left;
				Top = bounds.Top;
				Width = bounds.Width;
				Height = bounds.Height;
			}
		}

		private Int32 number = 1;
		public Int32 Number
		{
			get
			{
				return number;
			}
			set
			{
				number = value;
			}
		}
		private Boolean primairy = false;
		public Boolean Primairy
		{
			get
			{
				return primairy;
			}
			set
			{
				primairy = value;
			}
		}

		public frm_Identify Preview;  // preview form;

		public Windows windows;

		public Display()
		{
			Preview = new frm_Identify();
			Preview.Bounds = new Rectangle(left, top, width, height);
		}

		public Boolean isOn(Window window)
		{
			return Bounds.IntersectsWith(window.Bounds);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing) { }

				this.Left = this.Right = this.Top = this.Bottom = this.Width = this.Height = 0;
				this.Bounds = Rectangle.Empty;
				this.Number = 0;
				this.Primairy = false;
				this.Preview.Dispose();

				this.disposed = true;
			}
		}
		~Display()
		{
			Dispose(false);
		}
	}

	/// <summary>
	/// List of all display's;
	/// </summary>
	internal class Displays
	{
		private Boolean disposed = false;

		private Int32 scale = 1;
		public Int32 Scale
		{
			get { return scale; }
			set { scale = value; }
		}
		private Byte opacity = 255;
		public Byte Opacity
		{
			get { return opacity; }
			set { opacity = value; }
		}
		private Point offset = new Point(0,0);
		public Point Offset
		{
			get { return offset; }
			set { offset = value; }
		}
		private IntPtr handler;
		public IntPtr Handler
		{
			get { return handler; }
			set { handler = value; }
		}
		private Graphics graphics;
		public Graphics Graphics
		{
			get { return graphics; }
			set { graphics = value; }
		}


		public List<Display> All;

		public Displays()
		{
			All = new List<Display>();

			List<Window> windows = new Windows().All;

			foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
			{
				Display display = new Display();
				display.Top = screen.Bounds.Top;
				display.Right = screen.Bounds.Right;
				display.Bottom = screen.Bounds.Bottom;
				display.Left = screen.Bounds.Left;
				display.Number = All.Count + 1;
				display.Primairy = screen.Primary;

				display.windows = new Windows();

				display.Preview.Nr = display.Number.ToString();

				All.Add(display);
			}
		}

		public void Paint()
		{
			Paint(true, true);
		}
		public void Paint(Boolean paintDisplays, Boolean paintWindows)
		{
			Rectangle workingArea = WorkingArea();

			if (paintDisplays)
			{
				graphics.Clear(Color.Transparent);  // empty;

				graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;  // smoother borders;
			}
			foreach (Display display in All)
			{

				if (paintDisplays)
				{
					Rectangle rectBorder = new Rectangle(
						((Math.Abs(workingArea.Left) + display.Left) / scale) + offset.X,
						((Math.Abs(workingArea.Top) + display.Top) / scale) + offset.Y,
						display.Width / scale,
						display.Height / scale);
					Rectangle rectInnerCircle = new Rectangle(
						rectBorder.X + ((rectBorder.Width / 2) - (350 / 2) / scale),
						rectBorder.Y + ((rectBorder.Height / 2) - (350 / 2) / scale),
						350 / scale,
						350 / scale);
					Rectangle rectOuterCircle = new Rectangle(
						rectBorder.X + ((rectBorder.Width / 2) - (420 / 2) / scale),
						rectBorder.Y + ((rectBorder.Height / 2) - (420 / 2) / scale),
						420 / scale,
						420 / scale);

					graphics.DrawRectangle(new Pen(Color.Red), rectBorder);  // display border;

					GraphicsPath ellipseLine = new GraphicsPath();
					ellipseLine.FillMode = FillMode.Alternate;
					ellipseLine.AddEllipse(rectOuterCircle);  // white circle border;
					ellipseLine.AddEllipse(rectInnerCircle);  // transparent middle circle;
					graphics.FillPath(new SolidBrush(Color.FromArgb(195, 248, 248, 255)), ellipseLine);

					graphics.FillEllipse(new SolidBrush(Color.FromArgb(137, 0, 0, 122)), rectInnerCircle);  // blue circle;

					graphics.DrawText(  // number;
						display.Number.ToString(),
						new Point(rectBorder.X + (rectBorder.Width / 2), rectBorder.Y + (rectBorder.Height / 2)),
						new Font(new FontFamily("Arial"), 300 / scale, FontStyle.Bold),
						Color.White);
				}

				if (paintWindows)
				{
					try  // removing this try-catch crashes Visual Studio :(
					{
						foreach (Window window in display.windows.All)
						{
							if (window != null)
							{
								IntPtr handle = window.Handle;

								if (handler != null && handle != handler)  // don't make a thumbnail for this program;
								{
									if (!window.Unregister())  // unregister all thumbnails;
									{
										Debug.WriteLine(String.Format("ERROR: detroying thumbnail of window {0} ({1})!", window.Title, window.Handle));
									}

									if (handle == IntPtr.Zero || !Win32.IsWindow(handle))  // check if we should remove the thumbnail;
									{
										window.Dispose();
										break;
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
									if (placement.showCmd == Win32.SW.SHOWMAXIMIZED)  // when window is maximized, the borders are still included;
									{
										window.Bounds.X += SystemInformation.FrameBorderSize.Width;
										window.Bounds.Y += SystemInformation.FrameBorderSize.Height;
										window.Bounds.Width -= SystemInformation.FrameBorderSize.Width * 2;
										window.Bounds.Height -= SystemInformation.FrameBorderSize.Height * 2;
									}

									if (display.isOn(window))
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
											IntPtr thumbNew=Win32.DwmRegisterThumbnail(handler, handle);//, out thumbNew) == 0)  // draw thumbnail with succes;
											
												if (thumbNew != IntPtr.Zero)  // needs to be filled;
												{
													Rectangle thumbnailSource = new Rectangle(
														offsetter.Left, offsetter.Top,
														offsetter.Right, offsetter.Bottom);
													Rectangle thumbnailDestination = new Rectangle(
														(((Math.Abs(workingArea.Left) + windowPosition.Left) / scale) + 0) + offset.X,
														(((Math.Abs(workingArea.Top) + windowPosition.Top) / scale) + 0) + offset.Y,
														(((Math.Abs(workingArea.Left) + windowPosition.Right) / scale) + 0) + offset.X,
														(((Math.Abs(workingArea.Top) + windowPosition.Bottom) / scale) + 0) + offset.Y);

													Win32.DWM_THUMBNAIL_PROPERTIES props = new Win32.DWM_THUMBNAIL_PROPERTIES();
													props.dwFlags = Win32.DWM.TNP_OPACITY | Win32.DWM.TNP_VISIBLE | Win32.DWM.TNP_SOURCECLIENTAREAONLY | Win32.DWM.TNP_RECTDESTINATION | Win32.DWM.TNP_RECTSOURCE;
													props.opacity = opacity;
													props.fVisible = true;
													props.fSourceClientAreaOnly = false;
													props.rcSource = thumbnailSource;
													props.rcDestination = thumbnailDestination;
													Win32.DwmUpdateThumbnailProperties(thumbNew, props);

													window.Thumb = thumbNew;

									if (window.Title.Contains("Mozilla Firefox"))
									{
										//graphics.FillRectangle(Brushes.Green, thumbnailDestination);
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

		public override string ToString()
		{
			String[] rtrn = new String[All.Count];
			Int32 i = 0;
			All.ForEach(delegate(Display d)
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
			//return base.ToString();
		}

		public static Rectangle WorkingArea()
		{
			Displays displays = new Displays();
			Rectangle area = new Rectangle(Int32.MaxValue, Int32.MaxValue, 0, 0);

			foreach (Display display in displays.All)
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

		public List<Display> IntersectDisplay(Window w)
		{
			List<Display> list = new List<Display>();
			foreach (Display display in All)
			{
				Rectangle r = new Rectangle(display.Left, display.Top, display.Width, display.Height);
				if (r.IntersectsWith(w.Bounds))
				{
					list.Add(display);
				}
			}
			return list;
		}

		public void WindowAdd(IntPtr hwnd)
		{
			foreach (Display display in All)
			{
				display.windows.Add(hwnd);
			}
		}
		public void WindowMove(IntPtr hwnd)
		{
			foreach (Display display in All)
			{
				display.windows.Move(hwnd);
			}
		}
		public void WindowRemove(IntPtr hwnd)
		{
			foreach (Display display in All)
			{
				display.windows.Remove(hwnd);
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					foreach (Display display in this.All)
					{
						display.windows.Dispose();
						display.Dispose();
					}
				}

				this.All = null;

				this.disposed = true;
			}
		}
		~Displays()
		{
			Dispose(false);
		}
	}
}