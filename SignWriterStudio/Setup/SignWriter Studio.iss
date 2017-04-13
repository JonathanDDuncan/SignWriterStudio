; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "SignWriter Studio 1.2"
#define MyAppVerName "SignWriter Studio "
#define MyAppPublisher "Jonathan Duncan"
#define MyAppURL "http://www.signwriterstudio.com/"
#define MyAppExeName "SignWriterStudio.exe"
; #define MyDotNetName "Dot Net Framework 4.0 Instalation"
; #define MyDotNetStrapper "DN4Setup.exe"
; #define BaseSourceDir "C:\Users\Jonathan\Documents\SignWriter\SignWriter Studio"
#define BaseSourceDir "D:\SignWriter\SignWriter Studio"
#define VCRedistName "Visual C++ 2013 redistributable"
#define VCRedist2013 "vcredist_x86_2013.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{8CBC68CB-1C7A-49FA-93A5-EBF694FAA397}
AppName={#MyAppName}
AppVerName={#MyAppVerName}{#MyVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
LicenseFile= {#BaseSourceDir}\SignWriterStudio\License.txt
OutputDir={#BaseSourceDir}\SignWriterStudio\Setup
OutputBaseFilename={#MyAppVerName}{#MyVersion}
Compression=lzma
SolidCompression=yes
                                                
[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#BaseSourceDir}\SignWriterStudio\Sample Dictionary.SWS"; DestDir: "{userdocs}\SignWriter Studio Sample Files"; Flags: ignoreversion 
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SignWriterStudio.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SWEditor.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SWS.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SPML.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\General.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\Settings.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SWClasses.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SymbolCache.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\nunit.framework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SignWriterStudio.UI.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SignWriterStudio.Document.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SignWriterStudio.Dictionary.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SignWriterStudio.ImageEditor.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SignWriterStudio.Database.Dictionary.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SignWriterStudio.WebRequestPost.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\HtmlAgilityPack.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\TagList.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\DropDownControls.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\DbTags.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SQLiteAdapters.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\x86\CefSharp.BrowserSubprocess.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\x86\CefSharp.BrowserSubprocess.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\x86\CefSharp.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\x86\CefSharp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\x86\CefSharp.WinForms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\CefSharp.MinimalExample.WinForms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\d3dcompiler_43.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\icudtl.dat"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\cef_100_percent.pak"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\cef_200_percent.pak"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\cef_extensions.pak"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\devtools_resources.pak"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\cef.pak"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SQLite\SQLite.Interop.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SQLite\System.Data.SQLite.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SQLite\System.Data.SQLite.EF6.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\SQLite\System.Data.SQLite.Linq.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\packages\cef.redist.x86.3.2883.1552\CEF\x86\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
Source: "{#BaseSourceDir}\packages\cef.redist.x86.3.2883.1552\CEF\locales\*"; DestDir: "{app}\locales"; Flags: ignoreversion recursesubdirs
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\dist\*"; DestDir: "{app}\dist"; Flags: ignoreversion recursesubdirs
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\nunit.framework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\Foreign Dll\{#VCRedist2013}"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\Settings\Settings.dat"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio.UI\swsui.dat"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\SignWriterStudio.chm"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\bin\Release\BlankDict.dat"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio.Database.Dictionary\Upgrade210.sql"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio.Database.Dictionary\Upgrade211.sql"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio.Database.Dictionary\Upgrade220.sql"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio.Document\SWSDocument Blank.dat"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BaseSourceDir}\SignWriterStudio\Sample Document.SWSDoc"; DestDir: "{userdocs}\SignWriter Studio Sample Files"; Flags: ignoreversion

Source: "{#BaseSourceDir}\SymbolCache\ISWA2010.dat"; DestDir: "{app}"; Flags: ignoreversion

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}" ; WorkingDir: "{app}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; WorkingDir: "{app}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; WorkingDir: "{app}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#VCRedist2013}"; Parameters: "/install /passive /norestart" ; Description: "{cm:LaunchProgram,{#VCRedistName}}"; Flags: hidewizard
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#MyAppName}}"; Flags: nowait postinstall skipifsilent


