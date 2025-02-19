# Injectable

<a href="https://www.nuget.org/packages/Alkaline64.Injectable"><img src="https://img.shields.io/nuget/v/Alkaline64.Injectable.svg" alt="NuGet Version" /></a> 
<a href="https://www.nuget.org/packages/Alkaline64.Injectable"><img src="https://img.shields.io/nuget/dt/Alkaline64.Injectable.svg" alt="NuGet Download Count" /></a>

A library for registering injectable services using attributes. Lightweight, easy to use, with multi-assembly support.

## Features

The library is designed to move the service registration from the `Startup.cs` file to the service classes themselves, without making registrations implicit and giving up control. This results in the following benefits:

- The amount of code in the `Startup.cs` file is heavily reduced.
- The registration of services remains explicit (No broad registration of all classes which implement any interface in the whole assemly).
- Keyed services, specific registration orders, and 'TryAdd' registrations are still supported.


## Install

### Package Manager Console

```
PM> Install-Package Alkaline64.Injectable
```

### .NET CLI Console

```
> dotnet add package Alkaline64.Injectable
```

## Usage

### Registering services using the `Injectable` attribute

A service can be registered using the `Injectable` attribute. The attribute can be configured with the following parameters:
- `Lifetime` (mandatory): The lifetime of the service (Transient, Scoped, Singleton).
- `Type` (optional): The service type. If not set, the class itself is used.)
- `Key` (optional): The service key. If not set, the service is registered without a key.
- `AsTry` (optional): If set to true, the service is only registered if no other service with the same type and key is already registered.
- `Priority` (optional): The order in which the service is registered. The order is used to determine the order in which the services are registered. The default order is 0, and the services are registered in ascending order.

The attribute is used to define the service lifetime, the service type, and the service key. The attribute can be used on classes that implement an interface or on interfaces themselves.

Registering a service using a specific lifespan:
```csharp
using Alkaline64.Injectable;

[Injectable(Lifetime.Transient)]
public class TransientService { }

[Injectable(Lifetime.Scoped)]
public class ScopedService { }

[Injectable(Lifetime.Singleton)]
public class SingletonService { }
```

Registering a service with a specific `Type` (the most common use-case):
```csharp
using Alkaline64.Injectable;

[Injectable<IScopedService>(Lifetime.Scoped, Key = "Primary")]
public class ScopedService : IScopedService { }
```

Registering a service with a specific `Key`:
```csharp
using Alkaline64.Injectable;

[Injectable<IScopedService>(Lifetime.Scoped, Key = "Primary")]
public class ScopedService : IScopedService { }
```

Registering a service using `AsTry`:
```csharp
using Alkaline64.Injectable;

[Injectable<IScopedService>(Lifetime.Scoped, AsTry = true)]
public class ScopedService : IScopedService { }
```

Registering a service with a higher `Priority`:
```csharp
using Alkaline64.Injectable;

[Injectable<IScopedService>(Lifetime.Scoped, Priority = 7)]
public class ScopedService : IScopedService { }
```

### Configuring the DI Container

The services are only registered once they are added to the service collection. The library is designed to scan one or multiple assemblies for `Injectable` attributes during startup. The services can be added using the following Extension methods:

**Using a marker class/interface in a specific assembly:**

```csharp
using Alkaline64.Injectable.Extensions;

builder.Services.RegisterInjectables<MarkerClass>();
```

**Evaluating all dependencies from the startup assembly (optionally with masks):**

- The masks allow for * to be used as wildcards.
```csharp
using Alkaline64.Injectable.Extensions;

builder.Services.RegisterInjectables("Alkaline.Injectable.*", "Alkaline.OtherNamespace.*");
```

**Using an injection context:**

- Injection context allows for grouping multiple assemblies into one registration cycle without scanning all dependencies. (Relevant for projects where the registration order matters for certain services in separate assemblies)
```csharp
using Alkaline64.Injectable.Extensions;

var context = InjectionContext.NewContext();
context.RegisterInjectables<MarkerClass>();
context.RegisterInjectables<OtherMarkerClass>();

services.RegisterInjectables(context);
```