namespace ArcTestsData.Entities;

//Rule: All classes in ArcTestsData.Entities that inherit from Entity must not be public 
//They are internal to teh data layer, services access data only via repositories 
internal abstract class Entity
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; protected set; }
}