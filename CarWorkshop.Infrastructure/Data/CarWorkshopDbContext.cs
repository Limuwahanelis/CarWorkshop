﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Data
{
    public class CarWorkshopDbContext:IdentityDbContext
    {
        public DbSet<Domain.Entities.CarWorkshop> CarWorkshops { get; set; }
        public DbSet<Domain.Entities.CarWorkshopService> Services { get; set; }
        public CarWorkshopDbContext(DbContextOptions<CarWorkshopDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // for identitydbContext to configure its own tables

            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .OwnsOne(x => x.ContactDetails); // prevednts datbase from reading contact details as separate table, but makes it understand that contact details is just a separeate class to hold info

        }
    }
}
