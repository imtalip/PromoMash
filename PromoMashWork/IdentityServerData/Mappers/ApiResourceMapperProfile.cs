// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;

namespace IdentityServerData.Mappers
{
    /// <summary>
    /// Defines entity/model mapping for API resources.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class OurApiResourceMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="ApiResourceMapperProfile"/>
        /// </summary>
        public OurApiResourceMapperProfile()
        {
            CreateMap<ApiResourceProperty, KeyValuePair<string, string>>()
                .ReverseMap();

            CreateMap<ApiResource, IdentityServer4.Models.ApiResource>(MemberList.Destination)
                .ConstructUsing(src => new IdentityServer4.Models.ApiResource())
                .ForMember(x => x.ApiSecrets, opts => opts.MapFrom(x => x.Secrets))
                .ForMember(x=>x.AllowedAccessTokenSigningAlgorithms, opts => opts.ConvertUsing(AllowedSigningAlgorithmsConverter.Converter, x=>x.AllowedAccessTokenSigningAlgorithms))
                .ReverseMap()
                .ForMember(x => x.AllowedAccessTokenSigningAlgorithms, opts => opts.ConvertUsing(AllowedSigningAlgorithmsConverter.Converter, x => x.AllowedAccessTokenSigningAlgorithms));

            CreateMap<ApiResourceClaim, string>()
                .ConstructUsing(x => x.Type)
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));

            CreateMap<ApiResourceSecret, IdentityServer4.Models.Secret>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null))
                .ReverseMap();

            CreateMap<ApiResourceScope, string>()
                .ConstructUsing(x => x.Scope)
                .ReverseMap()
                .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));
        }
    }
}
