using Alkaline64.Injectable.Extensions;
using Alkaline64.Injectable.Tests.TryAdd;
using Alkaline64.Injectable.Tests.TryAdd.Services;
using Microsoft.Extensions.DependencyInjection;
using TUnit.Assertions.Extensions;

namespace Alkaline64.Injectable.Tests;

public class TryAddTests
{
    private const string Category = "TryAdd";

    [Category(Category)]
    [Test]
    public async Task MultipleInjectables_ReturnsFirstRegisters()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<TryAddAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<ITryAddProvider>();

        // Assert
        await Assert.That(service).IsTypeOf<TryAddService1>();
    }

    [Category(Category)]
    [Test]
    public async Task MultipleInjectables_RegistersOnlyOne()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<TryAddAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var resolvedServices = serviceProvider.GetServices<ITryAddProvider>().ToList();

        // Assert
        await Assert.That(resolvedServices.Count).IsEqualTo(1);
        await Assert.That(resolvedServices[0]).IsTypeOf<TryAddService1>();
    }
}
