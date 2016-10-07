using System;
using System.Reflection;
using nexus.core.logging.Properties;

[assembly: AssemblyTitle( AssemblyInfoiOS.APPID )]
[assembly: AssemblyProduct( AssemblyInfoiOS.APPID )]
[assembly: AssemblyDescription( "ios implementations of Core.Logging (sinks, decorators, and serializers)" )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "nexussays" )]
[assembly: AssemblyCopyright( "Copyright Malachi Griffie" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]
[assembly: AssemblyInformationalVersion( AssemblyInfoiOS.VERSION )]
[assembly: AssemblyVersion( AssemblyInfoiOS.VERSION )]
[assembly: AssemblyFileVersion( AssemblyInfoiOS.VERSION )]

namespace nexus.core.logging.Properties
{
   public static class AssemblyInfoiOS
   {
      public const String VERSION = AssemblyInfo.VERSION;
      public const String APPID = AssemblyInfo.APPID + "-ios";

      // ReSharper disable once ConditionIsAlwaysTrueOrFalse
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif
   }
}