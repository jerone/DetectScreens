﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DetectScreens.Properties
{


	/// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DetectScreens.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to frm_Identify.
        /// </summary>
        internal static string FrmIdentify_InitializeComponent_frm_Identify {
            get {
                return ResourceManager.GetString("FrmIdentify_InitializeComponent_frm_Identify", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This program needs Aero Glass, but it seems to be disabled. Click &apos;OK&apos; to still open the program.
        ///
        ///Warning: crashes may occur. Try at your own risk..
        /// </summary>
        internal static string Program_Main_CompositionNotEnabled {
            get {
                return ResourceManager.GetString("Program_Main_CompositionNotEnabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aero Glass disabled.
        /// </summary>
        internal static string Program_Main_CompositionNotEnabled_Title {
            get {
                return ResourceManager.GetString("Program_Main_CompositionNotEnabled_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your Operating System is not supported by this program. Try it on Windows Vista, Windows 7 and higher. Click &apos;OK&apos; to still open the program.
        ///
        ///Warning: crashes may occur. Try at your own risk..
        /// </summary>
        internal static string Program_Main_OSVistaOrHigher {
            get {
                return ResourceManager.GetString("Program_Main_OSVistaOrHigher", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to OS not supported.
        /// </summary>
        internal static string Program_Main_OSVistaOrHigher_Title {
            get {
                return ResourceManager.GetString("Program_Main_OSVistaOrHigher_Title", resourceCulture);
            }
        }
    }
}
