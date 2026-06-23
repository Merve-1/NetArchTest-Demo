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