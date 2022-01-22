REM %0 - œcie¿ka z której uruchamiany jest deploy.bat
REM %1 - katalog z binariami
REM %2 - katalog ze zrodlami
REM %3 - nazwa projektu

::Wywalam Net z nazwy projektu - w tym komponencie nie trzeba
set prj=%~3
REM call set prj=%%prj:Net=%%

@echo off
SET _deployBat=%~dp0
SET _destDir="%_deployBat%components.net\_ComNameToReplace_NET\_ComNameToReplace_.dll\"
SET _binDir=%1
SET _srcDir=%2
SET _confName="%prj%.dll.config"
SET _confName_upd="%prj%.dll.config_upd"

::kasuje cudzyslow
SET _destDir=%_destDir:"=%
SET _srcDir=%_srcDir:"=%
SET _binDir=%_binDir:"=%

::kasuje trailing backslash
IF %_destDir:~-1%==\ SET _destDir=%_destDir:~0,-1%
IF %_srcDir:~-1%==\ SET _srcDir=%_srcDir:~0,-1%
IF %_binDir:~-1%==\ SET _binDir=%_binDir:~0,-1%

@echo "Katalog docelowy" = %_destDir%
@echo "Katalog ze zrodlami" = %_srcDir%
@echo "Katalog z binariami" = %_binDir%

IF EXIST "%_destDir%" RMDIR "%_destDir%" /S /Q
IF NOT EXIST "%_destDir%\Bin" MKDIR "%_destDir%\Bin"
IF NOT EXIST "%_destDir%\Src" MKDIR "%_destDir%\Src"

::kopiowanie binarek
XCOPY "%_binDir%" "%_destDir%\Bin" /E/I/C/R/Y/H
REN "%_destDir%\Bin\Application.config" "Application.config_upd"
IF EXIST "%_destDir%\Bin\%_confName%" REN "%_destDir%\Bin\%_confName%" "%_confName_upd%"
::kopiowanie zrodel - w zrodlach jest katalog bin i katalog obj ktorych mozna sie pozbyc
XCOPY "%_srcDir%" "%_destDir%\Src" /E/I/C/R/Y/H
RMDIR "%_destDir%\Src\bin" /S /Q
RMDIR "%_destDir%\Src\obj" /S /Q

::kasuje PROJECT_NAME.dll.config_upd (Application.config_upd wystarczy)
IF EXIST "%_destDir%\Bin\%_confName_upd%" DEL "%_destDir%\Bin\%_confName_upd%"
