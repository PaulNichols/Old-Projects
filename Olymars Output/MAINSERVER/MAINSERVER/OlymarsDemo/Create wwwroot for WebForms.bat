@echo off
md "wwwroot for WebForms"
cd "wwwroot for WebForms"
md Bin
cd ..
del /Q "wwwroot for WebForms\Bin\*.*
del /Q "wwwroot for WebForms\*.aspx
del /Q "wwwroot for WebForms\*.htm
del /Q "wwwroot for WebForms\*.css
copy Bin\OlymarsDemo.dll "wwwroot for WebForms\Bin"
copy Bin\OlymarsDemo.pdb "wwwroot for WebForms\Bin"
copy Bin\OlymarsDemo.Forms.dll "wwwroot for WebForms\Bin"
copy Bin\OlymarsDemo.Forms.pdb "wwwroot for WebForms\Bin"
copy Web\Forms\CS\*.aspx "wwwroot for WebForms"
copy Web\Forms\CS\*.css "wwwroot for WebForms"
copy Web\Forms\CS\*.htm "wwwroot for WebForms"
IF NOT EXIST "wwwroot for WebForms\web.config" copy "Web\Forms\web.config" "wwwroot for WebForms"
if "%1" == "" pause
