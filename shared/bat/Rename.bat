E:
set SOURCE="E:\workspace\ARIA\client\AriaEditor\Assets\ZGame\AssetPackage\Export\CScript"
set DLL_PATH=E:\workspace\ARIA\client\AriaExtends\AriaExtends\bin\Debug\

cd %DLL_PATH%

cp -r -f %DLL_PATH%AriaExtends.dll %SOURCE%
cp -r -f %DLL_PATH%AriaExtends.pdb %SOURCE%

cd %SOURCE%

del /a /f /s /q *.bytes

for /f %%i in ('dir /b *.dll') do ren "%%i" %%~ni.dll.bytes
for /f %%i in ('dir /b *.pdb') do ren "%%i" %%~ni.pdb.bytes

pause