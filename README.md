# Avaliação de desenvolvedor B3

## Ferramentas 

### Back end
.Net 7.0

### Front End
node version: 22.0.0


## Para rodar o projeto

### Pelo arquivo .bat:

Dois cliques no arquivo **run-projects.bat** na raiz do projeto.

Observação: Algumas janelas de linha de comando vão ser abertas no processo. Não fechar as janelas.

Se o navegador não abrir com a URL http://localhost:4200, abrir esta URL em um Navegador http://localhost:4200.

Observação: Se ao abrir a URL http://localhost:4200 e o servidor do Angular ainda não estiver no ar, após finalizados os processo do .bat, atualizar a tela do navegador F5, ele poderá ser atualizado automaticamente.

Não obrigatório: O relatório de cobertura de código poderá será aberto no navegador.

Aguardar até o fim de todos os processos.

## Executar análise de cobertura

dotnet test B3EvaluationProject.UnitTests.csproj --collect:"XPlat Code Coverage"

reportgenerator -reports:./TestResults/*/coverage.cobertura.xml -targetdir:./TestResults/coverage-report
