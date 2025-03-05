﻿using apiStock.DTO;
using apiStock.Models;
using AutoMapper;

namespace apiStock.Utility
{
    public class AutoMappeeProfile : Profile
    {
        public AutoMappeeProfile() {

            CreateMap<Role, roleDTO>().ReverseMap();
            CreateMap<User, userListDTO>().ReverseMap();
        }
    }
}
