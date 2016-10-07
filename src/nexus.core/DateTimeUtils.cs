using System;

namespace nexus.core
{
   public static class DateTimeUtils
   {
      public static String ToIso8601String( this DateTime time, Boolean includeDelimeters = false )
      {
         return time.ToString(includeDelimeters ? "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK" : "yyyyMMdd'T'HHmmss.fffK");
      }
   }
}