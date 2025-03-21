﻿using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.Commands.EditCarWorkshop;
using CarWorkshop.Application.Entities;
using CarWorkshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Mappings
{
    public class CarWorkshopMappingProfile : Profile
    {
        public CarWorkshopMappingProfile(IUserContext userContext)
        {
            CurrentUser? user = userContext.GetCurrentUser();
            // properties with the same name and type are mapped automatically
            CreateMap<CarWorkshopForm, Domain.Entities.CarWorkshop>()
                .ForMember(x => x.ContactDetails, opt => opt.MapFrom(src => new CarWorkshopContactDetails()
                {
                    City = src.City,
                    PhoneNumber = src.PhoneNumber,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                }));
            CreateMap<Domain.Entities.CarWorkshop, CarWorkshopForm>()
                .ForMember(form => form.Street, opt => opt.MapFrom(src => src.ContactDetails.Street))
                .ForMember(form => form.City, opt => opt.MapFrom(src => src.ContactDetails.City))
                .ForMember(form => form.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode))
                .ForMember(form => form.PhoneNumber, opt => opt.MapFrom(src => src.ContactDetails.PhoneNumber))
                .ForMember(form => form.IsEditable, opt => opt.MapFrom(src => user!=null && (src.CreatedById == user.Id|| user.Roles.Contains("Moderator"))));


            CreateMap<CarWorkshopForm, EditCarWorkshopCommand>();

            CreateMap<CarWorkshopServiceDto, CarWorkshopService>().ReverseMap();

        }
    }
}
