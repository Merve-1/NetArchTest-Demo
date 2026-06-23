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

| Type | Modifier | Reason |
|---|---|---|
| `Entity` | `internal` | Never leaves data layer |
| `ProductEntity` | `internal` | Never leaves data layer |
| `OrderEntity` | `internal` | Never leaves data layer |
| `IRepository<T>` | `internal` | Never leaves data layer |
| `ProductRepository` | `internal` | Never leaves data layer |
| `OrderRepository` | `internal` | Never leaves data layer |
| `IProductService` | `public` | API needs to see this |
| `IOrderService` | `public` | API needs to see this |
| `ProductService` | `internal` | API uses it via interface only |
| `OrderService` | `internal` | API uses it via interface only |
| `ProductDto` | `public` | API needs to see this |
| `OrderDto` | `public` | API needs to see this |

The boundary is here:
```
ArcTestsData  →  everything internal (InternalsVisibleTo grants Services access)
ArcTestsServices  →  interfaces and DTOs public, service classes internal
ArcTestsApis  →  only sees public interfaces and DTOs
```

### Detailed Explaination 
[Notion Notes](https://www.notion.so/Architecture-Tests-3877a1b71e4180aba237daa5e8d2931f?source=copy_link)