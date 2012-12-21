&{
$start=pwd
$outdir = resolve-path .\output
$templates = resolve-path .\projecttemplates

. .\utility.ps1
. .\vs.ps1 $outdir $templates
close-solution
new-mvcapplication foobar
install-package Newtonsoft.Json
install-package twitter.bootstrap.mvc4.sample -source c:\code\github\twitter.bootstrap.mvc\output

build-project foobar
cd $start
}