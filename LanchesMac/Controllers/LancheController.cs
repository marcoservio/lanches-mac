using Humanizer;

using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class LancheController : Controller
{
    private readonly ILancheRepository _lancheRepository;

    public LancheController(ILancheRepository lancheRepository)
    {
        _lancheRepository = lancheRepository;
    }

    public IActionResult List(string categoria)
    {
        IEnumerable<Lanche> lanches;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrWhiteSpace(categoria))
        {
            lanches = _lancheRepository.Lanches.OrderBy(l => l.Id);
            categoriaAtual = "Todos os lanches";
        }
        else
        {
            lanches = _lancheRepository
                .Lanches
                .Where(l => l.Categoria.Nome.ToLower().Equals(categoria.ToLower())).OrderBy(l => l.Nome);

            categoriaAtual = categoria.Titleize();
        }

        var viewModel = new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        };

        return View(viewModel);
    }

    public IActionResult Details(int id)
    {
        var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.Id == id);

        return View(lanche);
    }

    public IActionResult Search(string searchString)
    {
        IEnumerable<Lanche> lanches;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrWhiteSpace(searchString))
        {
            lanches = _lancheRepository.Lanches.OrderBy(l => l.Id);
            categoriaAtual = "Todos os lanches";
        }
        else
        {
            lanches = _lancheRepository
                .Lanches
                .Where(l => l.Nome.ToLower().Contains(searchString.ToLower())).OrderBy(l => l.Nome);

            if (lanches.Any())
                categoriaAtual = "Lanches";
            else
                categoriaAtual = "Nenhum lanche foi encontrado";            
        }

        return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        });
    }
}
