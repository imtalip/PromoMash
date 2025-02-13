// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;

namespace IdentityServerData.Mappers
{
    /// <summary>
    /// Defines entity/model mapping for identity resources.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class OurIdentityResourceMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="IdentityResourceMapperProfile"/>
        /// </summary>
        public OurIdentityResourceMapperProfile()
        {
            CreateMap<IdentityResourceProperty, KeyValuePair<string, string>>()
                .ReverseMap();

            CreateMap<IdentityResource, IdentityServer4.Models.IdentityResource>(MemberList.Destination)
                .ConstructUsing(src => new IdentityServer4.Models.IdentityResource())
                .ReverseMap();

            CreateMap<IdentityResourceClaim, string>()
               .ConstructUsing(x => x.Type)
               .ReverseMap()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));
        }
    }
}
