/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license. See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Piranha;
using Piranha.Cache;

/// <summary>
/// Extension methods for adding DistributedCache to the application.
/// </summary>
public static class PiranhaDistributedCacheExtensions
{
    /// <summary>
    /// Adds the distributed cache service to the service collection.
    /// </summary>
    /// <param name="services">The current service collection</param>
    /// <returns>The updated service collection</returns>
    public static IServiceCollection AddPiranhaDistributedCache(this IServiceCollection services)
    {
        // Check dependent services
        if (!services.Any(s => s.ServiceType == typeof(IDistributedCache)))
        {
            throw new NotSupportedException("You need to register a IDistributedCache service in order to use Memory Cache in Piranha");
        }
        return services.AddSingleton<ICache, DistributedCache>();
    }

    /// <summary>
    /// Adds the distributed two level cache service for repository caching.
    /// </summary>
    /// <param name="services">The current service collection</param>
    /// <param name="clone"></param>
    /// <returns>The updated service collection</returns>
    public static IServiceCollection AddPiranhaDistributedTwoLevelCache(this IServiceCollection services, bool clone = false)
    {
        // Check dependent services
        if (!services.Any(s => s.ServiceType == typeof(IDistributedCache)))
        {
            throw new NotSupportedException("You need to register a IDistributedCache service in order to use distributed two level cache in Piranha");
        }

        // Check dependent services
        if (!services.Any(s => s.ServiceType == typeof(IMemoryCache)))
        {
            throw new NotSupportedException("You need to register a IMemoryCache service in order to use distributed two level cache in Piranha");
        }

        if (clone)
        {
            return services.AddScoped<ICache, DistributedTwoLevelCacheWithClone>();
        }
        return services.AddScoped<ICache, DistributedTwoLevelCache>();
    }

    /// <summary>
    /// Uses the distributed cache service in the current application.
    /// </summary>
    /// <param name="serviceBuilder">The current service builder</param>
    /// <param name="clone">If returned objects should be cloned</param>
    /// <returns>The updated service builder</returns>
    public static PiranhaServiceBuilder UseDistributedCache(this PiranhaServiceBuilder serviceBuilder, bool clone = false)
    {
        serviceBuilder.Services.AddPiranhaDistributedCache();

        return serviceBuilder;
    }

    /// <summary>
    /// Uses the distributed two level cache service in the current application.
    /// </summary>
    /// <param name="serviceBuilder">The current service builder</param>
    /// <param name="clone">If returned objects should be cloned</param>
    /// <returns>The updated service builder</returns>
    public static PiranhaServiceBuilder UseDistributedTwoLevelCache(this PiranhaServiceBuilder serviceBuilder, bool clone = false)
    {
        serviceBuilder.Services.AddPiranhaDistributedTwoLevelCache(clone);

        return serviceBuilder;
    }

}