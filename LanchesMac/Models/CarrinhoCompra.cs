﻿using LanchesMac.Context;

using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models;

public class CarrinhoCompra
{
    private readonly AppDbContext _context;

    public CarrinhoCompra(AppDbContext context)
    {
        _context = context;
    }

    public string Id { get; set; }
    public List<CarrinhoCompraItem> Itens { get; set; }

    public static CarrinhoCompra Get (IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        var context = services.GetService<AppDbContext>();

        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        session.SetString("CarrinhoId", carrinhoId);

        return new CarrinhoCompra(context)
        {
            Id = carrinhoId
        };
    }

    public void Add(Lanche lanche)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(s => 
                         s.Lanche.Id == lanche.Id && s.CarrinhoCompraId == Id);

        if(carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = Id,
                Lanche = lanche,
                Quantidade = 1
            };

            _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
        }

        _context.SaveChanges();
    }

    public int Remover(Lanche lanche)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(s =>
                         s.Lanche.Id == lanche.Id && s.CarrinhoCompraId == Id);

        var quantidadeLocal = 0;

        if(carrinhoCompraItem != null)
        {
            if (carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
                quantidadeLocal = carrinhoCompraItem.Quantidade;
            }
            else
                _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
        }

        _context.SaveChanges();

        return quantidadeLocal;
    }

    public List<CarrinhoCompraItem> GetItens()
    {
        return Itens ??= _context.CarrinhoCompraItens
                                                .Where(c => c.CarrinhoCompraId == Id)
                                                .Include(c => c.Lanche)
                                                .ToList();
    }

    public void Limpar()
    {
        var carrinhoItens = _context.CarrinhoCompraItens
                            .Where(c => c.CarrinhoCompraId == Id);

        _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);

        _context.SaveChanges();
    }

    public decimal GetTotal()
    {
        return _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == Id)
                                           .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
    }
}
