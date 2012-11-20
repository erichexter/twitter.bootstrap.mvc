cls
md output -ErrorAction SilentlyContinue
.nuget\nuget.exe pack src\navigation-routes-mvc4.nuspec -o output
.nuget\nuget.exe pack src\twitter-bootstrap-mvc.nuspec -o output
.nuget\nuget.exe pack src\twitter-bootstrap-mvc-sample.nuspec -o output

