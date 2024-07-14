## Observações ao rodar o arquivo run-projects.bat manualmente

Dois cliques no arquivo **run-projects.bat** na raiz do projeto.

Observação: Algumas janelas de linha de comando vão ser abertas no processo. Não fechar as janelas.

Se o navegador não abrir com a URL http://localhost:4200, abrir esta URL em um Navegador http://localhost:4200.

Observação: Se ao abrir a URL http://localhost:4200 e o servidor do Angular ainda não estiver no ar, após finalizados os processo do .bat, atualizar a tela do navegador F5, ele poderá ser atualizado automaticamente.

Não obrigatório: O relatório de cobertura de código poderá será aberto no navegador.

## Ferramentas 

### Back end
.Net 7.0

### Front End
node version: 22.0.0

# Variáveis
DOTNET = dotnet

NPM = npm

SOLUTION = B3EvaluationProject.sln

API_PROJECT = B3EvaluationProject.Api/B3EvaluationProject.Api.csproj

TEST_PROJECT = B3EvaluationProject.UnitTests/B3EvaluationProject.UnitTests.csproj

ANGULAR_DIR = B3EvaluationProject.FrontEnd

COVERAGE_REPORT = B3EvaluationProject.UnitTests/TestResults/coverage-report/index.html

BAT_FILE = run-projects.bat

# Regra padrão
all: run-bat

# Regra para executar o arquivo .bat
run-bat:

	@echo Executando o script $(BAT_FILE)...

	@$(BAT_FILE)


# Regra secundária
all: restore build test frontend

# Restaurar pacotes
restore:

	$(DOTNET) restore $(SOLUTION)

	cd $(ANGULAR_DIR) && $(NPM) install

# Compilar a solução
build:

	$(DOTNET) build $(SOLUTION)

# Executar testes unitários
test:

	$(DOTNET) test $(TEST_PROJECT) --collect:"XPlat Code Coverage"

	$(DOTNET) reportgenerator -reports:./B3EvaluationProject.UnitTests/TestResults/*/coverage.cobertura.xml -targetdir:./B3EvaluationProject.UnitTests/TestResults/coverage-report

# Construir o projeto Angular
frontend:

	cd $(ANGULAR_DIR) && $(NPM) run build

# Executar a API e o Frontend
run: run-api run-frontend

run-api:

	$(DOTNET) run --project $(API_PROJECT)

run-frontend:

	cd $(ANGULAR_DIR) && $(NPM) start

# Abrir o relatório de cobertura
coverage:

	start $(COVERAGE_REPORT)

# Limpar arquivos de construção
clean:

	$(DOTNET) clean $(SOLUTION)

	cd $(ANGULAR_DIR) && $(NPM) run clean

# Limpar arquivos gerados pelo npm
clean-npm:

	cd $(ANGULAR_DIR) && rm -rf node_modules package-lock.json

.PHONY: all restore build test frontend run run-api run-frontend coverage clean clean-npm



