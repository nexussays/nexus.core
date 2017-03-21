@echo off

set VERSION={"build-id":%BUILD_BUILDID%, "version":"%GITVERSION_SEMVER%", "branch":"%GITVERSION_BRANCHNAME%", "build-num":"%BUILD_BUILDNUMBER%", "user":"%BUILD_REQUESTEDFOR% <%BUILD_REQUESTEDFOREMAIL%>"}

if %GITVERSION_BRANCHNAME% == master (
   echo %VERSION% > version.json
   echo %VERSION%
   git tag -a "%GITVERSION_SEMVER%" -F version.json
   git push --progress origin tag %GITVERSION_SEMVER%
) else (
   echo %VERSION%
)