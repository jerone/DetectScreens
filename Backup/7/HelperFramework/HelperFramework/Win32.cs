using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace HelperFramework
{
	public class Win32
	{

		#region Value & Constant;

		public class GWL
		{
			public const int WNDPROC = (-4);
			public const int HINSTANCE = (-6);
			public const int HWNDPARENT = (-8);
			public const int STYLE = (-16);
			public const int EXSTYLE = (-20);
			public const int USERDATA = (-21);
			public const int ID = (-12);
		}

		public class DWM
		{
			public const int TNP_RECTDESTINATION = 0x00000001;
			public const int TNP_RECTSOURCE = 0x00000002;
			public const int TNP_OPACITY = 0x00000004;
			public const int TNP_VISIBLE = 0x00000008;
			public const int TNP_SOURCECLIENTAREAONLY = 0x00000010;
		}

		public class WS
		{
			public const uint OVERLAPPED = 0x00000000;
			public const uint POPUP = 0x80000000;
			public const uint CHILD = 0x40000000;
			public const uint MINIMIZE = 0x20000000;
			public const uint VISIBLE = 0x10000000;
			public const uint DISABLED = 0x08000000;
			public const uint CLIPSIBLINGS = 0x04000000;
			public const uint CLIPCHILDREN = 0x02000000;
			public const uint MAXIMIZE = 0x01000000;
			public const uint CAPTION = 0x00C00000;     /* BORDER | DLGFRAME  */
			public const uint BORDER = 0x00800000;
			public const uint DLGFRAME = 0x00400000;
			public const uint VSCROLL = 0x00200000;
			public const uint HSCROLL = 0x00100000;
			public const uint SYSMENU = 0x00080000;
			public const uint THICKFRAME = 0x00040000;
			public const uint GROUP = 0x00020000;
			public const uint TABSTOP = 0x00010000;

			public const uint MINIMIZEBOX = 0x00020000;
			public const uint MAXIMIZEBOX = 0x00010000;

			public const uint TILED = OVERLAPPED;
			public const uint ICONIC = MINIMIZE;
			public const uint SIZEBOX = THICKFRAME;
			public const uint TILEDWINDOW = OVERLAPPEDWINDOW;

			public const uint OVERLAPPEDWINDOW =
				(OVERLAPPED |
				 CAPTION |
				 SYSMENU |
				 THICKFRAME |
				 MINIMIZEBOX |
				 MAXIMIZEBOX);
			public const uint POPUPWINDOW =
				(POPUP |
				 BORDER |
				 SYSMENU);
			public const uint CHILDWINDOW = CHILD;
			public const ulong TARGETWINDOW = BORDER | VISIBLE;

			//Extended Window Styles
			public class EX
			{

				public const uint DLGMODALFRAME = 0x00000001;
				public const uint NOPARENTNOTIFY = 0x00000004;
				public const uint TOPMOST = 0x00000008;
				public const uint ACCEPTFILES = 0x00000010;
				public const uint TRANSPARENT = 0x00000020;

				//#if(WINVER >= 0x0400)
				public const uint MDICHILD = 0x00000040;
				public const uint TOOLWINDOW = 0x00000080;
				public const uint WINDOWEDGE = 0x00000100;
				public const uint CLIENTEDGE = 0x00000200;
				public const uint CONTEXTHELP = 0x00000400;

				public const uint RIGHT = 0x00001000;
				public const uint LEFT = 0x00000000;
				public const uint RTLREADING = 0x00002000;
				public const uint LTRREADING = 0x00000000;
				public const uint LEFTSCROLLBAR = 0x00004000;
				public const uint RIGHTSCROLLBAR = 0x00000000;

				public const uint CONTROLPARENT = 0x00010000;
				public const uint STATICEDGE = 0x00020000;
				public const uint APPWINDOW = 0x00040000;

				public const uint OVERLAPPEDWINDOW = (WINDOWEDGE | CLIENTEDGE);
				public const uint PALETTEWINDOW = (WINDOWEDGE | TOOLWINDOW | TOPMOST);
				//#endif /* WINVER >= 0x0400 */

				//#if(_WIN32_WINNT >= 0x0500)
				public const uint LAYERED = 0x00080000;
				//#endif /* _WIN32_WINNT >= 0x0500 */

				//#if(WINVER >= 0x0500)
				public const uint NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
				public const uint LAYOUTRTL = 0x00400000; // Right to left mirroring
				//#endif /* WINVER >= 0x0500 */

				//#if(_WIN32_WINNT >= 0x0500)
				public const uint COMPOSITED = 0x02000000;
				public const uint NOACTIVATE = 0x08000000;
				//#endif /* _WIN32_WINNT >= 0x0500 */
			}
		}

		/// <summary>
		/// ShellProc
		/// </summary>
		/// <remarks>http://msdn.microsoft.com/en-us/library/ms644991%28v=vs.85%29.aspx</remarks>
		public class HSHELL
		{
			public const int WINDOWCREATED = 1;
			public const int WINDOWDESTROYED = 2;
			public const int ACTIVATESHELLWINDOW = 3;
			public const int WINDOWACTIVATED = 4;
			public const int GETMINRECT = 5;
			public const int REDRAW = 6;
			public const int TASKMAN = 7;
			public const int LANGUAGE = 8;
			public const int SYSMENU = 9;
			public const int ENDTASK = 10;
			public const int ACCESSIBILITYSTATE = 11;
			public const int APPCOMMAND = 12;
			public const int WINDOWREPLACED = 13;
			public const int WINDOWREPLACING = 14;
			public const int HIGHBIT = 0x8000;
			public const int FLASH = (REDRAW | HIGHBIT);
			public const int RUDEAPPACTIVATED = WINDOWACTIVATED | HIGHBIT;  // WINDOWACTIVATED in fullscreen (or something);
		}

		/// <summary>
		/// Set Window Position Flags
		/// </summary>
		/// <remarks>http://www.pinvoke.net/default.aspx/user32/SetWindowPos.html</remarks>
		public class SW
		{
			public const UInt32 HIDE = 0;
			public const UInt32 SHOWNORMAL = 1;
			public const UInt32 NORMAL = 1;
			public const UInt32 SHOWMINIMIZED = 2;
			public const UInt32 SHOWMAXIMIZED = 3;
			public const UInt32 MAXIMIZE = 3;
			public const UInt32 SHOWNOACTIVATE = 4;
			public const UInt32 SHOW = 5;
			public const UInt32 MINIMIZE = 6;
			public const UInt32 SHOWMINNOACTIVE = 7;
			public const UInt32 SHOWNA = 8;
			public const UInt32 RESTORE = 9;
			public const UInt32 SHOWDEFAULT = 10;
			public const UInt32 FORCEMINIMIZE = 11;
			public const UInt32 MAX = 11;
		}

		public class SWP
		{
			public const UInt32 NOSIZE = 0x0001;
			public const UInt32 NOMOVE = 0x0002;
			public const UInt32 NOZORDER = 0x0004;
			public const UInt32 NOREDRAW = 0x0008;
			public const UInt32 NOACTIVATE = 0x0010;
			public const UInt32 FRAMECHANGED = 0x0020;
			public const UInt32 SHOWWINDOW = 0x0040;
			public const UInt32 HIDEWINDOW = 0x0080;
			public const UInt32 NOCOPYBITS = 0x0100;
			public const UInt32 NOOWNERZORDER = 0x0200;
			public const UInt32 NOSENDCHANGING = 0x0400;
		}

		/// <summary>
		/// Windows Messages
		/// </summary>
		/// <remarks>http://www.pinvoke.net/default.aspx/Constants/WM.html</remarks>
		public class WM
		{
			//...
			public const UInt32 ACTIVATE = 0x0006;

			public const UInt32 CHILDACTIVATE = 0x0022;

			public const UInt32 SIZING = 0x0214;

			public const UInt32 ENTERSIZEMOVE = 0x0231;
			public const UInt32 EXITSIZEMOVE = 0x0232;

			public const UInt32 WINDOWPOSCHANGING = 0x0046;
			public const UInt32 WINDOWPOSCHANGED = 0x0047;

			public const UInt32 SETCURSOR = 0x0020;
			public const UInt32 MOUSEACTIVATE = 0x0021;
			public const UInt32 MOUSEFIRST = 0x0200;
			public const UInt32 MOUSEHOVER = 0x02A1;
			public const UInt32 MOUSELAST = 0x020D;
			public const UInt32 MOUSELEAVE = 0x02A3;
			public const UInt32 MOUSEMOVE = 0x0200;
			public const UInt32 LBUTTONDOWN = 0x0201;
			public const UInt32 LBUTTONUP = 0x0202;
			public const UInt32 LBUTTONDBLCLK = 0x0203;
			public const UInt32 RBUTTONDOWN = 0x0204;
			public const UInt32 RBUTTONUP = 0x0205;
			public const UInt32 RBUTTONDBLCLK = 0x0206;
			public const UInt32 MBUTTONDOWN = 0x0207;
			public const UInt32 MBUTTONUP = 0x0208;
			public const UInt32 MBUTTONDBLCLK = 0x0209;
			public const UInt32 MOUSEWHEEL = 0x020A;
			public const UInt32 MOUSEHWHEEL = 0x020E;
			public const UInt32 MOVE = 0x0003;
			public const UInt32 MOVING = 0x0216;
			public const UInt32 NCACTIVATE = 0x0086;
			public const UInt32 NCCALCSIZE = 0x0083;
			public const UInt32 NCCREATE = 0x0081;
			public const UInt32 NCDESTROY = 0x0082;
			public const UInt32 NCHITTEST = 0x0084;
			public const UInt32 NCLBUTTONDBLCLK = 0x00A3;
			public const UInt32 NCLBUTTONDOWN = 0x00A1;
			public const UInt32 NCLBUTTONUP = 0x00A2;
			public const UInt32 NCMBUTTONDBLCLK = 0x00A9;
			public const UInt32 NCMBUTTONDOWN = 0x00A7;
			public const UInt32 NCMBUTTONUP = 0x00A8;
			public const UInt32 NCMOUSEMOVE = 0x00A0;
			public const UInt32 NCPAINT = 0x0085;
			public const UInt32 NCRBUTTONDBLCLK = 0x00A6;
			public const UInt32 NCRBUTTONDOWN = 0x00A4;
			public const UInt32 NCRBUTTONUP = 0x00A5;
			//...
		}

		/// <summary>
		/// Window HitTest Regions
		/// </summary>
		/// <remarks>http://www.pinvoke.net/default.aspx/Enums/WindowHitTestRegions.html</remarks>
		public class HT
		{
			public const int ERROR = -2;
			public const int TRANSPARENT = -1;
			public const int NOWHERE = 0;
			public const int CLIENT = 1;
			public const int CAPTION = 2;
			public const int SYSMENU = 3;
			public const int GROWBOX = 4;
			public const int MENU = 5;
			public const int HSCROLL = 6;
			public const int VSCROLL = 7;
			public const int MINBUTTON = 8;
			public const int MAXBUTTON = 9;
			public const int LEFT = 10;
			public const int RIGHT = 11;
			public const int TOP = 12;
			public const int TOPLEFT = 13;
			public const int TOPRIGHT = 14;
			public const int BOTTOM = 15;
			public const int BOTTOMLEFT = 16;
			public const int BOTTOMRIGHT = 17;
			public const int BORDER = 18;
			public const int OBJECT = 19;
			public const int CLOSE = 20;
			public const int HELP = 2;
		}

		public class SM
		{
			public const int CXMENUSIZE = 54;
		}


		#endregion Value & Constant;


		#region DWM P/Invoke;

		[DllImport("dwmapi.dll")]
		public static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern int DwmUnregisterThumbnail(IntPtr thumb);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out PSIZE size);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern int DwmUpdateThumbnailProperties(IntPtr thumb, ref DWM_THUMBNAIL_PROPERTIES props);

		#endregion DWM P/Invoke;


		#region Window P/Invoke;

		[DllImport("user32.dll", SetLastError = false)]
		public static extern IntPtr GetShellWindow();

		[DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
		public static extern ulong GetWindowLongA(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
		private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
		private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

		// This static method is required because Win32 does not support GetWindowLongPtr directly
		public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
		{
			return IntPtr.Size == 8 ? GetWindowLongPtr64(hWnd, nIndex) : GetWindowLongPtr32(hWnd, nIndex);
		}

		[DllImport("user32.dll")]
		public static extern int EnumWindows(EnumWindowsCallback lpEnumFunc, int lParam);
		public delegate bool EnumWindowsCallback(IntPtr hwnd, int lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		public static string GetWindowText(IntPtr hWnd)
		{
			int length = GetWindowTextLength(hWnd);
			StringBuilder sb = new StringBuilder(length + 1);
			GetWindowText(hWnd, sb, sb.Capacity);
			return sb.ToString();
		}

		[DllImport("kernel32.dll")]
		public static extern uint WinExec(string lpCmdLine, uint uCmdShow);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

		#endregion Window P/Invoke;


		#region Hook P/Invoke;

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int RegisterShellHookWindow(IntPtr hwnd);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool DeregisterShellHookWindow(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int RegisterWindowMessage(string name);

		[DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		#endregion Hooks P/Invoke;


		#region Glass P/Invoke;

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, MARGINS pMargins);
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, Rectangle pMargins);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern bool DwmIsCompositionEnabled();

		#endregion Glass P/Invoke;


		#region System P/Invoke;

		[DllImport("user32.dll")]
		public static extern int GetSystemMetrics(int smIndex);

		#endregion System P/Invoke;


		#region Interop Struct;

		public struct WINDOWPLACEMENT
		{
			public int length;
			public int flags;
			public int showCmd;
			public Point ptMinPosition;
			public Point ptMaxPosition;
			public Rectangle rcNormalPosition;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct DWM_THUMBNAIL_PROPERTIES
		{
			public int dwFlags;
			public Rectangle rcDestination;
			public Rectangle rcSource;
			public byte opacity;
			public bool fVisible;
			public bool fSourceClientAreaOnly;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left, Top, Right, Bottom;

			public int Width
			{
				get
				{
					return Right - Left;
				}
				set
				{
					Right = Left + value;
				}
			}
			public int Height
			{
				get
				{
					return Bottom - Top;
				}
				set
				{
					Bottom = Top + value;
				}
			}

			public RECT(int left, int top, int right, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			public RECT(Rectangle rect)
			{
				Left = rect.X;
				Top = rect.Y;
				Right = rect.Right;
				Bottom = rect.Bottom;
			}

			public Rectangle ToRectangle()
			{
				return new Rectangle(Left, Top, Right - Left, Bottom - Top);
			}
			public override String ToString()
			{
				return ToRectangle().ToString();
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PSIZE
		{
			public int x, y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public class MARGINS
		{
			public int Left, Right, Top, Bottom;

			public MARGINS(int left, int right, int top, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}
		}

		#endregion Interop Structs;

	}
}
