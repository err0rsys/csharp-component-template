@echo off

set package=_ComNameToReplace_NET
set dllfile=_ComNameToReplace_.dll

setlocal
cd /d %~dp0

net session >nul 2>&1
if %errorLevel% == 0 (
    echo Success: Administrative permissions confirmed.
) else (
    echo Run this batch AS Administrator. Exiting...
    pause >nul
    exit
)
echo.
set current_dir=%cd%

%windir%\Microsoft.NET\Framework\v4.0.30319\RegSvcs.exe /appname:%package% /u %current_dir%\%dllfile%
%windir%\Microsoft.NET\Framework\v4.0.30319\RegSvcs.exe /appdir:%current_dir% /appname:%package% %current_dir%\%dllfile%

echo.
pause