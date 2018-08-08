$root = (Get-Item $PSScriptRoot).Parent.FullName

$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$msbuild = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
if ($msbuild) {
  $msbuild = join-path $msbuild 'MSBuild\15.0\Bin\MSBuild.exe'
}

$tlbexp = "C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.7.2 Tools\TlbExp.exe"

function Build-Installer {
  & $msbuild "$root\ClassLibrary1.sln" /p:Configuration=Release /p:AllowedReferenceRelatedFileExtensions=.pdb
  
  $ClassLibrary1OutputDir = "$root\ClassLibrary1\bin\Release"
  $ClassLibrary1Dll = "$ClassLibrary1OutputDir\ClassLibrary1.dll"
  $ClassLibrary1Tlb = "$ClassLibrary1OutputDir\ClassLibrary1.tlb"
  & $tlbexp $ClassLibrary1Dll /out:$ClassLibrary1Tlb
  heat file $ClassLibrary1Tlb -cg "ClassLibrary1TLB" -ag -sfrag -srd -dr INSTALLFOLDER -out "ClassLibrary1.tlb.wxs" -var var.ClassLibrary1SourceDir
  heat file $ClassLibrary1Dll -cg "ClassLibrary1DLL" -ag -sfrag -srd -dr INSTALLFOLDER -out "ClassLibrary1.dll.wxs" -var var.ClassLibrary1SourceDir
  
  $wixOutputDir = "bin\Release"
  candle -out "$wixOutputDir\" "Product.wxs" "ClassLibrary1.dll.wxs" "ClassLibrary1.tlb.wxs" "GlddUI_Minimal.wxs" -ext "WixUIExtension" -dClassLibrary1SourceDir=$ClassLibrary1OutputDir
  pushd $wixOutputDir
  light -out "ClassLibrary1.msi" "Product.wixobj" "ClassLibrary1.dll.wixobj" "ClassLibrary1.tlb.wixobj" "GlddUI_Minimal.wixobj" -ext "WixUIExtension"
}