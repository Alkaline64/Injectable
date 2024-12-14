using Microsoft.Extensions.DependencyInjection;
using Alkaline64.Injectable.Tests.Lifetime.Services;
using Alkaline64.Injectable.Tests.Lifetime;
using Alkaline64.Injectable.Extensions;

namespace Alkaline64.Injectable.Tests;

public class LifetimeTests
{
    [Fact]
    public void SingletonInjectable_RetainsInstanceWithinScope()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<LifetimeTestsAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var baselineService = serviceProvider.GetRequiredService<SingletonService>();
        var additionalService = serviceProvider.GetRequiredService<SingletonService>();

        // Assert
        Assert.Equal(baselineService, additionalService);
        Assert.Equal(baselineService.Guid, additionalService.Guid);
    }

    [Fact]
    public void SingletonInjectable_RetainsInstanceBetweenScopes()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<LifetimeTestsAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var baselineScope = serviceProvider.CreateScope();
        var baselineService = baselineScope.ServiceProvider.GetRequiredService<SingletonService>();

        var additionalScope = serviceProvider.CreateScope();
        var additionalService = additionalScope.ServiceProvider.GetRequiredService<SingletonService>();

        // Assert
        Assert.Equal(baselineService, additionalService);
        Assert.Equal(baselineService.Guid, additionalService.Guid);
    }

    [Fact]
    public void ScopedInjectable_RetainsInstanceWithinScope()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<LifetimeTestsAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var baselineService = serviceProvider.GetRequiredService<ScopedService>();
        var additionalService = serviceProvider.GetRequiredService<ScopedService>();

        // Assert
        Assert.Equal(baselineService, additionalService);
        Assert.Equal(baselineService.Guid, additionalService.Guid);
    }

    [Fact]
    public void ScopedInjectable_ChangesInstanceBetweenScopes()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<LifetimeTestsAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var baselineScope = serviceProvider.CreateScope();
        var baselineService = baselineScope.ServiceProvider.GetRequiredService<ScopedService>();

        var additionalScope = serviceProvider.CreateScope();
        var additionalService = additionalScope.ServiceProvider.GetRequiredService<ScopedService>();

        // Assert
        Assert.NotEqual(baselineService, additionalService);
        Assert.NotEqual(baselineService.Guid, additionalService.Guid);
    }

    [Fact]
    public void TransientInjectable_ChangesInstanceWithinScope()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<LifetimeTestsAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var baselineService = serviceProvider.GetRequiredService<TransientService>();
        var additionalService = serviceProvider.GetRequiredService<TransientService>();

        // Assert
        Assert.NotEqual(baselineService, additionalService);
        Assert.NotEqual(baselineService.Guid, additionalService.Guid);
    }

    [Fact]
    public void Transientjectable_ChangesInstanceBetweenScopes()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<LifetimeTestsAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var baselineScope = serviceProvider.CreateScope();
        var baselineService = baselineScope.ServiceProvider.GetRequiredService<TransientService>();

        var additionalScope = serviceProvider.CreateScope();
        var additionalService = additionalScope.ServiceProvider.GetRequiredService<TransientService>();

        // Assert
        Assert.NotEqual(baselineService, additionalService);
        Assert.NotEqual(baselineService.Guid, additionalService.Guid);
    }
}
