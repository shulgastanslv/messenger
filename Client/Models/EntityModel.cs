using System;

namespace Client.Models;

public class EntityModel
{
    public EntityModel(Guid id)
    {
        Id = id;
    }

    public EntityModel()
    {
        Id = Guid.Empty;
    }

    public Guid Id { get; set; }
}