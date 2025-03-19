using Microsoft.Extensions.DependencyInjection;
using Alkaline64.Injectable.Extensions;
using Alkaline64.Injectable.Tests.Keyed;
using Alkaline64.Injectable.Tests.Keyed.Services;

namespace Alkaline64.Injectable.Tests;

public class KeyedTests
{
    private const string Category = "Keyed";

    [Category(Category)]
    [Test]
    [Arguments(typeof(KeyedService1), nameof(KeyedService1))]
    [Arguments(typeof(KeyedService2), nameof(KeyedService2))]
    [Arguments(typeof(KeyedService3), nameof(KeyedService3))]
    public async Task KeyedInjectable_ReturnsCorrectService(Type expectedType, string key)
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<KeyedAssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredKeyedService<IKeyedProvider>(key);

        // Assert
        await Assert.That(service).IsTypeOf(expectedType);
    }
}
