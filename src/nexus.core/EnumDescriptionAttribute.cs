// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Reflection;
using nexus.core.resharper;

namespace nexus.core
{
   /// <summary>
   /// Used to give Enum values a detailed description
   /// </summary>
   [AttributeUsage( AttributeTargets.Field )]
   public sealed class EnumDescriptionAttribute : Attribute
   {
      /// <summary>
      /// </summary>
      /// <param name="value"></param>
      public EnumDescriptionAttribute( String value )
      {
         StringValue = value;
      }

      /// <summary>
      /// The description
      /// </summary>
      public String StringValue { get; }
   }

   /// <summary>
   /// Extension method to get <see cref="EnumDescriptionAttribute" /> from an Enum
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class EnumDescriptionAttributeExtensions
   {
      /// <summary>
      /// Retrieve the <see cref="EnumDescriptionAttribute" /> from this enum value
      /// </summary>
      public static String GetEnumDescription( [NotNull] this Enum value, Boolean useNameIfNoStringValue = true )
      {
         Contract.Requires( value != null );

         //get the stringvalue attributes
         var attribs =
            value.GetType().GetRuntimeField( value.ToString() )
                 ?.GetCustomAttributes( typeof(EnumDescriptionAttribute), false ) as EnumDescriptionAttribute[];

         //return the first if there was a match or the attribute if not or null if specified not to return default value
         return attribs != null && attribs.Length > 0
            ? attribs[0].StringValue
            : (useNameIfNoStringValue ? value.ToString() : null);
      }
   }
}
