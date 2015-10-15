set war_i18n_cp=%JAVA_HOME%\lib
set war_i18n_cp=%war_i18n_cp%;lib\war-i18n.jar
set war_i18n_cp=%war_i18n_cp%;lib\poi-3.6-20091214.jar
set war_i18n_cp=%war_i18n_cp%;lib\poi-ooxml-3.6-20091214.jar
set war_i18n_cp=%war_i18n_cp%;lib\poi-ooxml-schemas-3.6-20091214.jar
set war_i18n_cp=%war_i18n_cp%;lib\poi-scratchpad-3.6-20091214.jar
set war_i18n_cp=%war_i18n_cp%;lib\pinyin4j-2.5.0.jar
set war_i18n_cp=%war_i18n_cp%;lib\ibatis-2.3.4.726.jar
set war_i18n_cp=%war_i18n_cp%;lib\mysql-connector-java-5.1.7-bin.jar

java -cp "%war_i18n_cp%" cn.time2play.war.tools.i18n.ExportApp conf\war-i18n_export.properties

pause