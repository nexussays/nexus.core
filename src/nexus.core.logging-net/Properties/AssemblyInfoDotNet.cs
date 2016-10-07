using System;
using System.Reflection;
using nexus.core.logging.Properties;

[assembly: AssemblyTitle( AssemblyInfoDotNet.APPID )]
[assembly: AssemblyProduct( AssemblyInfoDotNet.APPID )]
[assembly:
   AssemblyDescription( ".NET Framework 4.5 implementations of Core.Logging (sinks, decorators, and serializers)" )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "nexussays" )]
[assembly: AssemblyCopyright( "Copyright Malachi Griffie" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]
[assembly: AssemblyInformationalVersion( AssemblyInfoDotNet.VERSION )]
[assembly: AssemblyVersion( AssemblyInfoDotNet.VERSION )]
[assembly: AssemblyFileVersion( AssemblyInfoDotNet.VERSION )]

namespace nexus.core.logging.Properties
{
   public static class AssemblyInfoDotNet
   {
      public const String VERSION = AssemblyInfo.VERSION;
      public const String APPID = AssemblyInfo.APPID + "-net";

      // ReSharper disable once ConditionIsAlwaysTrueOrFalse
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif
   }
}