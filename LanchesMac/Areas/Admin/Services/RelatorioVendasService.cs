﻿using LanchesMac.Context;
using LanchesMac.Models;

using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Services;

public class RelatorioVendasService
{
    private readonly AppDbContext _context;

    public RelatorioVendasService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
        var resultado = from obj in _context.Pedidos select obj;

        if (minDate.HasValue)
            resultado = resultado.Where(x => x.Enviado >= minDate.Value);
        if(maxDate.HasValue)
            resultado = resultado.Where(x => x.Enviado <= maxDate.Value);

        return await resultado
            .Include(l => l.PedidoItens)
            .ThenInclude(l => l.Lanche)
            .OrderByDescending(x => x.Enviado)
            .ToListAsync();
    }
}
