﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProductFlow.Exception {
    using System;
    
    
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
    public class ResourceErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public ResourceErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ProductFlow.Exception.ResourceErrorMessages", typeof(ResourceErrorMessages).Assembly);
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
        ///   Looks up a localized string similar to Unsupported category..
        /// </summary>
        public static string CATEGORY_TYPE_NOT_SUPPORTED {
            get {
                return ResourceManager.GetString("CATEGORY_TYPE_NOT_SUPPORTED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The email is already in use..
        /// </summary>
        public static string EMAIL_ALREADY_REGISTERED {
            get {
                return ResourceManager.GetString("EMAIL_ALREADY_REGISTERED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The email cannot be empty..
        /// </summary>
        public static string EMAIL_EMPTY {
            get {
                return ResourceManager.GetString("EMAIL_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The email is invalid.
        /// </summary>
        public static string EMAIL_INVALID {
            get {
                return ResourceManager.GetString("EMAIL_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid e-mail and/or password..
        /// </summary>
        public static string EMAIL_OR_PASSWORD_INVALID {
            get {
                return ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your password should be a minimum of 8 characters, containing at least one uppercase letter, one lowercase letter, one number, and one special character (e.g., !, ?, *, .)..
        /// </summary>
        public static string INVALID_PASSWORD {
            get {
                return ResourceManager.GetString("INVALID_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The name cannot be empty..
        /// </summary>
        public static string NAME_EMPTY {
            get {
                return ResourceManager.GetString("NAME_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The name is required..
        /// </summary>
        public static string NAME_REQUIRED {
            get {
                return ResourceManager.GetString("NAME_REQUIRED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password entered is different from the current password..
        /// </summary>
        public static string PASSWORD_DIFFERENT_CURRENT_PASSWORD {
            get {
                return ResourceManager.GetString("PASSWORD_DIFFERENT_CURRENT_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The price must be greater than zero..
        /// </summary>
        public static string PRICE_MUST_BE_GREATER_THAN_ZERO {
            get {
                return ResourceManager.GetString("PRICE_MUST_BE_GREATER_THAN_ZERO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product not found..
        /// </summary>
        public static string PRODUCT_NOT_FOUND {
            get {
                return ResourceManager.GetString("PRODUCT_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to quantity in stock must be greater than or equal to zero..
        /// </summary>
        public static string QUANTITY_STOCK_MUST_BE_GREATER_THAN_OR_EQUAL_ZERO {
            get {
                return ResourceManager.GetString("QUANTITY_STOCK_MUST_BE_GREATER_THAN_OR_EQUAL_ZERO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Status is not valid..
        /// </summary>
        public static string STATUS_INVALID {
            get {
                return ResourceManager.GetString("STATUS_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown error..
        /// </summary>
        public static string UNKNOWN_VALUE {
            get {
                return ResourceManager.GetString("UNKNOWN_VALUE", resourceCulture);
            }
        }
    }
}