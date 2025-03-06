using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.Commands.CreateCarWorkshop;
using CarWorkshop.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //services.AddAutoMapper(typeof(CarWorkshopMappingProfile));
            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                IUserContext userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new CarWorkshopMappingProfile(userContext));
            }).CreateMapper()
            );
            services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
                
        }
    }
}
