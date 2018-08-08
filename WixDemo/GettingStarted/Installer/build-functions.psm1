$root = (Get-Item $PSScriptRoot).Parent.FullName

$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$msbuild = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
if ($msbuild) {
  $msbuild = join-path $msbuild 'MSBuild\15.0\Bin\MSBuild.exe'
}

function Build-Installer {
  & $msbuild "$root\WpfApp1.sln" /p:Configuration=Release /p:AllowedReferenceRelatedFileExtensions=.pdb
  
  $wpfApp1OutputDir = "$root\WpfApp1\bin\Release"

  heat dir "$wpfApp1OutputDir" -cg ProductComponentsFragment -ag -sreg -sfrag -srd -dr INSTALLFOLDER -out "ProductComponentsFragment.wxs" -var var.WpfApp1SourceDir
  
  $wixOutputDir = "bin\Release"
  candle -out "$wixOutputDir\" Product.wxs ProductComponentsFragment.wxs GlddUI_Minimal.wxs -ext WixUIExtension -dWpfApp1SourceDir="$wpfApp1OutputDir"
  pushd $wixOutputDir
  light -out "WpfApp1.msi" "Product.wixobj" "ProductComponentsFragment.wixobj" "GlddUI_Minimal.wixobj" -ext WixUIExtension
}