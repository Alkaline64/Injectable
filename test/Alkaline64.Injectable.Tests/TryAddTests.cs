using Alkaline64.Injectable.Extensions;
using Alkaline64.Injectable.Tests.TryAdd;
using Alkaline64.Injectable.Tests.TryAdd.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Alkaline64.Injectable.Tests;

public class TryAddTests
{
    [Fact]
    public void MultipleInjectables_ReturnsFirstRegisters()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<TryAddAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<ITryAddProvider>();

        // Assert
        Assert.IsType<TryAddService1>(service);
    }

    [Fact]
    public void MultipleInjectables_RegistersOnlyOne()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<TryAddAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var resolvedServices = serviceProvider.GetServices<ITryAddProvider>().ToList();

        // Assert
        Assert.Single(resolvedServices);
        Assert.IsType<TryAddService1>(resolvedServices[0]);
    }
}
