using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;
public class PedidoController : Controller
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly CarrinhoCompra _carrinhoCompra;

    public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
    {
        _pedidoRepository = pedidoRepository;
        _carrinhoCompra = carrinhoCompra;
    }

    [Authorize]
    public IActionResult Checkout()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Checkout(Pedido pedido)
    {
        int totalItens = 0;
        decimal precoTotal = decimal.Zero;

        var itens = _carrinhoCompra.GetItens();
        _carrinhoCompra.Itens = itens;

        if(_carrinhoCompra.Itens.Count == 0)
            ModelState.AddModelError("", "Seu carrinho está vaziom que tal incluir um lanche...");

        totalItens = itens.Sum(i => i.Quantidade);
        precoTotal = itens.Sum(i => i.Lanche.Preco * i.Quantidade);

        pedido.TotalItens = totalItens;
        pedido.Total = precoTotal;

        if (ModelState.IsValid)
        {
            _pedidoRepository.CriarPedido(pedido);

            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
            ViewBag.TotalPedido = _carrinhoCompra.GetTotal();

            _carrinhoCompra.Limpar();

            return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
        }

        return View(pedido);
    }
}
