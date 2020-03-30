
using System;
using System.Collections.Generic;
using UnityEngine;

public class Soggiorno : Room
{
    public Soggiorno()
    {
    }
    public override void EnterRoom()
    {
    }


    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Giornali;
        yield return EntityType.Telefono;
        yield return EntityType.Scrigno;
        yield return EntityType.Lampadina;
        yield return EntityType.Porta;
    }

    public override void ConfirmInteraction(EntityType e)
    {

    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        LogRandomFailure();
        return false;
    }

    public override RoomType GetRoomType()
    {
        return RoomType.Soggiorno;
    }

    public override void StartInteraction(EntityType e)
    {

    }

    public override void StartInventoryInteraction(EntityType inventory, EntityType e)
    {
    }
}
