﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources.Areas.Administration.ExamGroups {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ExamGroupsControllers {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExamGroupsControllers() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OJS.Web.App_GlobalResources.Areas.Administration.ExamGroups.ExamGroupsControllers" +
                            "", typeof(ExamGroupsControllers).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot add users to exam group that is not attached to a contest..
        /// </summary>
        public static string Cannot_add_users {
            get {
                return ResourceManager.GetString("Cannot_add_users", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You don&apos;t have privileges to attach this contest to an exam group!.
        /// </summary>
        public static string Cannot_attach_contest {
            get {
                return ResourceManager.GetString("Cannot_attach_contest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot delete exam group which has active contest attached to it..
        /// </summary>
        public static string Cannot_delete_group_with_active_contest {
            get {
                return ResourceManager.GetString("Cannot_delete_group_with_active_contest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot remove users from exam group that is not attached to a contest..
        /// </summary>
        public static string Cannot_remove_users {
            get {
                return ResourceManager.GetString("Cannot_remove_users", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Users were added successfully to the exam group..
        /// </summary>
        public static string Users_added {
            get {
                return ResourceManager.GetString("Users_added", resourceCulture);
            }
        }
    }
}
