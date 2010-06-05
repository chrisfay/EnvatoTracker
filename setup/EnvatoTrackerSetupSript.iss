; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{B2B08A97-48F4-40CE-94A5-C0FCE224A1B4}
AppName=EnvatoTracker
AppVerName=EnvatoTracker V0.5.0.0
AppPublisher=Chris Fay Consulting, LLC
AppPublisherURL=http://chrisfayconsulting.com
AppSupportURL=http://chrisfayconsulting.com
AppUpdatesURL=http://chrisfayconsulting.com
DefaultDirName={pf}\EnvatoTracker
DefaultGroupName=EnvatoTracker
AllowNoIcons=yes
OutputDir=D:\CodingProjects\EnvatoTracker\setup
OutputBaseFilename=EnvatoTracker_Setup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\CodingProjects\EnvatoTracker\EnvatoTracker\bin\Debug\EnvatoTracker.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\CodingProjects\EnvatoTracker\EnvatoTracker\bin\Debug\config.ini"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\CodingProjects\EnvatoTracker\EnvatoTracker\bin\Debug\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\CodingProjects\EnvatoTracker\EnvatoTracker\bin\Debug\Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\CodingProjects\EnvatoTracker\EnvatoTracker\bin\Debug\MONEY95.WAV"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\EnvatoTracker"; Filename: "{app}\EnvatoTracker.exe"; WorkingDir: {app}
Name: "{group}\{cm:UninstallProgram,EnvatoTracker}"; Filename: "{uninstallexe}"; WorkingDir: {app}
Name: "{commondesktop}\EnvatoTracker"; Filename: "{app}\EnvatoTracker.exe"; Tasks: desktopicon; WorkingDir: {app}
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\EnvatoTracker"; Filename: "{app}\EnvatoTracker.exe"; Tasks: quicklaunchicon; WorkingDir: {app}

[Run]
Filename: "{app}\EnvatoTracker.exe"; Description: "{cm:LaunchProgram,EnvatoTracker}"; Flags: nowait postinstall skipifsilent

