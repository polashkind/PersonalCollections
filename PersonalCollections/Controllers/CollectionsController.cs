using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalCollections.Data;
using PersonalCollections.Data.Enums;
using PersonalCollections.Data.Interfaces;
using PersonalCollections.Models;

namespace PersonalCollections.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICollectionsService _service;
        private readonly IItemsService _itemsService;

        public CollectionsController(UserManager<ApplicationUser> userManager, ICollectionsService service, IItemsService itemsService)
        {
            _userManager = userManager;
            _service = service;
            _itemsService = itemsService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            ViewBag.CurrentUser = currentUser;
            var result = await _service.GetAll(cancellationToken);
            return View(result);
        }

        public IActionResult Create()
        {
            var model = new Collection();
            ViewBag.Subjects = Enum.GetValues(typeof(ItemType))
                .Cast<ItemType>()
                .Select(type => new SelectListItem
                {
                    Value = type.ToString(),
                    Text = type.ToString()
                });

            return View(model);
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

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var collection = await _service.GetById(id, cancellationToken);

            if (collection == null) return View("Not Found");

            var items = await _itemsService.GetNonCollectionByType(collection.Subject, collection.Items, cancellationToken);
            ViewBag.Items = items;
            
            return View(collection);
        }

        public async Task<IActionResult> AddItemToCollection(int itemId, int collectionId, CancellationToken cancellationToken)
        {
            var collection = await _service.GetById(collectionId, cancellationToken);
            var item = await _itemsService.GetById(itemId, cancellationToken);

            // Add the item to the collection's Items list
            collection.Items ??= new List<Item>();
            collection.Items.Add(item);

            await _service.Update(collection, cancellationToken);
            await _itemsService.Update(item, cancellationToken);

            return RedirectToAction(nameof(Edit), new { id = collection.Id });
        }

        public async Task<IActionResult> RemoveItemFromCollection(int itemId, int collectionId, CancellationToken cancellationToken)
        {
            var collection = await _service.GetById(collectionId, cancellationToken);
            var item = await _itemsService.GetById(itemId, cancellationToken);

            collection.Items.Remove(item);

            await _service.Update(collection, cancellationToken);
            await _itemsService.Update(item, cancellationToken);

            return RedirectToAction(nameof(Edit), new { id = collection.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Collection collection, CancellationToken cancellationToken)
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

            await _service.Update(collection, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var collection = await _service.GetById(id, cancellationToken);
            await _service.Delete(collection, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string query, CancellationToken cancellationToken)
        {
            var allCollections = await _service.GetAll(cancellationToken);
            var result = allCollections.Where(c => query == null || c.Title.IndexOf(query, 0, StringComparison.OrdinalIgnoreCase) != -1).ToList();
            return View(nameof(Index), result);
        }
    }
}