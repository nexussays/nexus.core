using System;
using System.Reflection;
using nexus.core.Properties;

[assembly: AssemblyTitle( AssemblyInfo.APPID )]
[assembly: AssemblyProduct( AssemblyInfo.APPID )]
[assembly: AssemblyDescription( "Core utilities and data structures" )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "nexussays" )]
[assembly: AssemblyCopyright( "Copyright Malachi Griffie" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]
[assembly: AssemblyInformationalVersion( AssemblyInfo.VERSION )]
[assembly: AssemblyVersion( AssemblyInfo.VERSION_SHORT )]
[assembly: AssemblyFileVersion( AssemblyInfo.VERSION_SHORT )]

namespace nexus.core.Properties
{
   public static class AssemblyInfo
   {
      public const String VERSION = "0.16.2";
      public const String VERSION_SHORT = VERSION;
      public const String APPID = "nexus.core";

      // ReSharper disable once ConditionIsAlwaysTrueOrFalse
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif
   }
}