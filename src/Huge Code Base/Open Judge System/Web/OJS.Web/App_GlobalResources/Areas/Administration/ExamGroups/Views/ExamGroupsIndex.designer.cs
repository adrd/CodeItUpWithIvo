//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources.Areas.Administration.ExamGroups.Views {
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
    public class ExamGroupsIndex {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExamGroupsIndex() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OJS.Web.App_GlobalResources.Areas.Administration.ExamGroups.Views.ExamGroupsIndex" +
                            "", typeof(ExamGroupsIndex).Assembly);
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
        ///   Looks up a localized string similar to Users which are registered only in the &lt;a href=&quot;{0}&quot; target=&quot;_blank&quot;&gt;SoftUni platform&lt;/a&gt;, will be added with delay.
        /// </summary>
        public static string Add_external_users_delay_message {
            get {
                return ResourceManager.GetString("Add_external_users_delay_message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Add user.
        /// </summary>
        public static string Add_user {
            get {
                return ResourceManager.GetString("Add_user", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter the usernames of the users you want to add, separated by comma, space or new line.
        /// </summary>
        public static string Add_users_description {
            get {
                return ResourceManager.GetString("Add_users_description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Add multiple users.
        /// </summary>
        public static string Bulk_add_users {
            get {
                return ResourceManager.GetString("Bulk_add_users", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot add or remove users from exam group that does not have a contest.
        /// </summary>
        public static string Cannot_manipulate_users {
            get {
                return ResourceManager.GetString("Cannot_manipulate_users", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exam groups.
        /// </summary>
        public static string Page_title {
            get {
                return ResourceManager.GetString("Page_title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure you want to remove this user from the еxam group?.
        /// </summary>
        public static string Remove_user_prompt {
            get {
                return ResourceManager.GetString("Remove_user_prompt", resourceCulture);
            }
        }
    }
}
