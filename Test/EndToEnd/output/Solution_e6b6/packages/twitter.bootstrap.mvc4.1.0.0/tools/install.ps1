param($installPath, $toolsPath, $package, $project)


$proj = $project

$globalasax = $null
try
{
    $globalasax = $proj.ProjectItems.Item("global.asax")
}
catch {
    
}

if($globalasax -ne $null) {
    try{
    	$mvcapp = $globalasax.ProjectItems.Item("global.asax.cs").FileCodeModel.CodeElements | where-object {$_.Kind -eq 5}

    			$startup = $mvcapp.Children.Item(1).Members | Where-object {$_.Name -eq 'Application_Start'}
    			if($startup -ne $null) 
    			{	
				$editpoint=$startup.EndPoint.CreateEditPoint()
				$editpoint.LineUp(1)
				$editpoint.EndOfLine()
				$editpoint.InsertNewLine()
				$editpoint.Insert("            BootstrapSupport.BootstrapBundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);")
                $editpoint.InsertNewLine()
                $editpoint.Insert("            BootstrapMvcSample.ExampleLayoutsRouteConfig.RegisterRoutes(RouteTable.Routes);")

                }
    }
    catch{
        "Error adding bundle config call to the Application_Start"|out-default
        $_.Exception.ToString()| out-default
   }
}


#$project.ProjectItems.Item("codetemplates").ProjectItems.Item("addview").projectitems.item("cshtml").projectitems.item("bootstrapform.tt").properties.item("customtool").Value=""





