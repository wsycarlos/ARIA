export.bat 主要是为了导出多语言lang_xxx.xls 提供给翻译人员进行翻译。

第一导出,保证数据库hod_i18n无表,translator空,sourceExcelPath是run.bat导出的原始文件路径,outputPath是lang_xxx.xls存放目录,syslangExcelPath指向程序生成sys_lang.xls存放目录,dbVersion都是空
export.translator=

export.sourceExcelPath=E:/__SVN_CODE/goldrush/shared/out
export.outputPath=E:/__SVN_CODE/goldrush/shared/i18n/en
export.syslangExcelPath=E:/__SVN_CODE/goldrush/shared/i18n/zh_CN
export.dbVersion=
import.inputPath=E:/__SVN_CODE/goldrush/shared/i18n/en
import.dbVersion=

查看outputPath所指目录下应该若干lang_xxx.xls,这时候就可以进行翻译了,打开一个lang文件,进行翻译.
updatedb.bat 主要用于将翻译的文字进行版本管理.dbVersion不填写,则表示创建新版本(新表)。或者填写某一个已存在版本号(表名),将会覆盖那个版本的表

export.translator=db

export.sourceExcelPath=E:/__SVN_CODE/goldrush/shared/out
export.outputPath=E:/__SVN_CODE/goldrush/shared/i18n/en
export.syslangExcelPath=E:/__SVN_CODE/goldrush/shared/i18n/zh_CN
export.dbVersion=
import.inputPath=E:/__SVN_CODE/goldrush/shared/i18n/en
import.dbVersion=

替换导出exportAndReplace.bat,一般情况下,ExportAndReplaceApp是通过最后打版本的时候进行调用,直接生在目标版本的包中的。
export.sourceExcelPath export.outputPath export.syslangExcelPath 指向同一目录,放有待替换的策划xls和sys_lang.xls,会直接将中文替换成英文。
此处不会再使用lang_xxx.xls等文件,所有多语言都是从指定版本export.dbVersion中导出.

export.translator=db

export.sourceExcelPath=E:/__SVN_CODE/goldrush/shared/i18n/en
export.outputPath=E:/__SVN_CODE/goldrush/shared/i18n/en
export.syslangExcelPath=E:/__SVN_CODE/goldrush/shared/i18n/en
export.dbVersion=
import.inputPath=E:/__SVN_CODE/goldrush/shared/i18n/en
import.dbVersion=
