using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;
    private readonly CarrinhoCompra _carrinhoCompra;

    public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
    {
        _context = context;
        _carrinhoCompra = carrinhoCompra;
    }

    public void CriarPedido(Pedido pedido)
    {
        pedido.Enviado = DateTime.Now;
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();

        var carrinhoItens = _carrinhoCompra.Itens;

        foreach (var item in carrinhoItens)
        {
            var pedidoDetalhe = new PedidoDetalhe()
            {
                Quantidade = item.Quantidade,
                LancheId = item.Lanche.Id,
                PedidoId = pedido.Id,
                Preco = item.Lanche.Preco
            };

            _context.PedidoDetalhes.Add(pedidoDetalhe);
        }

        _context.SaveChanges();
    }
}
