using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Models;

namespace TestAPI.Profiles
{
    public class CommandsProfile:Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, Dtos.CommandReadDto>();
            CreateMap<Dtos.CommandCreateDto, Command>();
            CreateMap<Dtos.CommandUpdateDto, Command>();
            CreateMap<Command, Dtos.CommandUpdateDto>();
       
        }
    }
}
