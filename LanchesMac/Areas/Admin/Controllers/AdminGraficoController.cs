﻿using LanchesMac.Areas.Admin.Services;
using LanchesMac.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = nameof(Roles.Admin))]
public class AdminGraficoController : Controller
{
    private readonly GraficoVendasService _graficoVendas;

    public AdminGraficoController(GraficoVendasService graficoVendas)
    {
        _graficoVendas = graficoVendas;
    }

    public JsonResult VendasLanches(int dias)
    {
        var lanchesVendasTotais = _graficoVendas.GetVendasLanches(dias);

        return Json(lanchesVendasTotais);
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult VendasMensal()
    {
        return View();
    }

    [HttpGet]
    public IActionResult VendasSemanal()
    {
        return View();
    }
}
