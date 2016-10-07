using System;
using System.Reflection;
using nexus.core.logging.Properties;

[assembly: AssemblyTitle( AssemblyInfo.APPID )]
[assembly: AssemblyProduct( AssemblyInfo.APPID )]
[assembly: AssemblyDescription( "Android implementations of Core.Logging (sinks, decorators, and serializers)" )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "nexussays" )]
[assembly: AssemblyCopyright( "Copyright Malachi Griffie" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]
[assembly: AssemblyInformationalVersion( AssemblyInfo.VERSION )]
[assembly: AssemblyVersion( AssemblyInfo.VERSION )]
[assembly: AssemblyFileVersion( AssemblyInfo.VERSION )]

namespace nexus.core.logging.Properties.android
{
   public static class AssemblyInfo
   {
      public const String VERSION = "0.14.2";
      public const String APPID = "nexus.core.logging-android";

      // ReSharper disable once ConditionIsAlwaysTrueOrFalse
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif
   }
}