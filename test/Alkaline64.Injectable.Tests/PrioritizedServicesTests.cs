using Microsoft.Extensions.DependencyInjection;
using Alkaline64.Injectable.Tests.Shared;
using Alkaline64.Injectable.Tests.Context1;
using Alkaline64.Injectable.Tests.Context2;
using Alkaline64.Injectable.Extensions;
using Alkaline64.Injectable.Tests.Context1.Services;
using Alkaline64.Injectable.Tests.Context2.Services;

namespace Alkaline64.Injectable.Tests;

public class PrioritizedServicesTests
{
    private const string Category = "Priority";

    [Category(Category)]
    [Test]
    public async Task MultipleInjectablesInSingleAssembly_ReturnsHighestPriority()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<Context1AssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<IPrioritizedProvider>();

        // Assert
        await Assert.That(service).IsTypeOf<PrioritizedService1>();
    }

    [Category(Category)]
    [Test]
    public async Task MultipleInjectablesInSingleAssembly_AreInOrder()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<Context1AssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var resolvedServices = serviceProvider.GetServices<IPrioritizedProvider>().ToList();

        // Assert
        await Assert.That(resolvedServices.Count).IsEqualTo(2);
        await Assert.That(resolvedServices[0]).IsTypeOf<PrioritizedService2>();
        await Assert.That(resolvedServices[1]).IsTypeOf<PrioritizedService1>();
    }

    [Category(Category)]
    [Test]
    public async Task MultipleInjectablesInMultipleAssemblies_ReturnsHighestPriority()
    {
        // Arrange
        var services = new ServiceCollection();
        var context = InjectionContext.NewContext();
        context.AddInjectables<Context1AssemblyMarker>();
        context.AddInjectables<Context2AssemblyMarker>();
        services.RegisterInjectables(context);
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<IPrioritizedProvider>();

        // Assert
        await Assert.That(service).IsTypeOf<PrioritizedService3>();
    }

    [Category(Category)]
    [Test]
    public async Task MultipleInjectablesInMultipleAssemblies_AreInOrder()
    {
        // Arrange
        var services = new ServiceCollection();
        var context = InjectionContext.NewContext();
        context.AddInjectables<Context1AssemblyMarker>();
        context.AddInjectables<Context2AssemblyMarker>();
        services.RegisterInjectables(context);
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var resolvedServices = serviceProvider.GetServices<IPrioritizedProvider>().ToList();

        // Assert
        await Assert.That(resolvedServices.Count).IsEqualTo(4);
        await Assert.That(resolvedServices[0]).IsTypeOf<PrioritizedService2>();
        await Assert.That(resolvedServices[1]).IsTypeOf<PrioritizedService4>();
        await Assert.That(resolvedServices[2]).IsTypeOf<PrioritizedService1>();
        await Assert.That(resolvedServices[3]).IsTypeOf<PrioritizedService3>();
    }
}
