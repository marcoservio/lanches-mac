using LanchesMac.Models;
using LanchesMac.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components;

public class CarrinhoCompraResumo : ViewComponent
{
    private readonly CarrinhoCompra _carrinho;

    public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
    {
        _carrinho = carrinhoCompra;
    }

    public IViewComponentResult Invoke()
    {
        var itens = _carrinho.GetItens();

        _carrinho.Itens = itens;

        var carrinhoViewModel = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinho,
            Total = _carrinho.GetTotal()
        };

        return View(carrinhoViewModel);
    }
}
