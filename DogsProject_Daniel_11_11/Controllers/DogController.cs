using DogsProject_Daniel_11_11.Data;
using DogsProject_Daniel_11_11.Data.Domain;
using DogsProject_Daniel_11_11.Models.Dog;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DogsProject_Daniel_11_11.Controllers
{
    public class DogController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public DogController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: DogController
        public ActionResult Index()
        {
            var dogs = _context.Dogs.Select(d => new DogAllViewModel
            {
             Id   =  d.Id,
             Name = d.Name,
             Age = d.Age,
             Breed = d.Breed,
             DogPicture = d.Picture,
            }).ToList();
            
            return View(dogs);
        }

        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DogCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                Dog dogFromDb = new Dog
                {
                    Name = bindingModel.Name,
                    Age = bindingModel.Age,
                    Breed = bindingModel.Breed,
                    Picture = bindingModel.Picture,
                };
                
                _context.Dogs.Add(dogFromDb);
                _context.SaveChanges();
                
                return this.RedirectToAction("Success");
            }
            
            return this.View();
        }

        // GET: DogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
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
