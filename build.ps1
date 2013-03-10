Param($path = "output")

New-Item -ItemType d -Force $path -ErrorAction SilentlyContinue
.nuget\nuget.exe pack src\twitter-bootstrap-mvc.nuspec -o $path
.nuget\nuget.exe pack src\twitter-bootstrap-mvc-sample.nuspec -o $path
.nuget\nuget.exe pack src\twitter-bootstrap-mvc-templates.nuspec -o $path
.nuget\nuget.exe pack src\twitter-bootstrap-mvc-htmlhelpers.nuspec -o $path
$o= resolve-path $path

"To test these packages add a nuget source (in visual studio) to:"
"$o" 
" "