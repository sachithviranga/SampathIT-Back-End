using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using user.contracts.Common;
using user.entities.DTO;
using user.entities.Entities;

namespace user.data.Mappers
{
    public class EntityMapper : IEntityMapper
    {
        private MapperConfiguration _config;

        private IMapper _mapper;

        public EntityMapper()
        {
            Configure();
            Create();
        }

        private void Configure()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, ApplicationUser>().ReverseMap();
            });
        }
        private void Create()
        {
            _mapper = _config.CreateMapper();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

    }
}
