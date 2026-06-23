using ArcTestsServices.DTOs;
using ArcTestsServices.Interfaces;
using ArcTestsData.Interfaces;
using ArcTestsData.Entities;

namespace ArcTestsServices.Services;

internal class OrderService: IOrderService
{
    private readonly IRepository<OrderEntity> _orderRepository;

    public OrderService(IRepository<OrderEntity> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> GetOrderAsync(int id)
    {
        var entity = await _orderRepository.GetByIdAsync(id);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var entities = await _orderRepository.GetAllAsync();
        return entities.Select(MapToDto);
    }

    public async Task CreateOrderAsync(CreateOrderRequest request)
    {
        var entity = new OrderEntity(request.CustomerId, request.TotalAmount);
        await _orderRepository.AddAsync(entity);
    }

    private static OrderDto MapToDto(OrderEntity e)
        => new(e.Id, e.CustomerId, e.OrderDate, e.TotalAmount);
}