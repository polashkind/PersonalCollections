using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalCollections.Data;
using PersonalCollections.Data.Interfaces;
using PersonalCollections.Models;

namespace PersonalCollections.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly ICollectionsService _service;

        public CollectionsController(ICollectionsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await _service.GetAll(cancellationToken);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Collection collection, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(collection);
            }
            await _service.Create(collection, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var collectionDetails = await _service.GetById(id, cancellationToken);

            if (collectionDetails == null) return View("Empty");
            return View(collectionDetails);
        }
    }
}