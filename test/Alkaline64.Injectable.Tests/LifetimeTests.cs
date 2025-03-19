using Microsoft.Extensions.DependencyInjection;
using Alkaline64.Injectable.Tests.Lifetime.Services;
using Alkaline64.Injectable.Tests.Lifetime;
using Alkaline64.Injectable.Extensions;

namespace Alkaline64.Injectable.Tests;

public class LifetimeTests
{
    private const string Category = "Lifetime";

    [Category(Category)]
    [Test]
    public async Task SingletonInjectable_RetainsInstanceWithinScope()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<LifetimeTestsAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var baselineService = serviceProvider.GetRequiredService<SingletonService>();
        var additionalService = serviceProvider.GetRequiredService<SingletonService>();

        // Assert
        await Assert.That(additionalService).IsEqualTo(baselineService);
        await Assert.That(additionalService.Guid).IsEqualTo(baselineService.Guid);
    }

    [Category(Category)]
    [Test]
    public async Task SingletonInjectable_RetainsInstanceBetweenScopes()
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
        await Assert.That(additionalService).IsEqualTo(baselineService);
        await Assert.That(additionalService.Guid).IsEqualTo(baselineService.Guid);
    }

    [Category(Category)]
    [Test]
    public async Task ScopedInjectable_RetainsInstanceWithinScope()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<LifetimeTestsAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var baselineService = serviceProvider.GetRequiredService<ScopedService>();
        var additionalService = serviceProvider.GetRequiredService<ScopedService>();

        // Assert
        await Assert.That(additionalService).IsEqualTo(baselineService);
        await Assert.That(additionalService.Guid).IsEqualTo(baselineService.Guid);
    }

    [Category(Category)]
    [Test]
    public async Task ScopedInjectable_ChangesInstanceBetweenScopes()
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
        await Assert.That(additionalService).IsNotEqualTo(baselineService);
        await Assert.That(additionalService.Guid).IsNotEqualTo(baselineService.Guid);
    }

    [Category(Category)]
    [Test]
    public async Task TransientInjectable_ChangesInstanceWithinScope()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<LifetimeTestsAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var baselineService = serviceProvider.GetRequiredService<TransientService>();
        var additionalService = serviceProvider.GetRequiredService<TransientService>();

        // Assert
        await Assert.That(additionalService).IsNotEqualTo(baselineService);
        await Assert.That(additionalService.Guid).IsNotEqualTo(baselineService.Guid);
    }

    [Category(Category)]
    [Test]
    public async Task Transientjectable_ChangesInstanceBetweenScopes()
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
        await Assert.That(additionalService).IsNotEqualTo(baselineService);
        await Assert.That(additionalService.Guid).IsNotEqualTo(baselineService.Guid);
    }
}
