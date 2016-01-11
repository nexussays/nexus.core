// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Diagnostics.Contracts;

namespace nexus.core
{
   public static class ReflectionUtils
   {
      /*
      public static T GetCustomAttribute<T>( this MemberInfo prop, Boolean inherit )
      {
         Contract.Requires( prop != null );
         var attrs = prop.GetCustomAttributes( typeof(T), inherit );
         if(attrs.Length > 0)
         {
            return (T)attrs[0];
         }
         return default(T);
      }

      public static IEnumerable<T> GetCustomAttributes<T>( this MemberInfo prop, Boolean inherit )
      {
         Contract.Requires( prop != null );
         return prop.GetCustomAttributes( typeof(T), inherit ).Select( attr => (T)attr );
      }
      //*/

      public static Boolean IsNullableType( Type type )
      {
         Contract.Requires( type != null );
         return (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
      }
   }
}