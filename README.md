Component template for InfoDC platform.

Before first use:
1. start PowerShell as administrator and change execution policy as follows:
   set-executionpolicy unrestricted
2. make changes to the settings (file: prepare_settings.json)
   - set a valid path for Visual Studio,
   - set your name/surname/nickname,
3. start prepare.cmd script, enter the name for new component without the .dll extension e.g. MyNewComponent,
   then enter the COM + application name (formerly COM + package) with NET suffix e.g. MyPackageNET,
4. wait a moment patiently,
5. move created folder (e.g. MyNewComponent.dll) with contents to the target place (COM+NET).
6. start Visual Studio and build the new solution.
7. enjoy it.