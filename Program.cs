using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MultiApp;

// =============================================================
//  1. Calculadora de Comissão de Vendas
// =============================================================
public record Sale(
    [property: JsonPropertyName("vendedor")] string Seller,
    [property: JsonPropertyName("valor")] decimal Amount);

public record SalesRoot(
    [property: JsonPropertyName("vendas")] List<Sale> Vendas);

static class CommissionCalculator
{
    /// <summary>
    /// Calcula a comissão de acordo com o valor da venda.
    /// </summary>
    public static decimal ComputeCommission(decimal amount) => amount switch
    {
        < 100m => 0m,
        < 500m => amount * 0.01m,
        _ => amount * 0.05m,
    };

    public static void Run()
    {
        const string salesPath = "sales.json";
        if (!File.Exists(salesPath))
        {
            Console.Error.WriteLine($"Arquivo de vendas não encontrado: {salesPath}");
            return;
        }
        var json = File.ReadAllText(salesPath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var root = JsonSerializer.Deserialize<SalesRoot>(json, options) ?? new SalesRoot(new List<Sale>());

        var commissions = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        foreach (var sale in root.Vendas)
        {
            var com = ComputeCommission(sale.Amount);
            commissions.TryAdd(sale.Seller, 0m);
            commissions[sale.Seller] += com;
        }

        Console.WriteLine("=== Comissão por vendedor ===\n");
        foreach (var kvp in commissions)
        {
            Console.WriteLine($"{kvp.Key}: R$ {kvp.Value:N2}");
        }
    }
}

// =============================================================
//  2. Controle de Movimentação de Estoque
// =============================================================
public record Produto(
    [property: JsonPropertyName("codigoProduto")] int Codigo,
    [property: JsonPropertyName("descricaoProduto")] string Descricao,
    [property: JsonPropertyName("estoque")] int Quantidade);

public record InventarioRoot(
    [property: JsonPropertyName("estoque")] List<Produto> Produtos);

public class Movimento
{
    public int Id { get; init; }
    public int CodigoProduto { get; init; }
    public string Descricao { get; init; }
    public int Quantidade { get; init; } // positiva para entrada, negativa para saída
}

static class StockMovementManager
{
    private static int _nextId = 1;
    private static readonly List<Movimento> _historico = new();

    public static void Run()
    {
        const string inventoryPath = "inventory.json";
        if (!File.Exists(inventoryPath))
        {
            Console.Error.WriteLine($"Arquivo de estoque não encontrado: {inventoryPath}");
            return;
        }
        var json = File.ReadAllText(inventoryPath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var root = JsonSerializer.Deserialize<InventarioRoot>(json, options) ?? new InventarioRoot(new List<Produto>());

        while (true)
        {
            Console.WriteLine("\n--- Movimentação de Estoque ---");
            Console.WriteLine("Digite o código do produto (ou 0 para sair): ");
            if (!int.TryParse(Console.ReadLine(), out int codigo) || codigo < 0)
            {
                Console.WriteLine("Entrada inválida.");
                continue;
            }
            if (codigo == 0) break;

            var produto = root.Produtos.Find(p => p.Codigo == codigo);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                continue;
            }

            Console.Write("Entrada (+) ou Saída (-) da quantidade: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidade))
            {
                Console.WriteLine("Quantidade inválida.");
                continue;
            }

            Console.Write("Descrição da movimentação: ");
            var descricao = Console.ReadLine() ?? string.Empty;

            var movimento = new Movimento
            {
                Id = _nextId++,
                CodigoProduto = codigo,
                Quantidade = quantidade,
                Descricao = descricao
            };
            _historico.Add(movimento);

            // atualiza estoque
            var novoEstoque = produto.Quantidade + quantidade;
            if (novoEstoque < 0)
            {
                Console.WriteLine("Operação abortada – estoque ficaria negativo.");
                // desfaz movimento no histórico
                _historico.RemoveAt(_historico.Count - 1);
                _nextId--;
            }
            else
            {
                // substitui o produto atualizado
                root.Produtos.Remove(produto);
                root.Produtos.Add(produto with { Quantidade = novoEstoque });
                // salva novamente no arquivo
                var novoJson = JsonSerializer.Serialize(root, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(inventoryPath, novoJson);
                Console.WriteLine($"Movimento registrada (ID={movimento.Id}). Estoque atual de {produto.Descricao}: {novoEstoque}");
            }
        }
    }
}

// =============================================================
//  3. Cálculo de Juros Diários
// =============================================================
static class InterestCalculator
{
    /// <summary>
    /// Calcula juros simples a 2,5% por dia de atraso.
    /// Se a data de vencimento ainda não ocorreu, o valor é zero.
    /// </summary>
    public static decimal CalculateInterest(decimal principal, DateTime dueDate, DateTime today)
    {
        if (today <= dueDate) return 0m;
        var daysLate = (today - dueDate).Days;
        var dailyRate = 0.025m; // 2,5% ao dia
        return principal * dailyRate * daysLate;
    }

    public static void Run()
    {
        Console.Write("Valor principal (R$): ");
        if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
        {
            Console.WriteLine("Valor inválido.");
            return;
        }
        Console.Write("Data de vencimento (yyyy-MM-dd): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dueDate))
        {
            Console.WriteLine("Data inválida.");
            return;
        }
        var today = DateTime.Today;
        var interest = CalculateInterest(value, dueDate, today);
        Console.WriteLine($"Juros acumulado até {today:yyyy-MM-dd}: R$ {interest:N2}");
        Console.WriteLine($"Valor total a pagar: R$ {(value + interest):N2}");
    }
}

// =============================================================
//  Programa principal – menu de escolha
// =============================================================
class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== Aplicação Multi‑Função ===");
            Console.WriteLine("1 – Calcular comissão de vendedores");
            Console.WriteLine("2 – Movimentar estoque de produtos");
            Console.WriteLine("3 – Calcular juros de dívida");
            Console.WriteLine("0 – Sair");
            Console.Write("Escolha uma opção: ");
            var opt = Console.ReadLine();
            switch (opt)
            {
                case "1":
                    CommissionCalculator.Run();
                    break;
                case "2":
                    StockMovementManager.Run();
                    break;
                case "3":
                    InterestCalculator.Run();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
