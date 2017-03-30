#!/bin/sh

echo {\"build-id\":$BUILD_BUILDID, \"version\":\"$GITVERSION_SEMVER\", \"branch\":\"$GITVERSION_BRANCHNAME\", \"build-num\":\"$BUILD_BUILDNUMBER\", \"user\":\"$BUILD_REQUESTEDFOR \<$BUILD_REQUESTEDFOREMAIL\>\"}
echo {\"build-id\":$BUILD_BUILDID, \"version\":\"$GITVERSION_SEMVER\", \"branch\":\"$GITVERSION_BRANCHNAME\", \"build-num\":\"$BUILD_BUILDNUMBER\", \"user\":\"$BUILD_REQUESTEDFOR \<$BUILD_REQUESTEDFOREMAIL\>\"} > version.json