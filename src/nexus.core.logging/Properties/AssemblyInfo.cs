using System;
using System.Reflection;
using nexus.core.logging.Properties;

[assembly: AssemblyTitle( AssemblyInfo.APPID )]
[assembly: AssemblyProduct( AssemblyInfo.APPID )]
[assembly:
   AssemblyDescription(
      "An extremely light-weight, modular, cross-platform logging library. Logs can be text or structured data. Simple high-level API. Swap out how log entries are serialized, where they are output to, what additional structured data should be attached, etc. Or implement the interfaces from scratch."
      )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "nexussays" )]
[assembly: AssemblyCopyright( "Copyright Malachi Griffie" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]
[assembly: AssemblyInformationalVersion( AssemblyInfo.VERSION )]
[assembly: AssemblyVersion( AssemblyInfo.VERSION )]
[assembly: AssemblyFileVersion( AssemblyInfo.VERSION )]

namespace nexus.core.logging.Properties
{
   public static class AssemblyInfo
   {
      public const String VERSION = "0.14.2";
      public const String APPID = "nexus.core.logging";

      // ReSharper disable once ConditionIsAlwaysTrueOrFalse
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif
   }
}