using CarWorkshop.Application.Entities;
using CarWorkshop.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CarWorkshopMappingProfile));
            services.AddValidatorsFromAssemblyContaining<CarWorkshopFormValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
                
        }
    }
}
