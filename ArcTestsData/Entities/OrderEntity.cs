namespace ArcTestsData.Entities;

// Order Entity - Intentionally internal (not public)
internal sealed class OrderEntity : Entity
{
    public int CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; private set; }

    public OrderEntity(int CustomerId, decimal totalAmount)
    {
        this.CustomerId = CustomerId;
        this.TotalAmount = totalAmount;
    }

}