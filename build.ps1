Param($path = "output")

New-Item -ItemType d -Force $path -ErrorAction SilentlyContinue
.nuget\nuget.exe pack src\NavigationRoutes\navigation-routes-mvc4.nuspec -o $path
.nuget\nuget.exe pack src\Bootstrap\twitter-bootstrap-mvc.nuspec -o $path
.nuget\nuget.exe pack src\Bootstrap\twitter-bootstrap-mvc-sample.nuspec -o $path
.nuget\nuget.exe pack src\Bootstrap\twitter-bootstrap-mvc-templates.nuspec -o $path
$o= resolve-path $path

"To test these packages add a nuget source (in visual studio) to:"
"$o" 
" "