@echo off
title 楚汉多语言系统
echo ************************************************
echo * 1.上传台湾多语言到服务器				
echo * 2.上传北美多语言到服务器						
echo * 3.上传印尼多语言到服务器
echo * 4.台湾多语言到本地					
echo * 5.北美多语言到本地		
echo * 6.印尼多语言到本地		
echo * 7.out台湾					
echo * 8.out北美		
echo * 9.out印尼
echo * 10.上传泰国多语言到服务器
echo * 11.泰国多语言到本地
echo * 12.out泰国			
echo * 13.上传越南多语言到服务器
echo * 14.越南多语言到本地
echo * 15.out越南			
echo * 16.上传德语多语言到服务器
echo * 17.德语多语言到本地
echo * 18.out德语
echo * 19.上传俄语多语言到服务器
echo * 20.俄语多语言到本地
echo * 21.out俄语
echo ************************************************

set /p choose=请选择要更新的多语言：

set war_i18n_cp=%JAVA_HOME%\lib
set war_i18n_cp=%war_i18n_cp%;lib\war-i18n.jar
set war_i18n_cp=%war_i18n_cp%;lib\poi-3.6-20091214.jar
set war_i18n_cp=%war_i18n_cp%;lib\poi-ooxml-3.6-20091214.jar
set war_i18n_cp=%war_i18n_cp%;lib\poi-ooxml-schemas-3.6-20091214.jar
set war_i18n_cp=%war_i18n_cp%;lib\poi-scratchpad-3.6-20091214.jar
set war_i18n_cp=%war_i18n_cp%;lib\pinyin4j-2.5.0.jar
set war_i18n_cp=%war_i18n_cp%;lib\ibatis-2.3.4.726.jar
set war_i18n_cp=%war_i18n_cp%;lib\mysql-connector-java-5.1.7-bin.jar


set tw_scripts=D:\war2\shared\branches\1.3.0.1\scripts
set tw_out=D:\war2\shared\branches\1.3.0.1\楚汉本地导出

set bm_scripts=D:\war2\shared\trunk\策划\scripts
set bm_out=D:\war2\shared\trunk\策划\out

set ina_scripts=D:\war2\shared\trunk\策划\scripts
set ina_out=D:\war2\shared\trunk\策划\out

if %choose% EQU 1 call :updateZH_TW

if %choose% EQU 2 call :updateBM

if %choose% EQU 3 call :updateIna

if %choose% EQU 4 call :exportZH_TW

if %choose% EQU 5 call :exportBM

if %choose% EQU 6 call :exportIna

if %choose% EQU 7 call :outZH_TW

if %choose% EQU 8 call :outBM

if %choose% EQU 9 call :outIna

if %choose% EQU 10 call :updateTL

if %choose% EQU 11 call :exportTL

if %choose% EQU 12 call :outTL

if %choose% EQU 13 call :updateVN

if %choose% EQU 14 call :exportVN

if %choose% EQU 15 call :outVN

if %choose% EQU 16 call :updateDE

if %choose% EQU 17 call :exportDE

if %choose% EQU 18 call :outDE

if %choose% EQU 19 call :updateRU

if %choose% EQU 20 call :exportRU

if %choose% EQU 21 call :outRU

pause
exit

:updateZH_TW
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-tw.properties') do (set dir="%%i")
echo 保存词库的来源多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-tw.properties') do (set dir="%%i")
echo 保存词库的系统多语言目录：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-tw.properties
goto :eof

:updateBM
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-bm.properties') do (set dir="%%i")
echo 保存词库的来源多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-bm.properties') do (set dir="%%i")
echo 保存词库的系统多语言目录：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-bm.properties
goto :eof

:updateIna
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-ina.properties') do (set dir="%%i")
echo 保存词库的来源多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-ina.properties') do (set dir="%%i")
echo 保存词库的系统多语言目录：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-ina.properties
goto :eof

:exportZH_TW
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-tw.properties') do (set dir="%%i")
echo 导出多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-tw.properties') do (set dir="%%i")
echo 导出系统多语言目录(sys_lang)：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-tw.properties
goto :eof

:exportBM
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-bm.properties') do (set dir="%%i")
echo 导出多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-bm.properties') do (set dir="%%i")
echo 导出系统多语言目录(sys_lang)：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-bm.properties
goto :eof

:exportIna
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-ina.properties') do (set dir="%%i")
echo 导出多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-ina.properties') do (set dir="%%i")
echo 导出系统多语言目录(sys_lang)：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-ina.properties
goto :eof

:outZH_TW
echo scripts目录：%tw_scripts%
echo out目录：%tw_out%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %tw_scripts% %tw_out% true 1 6
goto :eof

:outBM
echo scripts目录：%bm_scripts%
echo out目录：%bm_out%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %bm_scripts% %bm_out% true 1 6
goto :eof

:outIna
echo scripts目录：%ina_scripts%
echo out目录：%ina_out%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %ina_scripts% %ina_out% true 1 6
goto :eof

:updateTL
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-tl.properties') do (set dir="%%i")
echo 保存词库的来源多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-tl.properties') do (set dir="%%i")
echo 保存词库的系统多语言目录：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-tl.properties
goto :eof

:exportTL
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-tl.properties') do (set dir="%%i")
echo 导出多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-tl.properties') do (set dir="%%i")
echo 导出系统多语言目录(sys_lang)：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-tl.properties
goto :eof

:outTL
echo scripts目录：%tw_scripts%
echo out目录：%tl_out%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %tl_scripts% %tw_out% true 1 6
goto :eof

:updateVN
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-vn.properties') do (set dir="%%i")
echo 保存词库的来源多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-vn.properties') do (set dir="%%i")
echo 保存词库的系统多语言目录：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-vn.properties
goto :eof

:exportVN
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-vn.properties') do (set dir="%%i")
echo 导出多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-vn.properties') do (set dir="%%i")
echo 导出系统多语言目录(sys_lang)：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-vn.properties
goto :eof

:outVN
echo scripts目录：%tw_scripts%
echo out目录：%tl_out%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %tl_scripts% %tw_out% true 1 6
goto :eof

:updateDE
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-de.properties') do (set dir="%%i")
echo 保存词库的来源多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-de.properties') do (set dir="%%i")
echo 保存词库的系统多语言目录：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-de.properties
goto :eof

:exportDE
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-de.properties') do (set dir="%%i")
echo 导出多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-de.properties') do (set dir="%%i")
echo 导出系统多语言目录(sys_lang)：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-de.properties
goto :eof

:outDE
echo scripts目录：%bm_scripts%
echo out目录：%bm_out%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %bm_scripts% %bm_out% true 1 6
goto :eof

:updateRU
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-ru.properties') do (set dir="%%i")
echo 保存词库的来源多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-ru.properties') do (set dir="%%i")
echo 保存词库的系统多语言目录：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-ru.properties
goto :eof

:exportRU
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-ru.properties') do (set dir="%%i")
echo 导出多语言目录：%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-ru.properties') do (set dir="%%i")
echo 导出系统多语言目录(sys_lang)：%dir%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-ru.properties
goto :eof

:outRU
echo scripts目录：%bm_scripts%
echo out目录：%bm_out%
set /p right=路径是否正确（y/n）？
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %bm_scripts% %bm_out% true 1 6
goto :eof