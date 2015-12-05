@echo OFF
@echo "%cd%"
@echo "%~dp0"
cd %~dp0
cd ..
nuget pack src\Core\Core.csproj -Prop Configuration=Release -OutputDirectory ".\\bin\\"
