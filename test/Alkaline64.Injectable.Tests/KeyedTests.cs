using Microsoft.Extensions.DependencyInjection;
using Alkaline64.Injectable.Extensions;
using Alkaline64.Injectable.Tests.Keyed;
using Alkaline64.Injectable.Tests.Keyed.Services;

namespace Alkaline64.Injectable.Tests;

public class KeyedTests
{
    [Theory]
    [InlineData(typeof(KeyedService1), nameof(KeyedService1))]
    [InlineData(typeof(KeyedService2), nameof(KeyedService2))]
    [InlineData(typeof(KeyedService3), nameof(KeyedService3))]
    public void KeyedInjectable_ReturnsCorrectService(Type expectedType, string key)
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<KeyedAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredKeyedService<IKeyedProvider>(key);

        // Assert
        Assert.IsType(expectedType, service);
    }
}
