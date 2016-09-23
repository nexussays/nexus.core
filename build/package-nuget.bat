@echo OFF
@echo "%cd%"
@echo "%~dp0"
cd %~dp0
cd ..

@echo **
@echo **nexus.core
nuget pack src\nexus.core\nexus.core.csproj -Prop Configuration=Release -OutputDirectory ".\\artifacts\\bin\\"

@echo **
@echo **nexus.core.logging
nuget pack src\nexus.core.logging\nexus.core.logging.csproj -IncludeReferencedProjects -Prop Configuration=Release -OutputDirectory ".\\artifacts\\bin\\"

@echo **
@echo **nexus.core.logging-net
nuget pack src\nexus.core.logging-net\nexus.core.logging-net.csproj -IncludeReferencedProjects -Prop Configuration=Release -OutputDirectory ".\\artifacts\\bin\\"

@echo **
@echo **nexus.core.logging-ios
nuget pack src\nexus.core.logging-ios\nexus.core.logging-ios.csproj -IncludeReferencedProjects -Prop Configuration=Release -OutputDirectory ".\\artifacts\\bin\\"

@echo **
@echo **nexus.core.logging-android
nuget pack src\nexus.core.logging-android\nexus.core.logging-android.csproj -IncludeReferencedProjects -Prop Configuration=Release -OutputDirectory ".\\artifacts\\bin\\"
