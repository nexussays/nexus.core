#load "./build/tasks.csx"

project = new Project
{
   Name = "nexus.core",
   Owner = "nexussays",
   Copyright = "Copyright M. Griffie <nexus@nexussays.com>",
   Description = "Cross-platform library of core utility methods and data structures.",
   Url = "https://github.com/nexussays/nexus.core",
   Artifacts = new []
   {
      new Artifact("nexus.core")
   },
   Packages = new Package[]
   {
      new NuGetPackage()
      {
         Id = "nexus.core",
         IsPublic = true,
         License = SourceLicense.MPL2,
      }
   }
};

RunTarget(target);
