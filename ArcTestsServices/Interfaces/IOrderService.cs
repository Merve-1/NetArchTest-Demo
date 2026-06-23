using ArcTestsServices.DTOs;

namespace ArcTestsServices.Interfaces;

public interface IOrderService
{
    Task<OrderDto?> GetOrderAsync(int id);
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task CreateOrderAsync(CreateOrderRequest request);
}