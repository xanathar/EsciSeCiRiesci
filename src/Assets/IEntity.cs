using UnityEngine;

public interface IEntity
{
    EntityType GetEntityType();
    Sprite Pick();
}