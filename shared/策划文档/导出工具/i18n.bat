@echo off
title ����������ϵͳ
echo ************************************************
echo * 1.�ϴ�̨������Ե�������				
echo * 2.�ϴ����������Ե�������						
echo * 3.�ϴ�ӡ������Ե�������
echo * 4.̨������Ե�����					
echo * 5.���������Ե�����		
echo * 6.ӡ������Ե�����		
echo * 7.out̨��					
echo * 8.out����		
echo * 9.outӡ��
echo * 10.�ϴ�̩�������Ե�������
echo * 11.̩�������Ե�����
echo * 12.out̩��			
echo * 13.�ϴ�Խ�϶����Ե�������
echo * 14.Խ�϶����Ե�����
echo * 15.outԽ��			
echo * 16.�ϴ���������Ե�������
echo * 17.��������Ե�����
echo * 18.out����
echo * 19.�ϴ���������Ե�������
echo * 20.��������Ե�����
echo * 21.out����
echo ************************************************

set /p choose=��ѡ��Ҫ���µĶ����ԣ�

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
set tw_out=D:\war2\shared\branches\1.3.0.1\�������ص���

set bm_scripts=D:\war2\shared\trunk\�߻�\scripts
set bm_out=D:\war2\shared\trunk\�߻�\out

set ina_scripts=D:\war2\shared\trunk\�߻�\scripts
set ina_out=D:\war2\shared\trunk\�߻�\out

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
echo ����ʿ����Դ������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-tw.properties') do (set dir="%%i")
echo ����ʿ��ϵͳ������Ŀ¼��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-tw.properties
goto :eof

:updateBM
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-bm.properties') do (set dir="%%i")
echo ����ʿ����Դ������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-bm.properties') do (set dir="%%i")
echo ����ʿ��ϵͳ������Ŀ¼��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-bm.properties
goto :eof

:updateIna
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-ina.properties') do (set dir="%%i")
echo ����ʿ����Դ������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-ina.properties') do (set dir="%%i")
echo ����ʿ��ϵͳ������Ŀ¼��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-ina.properties
goto :eof

:exportZH_TW
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-tw.properties') do (set dir="%%i")
echo ����������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-tw.properties') do (set dir="%%i")
echo ����ϵͳ������Ŀ¼(sys_lang)��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-tw.properties
goto :eof

:exportBM
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-bm.properties') do (set dir="%%i")
echo ����������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-bm.properties') do (set dir="%%i")
echo ����ϵͳ������Ŀ¼(sys_lang)��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-bm.properties
goto :eof

:exportIna
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-ina.properties') do (set dir="%%i")
echo ����������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-ina.properties') do (set dir="%%i")
echo ����ϵͳ������Ŀ¼(sys_lang)��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-ina.properties
goto :eof

:outZH_TW
echo scriptsĿ¼��%tw_scripts%
echo outĿ¼��%tw_out%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %tw_scripts% %tw_out% true 1 6
goto :eof

:outBM
echo scriptsĿ¼��%bm_scripts%
echo outĿ¼��%bm_out%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %bm_scripts% %bm_out% true 1 6
goto :eof

:outIna
echo scriptsĿ¼��%ina_scripts%
echo outĿ¼��%ina_out%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %ina_scripts% %ina_out% true 1 6
goto :eof

:updateTL
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-tl.properties') do (set dir="%%i")
echo ����ʿ����Դ������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-tl.properties') do (set dir="%%i")
echo ����ʿ��ϵͳ������Ŀ¼��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-tl.properties
goto :eof

:exportTL
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-tl.properties') do (set dir="%%i")
echo ����������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-tl.properties') do (set dir="%%i")
echo ����ϵͳ������Ŀ¼(sys_lang)��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-tl.properties
goto :eof

:outTL
echo scriptsĿ¼��%tw_scripts%
echo outĿ¼��%tl_out%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %tl_scripts% %tw_out% true 1 6
goto :eof

:updateVN
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-vn.properties') do (set dir="%%i")
echo ����ʿ����Դ������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-vn.properties') do (set dir="%%i")
echo ����ʿ��ϵͳ������Ŀ¼��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-vn.properties
goto :eof

:exportVN
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-vn.properties') do (set dir="%%i")
echo ����������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-vn.properties') do (set dir="%%i")
echo ����ϵͳ������Ŀ¼(sys_lang)��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-vn.properties
goto :eof

:outVN
echo scriptsĿ¼��%tw_scripts%
echo outĿ¼��%tl_out%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %tl_scripts% %tw_out% true 1 6
goto :eof

:updateDE
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-de.properties') do (set dir="%%i")
echo ����ʿ����Դ������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-de.properties') do (set dir="%%i")
echo ����ʿ��ϵͳ������Ŀ¼��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-de.properties
goto :eof

:exportDE
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-de.properties') do (set dir="%%i")
echo ����������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-de.properties') do (set dir="%%i")
echo ����ϵͳ������Ŀ¼(sys_lang)��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-de.properties
goto :eof

:outDE
echo scriptsĿ¼��%bm_scripts%
echo outĿ¼��%bm_out%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %bm_scripts% %bm_out% true 1 6
goto :eof

:updateRU
for /f %%i in ('findstr /b /i "IMPORT.i18nXlsDir" conf\war-i18n-ru.properties') do (set dir="%%i")
echo ����ʿ����Դ������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-ru.properties') do (set dir="%%i")
echo ����ʿ��ϵͳ������Ŀ¼��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.UpdateLangDBApp conf\war-i18n-ru.properties
goto :eof

:exportRU
for /f %%i in ('findstr /b /i "EXPORT.i18nXlsDir" conf\war-i18n-ru.properties') do (set dir="%%i")
echo ����������Ŀ¼��%dir%
for /f %%i in ('findstr /b /i "SYSLANG.i18nXlsdir" conf\war-i18n-ru.properties') do (set dir="%%i")
echo ����ϵͳ������Ŀ¼(sys_lang)��%dir%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n-ru.properties
goto :eof

:outRU
echo scriptsĿ¼��%bm_scripts%
echo outĿ¼��%bm_out%
set /p right=·���Ƿ���ȷ��y/n����
if %right% EQU n exit
java -cp "./*;./lib/*" cn.time2play.war.tools.data.ExportExcel %bm_scripts% %bm_out% true 1 6
goto :eof