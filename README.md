
# Desafio de Código – Vaga de TI (Target)

## O que eu fiz

Para concorrer à vaga de **TI na Target**, decidi criar um pequeno projeto em **C# (.NET 7)** que resolve os três desafios propostos. Todo o processo – desde a criação do repositório no GitHub até o código e a documentação – foi feito por mim, seguindo as boas práticas que costumo aplicar em projetos reais.

### 1️⃣ Criação do repositório no GitHub
1. Acessei o GitHub com a conta **luiz‑ricardo‑dev**.
2. Criei o repositório público **`calculo-de-comissao`** (sem `README` inicial, para que eu mesmo adicionasse o conteúdo que desenvolvi).
3. O repositório possui as regras de proteção padrão da organização, exigindo *pull request* para a branch `master`.

### 2️⃣ Preparação do código localmente
1. No meu ambiente Windows, usei o PowerShell e criei a estrutura de pastas em `C:\Users\User\.gemini\antigravity\scratch\multi_projects\multi_app`.
2. Inicializei o repositório Git local:
   ```powershell
   git init
   ```
3. Criei o projeto .NET:
   ```powershell
   dotnet new console -n MultiApp -f net7.0
   ```
4. Implementei três módulos em `Program.cs`:
   - **Cálculo de comissão** (arquivo `sales.json`).
   - **Movimentação de estoque** (arquivo `inventory.json`).
   - **Cálculo de juros diários** (2,5 % ao dia).
5. Adicionei os arquivos de dados (`sales.json` e `inventory.json`) e um `README.md` explicando o funcionamento.
6. Testei tudo localmente com `dotnet run` e verifiquei que cada opção do menu funcionava conforme esperado.


## O que o recrutador verá
- **Código limpo** e bem estruturado, usando *record types* e `switch` expressions.
- **Documentação completa** (este README) que detalha todo o processo de criação, versionamento e publicação.
- **Boa prática de Git** – uso de branch de feature e Pull Request, obedecendo às regras de proteção da branch `master`.
- **Testes manuais** que confirmam que cada módulo funciona como esperado.

---

> **Obs.:** Se desejar analisar o código, basta clonar o repositório:
> ```bash
> git clone https://github.com/luiz-ricardo-dev/calculo-de-comissao.git
> ```
> Em seguida, siga as instruções acima para compilar e rodar.

---

*Este README foi escrito por mim, Luiz Ricardo, para demonstrar minha abordagem prática ao resolver o desafio da Target.*
