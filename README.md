# NetArchTest-Demo
## Project Architecture 
![alt text](arc.png)

### Creation order 
```
1. ArcTestsData       ← build the foundation first
2. ArcTestsServices   ← depends on Data
3. ArcTestsApis       ← depends on Services
4. ArcTests           ← tests everything, written last
```

#### Step 1: ArcTestsData
- Entity base class
- IRepository<T> Interface
- ProductEntity, OrderEntity 
- ProductRepository, OrderRespository 

#### Step 2: ArcTestsService
- DTOs (ProductDto, OrderDto)
- IProductService, IOrderService
- ProductService, OrderService

#### Step 3: ArcTestsApis
- ProductController
- Program.cs  

#### Step 4 - ArcTests
- ArchitectureTests.cs 


```
Data      → foundation
Services  → walls
Apis      → roof
Tests     → inspection after everything is built
```

### In 'ArcTestsData.csproj' add the following
```
<ItemGroup>
    <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
        <_Parameter1>ArcTestsServices</_Parameter1>
    </AssemblyAttribute>
    <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
        <_Parameter1>ArcTestsApis</_Parameter1>
    </AssemblyAttribute>
    <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
        <_Parameter1>ArcTests</_Parameter1>
    </AssemblyAttribute>
</ItemGroup>
```

### In 'ArcTestsServices.csproj' add the following 
```
<ItemGroup>
    <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
        <_Parameter1>ArcTestsApis</_Parameter1>
    </AssemblyAttribute>
    <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
        <_Parameter1>ArcTests</_Parameter1>
    </AssemblyAttribute>
</ItemGroup>
```

### Visibility 

Here's the complete reference table with everything:

**ArcTestsData**

| Type | Modifier | Reason |
|---|---|---|
| `Entity` | `internal abstract` | Base class, never leaves data layer |
| `ProductEntity` | `internal` | Never leaves data layer, arch rule enforced by NetArchTest |
| `OrderEntity` | `internal sealed` | Never leaves data layer, arch rule enforced by NetArchTest |
| `IRepository<T>` | `internal` | Never leaves data layer, only used by repositories and services via InternalsVisibleTo |
| `ProductRepository` | `internal` | Never instantiated directly outside data layer, accessed via IRepository<T> |
| `OrderRepository` | `internal` | Never instantiated directly outside data layer, accessed via IRepository<T> |

**ArcTestsServices**

| Type | Modifier | Reason |
|---|---|---|
| `IProductService` | `public` | API must see this to inject it in controllers |
| `IOrderService` | `public` | API must see this to inject it in controllers |
| `ProductService` | `internal` | API never instantiates this directly, only uses IProductService |
| `OrderService` | `internal` | API never instantiates this directly, only uses IOrderService |
| `ProductDto` | `public` | API receives this as a response from the service layer |
| `OrderDto` | `public` | API receives this as a response from the service layer |
| `CreateProductRequest` | `public` | API sends this in the request body to create a product |
| `CreateOrderRequest` | `public` | API sends this in the request body to create an order |

**ArcTestsApis**

| Type | Modifier | Reason |
|---|---|---|
| `ProductsController` | `public` | ASP.NET Core routing engine must discover and instantiate it |
| `Program` | `public` | Entry point of the application, must be public |

**ArcTests**

| Type | Modifier | Reason |
|---|---|---|
| `ArchitectureTests` | `public` | NUnit test runner must discover and run it |

---

**The pattern:**

```
Anything the OUTSIDE world needs to see  →  public
Anything that stays INSIDE its own layer →  internal
```

```
ArcTestsData     → everything internal  (InternalsVisibleTo grants Services + Tests access)
ArcTestsServices → interfaces and DTOs public, service classes internal
ArcTestsApis     → everything public (framework needs to see controllers)
ArcTests         → everything public (test runner needs to see test classes)
```


### Detailed Explaination 
[Notion Notes](https://www.notion.so/Architecture-Tests-3877a1b71e4180aba237daa5e8d2931f?source=copy_link)