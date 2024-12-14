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
    [Fact]
    public void MultipleInjectablesInSingleAssembly_ReturnsHighestPriority()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<Context1AssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<IPrioritizedProvider>();

        // Assert
        Assert.IsType<PrioritizedService1>(service);
    }

    [Fact]
    public void MultipleInjectablesInSingleAssembly_AreInOrder()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterInjectables<Context1AssemblyMarker>();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var resolvedServices = serviceProvider.GetServices<IPrioritizedProvider>().ToList();

        // Assert
        Assert.Equal(2, resolvedServices.Count);
        Assert.IsType<PrioritizedService2>(resolvedServices[0]);
        Assert.IsType<PrioritizedService1>(resolvedServices[1]);
    }

    [Fact]
    public void MultipleInjectablesInMultipleAssemblies_ReturnsHighestPriority()
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
        Assert.IsType<PrioritizedService3>(service);
    }

    [Fact]
    public void MultipleInjectablesInMultipleAssemblies_AreInOrder()
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
        Assert.Equal(4, resolvedServices.Count);
        Assert.IsType<PrioritizedService2>(resolvedServices[0]);
        Assert.IsType<PrioritizedService4>(resolvedServices[1]);
        Assert.IsType<PrioritizedService1>(resolvedServices[2]);
        Assert.IsType<PrioritizedService3>(resolvedServices[3]);
    }
}
