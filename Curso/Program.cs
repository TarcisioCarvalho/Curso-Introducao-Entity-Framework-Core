using System;
using System.Collections.Generic;
using System.Linq;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //InserirDados();
            //InserirDadosEmMassa();
            //CadastrarPedido();
            //ConsultarPedidoCarregamentoAdiantado();
            AtualizarDados();
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(1);
            cliente.Nome = "Nome Alterado";

            db.SaveChanges();
        }
        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db
            .Pedidos
            .Include(p => p.Items)
            .ThenInclude(p => p.Produto)
            .ToList();

            System.Console.WriteLine(pedidos.Count);
        }
        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido 
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Items = new List<PedidoItem> 
                {
                    new PedidoItem 
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }
            };
            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void InserirDadosEmMassa()
        {
              Produto produto = new Produto
            {
                Descricao = "Primeiro Teste",
                CodigoBarras = "1234567891232",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };
            
                Cliente cliente = new Cliente
            {
                Nome = "Jeferson Jubileu",
                Cep = "99999000",
                Cidade = "Erechin",
                Estado = "RS",
                Telefone = "99000000001"
            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto,cliente);
            var registros = db.SaveChanges();
            System.Console.WriteLine($"O total de Registros {registros}");
        }

        private static void InserirDados()
        {
            Produto produto = new Produto
            {
                Descricao = "Primeiro Teste",
                CodigoBarras = "1234567891232",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            db.Produtos.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total de Registros {registros}");
        }
    }
}
