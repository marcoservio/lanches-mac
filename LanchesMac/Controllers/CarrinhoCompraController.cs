using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;
public class CarrinhoCompraController : Controller
{
    private readonly ILancheRepository _lancheRepository;
    private readonly CarrinhoCompra _carrinho;

    public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinho)
    {
        _lancheRepository = lancheRepository;
        _carrinho = carrinho;
    }

    public IActionResult Index()
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

    [Authorize]
    public IActionResult AdicionarItem(int id)
    {
        var lanche = _lancheRepository.Lanches.FirstOrDefault(p => p.Id == id);

        if(lanche != null)
            _carrinho.Add(lanche);

        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public IActionResult RemoverItem(int id)
    {
        var lanche = _lancheRepository.Lanches.FirstOrDefault(p => p.Id == id);

        if (lanche != null)
            _carrinho.Remover(lanche);

        return RedirectToAction(nameof(Index));
    }
}
