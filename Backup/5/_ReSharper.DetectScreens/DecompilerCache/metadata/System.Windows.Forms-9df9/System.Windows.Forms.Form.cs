// Type: System.Windows.Forms.Form
// Assembly: System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Windows.Forms.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>
	/// Represents a window or dialog box that makes up an application's user interface.
	/// </summary>
	/// <filterpriority>1</filterpriority>
	[Designer(
		"System.Windows.Forms.Design.FormDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
		, typeof (IRootDesigner))]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ToolboxItemFilter("System.Windows.Forms.Control.TopLevel")]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[ComVisible(true)]
	[DesignerCategory("Form")]
	[DefaultEvent("Load")]
	[InitializationEvent("Load")]
	public class Form : ContainerControl
	{
		static Form();

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Windows.Forms.Form"/> class.
		/// </summary>
		public Form();

		/// <summary>
		/// Gets or sets the button on the form that is clicked when the user presses the ENTER key.
		/// </summary>
		/// 
		/// <returns>
		/// An <see cref="T:System.Windows.Forms.IButtonControl"/> that represents the button to use as the accept button for the form.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		[SRDescription("FormAcceptButtonDescr")]
		[DefaultValue(null)]
		public IButtonControl AcceptButton { get; set; }

		/// <summary>
		/// Gets the currently active form for this application.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.Form"/> that represents the currently active form, or null if there is no active form.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public static Form ActiveForm { get; }

		/// <summary>
		/// Gets the currently active multiple-document interface (MDI) child window.
		/// </summary>
		/// 
		/// <returns>
		/// Returns a <see cref="T:System.Windows.Forms.Form"/> that represents the currently active MDI child window, or null if there are currently no child windows present.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		[SRDescription("FormActiveMDIChildDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Form ActiveMdiChild { get; }

		/// <summary>
		/// Gets or sets a value indicating whether the opacity of the form can be adjusted.
		/// </summary>
		/// 
		/// <returns>
		/// true if the opacity of the form can be changed; otherwise, false.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[SRDescription("ControlAllowTransparencyDescr")]
		public bool AllowTransparency { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the form adjusts its size to fit the height of the font used on the form and scales its controls.
		/// </summary>
		/// 
		/// <returns>
		/// true if the form will automatically scale itself and its controls based on the current font assigned to the form; otherwise, false. The default is true.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		[SRDescription("FormAutoScaleDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SRCategory("CatLayout")]
		[Obsolete(
			"This property has been deprecated. Use the AutoScaleMode property instead.  http://go.microsoft.com/fwlink/?linkid=14202"
			)]
		[Browsable(false)]
		public bool AutoScale { get; set; }

		/// <summary>
		/// Gets or sets the base size used for autoscaling of the form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that represents the base size that this form uses for autoscaling.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual Size AutoScaleBaseSize { get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		set; }

		/// <summary>
		/// Gets or sets a value indicating whether the form enables autoscrolling.
		/// </summary>
		/// 
		/// <returns>
		/// true to enable autoscrolling on the form; otherwise, false. The default is false.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[Localizable(true)]
		public override bool AutoScroll { get; set; }

		/// <summary>
		/// Resize the form according to the setting of <see cref="P:System.Windows.Forms.Form.AutoSizeMode"/>.
		/// </summary>
		/// 
		/// <returns>
		/// true if the form will automatically resize; false if it must be manually resized.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize { get; set; }

		/// <summary>
		/// Gets or sets the mode by which the form automatically resizes itself.
		/// </summary>
		/// 
		/// <returns>
		/// An <see cref="T:System.Windows.Forms.AutoSizeMode"/> enumerated value. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly"/>.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a valid <see cref="T:System.Windows.Forms.AutoSizeMode"/> value.</exception>
		[Browsable(true)]
		[Localizable(true)]
		[SRCategory("CatLayout")]
		[DefaultValue(1)]
		[SRDescription("ControlAutoSizeModeDescr")]
		public AutoSizeMode AutoSizeMode { get; set; }

		/// <returns>
		/// An <see cref="T:System.Windows.Forms.AutoValidate"/> enumerated value that indicates whether contained controls are implicitly validated on focus change. The default is <see cref="F:System.Windows.Forms.AutoValidate.Inherit"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		public override AutoValidate AutoValidate { get; set; }

		/// <returns>
		/// A <see cref="T:System.Drawing.Color"/> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor"/> property.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public override Color BackColor { get; set; }

		/// <summary>
		/// Gets or sets the border style of the form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.FormBorderStyle"/> that represents the style of border to display for the form. The default is FormBorderStyle.Sizable.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatAppearance")]
		[DefaultValue(4)]
		[DispId(-504)]
		[SRDescription("FormBorderStyleDescr")]
		public FormBorderStyle FormBorderStyle { get; set; }

		/// <summary>
		/// Gets or sets the button control that is clicked when the user presses the ESC key.
		/// </summary>
		/// 
		/// <returns>
		/// An <see cref="T:System.Windows.Forms.IButtonControl"/> that represents the cancel button for the form.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		[DefaultValue(null)]
		[SRDescription("FormCancelButtonDescr")]
		public IButtonControl CancelButton { get; set; }

		/// <summary>
		/// Gets or sets the size of the client area of the form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that represents the size of the form's client area.
		/// </returns>
		/// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new Size ClientSize { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether a control box is displayed in the caption bar of the form.
		/// </summary>
		/// 
		/// <returns>
		/// true if the form displays a control box in the upper left corner of the form; otherwise, false. The default is true.
		/// </returns>
		/// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatWindowStyle")]
		[DefaultValue(true)]
		[SRDescription("FormControlBoxDescr")]
		public bool ControlBox { get; set; }

		/// <returns>
		/// A <see cref="T:System.Windows.Forms.CreateParams"/> that contains the required creation parameters when the handle to the control is created.
		/// </returns>
		protected override CreateParams CreateParams { get; }

		/// <summary>
		/// Gets the default Input Method Editor (IME) mode supported by the control.
		/// </summary>
		/// 
		/// <returns>
		/// One of the <see cref="T:System.Windows.Forms.ImeMode"/> values.
		/// </returns>
		protected override ImeMode DefaultImeMode { get; }

		/// <returns>
		/// The default <see cref="T:System.Drawing.Size"/> of the control.
		/// </returns>
		protected override Size DefaultSize { get; }

		/// <summary>
		/// Gets or sets the size and location of the form on the Windows desktop.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Drawing.Rectangle"/> that represents the bounds of the form on the Windows desktop using desktop coordinates.
		/// </returns>
		/// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRDescription("FormDesktopBoundsDescr")]
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Rectangle DesktopBounds { get; set; }

		/// <summary>
		/// Gets or sets the location of the form on the Windows desktop.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Drawing.Point"/> that represents the location of the form on the desktop.
		/// </returns>
		/// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormDesktopLocationDescr")]
		[SRCategory("CatLayout")]
		[Browsable(false)]
		public Point DesktopLocation { get; set; }

		/// <summary>
		/// Gets or sets the dialog result for the form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.DialogResult"/> that represents the result of the form when used as a dialog box.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception><filterpriority>1</filterpriority>
		[Browsable(false)]
		[SRDescription("FormDialogResultDescr")]
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DialogResult DialogResult { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether a Help button should be displayed in the caption box of the form.
		/// </summary>
		/// 
		/// <returns>
		/// true to display a Help button in the form's caption bar; otherwise, false. The default is false.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatWindowStyle")]
		[DefaultValue(false)]
		[SRDescription("FormHelpButtonDescr")]
		public bool HelpButton { get; set; }

		/// <summary>
		/// Gets or sets the icon for the form.
		/// </summary>
		/// 
		/// <returns>
		/// An <see cref="T:System.Drawing.Icon"/> that represents the icon for the form.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatWindowStyle")]
		[Localizable(true)]
		[SRDescription("FormIconDescr")]
		[AmbientValue(null)]
		public Icon Icon { get; set; }

		/// <summary>
		/// Gets a value indicating whether the form is a multiple-document interface (MDI) child form.
		/// </summary>
		/// 
		/// <returns>
		/// true if the form is an MDI child form; otherwise, false.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		[SRDescription("FormIsMDIChildDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		public bool IsMdiChild { get; }

		/// <summary>
		/// Gets or sets a value indicating whether the form is a container for multiple-document interface (MDI) child forms.
		/// </summary>
		/// 
		/// <returns>
		/// true if the form is a container for MDI child forms; otherwise, false. The default is false.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatWindowStyle")]
		[DefaultValue(false)]
		[SRDescription("FormIsMDIContainerDescr")]
		public bool IsMdiContainer { get; set; }

		/// <summary>
		/// Gets a value indicating whether the form can use all windows and user input events without restriction.
		/// </summary>
		/// 
		/// <returns>
		/// true if the form has restrictions; otherwise, false. The default is true.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsRestrictedWindow { get; }

		/// <summary>
		/// Gets or sets a value indicating whether the form will receive key events before the event is passed to the control that has focus.
		/// </summary>
		/// 
		/// <returns>
		/// true if the form will receive all key events; false if the currently selected control on the form receives key events. The default is false.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		[DefaultValue(false)]
		[SRDescription("FormKeyPreviewDescr")]
		public bool KeyPreview { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:System.Drawing.Point"/> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.Form"/> in screen coordinates.
		/// </summary>
		/// 
		/// <returns>
		/// The <see cref="T:System.Drawing.Point"/> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.Form"/> in screen coordinates.
		/// </returns>
		[SettingsBindable(true)]
		public new Point Location { get; set; }

		/// <summary>
		/// Gets and sets the size of the form when it is maximized.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Drawing.Rectangle"/> that represents the bounds of the form when it is maximized.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Drawing.Rectangle.Top"/> property is greater than the height of the form.-or- The value of the <see cref="P:System.Drawing.Rectangle.Left"/> property is greater than the width of the form. </exception>
		protected Rectangle MaximizedBounds { get; set; }

		/// <summary>
		/// Gets the maximum size the form can be resized to.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that represents the maximum size for the form.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size"/> object are less than zero. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatLayout")]
		[DefaultValue(typeof (Size), "0, 0")]
		[SRDescription("FormMaximumSizeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Localizable(true)]
		public override Size MaximumSize { get; set; }

		/// <summary>
		/// Gets or sets the primary menu container for the form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.MenuStrip"/> that represents the container for the menu structure of the form. The default is null.
		/// </returns>
		[SRCategory("CatWindowStyle")]
		[DefaultValue(null)]
		[SRDescription("FormMenuStripDescr")]
		[TypeConverter(typeof (ReferenceConverter))]
		public MenuStrip MainMenuStrip { get; set; }

		/// <summary>
		/// Gets or sets the space between controls.
		/// </summary>
		/// 
		/// <returns>
		/// A value that represents the space between controls.
		/// </returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Padding Margin { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:System.Windows.Forms.MainMenu"/> that is displayed in the form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.MainMenu"/> that represents the menu to display in the form.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DefaultValue(null)]
		[SRDescription("FormMenuDescr")]
		[TypeConverter(typeof (ReferenceConverter))]
		public MainMenu Menu { get; set; }

		/// <summary>
		/// Gets or sets the minimum size the form can be resized to.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that represents the minimum size for the form.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size"/> object are less than zero. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[Localizable(true)]
		[SRCategory("CatLayout")]
		[SRDescription("FormMinimumSizeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public override Size MinimumSize { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the Maximize button is displayed in the caption bar of the form.
		/// </summary>
		/// 
		/// <returns>
		/// true to display a Maximize button for the form; otherwise, false. The default is true.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[DefaultValue(true)]
		[SRDescription("FormMaximizeBoxDescr")]
		[SRCategory("CatWindowStyle")]
		public bool MaximizeBox { get; set; }

		/// <summary>
		/// Gets an array of forms that represent the multiple-document interface (MDI) child forms that are parented to this form.
		/// </summary>
		/// 
		/// <returns>
		/// An array of <see cref="T:System.Windows.Forms.Form"/> objects, each of which identifies one of this form's MDI child forms.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		[Browsable(false)]
		[SRDescription("FormMDIChildrenDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatWindowStyle")]
		public Form[] MdiChildren { get; }

		/// <summary>
		/// Gets or sets the current multiple-document interface (MDI) parent form of this form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.Form"/> that represents the MDI parent form.
		/// </returns>
		/// <exception cref="T:System.Exception">The <see cref="T:System.Windows.Forms.Form"/> assigned to this property is not marked as an MDI container.-or- The <see cref="T:System.Windows.Forms.Form"/> assigned to this property is both a child and an MDI container form.-or- The <see cref="T:System.Windows.Forms.Form"/> assigned to this property is located on a different thread. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRDescription("FormMDIParentDescr")]
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Form MdiParent { get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		set; }

		/// <summary>
		/// Gets the merged menu for the form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.MainMenu"/> that represents the merged menu of the form.
		/// </returns>
		/// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("FormMergedMenuDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MainMenu MergedMenu { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		get; }

		/// <summary>
		/// Gets or sets a value indicating whether the Minimize button is displayed in the caption bar of the form.
		/// </summary>
		/// 
		/// <returns>
		/// true to display a Minimize button for the form; otherwise, false. The default is true.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[DefaultValue(true)]
		[SRDescription("FormMinimizeBoxDescr")]
		[SRCategory("CatWindowStyle")]
		public bool MinimizeBox { get; set; }

		/// <summary>
		/// Gets a value indicating whether this form is displayed modally.
		/// </summary>
		/// 
		/// <returns>
		/// true if the form is displayed modally; otherwise, false.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		[SRDescription("FormModalDescr")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatWindowStyle")]
		public bool Modal { get; }

		/// <summary>
		/// Gets or sets the opacity level of the form.
		/// </summary>
		/// 
		/// <returns>
		/// The level of opacity for the form. The default is 1.00.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[DefaultValue(1)]
		[SRCategory("CatWindowStyle")]
		[TypeConverter(typeof (OpacityConverter))]
		[SRDescription("FormOpacityDescr")]
		public double Opacity { get; set; }

		/// <summary>
		/// Gets an array of <see cref="T:System.Windows.Forms.Form"/> objects that represent all forms that are owned by this form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.Form"/> array that represents the owned forms for this form.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormOwnedFormsDescr")]
		[SRCategory("CatWindowStyle")]
		public Form[] OwnedForms { get; }

		/// <summary>
		/// Gets or sets the form that owns this form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.Form"/> that represents the form that is the owner of this form.
		/// </returns>
		/// <exception cref="T:System.Exception">A top-level window cannot have an owner. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRDescription("FormOwnerDescr")]
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Form Owner { get; set; }

		/// <summary>
		/// Gets the location and size of the form in its normal window state.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Drawing.Rectangle"/> that contains the location and size of the form in the normal window state.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Rectangle RestoreBounds { get; }

		/// <summary>
		/// Gets or sets a value indicating whether right-to-left mirror placement is turned on.
		/// </summary>
		/// 
		/// <returns>
		/// true if right-to-left mirror placement is turned on; otherwise, false for standard child control placement. The default is false.
		/// </returns>
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("ControlRightToLeftLayoutDescr")]
		[SRCategory("CatAppearance")]
		public virtual bool RightToLeftLayout { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the form is displayed in the Windows taskbar.
		/// </summary>
		/// 
		/// <returns>
		/// true to display the form in the Windows taskbar at run time; otherwise, false. The default is true.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormShowInTaskbarDescr")]
		[DefaultValue(true)]
		public bool ShowInTaskbar { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether an icon is displayed in the caption bar of the form.
		/// </summary>
		/// 
		/// <returns>
		/// true if the form displays an icon in the caption bar; otherwise, false. The default is true.
		/// </returns>
		[SRDescription("FormShowIconDescr")]
		[DefaultValue(true)]
		[SRCategory("CatWindowStyle")]
		public bool ShowIcon { get; set; }

		/// <summary>
		/// Gets a value indicating whether the window will be activated when it is shown.
		/// </summary>
		/// 
		/// <returns>
		/// True if the window will not be activated when it is shown; otherwise, false. The default is false.
		/// </returns>
		[Browsable(false)]
		protected virtual bool ShowWithoutActivation { get; }

		/// <summary>
		/// Gets or sets the size of the form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that represents the size of the form.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size Size { get; set; }

		/// <summary>
		/// Gets or sets the style of the size grip to display in the lower-right corner of the form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.SizeGripStyle"/> that represents the style of the size grip to display. The default is <see cref="F:System.Windows.Forms.SizeGripStyle.Auto"/>
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatWindowStyle")]
		[DefaultValue(0)]
		[SRDescription("FormSizeGripStyleDescr")]
		public SizeGripStyle SizeGripStyle { get; set; }

		/// <summary>
		/// Gets or sets the starting position of the form at run time.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.FormStartPosition"/> that represents the starting position of the form.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception><filterpriority>2</filterpriority>
		[DefaultValue(2)]
		[SRDescription("FormStartPositionDescr")]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		public FormStartPosition StartPosition { get; set; }

		/// <summary>
		/// Gets or sets the tab order of the control within its container.
		/// </summary>
		/// 
		/// <returns>
		/// An <see cref="T:System.Int32"/> containing the index of the control within the set of controls within its container that is included in the tab order.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int TabIndex { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
		/// </summary>
		/// 
		/// <returns>
		/// true if the user can give the focus to the control using the TAB key; otherwise, false. The default is true.
		/// </returns>
		/// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DispId(-516)]
		[SRDescription("ControlTabStopDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		public new bool TabStop { get; set; }

		/// <returns>
		/// The text associated with this control.
		/// </returns>
		[SettingsBindable(true)]
		public override string Text { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to display the form as a top-level window.
		/// </summary>
		/// 
		/// <returns>
		/// true to display the form as a top-level window; otherwise, false. The default is true.
		/// </returns>
		/// <exception cref="T:System.Exception">A Multiple-document interface (MDI) parent form must be a top-level window. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public bool TopLevel { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the form should be displayed as a topmost form.
		/// </summary>
		/// 
		/// <returns>
		/// true to display the form as a topmost form; otherwise, false. The default is false.
		/// </returns>
		/// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormTopMostDescr")]
		[DefaultValue(false)]
		public bool TopMost { get; set; }

		/// <summary>
		/// Gets or sets the color that will represent transparent areas of the form.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Drawing.Color"/> that represents the color to display transparently on the form.
		/// </returns>
		/// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRDescription("FormTransparencyKeyDescr")]
		[SRCategory("CatWindowStyle")]
		public Color TransparencyKey { get; set; }

		/// <summary>
		/// Gets or sets the form's window state.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.FormWindowState"/> that represents the window state of the form. The default is FormWindowState.Normal.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[SRCategory("CatLayout")]
		[DefaultValue(0)]
		[SRDescription("FormWindowStateDescr")]
		public FormWindowState WindowState { get; set; }

		/// <param name="value">true to make the control visible; otherwise, false. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetVisibleCore(bool value);

		/// <summary>
		/// Activates the form and gives it focus.
		/// </summary>
		/// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public void Activate();

		/// <summary>
		/// Activates the MDI child of a form.
		/// </summary>
		/// <param name="form">The child form to activate.</param>
		protected void ActivateMdiChild(Form form);

		/// <summary>
		/// Adds an owned form to this form.
		/// </summary>
		/// <param name="ownedForm">The <see cref="T:System.Windows.Forms.Form"/> that this form will own. </param><filterpriority>1</filterpriority>
		public void AddOwnedForm(Form ownedForm);

		/// <summary>
		/// Adjusts the scroll bars on the container based on the current control positions and the control currently selected.
		/// </summary>
		/// <param name="displayScrollbars">true to show the scroll bars; otherwise, false. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void AdjustFormScrollbars(bool displayScrollbars);

		/// <summary>
		/// Resizes the form according to the current value of the <see cref="P:System.Windows.Forms.Form.AutoScaleBaseSize"/> property and the size of the current font.
		/// </summary>
		[Obsolete(
			"This method has been deprecated. Use the ApplyAutoScaling method instead.  http://go.microsoft.com/fwlink/?linkid=14202"
			)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void ApplyAutoScaling();

		/// <summary>
		/// Closes the form.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">The form was closed while a handle was being created. </exception><exception cref="T:System.ObjectDisposedException">You cannot call this method from the <see cref="E:System.Windows.Forms.Form.Activated"/> event when <see cref="P:System.Windows.Forms.Form.WindowState"/> is set to <see cref="F:System.Windows.Forms.FormWindowState.Maximized"/>.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public void Close();

		/// <returns>
		/// A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection"/> assigned to the control.
		/// </returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Control.ControlCollection CreateControlsInstance();

		/// <summary>
		/// Creates the handle for the form. If a derived class overrides this function, it must call the base implementation.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void CreateHandle();

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"/> to process. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void DefWndProc(ref Message m);

		/// <summary>
		/// Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.Form"/>.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		protected override void Dispose(bool disposing);

		/// <summary>
		/// Gets the size when autoscaling the form based on a specified font.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.Drawing.SizeF"/> representing the autoscaled size of the form.
		/// </returns>
		/// <param name="font">A <see cref="T:System.Drawing.Font"/> representing the font to determine the autoscaled base size of the form. </param><filterpriority>2</filterpriority>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete(
			"This method has been deprecated. Use the AutoScaleDimensions property instead.  http://go.microsoft.com/fwlink/?linkid=14202"
			)]
		public static SizeF GetAutoScaleSize(Font font);

		/// <summary>
		/// Processes a mnemonic character.
		/// </summary>
		/// 
		/// <returns>
		/// true if the character was processed as a mnemonic by the control; otherwise, false.
		/// </returns>
		/// <param name="charCode">The character to process. </param>
		protected internal override bool ProcessMnemonic(char charCode);

		/// <summary>
		/// Centers the position of the form within the bounds of the parent form.
		/// </summary>
		protected void CenterToParent();

		/// <summary>
		/// Centers the form on the current screen.
		/// </summary>
		protected void CenterToScreen();

		/// <summary>
		/// Arranges the multiple-document interface (MDI) child forms within the MDI parent form.
		/// </summary>
		/// <param name="value">One of the <see cref="T:System.Windows.Forms.MdiLayout"/> values that defines the layout of MDI child forms. </param><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public void LayoutMdi(MdiLayout value);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Activated"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnActivated(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the data.</param>
		protected override void OnBackgroundImageChanged(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageLayoutChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		protected override void OnBackgroundImageLayoutChanged(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClosing(CancelEventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Closed"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClosed(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFormClosing(FormClosingEventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.FormClosed"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.FormClosedEventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFormClosed(FormClosedEventArgs e);

		/// <summary>
		/// Raises the CreateControl event.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnCreateControl();

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Deactivate"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDeactivate(EventArgs e);

		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnEnabledChanged(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Enter"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnEnter(EventArgs e);

		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e);

		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleCreated(EventArgs e);

		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleDestroyed(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.HelpButtonClicked"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHelpButtonClicked(CancelEventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Layout"/> event.
		/// </summary>
		/// <param name="levent">The event data.</param>
		protected override void OnLayout(LayoutEventArgs levent);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLoad(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.MaximizedBoundsChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMaximizedBoundsChanged(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.MaximumSizeChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMaximumSizeChanged(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.MinimumSizeChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMinimumSizeChanged(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.InputLanguageChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.InputLanguageChangedEventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnInputLanguageChanged(InputLanguageChangedEventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.InputLanguageChanging"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.InputLanguageChangingEventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnInputLanguageChanging(InputLanguageChangingEventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnVisibleChanged(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.MdiChildActivate"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMdiChildActivate(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.MenuStart"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMenuStart(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.MenuComplete"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMenuComplete(EventArgs e);

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnPaint(PaintEventArgs e);

		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnResize(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.RightToLeftLayoutChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Shown"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnShown(EventArgs e);

		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnTextChanged(EventArgs e);

		/// <summary>
		/// Processes a command key.
		/// </summary>
		/// 
		/// <returns>
		/// true if the keystroke was processed and consumed by the control; otherwise, false to allow further processing.
		/// </returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message"/>, passed by reference, that represents the Win32 message to process. </param><param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the key to process. </param>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData);

		/// <summary>
		/// Processes a dialog box key.
		/// </summary>
		/// 
		/// <returns>
		/// true if the keystroke was processed and consumed by the control; otherwise, false to allow further processing.
		/// </returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the key to process. </param>
		protected override bool ProcessDialogKey(Keys keyData);

		/// <summary>
		/// Processes a dialog character.
		/// </summary>
		/// 
		/// <returns>
		/// true if the character was processed by the control; otherwise, false.
		/// </returns>
		/// <param name="charCode">The character to process. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override bool ProcessDialogChar(char charCode);

		/// <returns>
		/// true if the message was processed by the control; otherwise, false.
		/// </returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message"/>, passed by reference, that represents the window message to process. </param>
		protected override bool ProcessKeyPreview(ref Message m);

		/// <returns>
		/// true if a control is selected; otherwise, false.
		/// </returns>
		/// <param name="forward">true to cycle forward through the controls in the <see cref="T:System.Windows.Forms.ContainerControl"/>; otherwise, false. </param>
		protected override bool ProcessTabKey(bool forward);

		/// <summary>
		/// Removes an owned form from this form.
		/// </summary>
		/// <param name="ownedForm">A <see cref="T:System.Windows.Forms.Form"/> representing the form to remove from the list of owned forms for this form. </param><filterpriority>1</filterpriority>
		public void RemoveOwnedForm(Form ownedForm);

		/// <summary>
		/// Selects this form, and optionally selects the next or previous control.
		/// </summary>
		/// <param name="directed">If set to true that the active control is changed </param><param name="forward">If directed is true, then this controls the direction in which focus is moved. If this is true, then the next control is selected; otherwise, the previous control is selected. </param>
		protected override void Select(bool directed, bool forward);

		/// <summary>
		/// Performs scaling of the form.
		/// </summary>
		/// <param name="x">Percentage to scale the form horizontally </param><param name="y">Percentage to scale the form vertically </param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float x, float y);

		/// <returns>
		/// A <see cref="T:System.Drawing.Rectangle"/> representing the bounds within which the control is scaled.
		/// </returns>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle"/> that specifies the area for which to retrieve the display bounds.</param><param name="factor">The height and width of the control's bounds.</param><param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified"/> that specifies the bounds of the control to use when defining its size and position.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified);

		/// <summary>
		/// Scales a control's location, size, padding and margin.
		/// </summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param><param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified"/> value that specifies the bounds of the control to use when defining its size and position.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified);

		/// <param name="x">The x-coordinate.</param><param name="y">The y-coordinate.</param><param name="width">The bounds width.</param><param name="height">The bounds height.</param><param name="specified">A value from the BoundsSpecified enumeration.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified);

		/// <summary>
		/// Sets the client size of the form. This will adjust the bounds of the form to make the client size the requested size.
		/// </summary>
		/// <param name="x">Requested width of the client region. </param><param name="y">Requested height of the client region.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetClientSizeCore(int x, int y);

		/// <summary>
		/// Sets the bounds of the form in desktop coordinates.
		/// </summary>
		/// <param name="x">The x-coordinate of the form's location. </param><param name="y">The y-coordinate of the form's location. </param><param name="width">The width of the form. </param><param name="height">The height of the form. </param><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public void SetDesktopBounds(int x, int y, int width, int height);

		/// <summary>
		/// Sets the location of the form in desktop coordinates.
		/// </summary>
		/// <param name="x">The x-coordinate of the form's location. </param><param name="y">The y-coordinate of the form's location. </param><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public void SetDesktopLocation(int x, int y);

		/// <summary>
		/// Shows the form with the specified owner to the user.
		/// </summary>
		/// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window"/> and represents the top-level window that will own this form. </param><exception cref="T:System.ArgumentException">The form specified in the <paramref name="owner"/> parameter is the same as the form being shown. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public void Show(IWin32Window owner);

		/// <summary>
		/// Shows the form as a modal dialog box with the currently active window set as its owner.
		/// </summary>
		/// 
		/// <returns>
		/// One of the <see cref="T:System.Windows.Forms.DialogResult"/> values.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">The form specified in the <paramref name="owner"/> parameter is the same as the form being shown.</exception><exception cref="T:System.InvalidOperationException">The form being shown is already visible.-or- The form being shown is disabled.-or- The form being shown is not a top-level window.-or- The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive"/>).</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		public DialogResult ShowDialog();

		/// <summary>
		/// Shows the form as a modal dialog box with the specified owner.
		/// </summary>
		/// 
		/// <returns>
		/// One of the <see cref="T:System.Windows.Forms.DialogResult"/> values.
		/// </returns>
		/// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window"/> that represents the top-level window that will own the modal dialog box. </param><exception cref="T:System.ArgumentException">The form specified in the <paramref name="owner"/> parameter is the same as the form being shown.</exception><exception cref="T:System.InvalidOperationException">The form being shown is already visible.-or- The form being shown is disabled.-or- The form being shown is not a top-level window.-or- The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive"/>).</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public DialogResult ShowDialog(IWin32Window owner);

		/// <summary>
		/// Gets a string representing the current instance of the form.
		/// </summary>
		/// 
		/// <returns>
		/// A string consisting of the fully qualified name of the form object's class, with the <see cref="P:System.Windows.Forms.Form.Text"/> property of the form appended to the end. For example, if the form is derived from the class MyForm in the MyNamespace namespace, and the <see cref="P:System.Windows.Forms.Form.Text"/> property is set to Hello, World, this method will return MyNamespace.MyForm, Text: Hello, World.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString();

		/// <summary>
		/// Updates which button is the default button.
		/// </summary>
		protected override void UpdateDefaultButton();

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.ResizeBegin"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnResizeBegin(EventArgs e);

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.ResizeEnd"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnResizeEnd(EventArgs e);

		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnStyleChanged(EventArgs e);

		/// <returns>
		/// true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating"/> or <see cref="E:System.Windows.Forms.Control.Validated"/> event handlers, this method will always return false.
		/// </returns>
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		public override bool ValidateChildren();

		/// <returns>
		/// true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating"/> or <see cref="E:System.Windows.Forms.Control.Validated"/> event handlers, this method will always return false.
		/// </returns>
		/// <param name="validationConstraints">Places restrictions on which controls have their <see cref="E:System.Windows.Forms.Control.Validating"/> event raised.</param>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool ValidateChildren(ValidationConstraints validationConstraints);

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"/> to process. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void WndProc(ref Message m);

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Form.AutoSize"/> property changes.
		/// </summary>
		[SRDescription("ControlOnAutoSizeChangedDescr")]
		[Browsable(true)]
		[SRCategory("CatPropertyChanged")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler AutoSizeChanged;

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Form.AutoValidate"/> property changes.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		public new event EventHandler AutoValidateChanged;

		/// <summary>
		/// Occurs when the Help button is clicked.
		/// </summary>
		[SRDescription("FormHelpButtonClickedDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[SRCategory("CatBehavior")]
		public event CancelEventHandler HelpButtonClicked;

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MaximizedBounds"/> property has changed.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("FormOnMaximizedBoundsChangedDescr")]
		public event EventHandler MaximizedBoundsChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MaximumSize"/> property has changed.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRDescription("FormOnMaximumSizeChangedDescr")]
		[SRCategory("CatPropertyChanged")]
		public event EventHandler MaximumSizeChanged;

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Form.Margin"/> property changes.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new event EventHandler MarginChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MinimumSize"/> property has changed.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("FormOnMinimumSizeChangedDescr")]
		public event EventHandler MinimumSizeChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Form.TabIndex"/> property changes.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged;

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Form.TabStop"/> property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged;

		/// <summary>
		/// Occurs when the form is activated in code or by the user.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRDescription("FormOnActivateDescr")]
		[SRCategory("CatFocus")]
		public event EventHandler Activated;

		/// <summary>
		/// Occurs when the form is closing.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SRDescription("FormOnClosingDescr")]
		public event CancelEventHandler Closing;

		/// <summary>
		/// Occurs when the form is closed.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnClosedDescr")]
		[Browsable(false)]
		public event EventHandler Closed;

		/// <summary>
		/// Occurs when the form loses focus and is no longer the active form.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRCategory("CatFocus")]
		[SRDescription("FormOnDeactivateDescr")]
		public event EventHandler Deactivate;

		/// <summary>
		/// Occurs before the form is closed.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnFormClosingDescr")]
		public event FormClosingEventHandler FormClosing;

		/// <summary>
		/// Occurs after the form is closed.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRDescription("FormOnFormClosedDescr")]
		[SRCategory("CatBehavior")]
		public event FormClosedEventHandler FormClosed;

		/// <summary>
		/// Occurs before a form is displayed for the first time.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRDescription("FormOnLoadDescr")]
		[SRCategory("CatBehavior")]
		public event EventHandler Load;

		/// <summary>
		/// Occurs when a multiple-document interface (MDI) child form is activated or closed within an MDI application.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRCategory("CatLayout")]
		[SRDescription("FormOnMDIChildActivateDescr")]
		public event EventHandler MdiChildActivate;

		/// <summary>
		/// Occurs when the menu of a form loses focus.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRDescription("FormOnMenuCompleteDescr")]
		[Browsable(false)]
		[SRCategory("CatBehavior")]
		public event EventHandler MenuComplete;

		/// <summary>
		/// Occurs when the menu of a form receives focus.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[Browsable(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnMenuStartDescr")]
		public event EventHandler MenuStart;

		/// <summary>
		/// Occurs after the input language of the form has changed.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnInputLangChangeDescr")]
		public event InputLanguageChangedEventHandler InputLanguageChanged;

		/// <summary>
		/// Occurs when the user attempts to change the input language for the form.
		/// </summary>
		/// <filterpriority>1</filterpriority>
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnInputLangChangeRequestDescr")]
		public event InputLanguageChangingEventHandler InputLanguageChanging;

		/// <summary>
		/// Occurs after the value of the <see cref="P:System.Windows.Forms.Form.RightToLeftLayout"/> property changes.
		/// </summary>
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		[SRCategory("CatPropertyChanged")]
		public event EventHandler RightToLeftLayoutChanged;

		/// <summary>
		/// Occurs whenever the form is first displayed.
		/// </summary>
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnShownDescr")]
		public event EventHandler Shown;

		/// <summary>
		/// Occurs when a form enters resizing mode.
		/// </summary>
		[SRCategory("CatAction")]
		[SRDescription("FormOnResizeBeginDescr")]
		public event EventHandler ResizeBegin;

		/// <summary>
		/// Occurs when a form exits resizing mode.
		/// </summary>
		[SRDescription("FormOnResizeEndDescr")]
		[SRCategory("CatAction")]
		public event EventHandler ResizeEnd;

		#region Nested type: ControlCollection

		/// <summary>
		/// Represents a collection of controls on the form.
		/// </summary>
		[ComVisible(false)]
		public class ControlCollection : Control.ControlCollection
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="T:System.Windows.Forms.Form.ControlCollection"/> class.
			/// </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.Form"/> to contain the controls added to the control collection. </param>
			[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
			public ControlCollection(Form owner);

			/// <summary>
			/// Adds a control to the form.
			/// </summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control"/> to add to the form. </param><exception cref="T:System.Exception">A multiple document interface (MDI) parent form cannot have controls added to it. </exception>
			public override void Add(Control value);

			/// <summary>
			/// Removes a control from the form.
			/// </summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.Control"/> to remove from the form. </param>
			public override void Remove(Control value);
		}

		#endregion
	}
}
