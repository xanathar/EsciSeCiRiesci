using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Cucina : Room
{
    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Porta;
        yield return EntityType.Guanti;
    }

    public override void EnterRoom()
    {
        Log("Sei in cucina");
    }

    public override void ConfirmInteraction(EntityType e)
    {
        if (e == EntityType.Porta)
            Travel(RoomType.Corridoio);
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        return false;
    }

    public override RoomType GetRoomType()
    {
        return RoomType.Cucina;
    }

    public override void StartInteraction(EntityType e)
    {
    }

    public override void StartInventoryInteraction(EntityType inventory, EntityType e)
    {
    }
}
