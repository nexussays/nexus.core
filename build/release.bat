@echo OFF
@echo "%cd%"
@echo "%~dp0"
cd %~dp0
cd ..
nuget pack src\nexus.core\nexus.core.csproj -Prop Configuration=Release -OutputDirectory ".\\artifacts\\bin\\"
nuget pack src\nexus.core.logging\nexus.core.logging.csproj -Prop Configuration=Release -OutputDirectory ".\\artifacts\\bin\\"
nuget pack src\nexus.core.logging-net\nexus.core.logging-net.csproj -Prop Configuration=Release -OutputDirectory ".\\artifacts\\bin\\"
nuget pack src\nexus.core.logging-ios\nexus.core.logging-ios.csproj -Prop Configuration=Release -OutputDirectory ".\\artifacts\\bin\\"
nuget pack src\nexus.core.logging-android\nexus.core.logging-android.csproj -Prop Configuration=Release -OutputDirectory ".\\artifacts\\bin\\"
