using System;
using System.Reflection;
using nexus.core.logging.Properties.ios;

[assembly: AssemblyTitle( AssemblyInfo.APPID )]
[assembly: AssemblyProduct( AssemblyInfo.APPID )]
[assembly: AssemblyDescription( "ios implementations of Core.Logging (sinks, decorators, and serializers)" )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "nexussays" )]
[assembly: AssemblyCopyright( "Copyright Malachi Griffie" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]
[assembly: AssemblyInformationalVersion( AssemblyInfo.VERSION )]
[assembly: AssemblyVersion( AssemblyInfo.VERSION )]
[assembly: AssemblyFileVersion( AssemblyInfo.VERSION )]

namespace nexus.core.logging.Properties.ios
{
   public static class AssemblyInfo
   {
      public const String VERSION = "0.14.2";
      public const String APPID = "nexus.core.logging-ios";

      // ReSharper disable once ConditionIsAlwaysTrueOrFalse
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif
   }
}