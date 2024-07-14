@echo off
setlocal

echo Iniciando os servers Front e Back do projeto. Aguarde ...

REM Obter o diretório do arquivo .bat
set SCRIPT_DIR=%~dp0

REM Verificar se o ReportGenerator está instalado
echo Verificando se o ReportGenerator esta instalado...
dotnet tool list -g | findstr /I "dotnet-reportgenerator-globaltool"
if %ERRORLEVEL% NEQ 0 (
    echo Instalando o ReportGenerator...
    dotnet tool install -g dotnet-reportgenerator-globaltool
    if %ERRORLEVEL% NEQ 0 (
        echo Falha ao instalar o ReportGenerator. Abortando...
        pause
        exit /b %ERRORLEVEL%
    )
)

REM Navegar para o diretório do front-end
cd B3EvaluationProject.FrontEnd

REM Desativar o prompt de compartilhamento de dados do Angular CLI
echo Configurando Angular CLI para desativar o prompt de compartilhamento de dados...
cmd /c "ng analytics off"


REM Instalar pacotes npm
echo Instalando pacotes npm...
cmd /c "npm install || (echo Falha ao instalar pacotes npm. && pause && exit /b 1)"

REM Verificar se o npm install foi bem-sucedido
if %ERRORLEVEL% NEQ 0 (
    exit /b %ERRORLEVEL%
)

REM Iniciar o servidor Angular
echo Iniciando o servidor Angular...
start cmd /k "npm start"

REM Verificar se o servidor Angular iniciou
if %ERRORLEVEL% NEQ 0 (
    exit /b %ERRORLEVEL%
)

REM Voltar para o diretório principal
cd ..

REM Iniciar a API .NET
echo Iniciando a API .NET...
start cmd /k "dotnet run --project B3EvaluationProject.Api/B3EvaluationProject.Api.csproj"

REM Aguardar 10 segundos para garantir que os servidores estejam em execução
timeout /t 10


REM Navegar para o diretório de testes unitários
cd B3EvaluationProject.Tests

REM Executar testes unitários com coleta de cobertura
echo Executando testes unitarios...
dotnet test B3EvaluationProject.Tests.csproj --collect:"XPlat Code Coverage"
if %ERRORLEVEL% NEQ 0 (
    echo Testes falharam. Vamos ignorar esse passo.
)


REM Gerar relatorio de cobertura
echo Gerando relatório de cobertura...
reportgenerator -reports:./TestResults/*/coverage.cobertura.xml -targetdir:./TestResults/coverage-report
if %ERRORLEVEL% NEQ 0 (
    echo Falha ao gerar relatório de cobertura. Vamos ignorar esse passo.
)

REM Aguardar 10 segundos para garantir que os servidores estejam em execução
timeout /t 10

REM Abrir o aplicativo Angular no navegador
start http://localhost:4200/

REM Abrir relatório no navegador
start "" "file:///%SCRIPT_DIR%B3EvaluationProject.Tests/TestResults/coverage-report/index.html"


pause
endlocal
