﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Domain.Entities
{
    public class CarWorkshop
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? About { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public CarWorkshopContactDetails ContactDetails { get; set; } = default!;

        public string EncodedName { get; private set; } = default!;

        public List<CarWorkshopService> Services { get; set; } = new();

        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }
        public void EncodeName()=> EncodedName = Name.ToLower().Replace(" ","-");

    }
}
