using Alkaline64.Injectable.Testables.Accessibility;
using Microsoft.Extensions.DependencyInjection;
using TestProject1;

namespace Alkaline64.Injectable.Tests
{
    public class InjectableTests
    {
        [Fact]
        public void InternalImplementation_IsInjectable()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.RegisterInjectables<TestablesAssemblyMarker>();

            // Assert
            var service = services.FirstOrDefault(s => s.ServiceType == typeof(IPublicInterface));

            Assert.NotNull(service);
            Assert.Equal("InternalImplementation", service.ImplementationType!.Name);
        }
    }
}
