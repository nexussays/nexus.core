using System;
using System.Reflection;
using nexus.core.logging.Properties;

[assembly: AssemblyTitle( AssemblyInfoAndroid.APPID )]
[assembly: AssemblyProduct( AssemblyInfoAndroid.APPID )]
[assembly: AssemblyDescription( "Android implementations of Core.Logging (sinks, decorators, and serializers)" )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "nexussays" )]
[assembly: AssemblyCopyright( "Copyright Malachi Griffie" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]
[assembly: AssemblyInformationalVersion( AssemblyInfoAndroid.VERSION )]
[assembly: AssemblyVersion( AssemblyInfoAndroid.VERSION )]
[assembly: AssemblyFileVersion( AssemblyInfoAndroid.VERSION )]

namespace nexus.core.logging.Properties
{
   public static class AssemblyInfoAndroid
   {
      public const String VERSION = AssemblyInfo.VERSION;
      public const String APPID = AssemblyInfo.APPID + "-android";

      // ReSharper disable once ConditionIsAlwaysTrueOrFalse
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif
   }
}