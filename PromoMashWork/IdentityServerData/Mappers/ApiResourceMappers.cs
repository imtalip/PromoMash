﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AutoMapper;
using IdentityServer4.EntityFramework.Entities;

namespace IdentityServerData.Mappers
{
    /// <summary>
    /// Extension methods to map to/from entity/model for API resources.
    /// </summary>
    public static class ApiResourceMappers
    {
        static ApiResourceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<OurApiResourceMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static IdentityServer4.Models.ApiResource OurToModel(this ApiResource entity)
        {
            return entity == null ? null : Mapper.Map<IdentityServer4.Models.ApiResource>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static ApiResource OurToEntity(this IdentityServer4.Models.ApiResource model)
        {
            return model == null ? null : Mapper.Map<ApiResource>(model);
        }
    }
}