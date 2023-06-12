namespace Domain.Primitives;

public abstract class Entity
{
    public Entity(Guid id)
        : this()
    {
        Id = id;
    }

    public Entity()
    {
    }

    public Guid Id { get; set; }
}