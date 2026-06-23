namespace ArcTestsData.Entities;


//Product entity - intentionally internal (not public) 
//NetArchTest will enforce this: entities in ArcTestsData.Entities must not be public
internal  class ProductEntity: Entity
{
    public string Name { get;private set; }= string.Empty;
    public decimal Price { get;private set; }
    public int StockQuantity { get;private set; }

    public ProductEntity(string name, decimal price, int stockQuantity)
    {
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
    }
}