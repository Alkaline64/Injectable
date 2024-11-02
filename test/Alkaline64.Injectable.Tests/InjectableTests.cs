using Alkaline64.Injectable.Tests.Accessibility;
using Alkaline64.Injectable.Tests.Accessibility.Accessibility;
using Microsoft.Extensions.DependencyInjection;

namespace Alkaline64.Injectable.Tests;

public class InjectableTests
{
    [Fact]
    public void InternalImplementation_IsInjectable()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.RegisterInjectables<AccessabilityTestsMarker>();

        // Assert
        var service = services.FirstOrDefault(s => s.ServiceType == typeof(IPublicInterface));

        Assert.NotNull(service);
        Assert.Equal("InternalImplementation", service.ImplementationType!.Name);
    }
}
