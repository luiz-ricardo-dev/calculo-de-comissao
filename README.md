# Desafio de Código – Vaga de TI (Target)

## Visão geral
Este repositório contém uma aplicação console **C# (.NET 7)** que resolve três problemas propostos em um mesmo código, facilitando a avaliação de competências técnicas para a vaga de TI na **Target**.

A aplicação foi organizada em três módulos independentes, mas acessíveis a partir de um **menu único**:

| Módulo | Descrição | Entrada | Saída |
|--------|-----------|---------|-------|
| **1 – Comissão de Vendedores** | Calcula a comissão de cada vendedor a partir de um arquivo JSON com as vendas. | `sales.json` (já incluído) | Listagem no console da comissão total por vendedor. |
| **2 – Movimentação de Estoque** | Permite registrar entradas e saídas de produtos, gera um identificador único e uma descrição livre para cada movimentação. | `inventory.json` (já incluído) | Atualiza o estoque no arquivo e exibe o estoque final do produto movimentado. |
| **3 – Cálculo de Juros Diários** | A partir de um valor principal e da data de vencimento, calcula juros simples de **2,5 % ao dia** até a data corrente. | Valor e data (digitação no console) | Valor dos juros e valor total a pagar. |

---

## Estrutura de pastas
```
multi_projects/                # raiz do projeto multi‑função
│
├─ multi_app/                 # aplicação principal
│   │   MultiApp.csproj      # projeto .NET 7
│   │   Program.cs           # código‑fonte com os três módulos
│   │   sales.json           # dados de vendas (exemplo)
│   │   README.md            # **este** arquivo
│   │
├─ stock_movement/            # dados de estoque usados pelo módulo 2
│   │   inventory.json       # estoque inicial dos produtos
│
└─ interest/                  # placeholder para possíveis extensões do módulo 3
    │   Interest.csproj      # (vazio, mantido por consistência)
```

---

## Pré‑requisitos
- **.NET 7 SDK** (ou superior) – <https://dotnet.microsoft.com/download>
- **Git** (opcional, para versionamento e envio ao GitHub)
- **PowerShell** ou **cmd** (Windows) para executar os comandos abaixo.

---

## Como compilar e executar
1. **Abra o PowerShell** e navegue até a pasta `multi_app`:
   ```powershell
   cd C:\Users\User\.gemini\antigravity\scratch\multi_projects\multi_app
   ```
2. **Compile** o projeto (gerará o executável na pasta `bin`):
   ```powershell
   dotnet build
   ```
3. **Execute** a aplicação:
   ```powershell
   dotnet run
   ```
4. O console exibirá um menu:
   ```text
   === Aplicação Multi‑Função ===
   1 – Calcular comissão de vendedores
   2 – Movimentar estoque de produtos
   3 – Calcular juros de dívida
   0 – Sair
   ```
   Digite a opção desejada e siga as instruções exibidas.

---

## Detalhes de cada módulo
### 1. Comissão de Vendedores
- **Arquivo de entrada:** `sales.json` (já incluso). Cada registro possui `vendedor` e `valor`.
- **Regra de cálculo:**
  - Valor < R$ 100 → 0 %
  - Valor < R$ 500 → 1 %
  - Valor ≥ R$ 500 → 5 %
- **Resultado:** lista no console, por exemplo:
  ```text
  João Silva: R$ 534,61
  Maria Souza: R$ 494,86
  Carlos Oliveira: R$ 403,03
  Ana Lima: R$ 449,69
  ```

### 2. Movimentação de Estoque
- **Arquivo de entrada/saída:** `inventory.json` (contém código, descrição e quantidade atual).
- **Campos da movimentação:**
  - `Id` – número sequencial gerado automaticamente.
  - `CodigoProduto` – identifica o produto.
  - `Descricao` – texto livre informado pelo usuário.
  - `Quantidade` – número positivo (entrada) ou negativo (saída).
- **Validação:** o programa impede que o estoque fique negativo.
- **Persistência:** ao confirmar a operação, o arquivo `inventory.json` é sobrescrito com o novo estoque.

### 3. Cálculo de Juros Diários
- **Entrada:**
  - Valor principal (ex.: `1500.75`).
  - Data de vencimento no formato `yyyy-MM-dd` (ex.: `2024-09-01`).
- **Regra:** se a data de vencimento já passou, aplica **2,5 % ao dia** de juros simples.
- **Saída:** mostra juros acumulados até a data de hoje e o total a pagar.
  ```text
  Juros acumulado até 2026-10-06: R$ 212,50
  Valor total a pagar: R$ 1712,50
  ```

---

## Publicação no GitHub (opcional)
Se quiser disponibilizar o código para avaliação, siga os passos abaixo:
1. Crie um repositório público na sua conta GitHub, por exemplo `calculo-de-comissao`.
2. Dentro da pasta `multi_app` execute:
   ```powershell
   git init
   git remote add origin https://github.com/luiz-ricardo-dev/calculo-de-comissao.git
   git add .
   git commit -m "Desafio Target – aplicação multi‑função"
   git push -u origin master
   ```
   - Quando o Git solicitar credenciais, use **seu nome de usuário** (`luiz-ricardo-dev`) e **o PAT** que você recebeu (`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`).
3. Verifique o repositório em `https://github.com/luiz-ricardo-dev/calculo-de-comissao`.

---

## Como o código atende ao desafio da Target
- **Clareza e organização** – Cada funcionalidade está encapsulada em uma classe estática com método `Run()`, facilitando a leitura e a manutenção.
- **Boas práticas C#** – Uso de *record types* (imutáveis), `switch` expression, `System.Text.Json` para (de)serialização, e `StringComparer.OrdinalIgnoreCase` nos dicionários.
- **Validação de entrada** – O programa verifica valores, datas e impede estoques negativos, demonstrando preocupação com integridade de dados.
- **Internacionalização mínima** – Formatação numérica (`N2`) e mensagens em português, adequadas ao público‑alvo.
- **Facilidade de execução** – Não há dependências externas além do .NET SDK; tudo roda em um simples `dotnet run`.
- **Documentação completa** – Este `README.md` contém instruções de compilação, uso, detalhes das regras de negócios e orientações para publicação.

---

## Próximos passos sugeridos (para um candidato que queira ir além)
- **Testes unitários** com `xUnit` para validar cada regra de negócio.
- **Interface gráfica** (WinForms, WPF ou MAUI) para melhorar a experiência do usuário.
- **Persistência em banco de dados** (ex.: SQLite + EF Core) ao invés de arquivos JSON.
- **CI/CD** com GitHub Actions automatizando build, teste e lint a cada *push*.
- **Docker** – criar um container para garantir que a aplicação executa em qualquer ambiente.

---

## Contato
Caso haja dúvidas técnicas ou necessidade de ajustes, fique à vontade para abrir *issues* no repositório ou entrar em contato diretamente.

**Boa sorte no processo seletivo!**

---

*Este README foi gerado automaticamente para atender ao desafio de código solicitado pela empresa **Target**.*
