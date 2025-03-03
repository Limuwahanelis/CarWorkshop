using AutoMapper;
using CarWorkshop.Application.Entities;
using CarWorkshop.Application.Interfaces;
using CarWorkshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Repositories
{
    public class CarWorkshopRepository : ICarWorkshopRepository
    {
        CarWorkshopDbContext _context;
        IMapper _mapper;
        public CarWorkshopRepository(CarWorkshopDbContext context,IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Create(CarWorkshopForm carWorkshopForm)
        {
            Domain.Entities.CarWorkshop carWorkshop = _mapper.Map<Domain.Entities.CarWorkshop>(carWorkshopForm);
            carWorkshop.EncodeName();
            _context.Add(carWorkshop);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Entities.CarWorkshop?> GetByName(string name)
        {
            return await _context.CarWorkshops.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }
}
