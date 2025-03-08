using AutoMapper;
using CarWorkshop.Application.Commands.CreateCarWorkshop;
using CarWorkshop.Application.Commands.CreateCarWorkshopService;
using CarWorkshop.Application.Entities;
using CarWorkshop.Application.Interfaces;
using CarWorkshop.Application.Querries;
using CarWorkshop.Application.Querries.GetCarWorkshopServices;
using CarWorkshop.Extensions;
using CarWorkshop.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

            //await _mediator.Send(command);// _carWorkshopRepository.Create(command);

            this.SetNotification("success", $"Created carworkshop {command.Name}");

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
        //////////// CarWorkshopService

        [Authorize(Roles = "Owner")]
        public IActionResult CreateCarWorkshopService()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Owner")]
        [Route("CarWorkshop/CarWorkshopService")]
        public async Task<IActionResult> CreateCarWorkshopService(CreateCarWorkshopServiceCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);// _carWorkshopRepository.Create(command);

            return Ok();
        }
        /////////////////////////////////
        [HttpGet]
        [Route("CarWorkshop/{encodedName}/CarWorkshopService")]
        public async Task<IActionResult> GetCarWorkshopServices(string encodedName)
        {
            var data = await _mediator.Send(new GetCarWorkshopServicesQuery() { EncodedName = encodedName });
            return Ok(data);
        }
    }
}
