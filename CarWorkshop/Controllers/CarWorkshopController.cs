using AutoMapper;
using CarWorkshop.Application.Commands.CreateCarWorkshop;
using CarWorkshop.Application.Entities;
using CarWorkshop.Application.Interfaces;
using CarWorkshop.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CarWorkshop.Controllers
{
    public class CarWorkshopController:Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CarWorkshopController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<CarWorkshopForm> workshops = await _mediator.Send(new GetAllCarWorkshopsQuerry());
            return View(workshops);
        }
        [Authorize(Roles ="Owner")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            
            await _mediator.Send(command);// _carWorkshopRepository.Create(command);
            return RedirectToAction(nameof(Index));
        }
        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            CarWorkshopForm workshop=await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            return View(workshop);
        }
        [Authorize(Roles ="Moderator,Owner")]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            CarWorkshopForm workshop = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));

            if(!workshop.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            EditCarWorkshopCommand model = _mapper.Map< EditCarWorkshopCommand >(workshop);
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Moderator,Owner")]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditCarWorkshopCommand carWorkshop)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit));
            }
            await _mediator.Send(carWorkshop);
            return RedirectToAction(nameof(Index));
        }
    }
}
