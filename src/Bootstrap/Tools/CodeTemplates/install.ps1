param($installPath, $toolsPath, $package, $project)

$proj = $project

# Clear the custom tool if we have any code templates.

$codeTemplatesFolder = $proj.ProjectItems.Item("CodeTemplates")

foreach($ttFile in $codeTemplatesFolder.ProjectItems.Item("AddController").ProjectItems){
	$ttFile.Properties.Item("CustomTool").Value = "";
}

foreach($ttLanguageFolder in $codeTemplatesFolder.ProjectItems.Item("AddView").ProjectItems){
	foreach($ttFile in $ttLanguageFolder.ProjectItems){
		$ttFile.Properties.Item("CustomTool").Value = "";
	}
}



