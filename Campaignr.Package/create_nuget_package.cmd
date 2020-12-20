rem pass the version number in so it can be used where %1 is
nuget pack .\Campaignr.Package.csproj -build  -OutputDirectory ..\dist\ -version %1 -Prop Configuration=Release -IncludeReferencedProjects