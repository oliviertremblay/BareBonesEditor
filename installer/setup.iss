[Setup]
AppName=BareBonesEditor
AppVersion=1.0.0
DefaultDirName={pf}\BareBonesEditor
DefaultGroupName=BareBonesEditor
OutputBaseFilename=setup
OutputDir=.
Compression=lzma
SolidCompression=yes

[Files]
Source: "..\publish\*"; DestDir: "{app}"; Flags: recursesubdirs

[Icons]
Name: "{group}\BareBonesEditor"; Filename: "{app}\BareBonesEditor.exe"

[Run]
Filename: "{app}\BareBonesEditor.exe"; Description: "Lancer BareBonesEditor"; Flags: nowait postinstall skipifsilent
