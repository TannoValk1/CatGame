using CatGame.Core.Dto;
using CatGame.Core.ServiceInterface;
using CatGame.Data;
using CatGame.Models.Cats;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatGame.Controllers
{
    public class CatsController : Controller
    {
     
        private readonly CatGameContext _context;
        private readonly ICatsServices _catsServices;
        private readonly IFileServices _fileServices;

        public CatsController(CatGameContext context, ICatsServices catsServices, IFileServices fileServices)
        {
            _context = context;
            _catsServices = catsServices;
            _fileServices = fileServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var resultingInventory = _context.Cats
                .OrderByDescending(y => y.CatLevel)
                .Select(x => new CatIndexViewModel
                {
                    ID = x.ID,
                    CatName = x.CatName,
                    CatType = (Models.Cats.CatType)(Core.Dto.CatType)x.CatType,
                    CatLevel = x.CatLevel,                    
                });
            return View(resultingInventory);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            CatCreateViewModel vm = new();
            return View("Create",vm);
        }
        [HttpPost , ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatCreateViewModel vm)
        {
            var dto = new CatDto()
            {
                CatName = vm.CatName,
                CatHealth = 100,
                CatXP = 0,
                CatXPNextLevel = 100,
                CatLevel = 0,
                CatType = (Core.Dto.CatType)vm.CatType,
                CatStatus = (Core.Dto.CatStatus)vm.CatStatus,
                PrimaryAttackName = vm.PrimaryAttackName,
                PrimaryAttackPower = vm.PrimaryAttackPower,
                SecondaryAttackName = vm.SecondaryAttackName,
                SecondaryAttackPower = vm.SecondaryAttackPower,
                SpecialAttackName = vm.SpecialAttackName,
                SpecialAttackPower = vm.SpecialAttackPower,
                CatWasBorn = vm.CatWasBorn,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Files = vm.Files,
                Image = vm.Image
                .Select(x => new FileToDatabaseDto
                {
                    ID = x.ImageID,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    CatID = x.CatID,
                }).ToArray()
            };
            var result = await _catsServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id )
        {
            var cat = await _catsServices.DetailsAsync(id);

            if (cat == null) 
            {
                return NotFound(); 
            }

            var images = await _context.FilesToDatabase
                .Where(t => t.CatID == id)
                .Select(y => new CatImageViewModel
                {
                    CatID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new CatDetailsViewModel();
            vm.ID = cat.ID;
            vm.CatName = cat.CatName;
            vm.CatHealth = cat.CatHealth;
            vm.CatXP = cat.CatXP;
            vm.CatLevel = cat.CatLevel;
            vm.CatType = (Models.Cats.CatType)cat.CatType;
            vm.CatStatus = (Models.Cats.CatStatus)cat.CatStatus;
            vm.PrimaryAttackName = cat.PrimaryAttackName;
            vm.PrimaryAttackPower = cat.PrimaryAttackPower;
            vm.SecondaryAttackName = cat.SecondaryAttackName;
            vm.SecondaryAttackPower = cat.SecondaryAttackPower;
            vm.SpecialAttackName = cat.SpecialAttackName;
            vm.SpecialAttackPower = cat.SpecialAttackPower;
            vm.Image.AddRange(images);
            
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            if (id == null) { return NotFound(); }

            var cat = await _catsServices.DetailsAsync(id);

            if (cat == null) { return NotFound(); }

            var images = await _context.FilesToDatabase
                .Where(x => x.CatID == id)
                .Select(y => new CatImageViewModel
                {
                    CatID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageCat = y.ImageCat,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new CatCreateViewModel();
            vm.ID = cat.ID;
            vm.CatName = cat.CatName;
            vm.CatHealth = cat.CatHealth;
            vm.CatXP = cat.CatXP;
            vm.CatXPNextLevel = cat.CatXPNextLevel;
            vm.CatLevel = cat.CatLevel;
            vm.CatType = (Models.Cats.CatType)cat.CatType;
            vm.CatStatus = (Models.Cats.CatStatus)cat.CatStatus;
            vm.PrimaryAttackName = cat.PrimaryAttackName;
            vm.PrimaryAttackPower = cat.PrimaryAttackPower;
            vm.SecondaryAttackName = cat.SecondaryAttackName;
            vm.SecondaryAttackPower = cat.SecondaryAttackPower;
            vm.SpecialAttackName = cat.SpecialAttackName;
            vm.SpecialAttackPower = cat.SpecialAttackPower;
            vm.CatDied = cat.CatDied;
            vm.CatWasBorn = cat.CatWasBorn;
            vm.CreatedAt = cat.CreatedAt;
            vm.UpdatedAt = DateTime.Now;
            vm.Image.AddRange(images);

            return View("Update", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CatCreateViewModel vm)
        {
            var dto = new CatDto()
            {
                ID = (Guid)vm.ID,
                CatName = vm.CatnName,
                CatHealth = 100,
                CatXP = 0,
                CatXPNextLevel = 100,
                CatLevel = 0,
                CatType = (Core.Dto.CatType)vm.CatType,
                CatStatus = (Core.Dto.CatStatus)vm.CatStatus,
                PrimaryAttackName = vm.PrimaryAttackName,
                PrimaryAttackPower = vm.PrimaryAttackPower,
                SecondaryAttackName = vm.SecondaryAttackName,
                SecondaryAttackPower = vm.SecondaryAttackPower,
                SpecialAttackName = vm.SpecialAttackName,
                SpecialAttackPower = vm.SpecialAttackPower,
                CatWasBorn = vm.CatWasBorn,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = DateTime.Now,
                Files = vm.Files,
                Image = vm.Image
                .Select(x => new FileToDatabaseDto
                {
                    ID = x.ImageID,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    CatID = x.CatID,
                }).ToArray()
            };
            var result = await _catsServices.Update(dto);

            if (result == null) { return RedirectToAction("Index"); }
            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null) { return NotFound(); }

            var cat = await _catsServices.DetailsAsync(id);

            if (cat == null) { return NotFound(); };

            var images = await _context.FilesToDatabase
                .Where(x => x.CatID == id)
                .Select( y => new CatImageViewModel
                {
                    CatID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();
            var vm = new CatDeleteViewModel();

            vm.ID = cat.ID;
            vm.CatName = cat.CatName;
            vm.CatHealth = cat.CatHealth;
            vm.CatXP = cat.CatXP;
            vm.CatXPNextLevel = cat.CatXPNextLevel;
            vm.CatLevel = cat.CatLevel;
            vm.CatType = (Models.Cat.CatType)cat.CatType;
            vm.CatStatus = (Models.Cat.CatStatus)cat.CatStatus;
            vm.PrimaryAttackName = cat.PrimaryAttackName;
            vm.PrimaryAttackPower = cat.PrimaryAttackPower;
            vm.SecondaryAttackName = cat.SecondaryAttackName;
            vm.SecondaryAttackPower = cat.SecondaryAttackPower;
            vm.SpecialAttackName = cat.SpecialAttackName;
            vm.SpecialAttackPower = cat.SpecialAttackPower;
            vm.CreatedAt = cat.CreatedAt;
            vm.UpdatedAt = DateTime.Now;
            vm.Image.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var catToDelete = await _catsServices.Delete(id);

            if (catToDelete == null) { return RedirectToAction("Index"); }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(CatImageViewModel vm)
        {
            var dto = new FileToDatabaseDto()
            {
                ID = vm.ImageID
            };
            var image = await _fileServices.RemoveImageFromDatabase(dto);
            if (image == null) { return RedirectToAction("Index"); }
            return RedirectToAction("Index");
        }
    }
}
