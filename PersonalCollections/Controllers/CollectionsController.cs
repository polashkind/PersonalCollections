using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalCollections.Data;
using PersonalCollections.Data.Interfaces;
using PersonalCollections.Models;

namespace PersonalCollections.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICollectionsService _service;

        public CollectionsController(UserManager<ApplicationUser> userManager, ICollectionsService service)
        {
            _userManager = userManager;
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
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            collection.CreatedByUserId = currentUser.Id;
            collection.UpdatedByUserId = currentUser.Id;
            collection.CreatedAt = DateTime.UtcNow;
            collection.UpdatedAt = DateTime.UtcNow;

            ModelState.Clear();
            TryValidateModel(collection);

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