namespace ArcTestsServices.DTOs;
//DTOs are the public contract between Services and API Layers 
//Services map internal entities, DTOs so the API never touches raw entities 
public record ProductDto(int Id, string Name, decimal Price, int StockQuantity);
public record OrderDto(int Id, int CustomerId,DateTime OrderDate, decimal TotalAmount);
public record CreateProductRequest(string Name, decimal Price, int StockQuantity);
public record CreateOrderRequest(int CustomerId, decimal TotalAmount);