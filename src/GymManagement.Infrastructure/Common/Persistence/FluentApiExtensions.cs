﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Common.Persistence
{
    public static class FluentApiExtensions
    {
        public static PropertyBuilder<T> HasListOfIdsCoverter<T>(this PropertyBuilder<T> propertyBuilder)
        {
            return propertyBuilder.HasConversion(
                new ListOfIdsConverter(),
                new ListOfIdsComparer());
        }
    }
}
