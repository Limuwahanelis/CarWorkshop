using CarWorkshop.Application.Commands.CreateCarWorkshop;
using CarWorkshop.Application.Entities;
using CarWorkshop.Application.Interfaces;
using CarWorkshop.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace CarWorkshop.Controllers
{
    public class CarWorkshopController:Controller
    {
        private readonly IMediator _mediator;

        public CarWorkshopController(IMediator mediator) 
        {
            this._mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<CarWorkshopForm> workshops = await _mediator.Send(new GetAllCarWorkshopsQuerry());
            return View(workshops);
        }
        public IActionResult Create()
        {
            return View();
        }
        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            CarWorkshopForm workshop=await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            return View(workshop);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
        {
            if( !ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);// _carWorkshopRepository.Create(command);
            return RedirectToAction(nameof(Index));
        }
    }
}
