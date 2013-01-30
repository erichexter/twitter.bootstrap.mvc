param([parameter(Mandatory = $true)]
      [string]$OutputPath,
      [parameter(Mandatory = $true)]
      [string]$TemplatePath)

# Make sure we stop on exceptions
$ErrorActionPreference = "Stop"
$FileKind = "{6BB5F8EE-4483-11D3-8BCF-00C04F8EC28C}"

function Get-SolutionDir {
    if($dte.Solution -and $dte.Solution.IsOpen) {
        return Split-Path $dte.Solution.FullName
    }
    else {
        throw "Solution not avaliable"
    }
}

function Ensure-Solution {
    if(!$dte.Solution -or !$dte.Solution.IsOpen) {
        New-Solution
    }
}

function Close-Solution 
{
    if ($dte.Solution -and $dte.Solution.IsOpen) 
    {
        $dte.Solution.Close()
    }
}

function Open-Solution 
{
    param
    (
        [string]
        [parameter(Mandatory = $true)]
        $Path
    )
    
    $dte.Solution.Open($Path)
}

function Ensure-Dir {
    param(
        [string]
        [parameter(Mandatory = $true)]
        $Path
    )
    if(!(Test-Path $Path)) {
        mkdir $Path | Out-Null
    }
}

function New-Solution {
    param(
        [string]$solutionName
    )

    if ($solutionName) {
        $name = $solutionName 
    }
    else {
        $id = New-Guid
        $name = "Solution_$id"
    }

    $solutionDir = Join-Path $OutputPath $name
    $solutionPath = Join-Path $solutionDir $name
    
    Ensure-Dir $solutionDir
     
    $dte.Solution.Create($solutionDir, $name) | Out-Null
    $dte.Solution.SaveAs($solutionPath) | Out-Null    
}

function New-Project {
    param(
         [parameter(Mandatory = $true)]
         [string]$TemplateName,
         [string]$ProjectName,
         [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    $id = New-Guid
    if (!$ProjectName) {
        $ProjectName = $TemplateName + "_$id"
    }
    
    # Make sure there is a solution
    Ensure-Solution
    
    # Get the zip file where the project template is located
    $projectTemplatePath = Join-Path $TemplatePath "$TemplateName.zip"
    
    # Find the vs template file
    $projectTemplateFilePath = @(Get-ChildItem $projectTemplatePath -Filter *.vstemplate)[0].FullName    

    # Get the output path of the project
    if($SolutionFolder) {
        $destPath = Join-Path (Get-SolutionDir) (Join-Path $SolutionFolder.Name $projectName)
    }
    else {
        $destPath = Join-Path (Get-SolutionDir) $projectName
    }
    
    # Store the active window so that we can set focus to it after the command completes
    # When we add a project to VS it usually tries to set focus to some page
    $window = $dte.ActiveWindow
    
    if($SolutionFolder) {
        $SolutionFolder.Object.AddFromTemplate($projectTemplateFilePath, $destPath, $projectName) | Out-Null
    }
    else {
        # Add the project to the solution from th template file specified
        $dte.Solution.AddFromTemplate($projectTemplateFilePath, $destPath, $projectName, $false) | Out-Null
    }
    
    # Close all active documents
    $dte.Documents | %{ try { $_.Close() } catch { } }

    # Change the configuration of the project to x86
    $dte.Solution.SolutionBuild.SolutionConfigurations | % { if ($_.PlatformName -eq 'x86') { $_.Activate() } } | Out-Null

    # Set the focus back on the shell
    $window.SetFocus()
    
    # Return the project
    if ($SolutionFolder) {
        $solutionFolderPath = Get-SolutionFolderPathRecursive $SolutionFolder
        $project = Get-Project "$($solutionFolderPath)$projectName" -ErrorAction SilentlyContinue
    }
    else {
        $project = Get-Project $projectName -ErrorAction SilentlyContinue
    }
    
    if(!$project) {
        $project = Get-Project "$destPath\"
    }
    
    $project
}

function Get-SolutionFolderPathRecursive([parameter(mandatory=$true)]$solutionFolder) {
    $path = ''
    while ($solutionFolder -ne $null) {
        $path = "$($solutionFolder.Name)\$path"
        $solutionFolder = $solutionFolder.ParentProjectItem.ContainingProject
    }
    return $path
}

function New-SolutionFolder {
    param(
        [string]$Name,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )
    
    $id = New-Guid
    if(!$Name) {
        $Name = "SolutionFolder_$id"
    }
    
    if(!$SolutionFolder) {
        # Make sure there is a solution
        Ensure-Solution

        $solution = Get-Interface $dte.Solution ([EnvDTE80.Solution2])
    }
    elseif($SolutionFolder.Object.AddSolutionFolder) {
        $solution = $SolutionFolder.Object
    }

    $solution.AddSolutionFolder($Name)
}

function New-ClassLibrary {
    param(        
        [string]$ProjectName,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    $SolutionFolder | New-Project ClassLibrary $ProjectName
}

function New-ConsoleApplication {
    param(        
        [string]$ProjectName,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    $SolutionFolder | New-Project ConsoleApplication $ProjectName
}

function New-WebApplication {
    param(        
        [string]$ProjectName,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    $SolutionFolder | New-Project EmptyWebApplicationProject40 $ProjectName
}

function New-MvcApplication { 
    param(        
        [string]$ProjectName,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    $SolutionFolder | New-Project EmptyMvcWebApplicationProjectTemplatev4.0.csaspx $ProjectName
}

function New-WebSite {
    param(        
        [string]$ProjectName,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    $SolutionFolder | New-Project EmptyWeb $ProjectName
}

function New-FSharpLibrary {
    param(        
        [string]$ProjectName,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    $SolutionFolder | New-Project FSharpLibrary $ProjectName
}

function New-FSharpConsoleApplication {
    param(        
        [string]$ProjectName,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    $SolutionFolder | New-Project FSharpConsoleApplication $ProjectName
}

function New-WPFApplication {
    param(        
        [string]$ProjectName,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    $SolutionFolder | New-Project WPFApplication $ProjectName
}

function New-SilverlightClassLibrary {
    param(        
        [string]$ProjectName,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    $SolutionFolder | New-Project SilverlightClassLibrary $ProjectName
}

function New-SilverlightApplication {
    param(        
        [string]$ProjectName,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    $SolutionFolder | New-Project SilverlightProject $ProjectName
}

function New-WindowsPhoneClassLibrary {
    param(        
        [string]$ProjectName,
        [parameter(ValueFromPipeline = $true)]$SolutionFolder
    )

    try {
        $SolutionFolder | New-Project WindowsPhoneClassLibrary $ProjectName
    }
    catch {
        # If we're unable to create the project that means we probably don't have some SDK installed
        # Signal to the runner that we want to skip this test        
        throw "SKIP: $($_)"
    }
}

function New-TextFile {
    $dte.ItemOperations.NewFile('General\Text File')
    $dte.ActiveDocument.Object("TextDocument")
}

function Build-Project {
    param(
        [parameter(Mandatory = $true)]
        $Project,
        [string]$Configuration
    )    
    if(!$Configuration) {
        # If no configuration was specified then use
        $Configuration = $dte.Solution.SolutionBuild.ActiveConfiguration.Name
    }
    
    # Build the project and wait for it to complete
    $dte.Solution.SolutionBuild.BuildProject($Configuration, $Project.UniqueName, $true)
}

function Get-AssemblyReference {
    param(
        [parameter(Mandatory = $true)]
        $Project,
        [parameter(Mandatory = $true)]
        [string]$Reference
    )    
    try {
        return $Project.Object.References.Item($Reference)
    }
    catch {        
    }
    return $null
}

function Get-PropertyValue {
    param(
        [parameter(Mandatory = $true)]
        $Project,
        [parameter(Mandatory = $true)]
        [string]$PropertyName
    )    
    try {
        $property = $Project.Properties.Item($PropertyName)        
        if($property) {
            return $property.Value
        }
    }
    catch {        
    }
    return $null
}

function Get-MsBuildPropertyValue {
    param(
        [parameter(Mandatory = $true)]
        $Project,
        [parameter(Mandatory = $true)]
        [string]$PropertyName
    )    

    $msBuildProject = Get-MsBuildProject $project
    return $msBuildProject.GetPropertyValue($PropertyName)

    return $null
}

function Get-MsBuildProject 
{
    param(
        [parameter(Mandatory = $true)]
        $project
    )

    $projectCollection = [Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection

    $loadedProjects = $projectCollection.GetLoadedProjects($project.FullName)
    if ($loadedProjects.Count -gt 0) {
        foreach ($p in $loadedProjects) {
            return $p
        }
    }

    $projectCollection.LoadProject($project.FullName)
}

function Get-ProjectDir {
    param(
        [parameter(Mandatory = $true)]
        $Project
    )
    Get-PropertyValue $Project FullPath
}

function Get-OutputPath {
    param(
        [parameter(Mandatory = $true)]
        $Project
    )
    
    $outputPath = $Project.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value
    Join-Path (Get-ProjectDir) $outputPath
}

function Get-Errors {
    $dte.ExecuteCommand("View.ErrorList", " ")
    
    # Make sure there are no errors in the error list
    $errorList = $dte.Windows | ?{ $_.Caption -eq 'Error List' } | Select -First 1
    
    if(!$errorList) {
        throw "Unable to locate the error list"
    }
    
    # Get the list of errors from the error list
    $errorList.Object.ErrorItems    
}

function Get-ProjectItemPath {
    param(
        [parameter(Mandatory = $true)]
        $Project,
        [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
        [string]$Path
    )
    $item = Get-ProjectItem $Project $Path
    
    if($item) {
        return $item.Properties.Item("FullPath").Value
    }
}

function Remove-ProjectItem {
    param(
        [parameter(Mandatory = $true)]
        $Project,
        [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
        [string]$Path
    )

    $item = Get-ProjectItem $Project $Path
    $path = Get-ProjectItemPath $Project $Path
    $item.Remove()
    Remove-Item $path
}

function Get-ProjectItem {
    param(
        [parameter(Mandatory = $true)]
        $Project,
        [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
        [string]$Path
    )
    Process {
        $pathParts = $Path.Split('\')
        $projectItems = $Project.ProjectItems
        
        foreach($part in $pathParts) {
            if(!$part -or $part -eq '') {
                continue
            }
            
            try {
                $subItem = $projectItems.Item($part)
            }
            catch {
                return $null
            }

            $projectItems = $subItem.ProjectItems
        }

        if($subItem.Kind -eq $FileKind) {
            return $subItem
        }
        
        # Force array
       return  ,$projectItems
    }
}

function Add-File {
    param(
        [parameter(Mandatory = $true)]
        $Project,
        [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
        [string]$FilePath
    )
    
    $Project.ProjectItems.AddFromFileCopy($FilePath) | out-null
}

function Add-ProjectReference {
    param (
        [parameter(Mandatory = $true)]
        $ProjectFrom,
        [parameter(Mandatory = $true)]
        $ProjectTo
    )

    if($ProjectFrom.Object.References.AddProject) {
        $ProjectFrom.Object.References.AddProject($ProjectTo) | Out-Null
    }
    elseif($ProjectFrom.Object.References.AddFromProject) {
        $ProjectFrom.Object.References.AddFromProject($ProjectTo) | Out-Null
    }
}

function Remove-Project {
    param (
        [parameter(Mandatory = $true)]
        $Project   
    )

    $dte.Solution.Remove($Project)
}

function Get-SolutionPath {
    $dte.Solution.Properties.Item("Path").Value
}

function Close-Solution {
    if ($dte.Solution) {
        $dte.Solution.Close()
    }
}

function Enable-PackageRestore {
    if (!$dte.Solution -or !$dte.Solution.IsOpen) 
    {
        throw "No solution is available."
    }

    $componentService = Get-VSComponentModel
    $packageRestoreManager = $componentService.GetService([NuGet.VisualStudio.IPackageRestoreManager])
    $packageRestoreManager.EnableCurrentSolutionForRestore($false)
}