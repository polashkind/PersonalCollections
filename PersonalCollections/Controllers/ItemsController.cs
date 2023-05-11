using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalCollections.Data.Interfaces;
using PersonalCollections.Data.ViewModels;
using PersonalCollections.Models;

namespace PersonalCollections.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemsService _service;

        public ItemsController(IItemsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var allItems = await _service.GetAll(cancellationToken);
            return View(allItems);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Item item, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            await _service.Create(item, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var itemDetails = await _service.GetById(id, cancellationToken);

            if (itemDetails == null) return View("Not Found");
            return View(itemDetails);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var itemDetails = await _service.GetById(id, cancellationToken);
            if (itemDetails == null) return View("NotFound");

            var response = new NewItemVM()
            {
                Id = itemDetails.Id,
                Title = itemDetails.Title,
                Description = itemDetails.Description,
                CollectionId = itemDetails.CollectionId,
            };

            var itemDropdownsData = await _service.GetNewItemDropdownsValues();
            ViewBag.Collections = new SelectList(itemDropdownsData.Collections, "Id", "Title");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewItemVM item, CancellationToken cancellationToken)
        {
            if (id != item.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var itemDropdownsData = await _service.GetNewItemDropdownsValues();
                ViewBag.Collections = new SelectList(itemDropdownsData.Collections, "Id", "Title");
                return View(item);
            }

            await _service.Update(item);
            return RedirectToAction(nameof(Index));
        }
    }
}