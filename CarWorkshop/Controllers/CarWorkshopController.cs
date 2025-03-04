using CarWorkshop.Application.Entities;
using CarWorkshop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace CarWorkshop.Controllers
{
    public class CarWorkshopController:Controller
    {
        private ICarWorkshopRepository _carWorkshopRepository;
        public CarWorkshopController(ICarWorkshopRepository carWorkshopRepository) 
        {
            _carWorkshopRepository = carWorkshopRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<CarWorkshopForm> workshops= await _carWorkshopRepository.GetAll();
            return View(workshops);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarWorkshopForm carWorkshop)
        {
            if( !ModelState.IsValid)
            {
                return View(carWorkshop);
            }
            await _carWorkshopRepository.Create(carWorkshop);
            return RedirectToAction(nameof(Create)); // TODO: refactor
        }
    }
}
