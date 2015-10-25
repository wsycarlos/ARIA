for %%i in (".\*.thrift") do thrift --gen csharp:server %%i
xcopy /s /e /i /y .\gen-csharp .\server &&rd /s /q .\gen-csharp
for %%i in (".\*.thrift") do thrift --gen csharp:cob %%i
xcopy /s /e /i /y .\gen-csharp\Limbo\Net .\..\..\Client\trunk\Assets\Net\Message &&rd /s /q .\gen-csharp
pause