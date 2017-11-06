namespace nexus.core.package
{
   /// <summary>
   /// </summary>
   public enum BuildType
   {
      /// <summary>
      /// The build type for this package is unknown
      /// </summary>
      Unknown,
      /// <summary>
      /// This is a debug build. Typically including symbols, and source file names and line numbers.
      /// </summary>
      Debug,
      /// <summary>
      /// This is a release build. Typically optimized.
      /// </summary>
      Release
   }
}