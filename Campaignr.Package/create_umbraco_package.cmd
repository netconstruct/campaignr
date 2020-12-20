rem requires UmbPack tool installed, see https://our.umbraco.com/documentation/Extending/Packages/UmbPack/
rem or run following command
rem dotnet tool install --global Umbraco.Tools.Packages --version "0.9.*"
rem pass the version number in so it can be used where %1 is
ECHO Packaging Campaignr Umbraco Package
CALL UmbPack pack .\package.xml -o ..\dist\ -v %1