using Alkaline64.Injectable.Extensions;
using Alkaline64.Injectable.Tests.Accessibility;
using Alkaline64.Injectable.Tests.Accessibility.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Alkaline64.Injectable.Tests;

public class AccessibilityTests
{
    [Fact]
    public void InternalImplementation_IsInjectable()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<AccessibilityTestsMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<IService>();

        // Assert
        Assert.NotNull(service);
        Assert.Equal("Alkaline64.Injectable.Tests.Accessibility.Services.InternalImplementation", service.GetType().FullName);
    }
}
