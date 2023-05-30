using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalCollections.Data.Enums;
using PersonalCollections.Data.Interfaces;
using PersonalCollections.Data.Services;
using PersonalCollections.Data.ViewModels;
using PersonalCollections.Models;

namespace PersonalCollections.Controllers
{
    public class ItemsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IItemsService _service;
        private readonly ICommentsService _commentService;

        public ItemsController(UserManager<ApplicationUser> userManager, IItemsService service, ICommentsService commentService)
        {
            _userManager = userManager;
            _service = service;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            ViewBag.CurrentUser = currentUser;
            var allItems = await _service.GetAll(cancellationToken);
            return View(allItems);
        }

        public IActionResult Create()
        {
            var model = new Item();
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
        public async Task<IActionResult> Create(Item item, CancellationToken cancellationToken)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            item.CreatedByUserId = currentUser.Id;
            item.UpdatedByUserId = currentUser.Id;
            item.CreatedAt = DateTime.UtcNow;
            item.UpdatedAt = DateTime.UtcNow;

            ModelState.Clear();
            TryValidateModel(item);

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(item);
            }

            await _service.Create(item, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var itemDetails = await _service.GetById(id, cancellationToken);
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            ViewBag.CurrentUser = currentUser;
            if (itemDetails == null) return View("Not Found");

            return View(itemDetails);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var item = await _service.GetById(id, cancellationToken);

            if (item == null) return View("Not Found");

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Item item, CancellationToken cancellationToken)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            item.CreatedByUserId = currentUser.Id;
            item.UpdatedByUserId = currentUser.Id;
            item.CreatedAt = DateTime.UtcNow;
            item.UpdatedAt = DateTime.UtcNow;

            ModelState.Clear();
            TryValidateModel(item);

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(item);
            }

            await _service.Update(item, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var item = await _service.GetById(id, cancellationToken);
            await _service.Delete(item, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string query, CancellationToken cancellationToken)
        {
            var allItems = await _service.GetAll(cancellationToken);
            var result = allItems.Where(c => query == null || c.Title.IndexOf(query, 0, StringComparison.OrdinalIgnoreCase) != -1).ToList();
            return View(nameof(Index), result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCommentToItem(NewCommentVM newComment, CancellationToken cancellationToken)
        {
            var item = await _service.GetById(newComment.ItemId, cancellationToken);
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            item.Comments?.Add(new Comment() {
                Content = newComment.Comment,
                ItemId = newComment.ItemId,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = currentUser.Id
            });

            await _service.Update(item, cancellationToken);

            return RedirectToAction(nameof(Details), new { id = newComment.ItemId });
        }

        public async Task<IActionResult> DeleteComment(int itemId, int commentId, CancellationToken cancellationToken)
        {
            var item = await _service.GetById(itemId, cancellationToken);
            var comment = await _commentService.GetById(commentId, cancellationToken);

            item.Comments.Remove(comment);

            await _service.Update(item, cancellationToken);

            return RedirectToAction(nameof(Details), new { id = item.Id });
        }

    }
}