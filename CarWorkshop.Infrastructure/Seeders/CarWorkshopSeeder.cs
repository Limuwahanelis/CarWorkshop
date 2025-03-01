using CarWorkshop.Domain.Entities;
using CarWorkshop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Seeders
{
    public class CarWorkshopSeeder
    {
        private readonly CarWorkshopDbContext _context;
        public CarWorkshopSeeder(CarWorkshopDbContext context)
        {
            _context = context;

        }
        public async Task Seed()
        {
            if (await _context.Database.CanConnectAsync())
            {
                if (!_context.CarWorkshops.Any())
                {
                    Domain.Entities.CarWorkshop mazdaAso = new Domain.Entities.CarWorkshop()
                    {
                        Name = "Mazda ASO",
                        Description = "Autoryzowany serwis Mazda",
                        ContactDetails = new CarWorkshopContactDetails()
                        {
                            City = "Katowice",
                            Street = "Piastowska 3851",
                            PostalCode = "1896-4576",
                            PhoneNumber = "+4812345678999",
                        }
                    };
                    mazdaAso.EncodeName();
                    _context.CarWorkshops.Add(mazdaAso);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
