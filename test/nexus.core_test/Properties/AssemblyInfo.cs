using System;
using System.Reflection;
using nexus.core;

[assembly: AssemblyTitle( AssemblyInfo.ID )]
[assembly: AssemblyProduct( AssemblyInfo.ID )]
[assembly: AssemblyDescription( AssemblyInfo.DESCRIPTION )]
[assembly: AssemblyInformationalVersion( "0.0.0.0" )]
[assembly: AssemblyVersion( "0.0.0.0" )]
[assembly: AssemblyFileVersion( "0.0.0.0" )]

// ReSharper disable once CheckNamespace

namespace nexus.core
{
   internal static partial class AssemblyInfo
   {
      internal const String ID = PROJECT_ID + "_test";
      internal const String DESCRIPTION = "Tests for " + PROJECT_ID;
   }
}