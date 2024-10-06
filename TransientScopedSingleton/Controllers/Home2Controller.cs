﻿using Microsoft.AspNetCore.Mvc;
using TransientScopedSingleton.Models;

namespace TransientScopedSingleton.Controllers
{
    public class Home2Controller : Controller
    {
        private readonly ITransitent transitent1;
        private readonly IScoped scoped1;
        private readonly ISingleton singleton1;

        private readonly ITransitent transitent2;
        private readonly IScoped scoped2;
        private readonly ISingleton singleton2;

        public Home2Controller(ITransitent transitent1, IScoped scoped1, ISingleton singleton1, ITransitent transitent2, IScoped scoped2, ISingleton singleton2)
        {
            this.transitent1 = transitent1;
            this.scoped1 = scoped1;
            this.singleton1 = singleton1;

            this.transitent2 = transitent2;
            this.scoped2 = scoped2;
            this.singleton2 = singleton2;
        }

        public IActionResult Index()
        {
            ViewBag.transient1 = transitent1;
            ViewBag.scoped1 = scoped1;
            ViewBag.singleton1 = singleton1;

            ViewBag.transient2 = transitent2;
            ViewBag.scoped2 = scoped2;
            ViewBag.singleton2 = singleton2;

            return View();
        }
    }
}
