@echo off

if %GITVERSION_BRANCHNAME% == master (
   git tag -a "%GITVERSION_SEMVER%" -F version.json
   git push --progress origin tag %GITVERSION_SEMVER%
)