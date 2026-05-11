using DogsApp.Core.Contracts;
using DogsProject_Daniel_11_11.Data;
using DogsProject_Daniel_11_11.Data.Domain;
using DogsProject_Daniel_11_11.Models.Breed;
using DogsProject_Daniel_11_11.Models.Dog;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Security.Claims;

namespace DogsProject_Daniel_11_11.Controllers
{
    [Authorize]
    public class DogController : Controller
    {
        private readonly IBreedService _breedService;
        private readonly IDogService _dogService;
        
        public DogController(IDogService dogService, IBreedService breedService)
        {
            this._dogService = dogService;
            this._breedService = breedService;
        }
        
        // GET: DogController
        [AllowAnonymous]
        public IActionResult Index(string searchStringBreed, string searchStringName)
        {
            List<DogAllViewModel> dogs = _dogService.GetDogs(searchStringBreed, searchStringName)
                .Select(dogFromDb => new DogAllViewModel
                {
                    Id = dogFromDb.Id,
                    Name = dogFromDb.Name,
                    Age = dogFromDb.Age,
                    BreedName = dogFromDb.Breed.Name,
                    DogPicture = dogFromDb.Picture,
                    FullName = dogFromDb.Owner.FirstName + " " + dogFromDb.Owner.LastName
                }).ToList();

            return this.View(dogs);
        }

        // GET: DogController/Details/5
        public IActionResult Details(int id)
        {
            Dog item = _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }

            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedName = item.Breed.Name,
                Picture = item.Picture,
                FullName = item.Owner.FirstName + " " + item.Owner.LastName
            };

            return View(dog);
        }

        // GET: DogController/Create
        public ActionResult Create()
        {
            var dog = new DogCreateViewModel();
            dog.Breeds = _breedService.GetBreeds().Select(c => new BreedPairViewModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            
            return View(dog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DogCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var created = _dogService.Create(bindingModel.Name, bindingModel.Age, bindingModel.BreedId, bindingModel.Picture, currentUserId);
                if (created)
                {
                    return this.RedirectToAction("Success");
                }
            }

            return this.View();
        }

        // GET: DogController/Edit/5
        public IActionResult Edit(int id)
        {
            Dog item = _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }

            DogEditViewModel dog = new DogEditViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedId = item.BreedId,
                Picture = item.Picture
            };

            dog.Breeds = _breedService.GetBreeds().Select(c => new BreedPairViewModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return View(dog);
        }

        [HttpPost]
        public IActionResult Edit(int id, DogEditViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var updated = _dogService.UpdateDog(id, bindingModel.Name, bindingModel.Age, bindingModel.BreedId, bindingModel.Picture);
                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
            }

            return View(bindingModel);
        }

        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {
            
            Dog item =  _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }

            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedName = item.Breed.Name,
                Picture = item.Picture,
                FullName = item.Owner.FirstName + " " + item.Owner.LastName
            };
            return View(dog);
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _dogService.RemoveById(id);

            if (deleted)
            {
                return this.RedirectToAction("Index", "Dog");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Success()
        {
            return this.View();
        }
    }
}
