using Alkaline64.Injectable.Extensions;
using Alkaline64.Injectable.Tests.Accessibility;
using Alkaline64.Injectable.Tests.Accessibility.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Alkaline64.Injectable.Tests;

public class AccessibilityTests
{
    private const string Category = "Accessibility";

    [Category(Category)]
    [Test]
    public async Task InternalImplementation_IsInjectable()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<AccessibilityTestsMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<IService>();

        // Assert
        await Assert.That(service).IsNotNull();
        await Assert.That(service.GetType().FullName).IsEqualTo("Alkaline64.Injectable.Tests.Accessibility.Services.InternalImplementation");
    }
}
